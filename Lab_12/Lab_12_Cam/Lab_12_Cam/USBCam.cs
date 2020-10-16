using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace Lab_12_Cam
{
    public class USBCam : IDisposable
    {
        /* Those contants are used to overload the unmanaged code functions
     * each constant represent a state*/

        private static USBCam instance= null;
        public bool is_enabled = false;
        Bitmap last_frame = null;
        const float image_diff = 0.1f;

        private const int WM_USER = 0x400;
        private const int WM_CAP = WM_USER;
        private const int WM_CAP_DRIVER_CONNECT = WM_CAP + 10;
        private const int WM_CAP_DRIVER_DISCONNECT = WM_CAP + 11;
        private const int WM_CAP_EDIT_COPY = 0x41e;
        private const int WM_CAP_SET_PREVIEW = WM_CAP + 50;
        private const int WM_CAP_SET_PREVIEWRATE = WM_CAP + 52;
        private const int WM_CAP_SET_SCALE = WM_CAP + 53;
        private const int WM_CAP_FILE_SET_CAPTURE_FILE = WM_CAP + 20;
        private const int WM_CAP_SEQUENCE = WM_CAP + 62;
        private const int WM_CAP_STOP = WM_CAP + 68;
        private const int WM_CAP_FILE_SAVEAS = WM_CAP + 23;
        private const int WM_CAP_FILE_SAVEDIB = WM_CAP + 25;
        private const int WM_CAP_DLG_VIDEOSOURCE = WM_CAP + 42;
        private const int WM_CAP_DLG_VIDEOFORMAT = WM_CAP + 41;

        private const int WS_CHILD = 0x40000000;
        private const int WS_VISIBLE = 0x10000000;
        private const short SWP_NOMOVE = 0x2;
        private short SWP_NOZORDER = 0x4;
        private short HWND_BOTTOM = 1;

        //This function enables enumerate the web cam devices
        [DllImport("avicap32.dll")]
        protected static extern bool capGetDriverDescriptionA(short wDriverIndex,
            [MarshalAs(UnmanagedType.VBByRefStr)]ref String lpszName,
           int cbName, [MarshalAs(UnmanagedType.VBByRefStr)] ref String lpszVer, int cbVer);

        //This function enables create a  window child with so that you can display it in a picturebox for example
        [DllImport("avicap32.dll")]
        protected static extern int capCreateCaptureWindowA([MarshalAs(UnmanagedType.VBByRefStr)] ref string
    lpszWindowName,
            int dwStyle, int x, int y, int nWidth, int nHeight, int hWndParent, int nID);

        //This function enables set changes to the size, position, and Z order of a child window
        [DllImport("user32")]
        protected static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        //This function enables send the specified message to a window or windows
        [DllImport("user32", EntryPoint = "SendMessageA")]
        protected static extern int SendMessage(int hwnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.AsAny)] object
    lParam);

        //This function enable destroy the window child
        [DllImport("user32")]
        protected static extern bool DestroyWindow(int hwnd);

        // Normal device ID
        int DeviceID = 0;
        // Handle value to preview window
        int hHwnd = 0;
        //The devices list
        public ArrayList ListOfDevices = new ArrayList();

        //The picture to be displayed
        public PictureBox Container { get; set; }

        // Connect to the device.
        /// <summary>
        /// This function is used to load the list of the devices
        /// </summary>
        public void Load()
        {
            string Name = String.Empty.PadRight(100);
            string Version = String.Empty.PadRight(100);
            bool EndOfDeviceList = false;
            short index = 0;

            // Load name of all avialable devices into the lstDevices .
            do
            {
                // Get Driver name and version
                EndOfDeviceList = capGetDriverDescriptionA(index, ref Name, 100, ref Version, 100);
                // If there was a device add device name to the list
                if (EndOfDeviceList) ListOfDevices.Add(Name.Trim());
                index += 1;
            }
            while (!(EndOfDeviceList == false));
        }

        private USBCam()
        {

        }

        public static USBCam getInstance()
        {
            if(instance == null)
            {
                instance = new USBCam();
            }
            return instance;
        }
        /// <summary>
        /// Function used to display the output from a video capture device, you need to create
        /// a capture window.
        /// </summary>
        public void OpenConnection()
        {
            string DeviceIndex = Convert.ToString(DeviceID);
            IntPtr oHandle = Container.Handle;

            // Open Preview window in picturebox .
            // Create a child window with capCreateCaptureWindowA so you can display it in a picturebox.

            hHwnd = capCreateCaptureWindowA(ref DeviceIndex, WS_VISIBLE | WS_CHILD, 0, 0, 640, 480, oHandle.ToInt32(), 0);

            // Connect to device
            if (SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, DeviceID, 0) != 0)
            {
                // Set the preview scale
                SendMessage(hHwnd, WM_CAP_SET_SCALE, -1, 0);
                // Set the preview rate in terms of milliseconds
                SendMessage(hHwnd, WM_CAP_SET_PREVIEWRATE, 66, 0);

                // Start previewing the image from the camera
                SendMessage(hHwnd, WM_CAP_SET_PREVIEW, -1, 0);

                // Resize window to fit in picturebox
                SetWindowPos(hHwnd, HWND_BOTTOM, 0, 0, Container.Height, Container.Width, SWP_NOMOVE | SWP_NOZORDER);

                is_enabled = true;
        }
            else
            {
                // Error connecting to device close window
                DestroyWindow(hHwnd);
            }
        }

        //Close windows
        void CloseConnection()

        {
            SendMessage(hHwnd, WM_CAP_DRIVER_DISCONNECT, DeviceID, 0);
            // close window
            DestroyWindow(hHwnd);
        }

        //Save image

        public void SaveImage()
        {
            SaveFileDialog sfdImage = new SaveFileDialog();
            sfdImage.Filter = "(*.jpg)|*.jpg";

            if (sfdImage.ShowDialog() == DialogResult.OK)
            {
                SaveImageWithName(sfdImage.FileName);
            }
        }

        public bool SaveImageWithName(String name, bool test_movment = false)
        {
            bool movment = false;
            IDataObject data;
            Image oImage;
            int message = SendMessage(hHwnd, WM_CAP_EDIT_COPY, 0, 0);
            data = Clipboard.GetDataObject();
            if (data != null && data.GetDataPresent(typeof(System.Drawing.Bitmap)))
            {
                oImage = (Image)data.GetData(typeof(System.Drawing.Bitmap));
                if(oImage != null)
                {
                    Bitmap tmp = last_frame;
                    last_frame = (Bitmap)oImage;
                    if (tmp != null){
                        if (test_movment)
                        {
                            movment = check_movment(tmp, last_frame);
                        }
                        tmp.Save(name, System.Drawing.Imaging.ImageFormat.Bmp);
                        tmp.Dispose();
                    }
                }
            }
            return movment;
        }

        //https://docs.microsoft.com/pl-pl/dotnet/api/system.drawing.bitmap.unlockbits?view=dotnet-plat-ext-3.1
        //zamiana na bitmape bo to coś wolne
        private bool check_movment(Bitmap image, Bitmap image2)
        {
            int frameCount = image.Width * image.Height;
            int frameDiff = 0;
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

            BitmapData data1 = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            BitmapData data2 = image2.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            int offset = data1.Stride - image.Width;
            unsafe
            {
                byte* ptr1 = (byte*)data1.Scan0.ToPointer();
                byte* ptr2 = (byte*)data2.Scan0.ToPointer();
                for (int i = 0; i < frameCount; i++)
                {
                    if (ptr1[i] != ptr2[i])
                    {
                        ptr1[i] = 0b00001001;
                        frameDiff++;
                    }
                }
            }

            image.UnlockBits(data1);
            image2.UnlockBits(data2);

            if ((float)frameDiff/frameCount>image_diff)
            {
                return true;
            }
            return false;
        }

        public void StartRecord(string name)
        {
            SendMessage(hHwnd, WM_CAP_FILE_SET_CAPTURE_FILE, 0, name);
            SendMessage(hHwnd, WM_CAP_SEQUENCE, 0, 0);
        }

        public void StopRecord(string name)
        {
            SendMessage(hHwnd, WM_CAP_STOP, 0, 0);
            SendMessage(hHwnd, WM_CAP_FILE_SAVEAS, 0, name);
        }

        public void ChangeParameters()
        {
            SendMessage(hHwnd, WM_CAP_DLG_VIDEOSOURCE, 0, 0);
        }

        public void ChangeResolution()
        {
            SendMessage(hHwnd, WM_CAP_DLG_VIDEOFORMAT, 0, 0);
        }

        /// <summary>
        /// This function is used to dispose the connection to the device
        /// </summary>
        #region IDisposable Members

        public void Dispose()

        {
            CloseConnection();
            is_enabled = false;
        }
        #endregion
    }
}
