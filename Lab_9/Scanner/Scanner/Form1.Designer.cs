namespace Scanner
{
    partial class scannerForm
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
            this.connectButton = new System.Windows.Forms.Button();
            this.scanGroup = new System.Windows.Forms.GroupBox();
            this.scanButton = new System.Windows.Forms.Button();
            this.colorComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dpiInput = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.contrastBar = new System.Windows.Forms.TrackBar();
            this.brightnessBar = new System.Windows.Forms.TrackBar();
            this.scanPictureBox = new System.Windows.Forms.PictureBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.scanGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dpiInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scanPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(8, 8);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(97, 23);
            this.connectButton.TabIndex = 0;
            this.connectButton.Text = "Wybierz skaner";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // scanGroup
            // 
            this.scanGroup.Controls.Add(this.scanButton);
            this.scanGroup.Controls.Add(this.colorComboBox);
            this.scanGroup.Controls.Add(this.label4);
            this.scanGroup.Controls.Add(this.dpiInput);
            this.scanGroup.Controls.Add(this.label3);
            this.scanGroup.Controls.Add(this.label2);
            this.scanGroup.Controls.Add(this.label1);
            this.scanGroup.Controls.Add(this.contrastBar);
            this.scanGroup.Controls.Add(this.brightnessBar);
            this.scanGroup.Location = new System.Drawing.Point(8, 37);
            this.scanGroup.Name = "scanGroup";
            this.scanGroup.Size = new System.Drawing.Size(509, 182);
            this.scanGroup.TabIndex = 1;
            this.scanGroup.TabStop = false;
            this.scanGroup.Text = "Skanowanie";
            // 
            // scanButton
            // 
            this.scanButton.Location = new System.Drawing.Point(9, 149);
            this.scanButton.Name = "scanButton";
            this.scanButton.Size = new System.Drawing.Size(496, 25);
            this.scanButton.TabIndex = 8;
            this.scanButton.Text = "Skanuj";
            this.scanButton.UseVisualStyleBackColor = true;
            this.scanButton.Click += new System.EventHandler(this.scanButton_Click);
            // 
            // colorComboBox
            // 
            this.colorComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorComboBox.FormattingEnabled = true;
            this.colorComboBox.Items.AddRange(new object[] {
            "Kolor",
            "Skala szarości",
            "1-bitowy"});
            this.colorComboBox.Location = new System.Drawing.Point(9, 122);
            this.colorComboBox.Name = "colorComboBox";
            this.colorComboBox.Size = new System.Drawing.Size(496, 21);
            this.colorComboBox.TabIndex = 7;
            this.colorComboBox.SelectedIndexChanged += new System.EventHandler(this.colorComboBox_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Tryb Kolorów:";
            // 
            // dpiInput
            // 
            this.dpiInput.Location = new System.Drawing.Point(9, 83);
            this.dpiInput.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.dpiInput.Name = "dpiInput";
            this.dpiInput.Size = new System.Drawing.Size(91, 20);
            this.dpiInput.TabIndex = 5;
            this.dpiInput.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.dpiInput.ValueChanged += new System.EventHandler(this.dpiInput_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Rozdzielczość:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(264, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Kontrast:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Jasność:";
            // 
            // contrastBar
            // 
            this.contrastBar.Location = new System.Drawing.Point(267, 32);
            this.contrastBar.Maximum = 1000;
            this.contrastBar.Minimum = -1000;
            this.contrastBar.Name = "contrastBar";
            this.contrastBar.Size = new System.Drawing.Size(238, 45);
            this.contrastBar.TabIndex = 1;
            this.contrastBar.TickFrequency = 50;
            this.contrastBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            // 
            // brightnessBar
            // 
            this.brightnessBar.Location = new System.Drawing.Point(9, 32);
            this.brightnessBar.Maximum = 1000;
            this.brightnessBar.Minimum = -1000;
            this.brightnessBar.Name = "brightnessBar";
            this.brightnessBar.Size = new System.Drawing.Size(238, 45);
            this.brightnessBar.TabIndex = 0;
            this.brightnessBar.TickFrequency = 50;
            this.brightnessBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            // 
            // scanPictureBox
            // 
            this.scanPictureBox.Location = new System.Drawing.Point(8, 256);
            this.scanPictureBox.Name = "scanPictureBox";
            this.scanPictureBox.Size = new System.Drawing.Size(509, 719);
            this.scanPictureBox.TabIndex = 2;
            this.scanPictureBox.TabStop = false;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(17, 225);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(496, 25);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "Zapisz obraz";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // scannerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 982);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.scanPictureBox);
            this.Controls.Add(this.scanGroup);
            this.Controls.Add(this.connectButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "scannerForm";
            this.Text = "Skaner";
            this.Load += new System.EventHandler(this.scannerForm_Load);
            this.scanGroup.ResumeLayout(false);
            this.scanGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dpiInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scanPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.GroupBox scanGroup;
        private System.Windows.Forms.NumericUpDown dpiInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar contrastBar;
        private System.Windows.Forms.TrackBar brightnessBar;
        private System.Windows.Forms.Button scanButton;
        private System.Windows.Forms.ComboBox colorComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox scanPictureBox;
        private System.Windows.Forms.Button saveButton;
    }
}

