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
            this.CamView = new System.Windows.Forms.PictureBox();
            this.Connect = new System.Windows.Forms.Button();
            this.Disconnect = new System.Windows.Forms.Button();
            this.Picture = new System.Windows.Forms.Button();
            this.Record = new System.Windows.Forms.Button();
            this.Parameters = new System.Windows.Forms.Button();
            this.Resolution = new System.Windows.Forms.Button();
            this.Page = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CamView)).BeginInit();
            this.SuspendLayout();
            // 
            // CamView
            // 
            this.CamView.Location = new System.Drawing.Point(10, 12);
            this.CamView.Name = "CamView";
            this.CamView.Size = new System.Drawing.Size(426, 360);
            this.CamView.TabIndex = 0;
            this.CamView.TabStop = false;
            this.CamView.Click += new System.EventHandler(this.CamView_Click);
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(10, 399);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(75, 23);
            this.Connect.TabIndex = 1;
            this.Connect.Text = "Połącz";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // Disconnect
            // 
            this.Disconnect.Location = new System.Drawing.Point(91, 399);
            this.Disconnect.Name = "Disconnect";
            this.Disconnect.Size = new System.Drawing.Size(75, 22);
            this.Disconnect.TabIndex = 2;
            this.Disconnect.Text = "Rozłącz";
            this.Disconnect.UseVisualStyleBackColor = true;
            this.Disconnect.Click += new System.EventHandler(this.Disconnect_Click);
            // 
            // Picture
            // 
            this.Picture.Location = new System.Drawing.Point(10, 428);
            this.Picture.Name = "Picture";
            this.Picture.Size = new System.Drawing.Size(75, 26);
            this.Picture.TabIndex = 3;
            this.Picture.Text = "Zrób zdjęcie";
            this.Picture.UseVisualStyleBackColor = true;
            this.Picture.Click += new System.EventHandler(this.Picture_Click);
            // 
            // Record
            // 
            this.Record.Location = new System.Drawing.Point(93, 432);
            this.Record.Name = "Record";
            this.Record.Size = new System.Drawing.Size(125, 21);
            this.Record.TabIndex = 4;
            this.Record.Text = "Rozpocznij nagrywanie";
            this.Record.UseVisualStyleBackColor = true;
            this.Record.Click += new System.EventHandler(this.Record_Click);
            // 
            // Parameters
            // 
            this.Parameters.Location = new System.Drawing.Point(224, 432);
            this.Parameters.Name = "Parameters";
            this.Parameters.Size = new System.Drawing.Size(91, 21);
            this.Parameters.TabIndex = 5;
            this.Parameters.Text = "Właściwości";
            this.Parameters.UseVisualStyleBackColor = true;
            this.Parameters.Click += new System.EventHandler(this.Parameters_Click);
            // 
            // Resolution
            // 
            this.Resolution.Location = new System.Drawing.Point(329, 432);
            this.Resolution.Name = "Resolution";
            this.Resolution.Size = new System.Drawing.Size(107, 21);
            this.Resolution.TabIndex = 6;
            this.Resolution.Text = "Rozdzielczość";
            this.Resolution.UseVisualStyleBackColor = true;
            this.Resolution.Click += new System.EventHandler(this.Resolution_Click);
            // 
            // Page
            // 
            this.Page.Location = new System.Drawing.Point(10, 460);
            this.Page.Name = "Page";
            this.Page.Size = new System.Drawing.Size(92, 21);
            this.Page.TabIndex = 7;
            this.Page.Text = "Otwrzórz stronę";
            this.Page.UseVisualStyleBackColor = true;
            this.Page.Click += new System.EventHandler(this.Page_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(455, 511);
            this.Controls.Add(this.Page);
            this.Controls.Add(this.Resolution);
            this.Controls.Add(this.Parameters);
            this.Controls.Add(this.Record);
            this.Controls.Add(this.Picture);
            this.Controls.Add(this.Disconnect);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.CamView);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CamView)).EndInit();
            this.ResumeLayout(false);

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
    }
}

