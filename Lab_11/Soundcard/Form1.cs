using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace UPLab5
{
    public partial class Form1 : Form
    {

        Soundcard sc;
        SoundcardWF scw;
        SoundcardDX scd;
        public bool isWav;
        public bool isRecording;
        private Stopwatch recordTimer;

        public Form1()
        {
            InitializeComponent();

            sc = new Soundcard();
            scw = new SoundcardWF();
            scd = new SoundcardDX(this.Handle);
            recordTimer = new Stopwatch();
         

            // Dezaktywacja elementów przy pierwszym uruchomieniu aplikacji, aż do wyboru trybu.
            loadButton.Enabled = false;
            playButton.Enabled = false;
            stopButton.Enabled = false;
            tableLayoutPanel1.Visible = false;
            filenameLabel.Text = null;
            isRecording = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Dodanie trybów odtwarzania do comboBoxa.
            // W aplikacji korzystamy z oznaczeń:
            // comboBox.SelectedIndex == 0 // tryb activeX oraz PlaySound
            // comboBox.SelectedIndex == 1 // tryb WaveForm & Auxilary Audio
            // comboBox.SelectedIndex == 2 // tryb DirectSound
            comboBox.Items.Add("ActiveX");
            comboBox.Items.Add("Waveform & Auxillary Audio");
            comboBox.Items.Add("DirectSound");

            // Wyłącza autostart przy inicjalizacji ActiveX.
            WMPlay.settings.autoStart = false;
        }


        // Metoda ładująca plik do odtworzenia
        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd1 = new OpenFileDialog();

            ofd1.InitialDirectory = "c:\\";
            // Możliwość ładowania pliku mp3 i wav w trybie activeX oraz tylko wav w pozostałych.
            if (comboBox.SelectedIndex == 0)
            {
                ofd1.Filter = "Music files (*.wav, *.mp3)|*.wav;*.mp3";
            } else ofd1.Filter = "Music files(*.wav)| *.wav";

            ofd1.FilterIndex = 0;
            ofd1.RestoreDirectory = true;

                
            if (ofd1.ShowDialog() == DialogResult.OK)
            {
                
                // nazwa pliku w informacjach
                filenameLabel.Text = System.IO.Path.GetFileName(ofd1.FileName);
                String spath = ofd1.FileName;
                
                // określa czy pliku jest wav, czy mp3
                if (Path.GetExtension(spath) == ".wav")
                {
                    isWav = true;
                    scd.readHeader(ofd1.FileName); // laduje informacje o pliku
                    fillHeaderInfo(spath);
                } else
                {
                    isWav = false;
                    fillHeaderInfo(null);
                }

                if (comboBox.SelectedIndex == 0) WMPlay.URL = ofd1.FileName; // ładuje plik w ActiveX
                sc.loadMusic(spath); // ładuje plik dla PlaySound
                scw.loadMusic(spath); // ładuje plik dla Waveform & Auxillary audio
                scd.loadMusic(spath); // ładuje plik w DirectSound

            }
            else Console.WriteLine("Blad zaladowania pliku");
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            if (isWav)
            {
                if (comboBox.SelectedIndex == 0)
                {
                    sc.playMusic();
                }
                else if (comboBox.SelectedIndex == 1)
                {
                    scw.playMusic();
                }
                else if (comboBox.SelectedIndex == 2)
                {
                    scd.playMusic();

                }
            } else
            {
                MessageBox.Show("Pliki mp3 można odtwarzać tylko za pomocą ActiveX. Użyj odtwarzacza po lewej.", "Brak pliku wav", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            if (comboBox.SelectedIndex == 0)
            {
                sc.stopMusic();
            }
            else if (comboBox.SelectedIndex == 1)
            {
                scw.stopMusic();
            }
            else if (comboBox.SelectedIndex == 2)
            {
                scd.stopMusic(); 
            }
        }

        // aktywacja i dezaktywacja elementów formularza w zależności od wybranego trybu
        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadButton.Enabled = true;
            playButton.Enabled = true;
            stopButton.Enabled = true;
        }

        // metoda do wypełniania tabeli danymi z headera wav
        private void fillHeaderInfo(String spath)
        {

            if (isWav)
            {
                scd.readHeader(spath);

                label4.Text = scd.getChunkId();
                label6.Text = scd.getChunkSize().ToString();
                label8.Text = scd.getFormat();
                label10.Text = scd.getSubChunkId();
                label12.Text = scd.getSubChunkSize().ToString();
                label14.Text = scd.getAudioFormat();
                label16.Text = scd.getNumChannels().ToString();
                label18.Text = scd.getSampleRate().ToString();
                label20.Text = scd.getBytesPerSecond().ToString();
                label22.Text = scd.getBlockAlign().ToString();
                label24.Text = scd.getBitsPerSample().ToString();
                label26.Text = scd.getDataChunkId();
                label28.Text = scd.getDataSize().ToString();

                tableLayoutPanel1.Visible = true;
            } else
            {
                tableLayoutPanel1.Visible = false;
            }

        }

        private void recordButton_Click(object sender, EventArgs e)
        { 
            if (isRecording)
            {
                recordButton.Text = "Nagraj";
                scw.record(isRecording);
                recordTimer.Reset();
                isRecording = false;
            } else
            {
                recordButton.Text = "Zatrzymaj nagrywanie i zapisz";
                scw.record(isRecording);
                recordTimer.Start();
                isRecording = true;

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timerLabel.Text = string.Format("{0:mm\\:ss\\:fff}", recordTimer.Elapsed);

        }
    }
}
