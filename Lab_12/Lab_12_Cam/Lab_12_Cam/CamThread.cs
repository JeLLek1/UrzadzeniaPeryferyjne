using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Lab_12_Cam
{
    class CamThread
    {
        public Thread thr;
        public CamThread()
        {
            thr = new Thread(this.RunThread);
            thr.Start();
        }
        void RunThread()
        {

            IDataObject test = USBCam.getInstance().test();
            int i = 10;
        }
    }
}
