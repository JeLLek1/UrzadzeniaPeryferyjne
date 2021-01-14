using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Lab_8
{
    public partial class EAN13 : Form
    {
        private Ean13 Ean13;

        public EAN13()
        {
            InitializeComponent();
        }

        private void EAN13_Load(object sender, EventArgs e)
        {
            buttonAccept.Enabled = false;
            buttonPrint.Enabled = false;
        }

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            var code = textBoxCode.Text;
            if (code.Length == 12)
                try
                {
                    Ean13 = new Ean13(code);
                    textBoxDigits.Text = Ean13.BarCode;
                    textBoxCheck.Text = Ean13.CheckSum;
                    if (pictureBoxBarCode.Image != null) pictureBoxBarCode.Image.Dispose();

                    pictureBoxBarCode.Image = Ean13.GenerateBarCode();
                    buttonPrint.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Wystąpił problem podczas tworzenia kodu kreskowego", "Błąd", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
        }

        private void textBoxCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                MessageBox.Show("Dozwolone są jedynie cyfry", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void textBoxCode_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text.Length == 12)
                buttonAccept.Enabled = true;
            else
                buttonAccept.Enabled = false;
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            if (Ean13 == null || Ean13.GetBarcodeBitmap() == null)
                MessageBox.Show("Brak przypisanego obrazka", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            var printDocument = new PrintDocument();
            var printDialog = new PrintDialog();
            printDocument.PrintPage += (s, arg) =>
            {
                var mmPerInch = 25.4f;
                var eanSize = Ean13.GetCodeSize();
                var barCode = Ean13.GetBarcodeBitmap();

                arg.Graphics.DrawImage(Ean13.GetBarcodeBitmap(),
                    new RectangleF(
                        arg.PageSettings.Margins.Left,
                        arg.PageSettings.Margins.Top,
                        100 * eanSize.X / mmPerInch,
                        100 * eanSize.Y / mmPerInch));
            };
            printDialog.Document = printDocument;
            if (printDialog.ShowDialog() == DialogResult.OK) printDocument.Print();
        }
    }
}