
namespace Lab_8
{
    partial class EAN13
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelCode = new System.Windows.Forms.Label();
            this.textBoxCode = new System.Windows.Forms.TextBox();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.labelGS1 = new System.Windows.Forms.Label();
            this.textBoxDigits = new System.Windows.Forms.TextBox();
            this.labelCheck = new System.Windows.Forms.Label();
            this.textBoxCheck = new System.Windows.Forms.TextBox();
            this.pictureBoxBarCode = new System.Windows.Forms.PictureBox();
            this.buttonPrint = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBarCode)).BeginInit();
            this.SuspendLayout();
            // 
            // labelCode
            // 
            this.labelCode.AutoSize = true;
            this.labelCode.Location = new System.Drawing.Point(12, 9);
            this.labelCode.Name = "labelCode";
            this.labelCode.Size = new System.Drawing.Size(66, 13);
            this.labelCode.TabIndex = 0;
            this.labelCode.Text = "Kod EAN13:";
            // 
            // textBoxCode
            // 
            this.textBoxCode.Location = new System.Drawing.Point(15, 25);
            this.textBoxCode.MaxLength = 12;
            this.textBoxCode.Name = "textBoxCode";
            this.textBoxCode.Size = new System.Drawing.Size(292, 20);
            this.textBoxCode.TabIndex = 1;
            this.textBoxCode.TextChanged += new System.EventHandler(this.textBoxCode_TextChanged);
            this.textBoxCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxCode_KeyPress);
            // 
            // buttonAccept
            // 
            this.buttonAccept.Location = new System.Drawing.Point(313, 23);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.Size = new System.Drawing.Size(75, 23);
            this.buttonAccept.TabIndex = 2;
            this.buttonAccept.Text = "Zatwierdź";
            this.buttonAccept.UseVisualStyleBackColor = true;
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // labelGS1
            // 
            this.labelGS1.AutoSize = true;
            this.labelGS1.Location = new System.Drawing.Point(12, 60);
            this.labelGS1.Name = "labelGS1";
            this.labelGS1.Size = new System.Drawing.Size(237, 13);
            this.labelGS1.TabIndex = 3;
            this.labelGS1.Text = "prefiks GS1, Kod producenta oraz Kod produktu:";
            // 
            // textBoxDigits
            // 
            this.textBoxDigits.Location = new System.Drawing.Point(15, 76);
            this.textBoxDigits.Name = "textBoxDigits";
            this.textBoxDigits.ReadOnly = true;
            this.textBoxDigits.Size = new System.Drawing.Size(292, 20);
            this.textBoxDigits.TabIndex = 4;
            // 
            // labelCheck
            // 
            this.labelCheck.AutoSize = true;
            this.labelCheck.Location = new System.Drawing.Point(310, 60);
            this.labelCheck.Name = "labelCheck";
            this.labelCheck.Size = new System.Drawing.Size(81, 13);
            this.labelCheck.TabIndex = 9;
            this.labelCheck.Text = "Cyfra kontrolna:";
            // 
            // textBoxCheck
            // 
            this.textBoxCheck.Location = new System.Drawing.Point(313, 76);
            this.textBoxCheck.Name = "textBoxCheck";
            this.textBoxCheck.ReadOnly = true;
            this.textBoxCheck.Size = new System.Drawing.Size(75, 20);
            this.textBoxCheck.TabIndex = 10;
            // 
            // pictureBoxBarCode
            // 
            this.pictureBoxBarCode.Location = new System.Drawing.Point(15, 111);
            this.pictureBoxBarCode.Name = "pictureBoxBarCode";
            this.pictureBoxBarCode.Size = new System.Drawing.Size(373, 259);
            this.pictureBoxBarCode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxBarCode.TabIndex = 11;
            this.pictureBoxBarCode.TabStop = false;
            // 
            // buttonPrint
            // 
            this.buttonPrint.Location = new System.Drawing.Point(15, 376);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(373, 23);
            this.buttonPrint.TabIndex = 12;
            this.buttonPrint.Text = "Drukuj";
            this.buttonPrint.UseVisualStyleBackColor = true;
            this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
            // 
            // EAN13
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 406);
            this.Controls.Add(this.buttonPrint);
            this.Controls.Add(this.pictureBoxBarCode);
            this.Controls.Add(this.textBoxCheck);
            this.Controls.Add(this.labelCheck);
            this.Controls.Add(this.textBoxDigits);
            this.Controls.Add(this.labelGS1);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.textBoxCode);
            this.Controls.Add(this.labelCode);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "EAN13";
            this.Text = "EAN13";
            this.Load += new System.EventHandler(this.EAN13_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBarCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelCode;
        private System.Windows.Forms.TextBox textBoxCode;
        private System.Windows.Forms.Button buttonAccept;
        private System.Windows.Forms.Label labelGS1;
        private System.Windows.Forms.TextBox textBoxDigits;
        private System.Windows.Forms.Label labelCheck;
        private System.Windows.Forms.TextBox textBoxCheck;
        private System.Windows.Forms.PictureBox pictureBoxBarCode;
        private System.Windows.Forms.Button buttonPrint;
    }
}

