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
        short recordState = 0;
        string captureFile = "video";
        private System.Windows.Forms.Timer timer;
        public Form1()
        {
            InitializeComponent();
        }

        private void disableButtons()
        {
            Connect.Enabled = true;
            Disconnect.Enabled = false;
            Picture.Enabled = false;
            Record.Enabled = false;
            Record.Text = "Rozpocznij nagrywanie";
            Parameters.Enabled = false;
            Resolution.Enabled = false;
            Page.Enabled = false;
        }

        private void enableButtons()
        {
            Connect.Enabled = false;
            Disconnect.Enabled = true;
            Picture.Enabled = true;
            Record.Enabled = true;
            Parameters.Enabled = true;
            Resolution.Enabled = true;
            Page.Enabled = true;
        }

        private void tick_function(object sender, EventArgs e)
        {
            if (USBCam.getInstance().is_enabled)
            {
                string temp = Directory.GetCurrentDirectory() + "\\appTemp.jpg";
                string desc = Directory.GetCurrentDirectory() + "\\app.jpg";
                try
                {
                    if(USBCam.getInstance().SaveImageWithName(temp, true))
                    {
                        MessageBox.Show("Nie ruszaj się!", "Stop", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    if (File.Exists(desc))
                        File.Delete(desc);
                    System.IO.File.Move(Directory.GetCurrentDirectory() + "\\appTemp.jpg", Directory.GetCurrentDirectory() + "\\app.jpg");
                }
                catch (Exception) { }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            USBCam.getInstance().Container = CamView;
            disableButtons();
            timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(tick_function);
            timer.Interval = 500;
            timer.Start();
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
            USBCam.getInstance().SaveImage();
        }

        private void Record_Click(object sender, EventArgs e)
        {

            if (recordState == 0)
            {
                recordState = 1;
                InputBox("Wprowadź nazwę pliku", "format .avi", ref captureFile);
                Record.Text = "Zatrzymaj nagrywanie";
                USBCam.getInstance().StartRecord(captureFile);
            }
            else
            {
                recordState = 0;
                Record.Text = "Rozpocznij nagrywanie";
                USBCam.getInstance().StopRecord(captureFile);
            }
        }

        public static DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
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
    }
}
