using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Bluetooth
{
    class BluetoothControler
    {
        private static BluetoothControler instance = null;
        public BluetoothClient bc { get; set; } = null;
        public Dictionary<string, BluetoothAddress> BTAdapters { get; set; }
        public Dictionary<string, BluetoothAddress> BTDevices { get; set; }

        EventHandler<BluetoothWin32AuthenticationEventArgs> authHandler;
        BluetoothWin32Authentication authenticator;

        private BluetoothControler()
        {
            this.BTAdapters = new Dictionary<string, BluetoothAddress>();
            this.BTAdapters.Add("Wybierz adapter", null);
            this.BTDevices = new Dictionary<string, BluetoothAddress>();
            this.BTDevices.Add("Wybierz urządzenie bluetooth", null);
            this.getBTAdapters();

            authHandler = new EventHandler<BluetoothWin32AuthenticationEventArgs>(handleAuthRequests);
            authenticator = new BluetoothWin32Authentication(authHandler);

        }

        public static BluetoothControler getInstance()
        {
            if (instance == null)
            {
                instance = new BluetoothControler();
            }
            return instance;
        }

        private void getBTAdapters()
        {
            BluetoothRadio[] br = BluetoothRadio.GetAllRadios();
            for (int i = 0; i < br.Length; i++)
            {
                this.BTAdapters.Add(br[i].Name, br[i].LocalAddress);
            }
        }

        public void selectBTAdapter(Object selectedItem)
        {
            BluetoothAddress ba = ((KeyValuePair<string, BluetoothAddress>)selectedItem).Value;
            this.BTDevices.Clear();
            this.BTDevices.Add("Wybierz urządzenie bluetooth", null);
            if (ba != null)
            {
                BluetoothEndPoint endPoint = new BluetoothEndPoint(ba, BluetoothService.ObexFileTransfer);
                bc = new BluetoothClient(endPoint);
                this.loadDevicesList();
            }
            else
            {
                bc = null;
            }
        }

        public void loadDevicesList()
        {
            BluetoothDeviceInfo[] di = this.bc.DiscoverDevices(255, false, false, false, true);
            for (int i = 0; i < di.Length; i++)
            {
                int k = 0;
                string name = di[i].DeviceName;
                while (this.BTDevices.ContainsKey(di[i].DeviceName))
                {
                    name = di[i].DeviceName + " (" + k.ToString() + ")";
                }
                this.BTDevices.Add(name, di[i].DeviceAddress);
            }
        }

        public bool pairWithDevice(Object selectedItem)
        {
            BluetoothDeviceInfo di = this.getDeviceInfo(selectedItem);
            if (di != null)
            {
                return BluetoothSecurity.PairRequest(di.DeviceAddress, null);
            }
            return false;
        }
        //https://github.com/inthehand/32feet/wiki/BluetoothWin32Authentication
        //telefon używa tylko BluetoothAuthenticationMethod.NumericComparison e.JustWorksNumericComparison == false
        //reszta nie jest testowana i przepisana z VB tak żeby błędów nie generowało tylko XD
        private void handleAuthRequests(object sender, BluetoothWin32AuthenticationEventArgs e)
        {
            switch (e.AuthenticationMethod)
            {
                case BluetoothAuthenticationMethod.Legacy:
                    //przepisane z dokumentacji, i tak nie będzie z telefonem to użyte.
                    //nawet nwm po co na sztywno te piny wpisane i czy w ogóle działa bo dokumentacja to VB czy coś
                    string address = e.Device.DeviceAddress.ToString();
                    if(address.Substring(0,8).Equals("0099880D") || address.Substring(0, 8).Equals("0099880E"))
                        e.Pin = "5276";
                    else if (address.Substring(0, 8).Equals("0099880D"))
                        e.Pin = "ásdfghjkl";
                    break;

                case BluetoothAuthenticationMethod.OutOfBand:
                    //ConfirmOob();
                    //To jest coś z kluczem kryptograficznym czy coś, ogólnie za mało informacji na ten temat jest
                    //dokumentacja mówi "ConfirmOob should be called -- this is untested"
                    //dlatego zostawiam confirm i tyle
                    e.Confirm = true;
                    break;

                case BluetoothAuthenticationMethod.NumericComparison:
                    if (e.JustWorksNumericComparison == true)
                        //jeżeli e.JustWorksNumericComparison to ma być zwykły komunikat o zezwoleniu na parowanie
                        e.Confirm = (MessageBox.Show("Sparować z urządzeniem "+ e.Device.DeviceAddress.ToString() +"?", "Paruj urządzenia", MessageBoxButtons.YesNo) == DialogResult.Yes);
                    else
                        //jeżeli nie, to dodatkowo wyświetlany jest klucz
                        e.Confirm = (MessageBox.Show("Parowanie z urządzeniem " + e.Device.DeviceAddress.ToString()+"?\n Potwierdź kod wyświetlany na ekranie urządzenia: "+ e.NumberOrPasskeyAsString, "Paruj urządzenia", MessageBoxButtons.YesNo) == DialogResult.Yes);

                    break;
                //Prawdopodobnie oba są takie same ale No cóż dokumentacja XD
                case BluetoothAuthenticationMethod.PasskeyNotification:
                    //It seems that Passkey is "please input the passkey as displayed on the other device",
                    //and PasskeyNotification is "here's the passkey to type on the remote device, confirm that".
                    //So if it is PasskeyNotification, then the Confirm propery needs to be set.
                    //tyle jest napisane więc zostawiam samo confirm. Nie wiem który element mam ustawić jako to hasło.
                    e.Confirm = true;
                    break;
                case BluetoothAuthenticationMethod.Passkey:
                    //tak samo tutaj próba skopiowania z dokumentacji XD
                    var value = -1;
                    try
                    {
                        string code = Form1.displayStringPrompt("Parowanie z urządzeniem " + e.Device.DeviceAddress.ToString(), "Wprowadź kod wyświetlany na ekranie");
                        value = Int32.Parse(code);
                    }
                    catch (Exception) { }
                    if (value >= 0 && value <= 1000000)
                    {
                        e.ResponseNumberOrPasskey = value;
                        e.Confirm = true;
                    }
                    break;
                default:
                    break;

            }
        }

        public BluetoothDeviceInfo getDeviceInfo(Object selectedItem)
        {
            BluetoothAddress adrs = ((KeyValuePair<string, BluetoothAddress>)selectedItem).Value;
            if (adrs == null) return null;
            return new BluetoothDeviceInfo(adrs);
        }

        public ObexStatusCode sendFile(string filePatch, Object selectedItem)
        {
            BluetoothDeviceInfo bd = this.getDeviceInfo(selectedItem);
            if (bd == null) return ObexStatusCode.InternalServerError;

            try {
                string FileName = filePatch.Substring(filePatch.LastIndexOf("\\"));
                Uri uri = new Uri("obex://" + bd.DeviceAddress.ToString() + "/" + filePatch);
                ObexWebRequest request = new ObexWebRequest(uri);
                request.ReadFile(filePatch);
                ObexWebResponse response =
                (ObexWebResponse)request.GetResponse();
                response.Close();
                return response.StatusCode;
            }
            catch (Exception)
            {
                return ObexStatusCode.InternalServerError;
            }
        }
    }
}
