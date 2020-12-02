using System;
using System.Runtime.InteropServices;

namespace JoystickProgram
{
    class MouseControler
    {
        static double moovPesSecond = 1000.0;
        bool isEnabled = false;
        double mouseX, mouseY;
        bool leftDown = false, rightDown = false;

        [Flags]
        private enum MouseEventFlags
        {
            LeftDown = 0x00000002,
            LeftUp = 0x00000004,
            MiddleDown = 0x00000020,
            MiddleUp = 0x00000040,
            Move = 0x00000001,
            Absolute = 0x00008000,
            RightDown = 0x00000008,
            RightUp = 0x00000010
        }

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetCursorPos(out MousePoint lpMousePoint);

        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        private static MousePoint GetCursorPosition()
        {
            MousePoint currentMousePoint;
            var gotPoint = GetCursorPos(out currentMousePoint);
            if (!gotPoint) { currentMousePoint = new MousePoint(0, 0); }
            return currentMousePoint;
        }

        private void MouseEvent(MouseEventFlags value)
        {
            MousePoint position = GetCursorPosition();

            mouse_event
                ((int)value,
                 position.X,
                 position.Y,
                 0,
                 0)
                ;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MousePoint
        {
            public int X;
            public int Y;

            public MousePoint(int x, int y)
            {
                X = x;
                Y = y;
            }
            public bool checkDiff(double x, double y)
            {
                if(X != (int)x || Y != (int)y)
                    return true;
            
                return false;
            }
        }

        public void enable()
        {
            this.isEnabled = true;
            MousePoint cursorPos = MouseControler.GetCursorPosition();
            this.mouseX = (double)cursorPos.X;
            this.mouseY = (double)cursorPos.Y;
        }
        public void disable()
        {
            this.isEnabled = false;
        }

        public bool IsEnabled()
        {
            return this.isEnabled;
        }

        public void MouseInput(double dt, double[] leftStick)
        {
            if (!this.isEnabled) return;

            MousePoint cursorPos = MouseControler.GetCursorPosition();
            if (cursorPos.checkDiff(this.mouseX, this.mouseY))
            {
                this.mouseX = (double)cursorPos.X;
                this.mouseY = (double)cursorPos.Y;
            }

            this.mouseX += dt * leftStick[0] * moovPesSecond;
            this.mouseY += dt * leftStick[1] * moovPesSecond;
            MouseControler.SetCursorPos((int)this.mouseX, (int)this.mouseY);

            if (!this.leftDown && leftStick[2] < -0.5)
            {
                this.MouseEvent(MouseEventFlags.LeftDown);
                this.leftDown = true;
            }
            if (this.leftDown && leftStick[2] >= -0.5)
            {
                this.MouseEvent(MouseEventFlags.LeftUp);
                this.leftDown = false;
            }
            if (!this.rightDown && leftStick[2] > 0.5)
            {
                this.MouseEvent(MouseEventFlags.RightDown);
                this.rightDown = true;
            }
            if (this.rightDown && leftStick[2] <= 0.5)
            {
                this.MouseEvent(MouseEventFlags.RightUp);
                this.rightDown = false;
            }
        }
    }
}
