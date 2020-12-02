using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace JoystickProgram
{
    public partial class JoystickForm : Form
    {
        JoystickControler joystickControler = new JoystickControler();
        BackgroundWorker InputReader_bg = new BackgroundWorker();
        MouseControler mouseControler;
        DrawContoler drawContoler;
        Stopwatch timer;

        bool appRunning = true;

        public JoystickForm()
        {
            InitializeComponent();
        }

        private void JoystickForm_Load(object sender, EventArgs e)
        {
            this.loadDevicesList();

            this.mouseControler = new MouseControler();
            this.drawContoler = new DrawContoler(this.drawBox);
            this.tabControl.Visible = false;
            this.timer = Stopwatch.StartNew();
            this.InputReader_bg.DoWork += new DoWorkEventHandler(BackgroundInputRead);
            this.InputReader_bg.RunWorkerAsync();
        }

        private void BackgroundInputRead(object sender, DoWorkEventArgs e)
        {
            while (appRunning)
            {
                // czas między ostatnim sczcytaniem
                this.timer.Stop();
                double dt = (double)this.timer.ElapsedTicks / Stopwatch.Frequency;
                this.timer = Stopwatch.StartNew();

                if (!this.joystickControler.isJoystickChoosen()) continue;
                bool showNew = false;
                if (this.joystickControler.areNewEvents())
                    showNew = true;

                var leftStick = this.joystickControler.GetPositionStick();
                var rightStick = this.joystickControler.GetRotationStick();
                var buttons = this.joystickControler.GetButtonsPressed();

                if (showNew)
                    this.BuildInfo(leftStick, rightStick, buttons);

                this.mouseControler.MouseInput(dt, leftStick);
                this.drawContoler.DrawInput(dt, leftStick);
            }
        }

        private void BuildInfo(double[] leftStick, double[] rightStick, Dictionary<string, bool> buttons)
        {
            joystickInfo.Invoke((Action)delegate
            {
                joystickInfo.Clear();
                joystickInfo.AppendText("Status drążków [0,1]:");
                joystickInfo.AppendText(Environment.NewLine);
                joystickInfo.AppendText($"Lewy drążek: X: {leftStick[0]}, Y: {leftStick[1]}");
                joystickInfo.AppendText(Environment.NewLine);
                joystickInfo.AppendText($"Prawy drążek: X: {rightStick[0]}, Y: {rightStick[1]}");
                joystickInfo.AppendText(Environment.NewLine);
                joystickInfo.AppendText("Status drążków LT, RT [0,1]:\n");
                joystickInfo.AppendText(Environment.NewLine);
                joystickInfo.AppendText($"Prawy drążek: {(leftStick[2] < 0 ? -leftStick[2] : 0)}, Lewy drążek: {(leftStick[2] > 0 ? leftStick[2] : 0)}");
                joystickInfo.AppendText(Environment.NewLine);
                joystickInfo.AppendText("Status przycisków:");
                foreach (KeyValuePair<string, bool> entry in buttons)
                {
                    joystickInfo.AppendText(Environment.NewLine);
                    joystickInfo.AppendText($"{ entry.Key}: {(entry.Value ? "Tak" : "Nie")}");
                }
            });
            
        }

        private void loadDevicesList()
        {
            JoystickSelect.DataSource = new BindingSource(joystickControler.loadDevicesList(), null);
            JoystickSelect.DisplayMember = "KEY";
            JoystickSelect.ValueMember = "VALUE";
        }

        private void JoystickSelect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ReloadButton_Click(object sender, EventArgs e)
        {
            this.loadDevicesList();
        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            
            if (this.joystickControler.ChooseJoystick(((KeyValuePair<string, Guid>)(JoystickSelect.SelectedItem)).Value))
            {
                MessageBox.Show("Wybrano urządzenie "+this.joystickControler.GetName(), "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.tabControl.Visible = true;
            }
            else
            {
                MessageBox.Show("Wybrano nieprawidłowe urządzenie", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.tabControl.Visible = false;
                this.drawContoler.isActive = false;
                this.mouseControler.disable();
                this.enableMouseControl.Text = "Włącz sterowanie myszą";
                this.drawContoler.isActive = false;

            }
        }

        private void JoystickForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            appRunning = false;
        }

        private void colorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            colorDialog.Color = this.drawContoler.BrushColor;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.drawContoler.BrushColor = colorDialog.Color;
            }
        }

        private void JoystickForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && this.drawContoler.isActive)
            {
                this.drawContoler.isActive = false;
            }
        }

        private void drawButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Naciśnij esc aby wyjść", "Aktywacja trybu rysowania", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.drawContoler.isActive = true;
        }

        private void enableMouseControl_Click(object sender, EventArgs e)
        {
            if (this.mouseControler.IsEnabled())
            {
                this.mouseControler.disable();
                this.enableMouseControl.Text = "Włącz sterowanie myszą";
            }
            else
            {
                this.mouseControler.enable();
                this.enableMouseControl.Text = "Wyłącz sterowanie myszą";
            }
                
        }

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            this.drawContoler.isActive = false;
            this.mouseControler.disable();
            this.enableMouseControl.Text = "Włącz sterowanie myszą";

            this.drawContoler.isActive = (tabControl.SelectedIndex == 2);
        }
    }
}
