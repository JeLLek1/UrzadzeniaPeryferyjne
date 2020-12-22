using System.Media;

namespace UPLab5
{
    class Soundcard
    {
        private SoundPlayer player;
        private string spath;

        public Soundcard()
        {
            player = new SoundPlayer();
        }
        
        public void loadMusic(string spath)
        {
            this.spath = spath;
            player.SoundLocation = spath;
        }

        public void playMusic()
        {
            if (spath!=null)
            player.Play();
        }
        public void stopMusic()
        {
            if(spath!=null)
            player.Stop();
        }

    }
}
