using System;
using System.Runtime.InteropServices;
namespace UPLab5
{
    class SoundcardWF
    {

        String spath;

        [DllImport("winmm.DLL")]
        private static extern bool PlaySound(string szSound, System.IntPtr hMod, PlaySoundFlags flags);
        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);


        [System.Flags]
        public enum PlaySoundFlags : int
        {
            SND_SYNC = 0x0000,
            SND_ASYNC = 0x0001,
            SND_NODEFAULT = 0x0002,
            SND_LOOP = 0x0008,
            SND_NOSTOP = 0x0010,
            SND_NOWAIT = 0x00002000,
            SND_FILENAME = 0x00020000,
            SND_RESOURCE = 0x00040004
        }

        public void playMusic()
        {
            if (spath != null)
            {
                PlaySound(spath, new System.IntPtr(), PlaySoundFlags.SND_ASYNC);
            }
            else Console.WriteLine("spath is null");
        }
        public void stopMusic()
        {
            if (spath != null)
            {
                PlaySound(null, new System.IntPtr(), PlaySoundFlags.SND_ASYNC);
            }
            else Console.WriteLine("spath is null");
        }

        public void loadMusic(String spath)
        {
            this.spath = spath;
        }

        public void record(bool isRecording)
        {
            if (!isRecording)
            {
                mciSendString("open new Type waveaudio Alias recsound", "", 0, 0);
                mciSendString("record recsound", "", 0, 0);
            } else
            {
                mciSendString("save recsound recorded_audio.wav", "", 0, 0);
                mciSendString("close recsound ", "", 0, 0);
            }
        }


    }
}
