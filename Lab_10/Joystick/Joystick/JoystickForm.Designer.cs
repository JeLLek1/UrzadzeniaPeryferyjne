
namespace JoystickProgram
{
    partial class JoystickForm
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.JoystickSelect = new System.Windows.Forms.ComboBox();
            this.ReloadButton = new System.Windows.Forms.Button();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.buttonsPage = new System.Windows.Forms.TabPage();
            this.joystickInfo = new System.Windows.Forms.TextBox();
            this.mousePage = new System.Windows.Forms.TabPage();
            this.enableMouseControl = new System.Windows.Forms.Button();
            this.drawPage = new System.Windows.Forms.TabPage();
            this.clearButton = new System.Windows.Forms.Button();
            this.drawBox = new System.Windows.Forms.PictureBox();
            this.colorButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.buttonsPage.SuspendLayout();
            this.mousePage.SuspendLayout();
            this.drawPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawBox)).BeginInit();
            this.SuspendLayout();
            // 
            // JoystickSelect
            // 
            this.JoystickSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.JoystickSelect.FormattingEnabled = true;
            this.JoystickSelect.Location = new System.Drawing.Point(12, 12);
            this.JoystickSelect.Name = "JoystickSelect";
            this.JoystickSelect.Size = new System.Drawing.Size(475, 21);
            this.JoystickSelect.TabIndex = 0;
            this.JoystickSelect.SelectedIndexChanged += new System.EventHandler(this.JoystickSelect_SelectedIndexChanged);
            // 
            // ReloadButton
            // 
            this.ReloadButton.Location = new System.Drawing.Point(12, 39);
            this.ReloadButton.Name = "ReloadButton";
            this.ReloadButton.Size = new System.Drawing.Size(237, 22);
            this.ReloadButton.TabIndex = 1;
            this.ReloadButton.Text = "Odśwież listę urządzeń";
            this.ReloadButton.UseVisualStyleBackColor = true;
            this.ReloadButton.Click += new System.EventHandler(this.ReloadButton_Click);
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(250, 39);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(237, 22);
            this.ConnectButton.TabIndex = 2;
            this.ConnectButton.Text = "Połącz z wybranym";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.buttonsPage);
            this.tabControl.Controls.Add(this.mousePage);
            this.tabControl.Controls.Add(this.drawPage);
            this.tabControl.Location = new System.Drawing.Point(12, 67);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(475, 522);
            this.tabControl.TabIndex = 0;
            this.tabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl_Selected);
            // 
            // buttonsPage
            // 
            this.buttonsPage.Controls.Add(this.joystickInfo);
            this.buttonsPage.Location = new System.Drawing.Point(4, 22);
            this.buttonsPage.Name = "buttonsPage";
            this.buttonsPage.Padding = new System.Windows.Forms.Padding(3);
            this.buttonsPage.Size = new System.Drawing.Size(467, 496);
            this.buttonsPage.TabIndex = 0;
            this.buttonsPage.Text = "Pokaż przyciski";
            this.buttonsPage.UseVisualStyleBackColor = true;
            // 
            // joystickInfo
            // 
            this.joystickInfo.Location = new System.Drawing.Point(6, 6);
            this.joystickInfo.Multiline = true;
            this.joystickInfo.Name = "joystickInfo";
            this.joystickInfo.ReadOnly = true;
            this.joystickInfo.Size = new System.Drawing.Size(455, 484);
            this.joystickInfo.TabIndex = 0;
            // 
            // mousePage
            // 
            this.mousePage.Controls.Add(this.enableMouseControl);
            this.mousePage.Location = new System.Drawing.Point(4, 22);
            this.mousePage.Name = "mousePage";
            this.mousePage.Padding = new System.Windows.Forms.Padding(3);
            this.mousePage.Size = new System.Drawing.Size(467, 496);
            this.mousePage.TabIndex = 1;
            this.mousePage.Text = "Sterowanie myszą";
            this.mousePage.UseVisualStyleBackColor = true;
            // 
            // enableMouseControl
            // 
            this.enableMouseControl.Location = new System.Drawing.Point(6, 6);
            this.enableMouseControl.Name = "enableMouseControl";
            this.enableMouseControl.Size = new System.Drawing.Size(455, 31);
            this.enableMouseControl.TabIndex = 0;
            this.enableMouseControl.Text = "Włącz sterowanie myszą";
            this.enableMouseControl.UseVisualStyleBackColor = true;
            this.enableMouseControl.Click += new System.EventHandler(this.enableMouseControl_Click);
            // 
            // drawPage
            // 
            this.drawPage.Controls.Add(this.saveButton);
            this.drawPage.Controls.Add(this.clearButton);
            this.drawPage.Controls.Add(this.drawBox);
            this.drawPage.Controls.Add(this.colorButton);
            this.drawPage.Location = new System.Drawing.Point(4, 22);
            this.drawPage.Name = "drawPage";
            this.drawPage.Padding = new System.Windows.Forms.Padding(3);
            this.drawPage.Size = new System.Drawing.Size(467, 496);
            this.drawPage.TabIndex = 2;
            this.drawPage.Text = "Rysowanie";
            this.drawPage.UseVisualStyleBackColor = true;
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(234, 6);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(227, 28);
            this.clearButton.TabIndex = 3;
            this.clearButton.Text = "Wyczyść";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // drawBox
            // 
            this.drawBox.Location = new System.Drawing.Point(8, 40);
            this.drawBox.Name = "drawBox";
            this.drawBox.Size = new System.Drawing.Size(452, 419);
            this.drawBox.TabIndex = 2;
            this.drawBox.TabStop = false;
            // 
            // colorButton
            // 
            this.colorButton.Location = new System.Drawing.Point(6, 6);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(227, 28);
            this.colorButton.TabIndex = 0;
            this.colorButton.Text = "Wybierz kolor";
            this.colorButton.UseVisualStyleBackColor = true;
            this.colorButton.Click += new System.EventHandler(this.colorButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(6, 465);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(454, 28);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Zapisz do pliku";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // JoystickForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 601);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.ReloadButton);
            this.Controls.Add(this.JoystickSelect);
            this.Name = "JoystickForm";
            this.Text = "Joystick";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.JoystickForm_FormClosing);
            this.Load += new System.EventHandler(this.JoystickForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.JoystickForm_KeyDown);
            this.tabControl.ResumeLayout(false);
            this.buttonsPage.ResumeLayout(false);
            this.buttonsPage.PerformLayout();
            this.mousePage.ResumeLayout(false);
            this.drawPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.drawBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox JoystickSelect;
        private System.Windows.Forms.Button ReloadButton;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage buttonsPage;
        private System.Windows.Forms.TabPage mousePage;
        private System.Windows.Forms.TabPage drawPage;
        private System.Windows.Forms.Button enableMouseControl;
        private System.Windows.Forms.PictureBox drawBox;
        private System.Windows.Forms.Button colorButton;
        private System.Windows.Forms.TextBox joystickInfo;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button saveButton;
    }
}

