namespace Bluetooth
{
    partial class Bluetooth
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
            this.label1 = new System.Windows.Forms.Label();
            this.BTAdapterCB = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.BTDeviceCB = new System.Windows.Forms.ComboBox();
            this.AdapterInfo = new System.Windows.Forms.Label();
            this.PairButton = new System.Windows.Forms.Button();
            this.deviceSelectedBox = new System.Windows.Forms.GroupBox();
            this.ConnectInput = new System.Windows.Forms.TextBox();
            this.AutorisationInput = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.DeviceAddressInput = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DeviceNameInput = new System.Windows.Forms.TextBox();
            this.ParingText = new System.Windows.Forms.Label();
            this.OperationsBox = new System.Windows.Forms.GroupBox();
            this.refreshData = new System.Windows.Forms.Button();
            this.SendingText = new System.Windows.Forms.Label();
            this.SendFileButton = new System.Windows.Forms.Button();
            this.LoadDevicesButton = new System.Windows.Forms.Button();
            this.deviceSelectedBox.SuspendLayout();
            this.OperationsBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Wybierz adapter BT:";
            // 
            // BTAdapterCB
            // 
            this.BTAdapterCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BTAdapterCB.FormattingEnabled = true;
            this.BTAdapterCB.Location = new System.Drawing.Point(15, 25);
            this.BTAdapterCB.Name = "BTAdapterCB";
            this.BTAdapterCB.Size = new System.Drawing.Size(345, 21);
            this.BTAdapterCB.TabIndex = 3;
            this.BTAdapterCB.SelectedIndexChanged += new System.EventHandler(this.BTAdapterCB_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Wybierz urządzenie BT";
            // 
            // BTDeviceCB
            // 
            this.BTDeviceCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BTDeviceCB.FormattingEnabled = true;
            this.BTDeviceCB.Location = new System.Drawing.Point(15, 91);
            this.BTDeviceCB.Name = "BTDeviceCB";
            this.BTDeviceCB.Size = new System.Drawing.Size(345, 21);
            this.BTDeviceCB.TabIndex = 5;
            this.BTDeviceCB.SelectedIndexChanged += new System.EventHandler(this.BTDeviceCB_SelectedIndexChanged);
            // 
            // AdapterInfo
            // 
            this.AdapterInfo.AutoSize = true;
            this.AdapterInfo.Location = new System.Drawing.Point(132, 54);
            this.AdapterInfo.Name = "AdapterInfo";
            this.AdapterInfo.Size = new System.Drawing.Size(0, 13);
            this.AdapterInfo.TabIndex = 6;
            // 
            // PairButton
            // 
            this.PairButton.Location = new System.Drawing.Point(15, 118);
            this.PairButton.Name = "PairButton";
            this.PairButton.Size = new System.Drawing.Size(109, 22);
            this.PairButton.TabIndex = 7;
            this.PairButton.Text = "Paruj";
            this.PairButton.UseVisualStyleBackColor = true;
            this.PairButton.Click += new System.EventHandler(this.PairButton_Click);
            // 
            // deviceSelectedBox
            // 
            this.deviceSelectedBox.Controls.Add(this.ConnectInput);
            this.deviceSelectedBox.Controls.Add(this.AutorisationInput);
            this.deviceSelectedBox.Controls.Add(this.label7);
            this.deviceSelectedBox.Controls.Add(this.DeviceAddressInput);
            this.deviceSelectedBox.Controls.Add(this.label6);
            this.deviceSelectedBox.Controls.Add(this.label5);
            this.deviceSelectedBox.Controls.Add(this.label4);
            this.deviceSelectedBox.Controls.Add(this.DeviceNameInput);
            this.deviceSelectedBox.Location = new System.Drawing.Point(15, 146);
            this.deviceSelectedBox.Name = "deviceSelectedBox";
            this.deviceSelectedBox.Size = new System.Drawing.Size(345, 146);
            this.deviceSelectedBox.TabIndex = 8;
            this.deviceSelectedBox.TabStop = false;
            this.deviceSelectedBox.Text = "Informacje na temat urządzenia:";
            // 
            // ConnectInput
            // 
            this.ConnectInput.Enabled = false;
            this.ConnectInput.Location = new System.Drawing.Point(166, 111);
            this.ConnectInput.Name = "ConnectInput";
            this.ConnectInput.ReadOnly = true;
            this.ConnectInput.Size = new System.Drawing.Size(170, 20);
            this.ConnectInput.TabIndex = 16;
            // 
            // AutorisationInput
            // 
            this.AutorisationInput.Enabled = false;
            this.AutorisationInput.Location = new System.Drawing.Point(6, 111);
            this.AutorisationInput.Name = "AutorisationInput";
            this.AutorisationInput.ReadOnly = true;
            this.AutorisationInput.Size = new System.Drawing.Size(154, 20);
            this.AutorisationInput.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 56);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Adres urządzenia:";
            // 
            // DeviceAddressInput
            // 
            this.DeviceAddressInput.Enabled = false;
            this.DeviceAddressInput.Location = new System.Drawing.Point(6, 72);
            this.DeviceAddressInput.Name = "DeviceAddressInput";
            this.DeviceAddressInput.ReadOnly = true;
            this.DeviceAddressInput.Size = new System.Drawing.Size(330, 20);
            this.DeviceAddressInput.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(163, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Połączono:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Autoryzacja:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Nazwa urządzenia:";
            // 
            // DeviceNameInput
            // 
            this.DeviceNameInput.Enabled = false;
            this.DeviceNameInput.Location = new System.Drawing.Point(6, 33);
            this.DeviceNameInput.Name = "DeviceNameInput";
            this.DeviceNameInput.ReadOnly = true;
            this.DeviceNameInput.Size = new System.Drawing.Size(330, 20);
            this.DeviceNameInput.TabIndex = 9;
            // 
            // ParingText
            // 
            this.ParingText.AutoSize = true;
            this.ParingText.Location = new System.Drawing.Point(132, 123);
            this.ParingText.Name = "ParingText";
            this.ParingText.Size = new System.Drawing.Size(0, 13);
            this.ParingText.TabIndex = 9;
            // 
            // OperationsBox
            // 
            this.OperationsBox.Controls.Add(this.refreshData);
            this.OperationsBox.Controls.Add(this.SendingText);
            this.OperationsBox.Controls.Add(this.SendFileButton);
            this.OperationsBox.Location = new System.Drawing.Point(12, 298);
            this.OperationsBox.Name = "OperationsBox";
            this.OperationsBox.Size = new System.Drawing.Size(347, 47);
            this.OperationsBox.TabIndex = 10;
            this.OperationsBox.TabStop = false;
            this.OperationsBox.Text = "Operacje";
            // 
            // refreshData
            // 
            this.refreshData.Location = new System.Drawing.Point(249, 19);
            this.refreshData.Name = "refreshData";
            this.refreshData.Size = new System.Drawing.Size(90, 23);
            this.refreshData.TabIndex = 2;
            this.refreshData.Text = "OdświerzDane";
            this.refreshData.UseVisualStyleBackColor = true;
            this.refreshData.Click += new System.EventHandler(this.refreshData_Click);
            // 
            // SendingText
            // 
            this.SendingText.AutoSize = true;
            this.SendingText.Location = new System.Drawing.Point(84, 24);
            this.SendingText.Name = "SendingText";
            this.SendingText.Size = new System.Drawing.Size(0, 13);
            this.SendingText.TabIndex = 1;
            // 
            // SendFileButton
            // 
            this.SendFileButton.Location = new System.Drawing.Point(3, 19);
            this.SendFileButton.Name = "SendFileButton";
            this.SendFileButton.Size = new System.Drawing.Size(75, 23);
            this.SendFileButton.TabIndex = 0;
            this.SendFileButton.Text = "Prześlij Plik";
            this.SendFileButton.UseVisualStyleBackColor = true;
            this.SendFileButton.Click += new System.EventHandler(this.SendFileButton_Click);
            // 
            // LoadDevicesButton
            // 
            this.LoadDevicesButton.Location = new System.Drawing.Point(15, 49);
            this.LoadDevicesButton.Name = "LoadDevicesButton";
            this.LoadDevicesButton.Size = new System.Drawing.Size(111, 23);
            this.LoadDevicesButton.TabIndex = 11;
            this.LoadDevicesButton.Text = "Wczytaj urządzenia";
            this.LoadDevicesButton.UseVisualStyleBackColor = true;
            this.LoadDevicesButton.Click += new System.EventHandler(this.LoadDevicesButton_Click);
            // 
            // Bluetooth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 375);
            this.Controls.Add(this.LoadDevicesButton);
            this.Controls.Add(this.OperationsBox);
            this.Controls.Add(this.ParingText);
            this.Controls.Add(this.deviceSelectedBox);
            this.Controls.Add(this.AdapterInfo);
            this.Controls.Add(this.BTDeviceCB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BTAdapterCB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PairButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Bluetooth";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.deviceSelectedBox.ResumeLayout(false);
            this.deviceSelectedBox.PerformLayout();
            this.OperationsBox.ResumeLayout(false);
            this.OperationsBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox BTAdapterCB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox BTDeviceCB;
        private System.Windows.Forms.Label AdapterInfo;
        private System.Windows.Forms.Button PairButton;
        private System.Windows.Forms.GroupBox deviceSelectedBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox DeviceNameInput;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox DeviceAddressInput;
        private System.Windows.Forms.TextBox ConnectInput;
        private System.Windows.Forms.TextBox AutorisationInput;
        private System.Windows.Forms.Label ParingText;
        private System.Windows.Forms.GroupBox OperationsBox;
        private System.Windows.Forms.Button SendFileButton;
        private System.Windows.Forms.Label SendingText;
        private System.Windows.Forms.Button refreshData;
        private System.Windows.Forms.Button LoadDevicesButton;
    }
}

