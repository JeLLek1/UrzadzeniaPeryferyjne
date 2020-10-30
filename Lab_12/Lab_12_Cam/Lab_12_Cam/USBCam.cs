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
using System.Security.Cryptography;

namespace Lab_12_Cam
{
    public class USBCam : IDisposable
    {
        //stałe przechowujące kody komend

        private static USBCam instance= null;
        public bool is_enabled = false;
        Bitmap last_frame = null;
        public float image_diff = 0.4f;

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
        private const short SWP_NOZORDER = 0x4;
        private const short HWND_BOTTOM = 1;

        //pobranie urządzenia przechwytywania o podanym id
        [DllImport("avicap32.dll")]
        protected static extern bool capGetDriverDescriptionA(short wDriverIndex,
            [MarshalAs(UnmanagedType.VBByRefStr)]ref String lpszName,
           int cbName, [MarshalAs(UnmanagedType.VBByRefStr)] ref String lpszVer, int cbVer);

        //tworzenie okna (dziecka) aby przechytywać obraz i wyświetlać go później w PictureBox
        [DllImport("avicap32.dll")]
        protected static extern int capCreateCaptureWindowA([MarshalAs(UnmanagedType.VBByRefStr)] ref string
    lpszWindowName,
            int dwStyle, int x, int y, int nWidth, int nHeight, int hWndParent, int nID);

        //zmiana ustawień pozycji okna strumienia
        [DllImport("user32")]
        protected static extern int SetWindowPos(int hwnd, int hWndInsertAfter, int x, int y, int cx, int cy, int wFlags);

        //Wysyłanie wiadomości do okna
        [DllImport("user32", EntryPoint = "SendMessageA")]
        protected static extern int SendMessage(int hwnd, int wMsg, int wParam, [MarshalAs(UnmanagedType.AsAny)] object
    lParam);

        //pozwala na usuwanie okna podlgądu
        [DllImport("user32")]
        protected static extern bool DestroyWindow(int hwnd);

        // Normal device ID
        public int DeviceID = 0;
        //zmienna przechowująca wartość okna podglądu aby módz zarządzać komunikacją
        int hHwnd = 0;
        //Lista urządzeń
        public ArrayList ListOfDevices = new ArrayList();

        //kontener PictureBox do przechwytywania podglądu
        public PictureBox Container { get; set; }

        //Wczytanie dostępnych urządzeń
        public void Load()
        {
            //zmienne przechowujące tymczasowo informacje o urządzeniu
            string Name = String.Empty.PadRight(100);
            string Version = String.Empty.PadRight(100);
            bool EndOfDeviceList = false;
            short index = 0;

            //dopuki istnieje lista urządzeń
            do
            {
                //Pobranie wersji i nazwy urządzenia
                EndOfDeviceList = capGetDriverDescriptionA(index, ref Name, 100, ref Version, 100);
                //Jeżeli nie jest to ostatnie urzadzenie to dodanie go do listy
                if (EndOfDeviceList) ListOfDevices.Add(Name.Trim());
                index += 1;
            }
            while (!(EndOfDeviceList == false));
        }
        //konstruktor
        private USBCam()
        {
            //wczytanie dostępnych urządzeń
            Load();
        }

        //singleton (pobranie instancji)
        public static USBCam getInstance()
        {
            if(instance == null)
            {
                instance = new USBCam();
            }
            return instance;
        }
        
        //otwarcie połączenia z kamerą
        public void OpenConnection()
        {
            //MCI Device (przechytywania streemowania)
            string DeviceIndex = Convert.ToString(DeviceID);
            //element przechwytujący stream
            IntPtr oHandle = Container.Handle;

            //otwarcie okna przechwytu obrazu po podaniu MCI, stałych ustawień i wskaźnika na element przechwytujący
            hHwnd = capCreateCaptureWindowA(ref DeviceIndex, WS_VISIBLE | WS_CHILD, 0, 0, 640, 480, oHandle.ToInt32(), 0);

            //połączenie z kamerą
            if (SendMessage(hHwnd, WM_CAP_DRIVER_CONNECT, DeviceID, 0) != 0)
            {
                //skala przechytywaneko podglądu
                SendMessage(hHwnd, WM_CAP_SET_SCALE, -1, 0);
                //prędkość podglądu w milisekundach
                SendMessage(hHwnd, WM_CAP_SET_PREVIEWRATE, 66, 0);

                //rozpoczęcie podglądu
                SendMessage(hHwnd, WM_CAP_SET_PREVIEW, -1, 0);

                //rozszczerzenie ekranu aby pasował do PistureBox
                SetWindowPos(hHwnd, HWND_BOTTOM, 0, 0, Container.Height, Container.Width, SWP_NOMOVE | SWP_NOZORDER);

                is_enabled = true;
        }
            else
            {
                //Błąd połączenia, niszczenie podglądu
                DestroyWindow(hHwnd);

                is_enabled = false;
                throw new Exception("Błąd połączenia");
            }
        }

        //zamknięcie okna przechwytywania obrazu oraz połączenia z kamerą
        void CloseConnection()

        {
            SendMessage(hHwnd, WM_CAP_DRIVER_DISCONNECT, DeviceID, 0);
            // close window
            DestroyWindow(hHwnd);
        }

        //zapis zdjęcia z podaną nazwą + dodatkowo jeżeli wymagane użycie metody sprawdzającej różnice z ostatnią klatką
        public bool SaveImageWithName(String name, bool test_movment = false)
        {
            //czy wykryto ruch
            bool movment = false;
            IDataObject data;
            Image oImage;
            //pobranie aktualnej klatki kamery
            int message = SendMessage(hHwnd, WM_CAP_EDIT_COPY, 0, 0);
            data = Clipboard.GetDataObject();
            //jeżeli udało się pobrać klatkę 
            if (data != null && data.GetDataPresent(typeof(System.Drawing.Bitmap)))
            {
                //wczytanie klatki jako obrazka
                oImage = (Image)data.GetData(typeof(System.Drawing.Bitmap));
                if(oImage != null)
                {
                    //bitmapy klatek ostatniej i aktualnej
                    Bitmap tmp = last_frame;
                    last_frame = (Bitmap)oImage;
                    //jeżeli wcześniejsza klatka jest już ustawiona
                    if (tmp != null){
                        //wykrywanie ruchu jeżeli jest potrzeba
                        if (test_movment)
                        {
                            movment = check_movment(tmp, last_frame);
                        }
                        //zapis obrazka do podanego pliku
                        tmp.Save(name, System.Drawing.Imaging.ImageFormat.Bmp);
                        //zwolnienie zasobu obrazka
                        tmp.Dispose();
                    }
                }
            }
            return movment;
        }
        //wykrywanie róznicy w pikselach i zamiana róznych pikseli na czerwone
        private bool check_movment(Bitmap image, Bitmap image2)
        {
            //ilość pikseli
            int frameCount = image.Width * image.Height;
            //ilość różnych pikseli
            int frameDiff = 0;
            //rozmiar bitmapy
            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            //wczytanie bitmapy do "tablicy" aby mieć swobodny dostęp do danych pikseli i zamiana na formatowanie 8bitowe dla skrócenia operacji
            BitmapData data1 = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
            BitmapData data2 = image2.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

            //operacje na wskaźnikach
            unsafe
            {
                //zamiana bitmapy na tablice bajtów
                byte* ptr1 = (byte*)data1.Scan0.ToPointer();
                byte* ptr2 = (byte*)data2.Scan0.ToPointer();
                //dla wszystkich pikseli
                for (int i = 0; i < frameCount; i++)
                {
                    //jeżeli są różne zamiana na czerwone i zliczanie
                    if (ptr1[i] != ptr2[i])
                    {
                        ptr1[i] = 0b00001001;
                        frameDiff++;
                    }
                }
            }
            //odblokowanie bitów
            image.UnlockBits(data1);
            image2.UnlockBits(data2);

            //obliczanie stopnia różnicy i jeżeli stopień różnicy większy od ustawionego to zwraca true
            if ((float)frameDiff/frameCount>image_diff)
            {
                return true;
            }
            return false;
        }
        //włączanie nagrywania i zapisu do danego pliku
        public void StartRecord(string name)
        {
            SendMessage(hHwnd, WM_CAP_FILE_SET_CAPTURE_FILE, 0, name);
            SendMessage(hHwnd, WM_CAP_SEQUENCE, 0, 0);
        }
        //zatrzymanie zapisywania nagrywania w podanym pliku
        public void StopRecord(string name)
        {
            SendMessage(hHwnd, WM_CAP_STOP, 0, 0);
            SendMessage(hHwnd, WM_CAP_FILE_SAVEAS, 0, name);
        }
        //wywołanie okna zmiany właściwości kamery
        public void ChangeParameters()
        {
            SendMessage(hHwnd, WM_CAP_DLG_VIDEOSOURCE, 0, 0);
        }
        //wysołanie okna zmiany rozdzielczości/formatu
        public void ChangeResolution()
        {
            SendMessage(hHwnd, WM_CAP_DLG_VIDEOFORMAT, 0, 0);
        }

        //zwolnienie połączenia z kamerą
        #region IDisposable Members

        public void Dispose()

        {
            CloseConnection();
            is_enabled = false;
        }
        #endregion
    }
}