using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InTheHand.Net.Sockets;

namespace Bluetooth
{
    public partial class Form1 : Form
    {
        BluetoothControler bt;
        BackgroundWorker bg_search;
        BackgroundWorker bg_pair;
        BackgroundWorker bg_sendFile;
        public Form1()
        {
            
            InitializeComponent();
            bt = BluetoothControler.getInstance();
            bg_search = new BackgroundWorker();
            bg_search.DoWork                += new DoWorkEventHandler(bg_search_devices);
            bg_search.RunWorkerCompleted    += new RunWorkerCompletedEventHandler(bg_search_devices_end);
            bg_pair = new BackgroundWorker();
            bg_pair.DoWork                  += new DoWorkEventHandler(bg_pair_to_device);
            bg_pair.RunWorkerCompleted      += new RunWorkerCompletedEventHandler(bg_pair_to_device_end);
            bg_sendFile = new BackgroundWorker();
            bg_sendFile.DoWork              += new DoWorkEventHandler(bg_send_to_device);
            bg_sendFile.RunWorkerCompleted  += new RunWorkerCompletedEventHandler(bg_send_to_device_end);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BTAdapterCB.DataSource      = new BindingSource(bt.BTAdapters, null);
            BTAdapterCB.DisplayMember   = "KEY";
            BTAdapterCB.ValueMember     = "VALUE";
            BTDeviceCB.DisplayMember    = "KEY";
            BTDeviceCB.ValueMember      = "VALUE";
            BTDeviceCB.Enabled          = false;
            PairButton.Enabled          = false;
            deviceSelectedBox.Enabled   = false;
            OperationsBox.Enabled       = false;
        }

        private void BTAdapterCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            BTAdapterCB.Enabled = false;
            if (!bg_search.IsBusy)
            {
                BTDeviceCB.Enabled = false;
                this.UseWaitCursor = true;
                this.UseWaitCursor = true;
                AdapterInfo.Text = "Trwa wczytywanie urządzeń...";
                BTDeviceCB.DataSource = new BindingSource(bt.BTDevices, null);
                object[] parameters = new object[] { BTAdapterCB.SelectedItem };
                bg_search.RunWorkerAsync(parameters);
            }

        }

        private void bg_search_devices(object sender, DoWorkEventArgs e)
        {
            object[] parameters = e.Argument as object[];
            BluetoothControler bt = BluetoothControler.getInstance();
            bt.selectBTAdapter(parameters[0]);
        }

        private void bg_search_devices_end(object sender, RunWorkerCompletedEventArgs e)
        {
            this.UseWaitCursor = false;
            if(bt.bc != null)
            {
                MessageBox.Show("Zakończono wyszukiwać urządzenia", "Koniec!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BTDeviceCB.Enabled = true;
            }
            BTDeviceCB.DataSource = new BindingSource(bt.BTDevices, null);
            BTAdapterCB.Enabled = true;
            AdapterInfo.Text = "";
        }

        private void BTDeviceCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.setDeviceInfo();
        }

        private void PairButton_Click(object sender, EventArgs e)
        {
            if (!bg_pair.IsBusy)
            {
                ParingText.Text = "Parowanie...";
                PairButton.Enabled = false;
                object[] parameters = new object[] { BTDeviceCB.SelectedItem };
                bg_pair.RunWorkerAsync(parameters);
            }
        }

        private void bg_pair_to_device(object sender, DoWorkEventArgs e)
        {
            object[] parameters = e.Argument as object[];
            BluetoothControler bt = BluetoothControler.getInstance();
            bool result = bt.pairWithDevice(parameters[0]);
            e.Result = result;
        }

        private void bg_pair_to_device_end(object sender, RunWorkerCompletedEventArgs e)
        {
            ParingText.Text = "";
            if (e.Error != null || !(bool)e.Result)
            {
                MessageBox.Show("Nie udało się sparować z urządzeniem", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Sparowano z urządzeniem", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            setDeviceInfo();
        }

        private bool setDeviceInfo(bool forceNotPaired = false)
        {
            BluetoothDeviceInfo info = bt.getDeviceInfo(BTDeviceCB.SelectedItem);
            if(info != null)
            {
                deviceSelectedBox.Enabled = true;
                DeviceNameInput.Text = info.DeviceName;
                DeviceAddressInput.Text = info.DeviceAddress.ToString();
                if (forceNotPaired)
                {
                    AutorisationInput.Text = "Nie";
                    PairButton.Enabled = true;
                    OperationsBox.Enabled = false;
                    ConnectInput.Text = "Nie";
                    return false;
                }
                else
                {
                    AutorisationInput.Text = info.Authenticated ? "Tak" : "Nie";
                    PairButton.Enabled = !info.Authenticated;
                    OperationsBox.Enabled = info.Authenticated;
                    ConnectInput.Text = info.Connected ? "Tak" : "Nie";
                    return info.Authenticated;
                }
            }
            deviceSelectedBox.Enabled = false;
            DeviceNameInput.Text = "";
            DeviceAddressInput.Text = "";
            AutorisationInput.Text = "";
            ConnectInput.Text = "";
            PairButton.Enabled = false;
            OperationsBox.Enabled = false;
            return false;
        }

        private void SendFileButton_Click(object sender, EventArgs e)
        {
            if (!this.setDeviceInfo())
            {
                MessageBox.Show("Urządzenie nie jest już sparowane!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!bg_sendFile.IsBusy)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    SendingText.Text = "Wysyłanie...";
                    OperationsBox.Enabled = false;
                    object[] parameters = new object[] { BTDeviceCB.SelectedItem, openFileDialog.FileName };
                    bg_sendFile.RunWorkerAsync(parameters);
                }
            }
        }

        private void bg_send_to_device(object sender, DoWorkEventArgs e)
        {
            object[] parameters = e.Argument as object[];
            BluetoothControler bt = BluetoothControler.getInstance();
            e.Result = bt.sendFile((string)parameters[1], parameters[0]);

        }

        private void bg_send_to_device_end(object sender, RunWorkerCompletedEventArgs e)
        {
            SendingText.Text = "";
            if(e.Error != null)
            {
                MessageBox.Show("Nie udało się wysłać pliku. Spróbuj sparować urządzenie ponownie", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                setDeviceInfo(true);
            }
            switch ((InTheHand.Net.ObexStatusCode)e.Result)
            {
                case InTheHand.Net.ObexStatusCode.Final | InTheHand.Net.ObexStatusCode.OK:
                case InTheHand.Net.ObexStatusCode.OK:
                case InTheHand.Net.ObexStatusCode.Final | InTheHand.Net.ObexStatusCode.Continue:
                case InTheHand.Net.ObexStatusCode.Continue:
                    MessageBox.Show("Plik został wysłany", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    setDeviceInfo();
                    break;
                case InTheHand.Net.ObexStatusCode.Forbidden:
                case InTheHand.Net.ObexStatusCode.Forbidden | InTheHand.Net.ObexStatusCode.Final:
                    MessageBox.Show("Przerwano wysyłanie pliku", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    setDeviceInfo();
                    break;
                default:
                    MessageBox.Show("Nie udało się wysłać pliku. Spróbuj sparować urządzenie ponownie", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    setDeviceInfo(true);
                    break;
            }

        }

        public static string displayStringPrompt(string caption, string text)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = caption;
            label.Text = text;
            textBox.Text = "";

            buttonOk.Text = "OK";
            buttonOk.DialogResult = DialogResult.OK;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk});
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;

            DialogResult dialogResult = form.ShowDialog();
            return textBox.Text;
        }
    }
}
