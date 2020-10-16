using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Lab_12_Cam
{
    public partial class Form1 : Form
    {
        //informacja o tym czy nagranie jest włączone
        short recordState = 0;
        //informacja o pliku przechowującym nagranie
        string captureFile;
        //timer przechowujący funkcję wykrywania ruchu i zapisu do pliku dla strony
        private System.Windows.Forms.Timer timer;
        //czy wykrywanie ruchu zostało włączone
        bool motionCapture = false;
        //czy jest wyświetlany aktualnie komunikat o wykrytym ruchu
        bool motionInfo = false;
        public Form1()
        {
            InitializeComponent();
        }
        //wyłączanie przyciskó i ustawianie domyślnych wartości nazw (jeżeli rozłączono z kamerą)
        private void disableButtons()
        {
            Connect.Enabled = true;
            Disconnect.Enabled = false;
            Picture.Enabled = false;
            Record.Enabled = false;
            Record.Text = "Rozpocznij nagrywanie";
            Motion.Text = "Włącz wykrywanie ruchu";
            motionCapture = false;
            Parameters.Enabled = false;
            Resolution.Enabled = false;
            Page.Enabled = false;
            Motion.Enabled = false;
        }
        //włączanie przycisków (jeżeli połączono z kamerą)
        private void enableButtons()
        {
            Connect.Enabled = false;
            Disconnect.Enabled = true;
            Picture.Enabled = true;
            Record.Enabled = true;
            Parameters.Enabled = true;
            Resolution.Enabled = true;
            Page.Enabled = true;
            Motion.Enabled = true;
        }
        //funkcja wykrywania ruchu i zapisu do pliku dla strony internetowej
        private void tick_function(object sender, EventArgs e)
        {
            if (USBCam.getInstance().is_enabled)
            {
                string temp = Directory.GetCurrentDirectory() + "\\appTemp.jpg";
                string desc = Directory.GetCurrentDirectory() + "\\app.jpg";
                try
                {
                    bool test = USBCam.getInstance().SaveImageWithName(temp, motionCapture);
                    if (File.Exists(desc))
                        File.Delete(desc);
                    System.IO.File.Move(Directory.GetCurrentDirectory() + "\\appTemp.jpg", Directory.GetCurrentDirectory() + "\\app.jpg");
                    if (test && !motionInfo)
                    {
                        motionInfo = true;
                        if (MessageBox.Show("Nie ruszaj się!", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            motionInfo = false;
                        }
                    }
                }
                catch (Exception) { }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            USBCam cam = USBCam.getInstance();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            cam.Container = CamView;
            disableButtons();
            timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(tick_function);
            timer.Interval = 500;
            timer.Start();
            MCI_combo_box.DataSource = cam.ListOfDevices;
            MotionSens.Value = Convert.ToDecimal(1-cam.image_diff);
        }

        private void CamView_Click(object sender, EventArgs e)
        {
        }

        private void Disconnect_Click(object sender, EventArgs e)
        {
            try
            {
                USBCam.getInstance().Dispose();
                disableButtons();
            }
            catch(Exception exc) {
                MessageBox.Show(exc.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            try
            {
                USBCam.getInstance().OpenConnection();
                enableButtons();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Picture_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfdImage = new SaveFileDialog();
            sfdImage.Filter = "(*.jpg)|*.jpg";

            if (sfdImage.ShowDialog() == DialogResult.OK)
            {

                USBCam.getInstance().SaveImageWithName(sfdImage.FileName);
            }
        }

        private void Record_Click(object sender, EventArgs e)
        {

            if (recordState == 0)
            {
                SaveFileDialog sfdImage = new SaveFileDialog();
                sfdImage.Filter = "(*.avi)|*.avi";

                if (sfdImage.ShowDialog() == DialogResult.OK)
                {
                    Record.Text = "Zatrzymaj nagrywanie";
                    captureFile = sfdImage.FileName;
                    USBCam.getInstance().StartRecord(captureFile);
                    recordState = 1;
                }
                
            }
            else
            {
                recordState = 0;
                Record.Text = "Rozpocznij nagrywanie";
                USBCam.getInstance().StopRecord(captureFile);
            }
        }

        private void Parameters_Click(object sender, EventArgs e)
        {
            USBCam.getInstance().ChangeParameters();
        }

        private void Resolution_Click(object sender, EventArgs e)
        {
            USBCam.getInstance().ChangeResolution();
        }

        private void Page_Click(object sender, EventArgs e)
        {
            
            ProcessStartInfo sInfo = new ProcessStartInfo(Directory.GetCurrentDirectory()+"\\app.html");
            Process.Start(sInfo);

        }

        private void Motion_Click(object sender, EventArgs e)
        {
            if (motionCapture)
            {
                motionCapture = false;
                Motion.Text = "Włącz wykrywanie ruchu";
            }
            else
            {
                motionCapture = true;
                ProcessStartInfo sInfo = new ProcessStartInfo(Directory.GetCurrentDirectory() + "\\app.html");
                Process.Start(sInfo);
                Motion.Text = "Wyłącz wykrywanie ruchu";
            }
        }

        private void MCI_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            USBCam cam = USBCam.getInstance();
            if (cam.is_enabled)
            {
                int index = ((ComboBox)sender).SelectedIndex;

                cam.Dispose();
                cam.DeviceID = index;

                try
                {
                    cam.OpenConnection();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    disableButtons();
                }
            }
        }

        private void MotionSens_ValueChanged(object sender, EventArgs e)
        {
            USBCam.getInstance().image_diff = 1-(float)((NumericUpDown)sender).Value;
        }
    }
}
