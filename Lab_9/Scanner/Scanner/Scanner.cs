using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Scanner
{
    class Scanner
    {
        WIA.DeviceManager manager;
        WIA.Device scanner = null;

        //instancja formularza
        scannerForm form;

        //w WiaDef.h można znaleźć wszystkie
        const string wiaFormatBMP = "{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}"; //wskazuje format pliku BMP
        const string wiaEventScanImage = "{A6C5A715-8C6E-11D2-977A-0000F87A926F}"; //kod eventu skanowania
        const int WIA_IPS_CUR_INTENT = 6146; //tryb kolorów
        const int WIA_IPS_XRES = 6147; //DPI poziomo
        const int WIA_IPS_YRES = 6148; //DPI pionowo
        const int WIA_IPS_BRIGHTNESS = 6154; //jasność [−1000;1000]
        const int WIA_IPS_CONTRAST = 6155; //kontrast [−1000;1000]

        const int WIA_INTENT_IMAGE_TYPE_COLOR = 0x00000001; //tryb skanowania kolor
        const int WIA_INTENT_IMAGE_TYPE_GRAYSCALE = 0x00000002; //tryb skanowania odcienie szarości
        const int WIA_INTENT_IMAGE_TYPE_TEXT = 0x00000004; //tryb skanowania tekst


        public Scanner(scannerForm form)
        {
            manager = new WIA.DeviceManager();
            manager.RegisterEvent(wiaEventScanImage);
            manager.OnEvent += new WIA._IDeviceManagerEvents_OnEventEventHandler(dm_OnEvent);
            this.form = form;
        }

        private void dm_OnEvent(string EventID, string DeviceID, string ItemID)
        {
            string test = EventID + " " + DeviceID + " " + ItemID;
            WIA.Device scannerFromEvent = null;
            foreach (WIA.DeviceInfo info in manager.DeviceInfos)
            {
                if (info.DeviceID == DeviceID)
                {
                    scannerFromEvent = info.Connect();
                    break;
                }
            }
            if (scannerFromEvent == null) return;
            WIA.ImageFile scannedImage = (WIA.ImageFile)scannerFromEvent.Items[1].Transfer(wiaFormatBMP);
            this.form.setmsFromEvent(new MemoryStream((byte[])scannedImage.FileData.get_BinaryData()));
        }

        //czy wybrano skaner
        public bool isScannerSelected()
        {
            return (this.scanner == null) ? false : true;
        }

        //okno dialogowe wyboru skanera
        public bool selectScanner()
        {
            //(typ urządzenia, czy zawsze pokazać okno wyboru? nwm kiedy nie pokaże jak sprawdzałem z false to i tak pokazywało, Czy bez wybranego urządzenia ma być błąd).
            this.scanner = new WIA.CommonDialog().ShowSelectDevice(WIA.WiaDeviceType.ScannerDeviceType, false, false);
            //czy wybrano skaner
            if(this.scanner == null)
            {
                return false;
            }
            return true;
        }

        //skanowanie pliku
        public MemoryStream scan(int colorType, Decimal dpi, int brightness, int contrast)
        {
            //połączenie z wybranym skanerem (jeżeli dostępny w liście)
            foreach (WIA.DeviceInfo info in manager.DeviceInfos)
            {
                if (info.DeviceID == this.scanner.DeviceID)
                {
                    this.scanner = info.Connect();
                    break;
                }
            }
            MemoryStream ms = null;
            if(this.scanner == null)
                return null;

            try
            {
                //obraz skanera
                WIA.Item scanerImage = this.scanner.Items[1];
                //tryb kolorów
                switch (colorType)
                {
                    case 0:
                        setWIAProperty(scanerImage.Properties, WIA_IPS_CUR_INTENT.ToString(), WIA_INTENT_IMAGE_TYPE_COLOR);
                        break;
                    case 1:
                        setWIAProperty(scanerImage.Properties, WIA_IPS_CUR_INTENT.ToString(), WIA_INTENT_IMAGE_TYPE_GRAYSCALE);
                        break;
                    default:
                        setWIAProperty(scanerImage.Properties, WIA_IPS_CUR_INTENT.ToString(), WIA_INTENT_IMAGE_TYPE_TEXT);
                        break;
                }
                //rozdzielczość
                Int32 dpi32 = Convert.ToInt32(dpi);
                setWIAProperty(scanerImage.Properties, WIA_IPS_XRES.ToString(), dpi32);
                setWIAProperty(scanerImage.Properties, WIA_IPS_YRES.ToString(), dpi32);
                //jasność
                setWIAProperty(scanerImage.Properties, WIA_IPS_BRIGHTNESS.ToString(), brightness);
                //kontrast
                setWIAProperty(scanerImage.Properties, WIA_IPS_CONTRAST.ToString(), contrast);

                //obraz jakiś bo jest Items[-]
                WIA.ImageFile scannedImage = (WIA.ImageFile)(new WIA.CommonDialog()).ShowTransfer(scanerImage, wiaFormatBMP, false);
                //Przekonwertowanie zeskanowanego obrazu na MemoryStream
                ms =  new MemoryStream((byte[])scannedImage.FileData.get_BinaryData());
            }
            catch(Exception)
            {
                this.scanner = null;
                return null;
            }

            return ms;
        }
        //czy skaner obsługuje wybrane DPI
        public bool checkDPI(Decimal dpi)
        {
            try
            {
                WIA.Item scanerImage = this.scanner.Items[1];
                Int32 dpi32 = Convert.ToInt32(dpi);
                setWIAProperty(scanerImage.Properties, WIA_IPS_XRES.ToString(), dpi32);
                setWIAProperty(scanerImage.Properties, WIA_IPS_YRES.ToString(), dpi32);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private void setWIAProperty(WIA.IProperties properties, object propName, object propValue)
        {
            properties.get_Item(ref propName).set_Value(ref propValue); ;
        }
    }
}
