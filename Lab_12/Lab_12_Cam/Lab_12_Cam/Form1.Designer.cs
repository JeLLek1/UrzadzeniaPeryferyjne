namespace Lab_12_Cam
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.CamView = new System.Windows.Forms.PictureBox();
            this.Connect = new System.Windows.Forms.Button();
            this.Disconnect = new System.Windows.Forms.Button();
            this.Picture = new System.Windows.Forms.Button();
            this.Record = new System.Windows.Forms.Button();
            this.Parameters = new System.Windows.Forms.Button();
            this.Resolution = new System.Windows.Forms.Button();
            this.Page = new System.Windows.Forms.Button();
            this.Motion = new System.Windows.Forms.Button();
            this.MCI_combo_box = new System.Windows.Forms.ComboBox();
            this.MDI_text = new System.Windows.Forms.Label();
            this.MotionSens = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.CamView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MotionSens)).BeginInit();
            this.SuspendLayout();
            // 
            // CamView
            // 
            resources.ApplyResources(this.CamView, "CamView");
            this.CamView.Name = "CamView";
            this.CamView.TabStop = false;
            this.CamView.Click += new System.EventHandler(this.CamView_Click);
            // 
            // Connect
            // 
            resources.ApplyResources(this.Connect, "Connect");
            this.Connect.Name = "Connect";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // Disconnect
            // 
            resources.ApplyResources(this.Disconnect, "Disconnect");
            this.Disconnect.Name = "Disconnect";
            this.Disconnect.UseVisualStyleBackColor = true;
            this.Disconnect.Click += new System.EventHandler(this.Disconnect_Click);
            // 
            // Picture
            // 
            resources.ApplyResources(this.Picture, "Picture");
            this.Picture.Name = "Picture";
            this.Picture.UseVisualStyleBackColor = true;
            this.Picture.Click += new System.EventHandler(this.Picture_Click);
            // 
            // Record
            // 
            resources.ApplyResources(this.Record, "Record");
            this.Record.Name = "Record";
            this.Record.UseVisualStyleBackColor = true;
            this.Record.Click += new System.EventHandler(this.Record_Click);
            // 
            // Parameters
            // 
            resources.ApplyResources(this.Parameters, "Parameters");
            this.Parameters.Name = "Parameters";
            this.Parameters.UseVisualStyleBackColor = true;
            this.Parameters.Click += new System.EventHandler(this.Parameters_Click);
            // 
            // Resolution
            // 
            resources.ApplyResources(this.Resolution, "Resolution");
            this.Resolution.Name = "Resolution";
            this.Resolution.UseVisualStyleBackColor = true;
            this.Resolution.Click += new System.EventHandler(this.Resolution_Click);
            // 
            // Page
            // 
            resources.ApplyResources(this.Page, "Page");
            this.Page.Name = "Page";
            this.Page.UseVisualStyleBackColor = true;
            this.Page.Click += new System.EventHandler(this.Page_Click);
            // 
            // Motion
            // 
            resources.ApplyResources(this.Motion, "Motion");
            this.Motion.Name = "Motion";
            this.Motion.UseVisualStyleBackColor = true;
            this.Motion.Click += new System.EventHandler(this.Motion_Click);
            // 
            // MCI_combo_box
            // 
            this.MCI_combo_box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MCI_combo_box.FormattingEnabled = true;
            resources.ApplyResources(this.MCI_combo_box, "MCI_combo_box");
            this.MCI_combo_box.Name = "MCI_combo_box";
            this.MCI_combo_box.SelectedIndexChanged += new System.EventHandler(this.MCI_combobox_SelectedIndexChanged);
            // 
            // MDI_text
            // 
            resources.ApplyResources(this.MDI_text, "MDI_text");
            this.MDI_text.Name = "MDI_text";
            // 
            // MotionSens
            // 
            this.MotionSens.DecimalPlaces = 1;
            this.MotionSens.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            resources.ApplyResources(this.MotionSens, "MotionSens");
            this.MotionSens.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MotionSens.Name = "MotionSens";
            this.MotionSens.ValueChanged += new System.EventHandler(this.MotionSens_ValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MotionSens);
            this.Controls.Add(this.MDI_text);
            this.Controls.Add(this.MCI_combo_box);
            this.Controls.Add(this.Motion);
            this.Controls.Add(this.Page);
            this.Controls.Add(this.Resolution);
            this.Controls.Add(this.Parameters);
            this.Controls.Add(this.Record);
            this.Controls.Add(this.Picture);
            this.Controls.Add(this.Disconnect);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.CamView);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CamView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MotionSens)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox CamView;
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.Button Disconnect;
        private System.Windows.Forms.Button Picture;
        private System.Windows.Forms.Button Record;
        private System.Windows.Forms.Button Parameters;
        private System.Windows.Forms.Button Resolution;
        private System.Windows.Forms.Button Page;
        private System.Windows.Forms.Button Motion;
        private System.Windows.Forms.ComboBox MCI_combo_box;
        private System.Windows.Forms.Label MDI_text;
        private System.Windows.Forms.NumericUpDown MotionSens;
        private System.Windows.Forms.Label label1;
    }
}

