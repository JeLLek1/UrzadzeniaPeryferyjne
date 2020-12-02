using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace JoystickProgram
{
    class DrawContoler
    {
        static double moovPesSecond = 500.0;
        static int brushWidth = 8, brushHeight = 8;
        PictureBox box;
        private System.Windows.Forms.Timer timer;
        bool showCursor = false;
        bool isEnabled = true;
        double posX =0, posY=0;
        Bitmap bitmap;
        Bitmap bitmapWithCursor;

        private Color _brushColor = Color.Black;

        public Color BrushColor {
            set {
                if (value != null)
                    _brushColor = value;

                SetCursor();
            }
            get {
                return _brushColor;
            }
        }
        public bool isActive = false;

        public DrawContoler(PictureBox box)
        {
            this.box = box;
            timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(tick_function);
            timer.Interval = 1000;
            timer.Start();
            bitmap = new Bitmap(box.Width, box.Height, PixelFormat.Format24bppRgb);
            using (Graphics grp = Graphics.FromImage(bitmap))
            {
                grp.FillRectangle(Brushes.White, 0, 0, bitmap.Width, bitmap.Height);
            }
            bitmapWithCursor = new Bitmap(bitmap);
            box.Image = bitmapWithCursor;
        }

        public void DrawInput(double dt, double[] leftStick)
        {
            if (!isEnabled) return;
            
            double movX = dt * leftStick[0] * moovPesSecond;
            double movY = dt * leftStick[1] * moovPesSecond;
            if (movX != 0.0 || movY != 0.0)
            {
                this.posX += dt * leftStick[0] * moovPesSecond;
                if (this.posX > this.box.Width - brushWidth) this.posX = this.box.Width - brushWidth;
                if (this.posX < 0) this.posX = 0;
                this.posY += dt * leftStick[1] * moovPesSecond;
                if (this.posY > this.box.Height - brushHeight) this.posY = this.box.Height - brushHeight;
                if (this.posY < 0) this.posY = 0;
                if (leftStick[2] < -0.5)
                {
                    using (Graphics grp = Graphics.FromImage(bitmap))
                    {
                        grp.FillRectangle(new SolidBrush(_brushColor), (int)posX, (int)posY, brushWidth, brushHeight);
                    }
                }
                SetCursor();
            }
        }

        private void tick_function(object sender, EventArgs e)
        {
            if (!isEnabled) return;
            showCursor = !showCursor;
            SetCursor();
        }
        private void SetCursor()
        {
            try
            {
                bitmapWithCursor = new Bitmap(bitmap);

                if (showCursor)
                {
                    using (Graphics grp = Graphics.FromImage(bitmapWithCursor))
                    {
                        grp.FillRectangle(new SolidBrush(_brushColor), (int)posX, (int)posY, brushWidth, brushHeight);
                    }
                }
                else
                {
                    using (Graphics grp = Graphics.FromImage(bitmapWithCursor))
                    {
                        grp.FillRectangle(Brushes.Gray, (int)posX, (int)posY, brushWidth, brushHeight);
                    }
                }
                box.Image = bitmapWithCursor;
            }
            catch { }
        }
    }
}
