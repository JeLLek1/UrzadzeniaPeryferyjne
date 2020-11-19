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

namespace Scanner
{
    public partial class scannerForm : Form
    {
        Scanner scanner;
        int selectedColor = 0;
        MemoryStream ms = null;
        Decimal dpiInputValue; //przechowanie dla testu czy da się ustawić

        private System.Windows.Forms.Timer timer;//timer do przechwytywania zdjęcia z eventu
        MemoryStream msFromEvent = null; //przechowuje zeskanowane z eventu zdjęcie
        public scannerForm()
        {
            InitializeComponent();
            scanner = new Scanner(this);
            timer = new System.Windows.Forms.Timer();
            //inicjalizacja timera
            timer.Tick += new EventHandler(tick_function);
            timer.Interval = 500;
            timer.Start();
        }
        
        //Funckja sprawdzająca, czy nie pobrano zdjęcia z eventu
        private void tick_function(object sender, EventArgs e)
        {
            if (this.msFromEvent == null) return;
            MemoryStream msTemp = this.msFromEvent;
            this.msFromEvent = null;
            if (MessageBox.Show("Skaner wysłał zeskanowany plik. Wczytać go do programu?", "Skan z urządzenia", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                if (this.ms != null) this.ms.Dispose();
                this.ms = msTemp;
                this.scanPictureBox.Image = Image.FromStream(this.ms);
                this.saveButton.Enabled = true;
            }
            else
            {
                msTemp.Dispose();
            }
        }

        //ustawienie memoryStream z eventu
        public void setmsFromEvent(MemoryStream ms)
        {
            this.msFromEvent = ms;
        }

        private void scannerForm_Load(object sender, EventArgs e)
        {
            scanPictureBox.SizeMode = PictureBoxSizeMode.StretchImage; //zmiana wielkości obrazka w zależności od wielkości PictureBox
            this.colorComboBox.SelectedIndex = selectedColor; //inicjalizacja listy trybu kolorów
            this.scanGroup.Enabled = false; //najpierw trzeba wybrać skaner
            this.saveButton.Enabled = false; //najpierw trzeba pobrać zdjęcie
            this.dpiInputValue = this.dpiInput.Value;
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            this.scanner.selectScanner();
            this.scanGroup.Enabled = (this.scanner.isScannerSelected());

        }

        private void scanButton_Click(object sender, EventArgs e)
        {
            if(this.ms != null) ms.Dispose();
            this.ms = this.scanner.scan(this.selectedColor, this.dpiInput.Value, this.brightnessBar.Value, this.contrastBar.Value);
            this.saveButton.Enabled = (this.ms != null);

            if (this.ms == null)
            {
                MessageBox.Show("Błąd podczas skanowania. Spróbuj ponownie wybrać skaner", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.scanGroup.Enabled = false;
                return;
            }
            this.scanPictureBox.Image = Image.FromStream(this.ms);
        }

        private void colorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.selectedColor = this.colorComboBox.SelectedIndex;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (this.ms == null) return;

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Bitmap Image|*.bmp";
            dialog.Title = "Zapisz skan do pliku BMP";
            dialog.InitialDirectory = Directory.GetCurrentDirectory();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileStream fs = (System.IO.FileStream)dialog.OpenFile();
                new Bitmap(this.ms).Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                fs.Close();
            }
        }

        private void dpiInput_ValueChanged(object sender, EventArgs e)
        {
            if (this.scanner.checkDPI(this.dpiInput.Value))
            {
                this.dpiInputValue = this.dpiInput.Value;
            }
            else
            {
                MessageBox.Show("Niestety podane DPI nie jest dosptępne", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.dpiInput.Value = this.dpiInputValue;
            }
        }
    }
}
