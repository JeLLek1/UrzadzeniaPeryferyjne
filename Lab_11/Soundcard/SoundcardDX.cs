using System;
using System.IO;
using SharpDX;
using SharpDX.DirectSound;
using SharpDX.Multimedia;

namespace UPLab5
{
    class SoundcardDX
    {
     
        //zmienne directsound
        private DirectSound dSound;
        private SoundBufferDescription primaryBufferDesc;
        private PrimarySoundBuffer primarySoundBuffer;
        private SecondarySoundBuffer secondarySoundBuffer;
        private byte[] waveData;

        //zmienne pliku wav
        private WaveFormatEncoding audioFormat;
        private short bitsPerSample;
        private short blockAlign;
        private int bytesPerSecond;
        private string chunkId;
        private int chunkSize;
        private string dataChunkId;
        private int dataSize;
        private string format;
        private short numChannels;
        private int sampleRate;
        private string subChunkId;
        private int subChunkSize;



        public SoundcardDX(IntPtr handle)
        {
            // tworzy nowy obiekt directsound
            dSound = new DirectSound();
            dSound.SetCooperativeLevel(handle, CooperativeLevel.Priority);

            // bufor pierwszego rzędu
            primaryBufferDesc = new SoundBufferDescription();
            primaryBufferDesc.Flags = BufferFlags.PrimaryBuffer;
            primaryBufferDesc.AlgorithmFor3D = Guid.Empty;

            primarySoundBuffer = new PrimarySoundBuffer(dSound, primaryBufferDesc);

          
            primarySoundBuffer.Play(0, PlayFlags.Looping);

         
        }


        public void playMusic()
        {
           if (secondarySoundBuffer != null)
            {
                secondarySoundBuffer.Play(0, PlayFlags.None);
            }
            

        }
        public void stopMusic()
        {
            if (secondarySoundBuffer != null)
            {
                secondarySoundBuffer.Stop();
            }
        }

        public void readHeader(string spath)
        {
            // otworzenie pliku wav
            var reader = new BinaryReader(File.OpenRead(spath));

            // czytanie poszczególnych wartości nagłówka wav
            chunkId = new string(reader.ReadChars(4));
            chunkSize = reader.ReadInt32();
            format = new string(reader.ReadChars(4));
            subChunkId = new string(reader.ReadChars(4));
            subChunkSize = reader.ReadInt32();
            audioFormat = (WaveFormatEncoding)reader.ReadInt16();
            numChannels = reader.ReadInt16();
            sampleRate = reader.ReadInt32();
            bytesPerSecond = reader.ReadInt32();
            blockAlign = reader.ReadInt16();
            bitsPerSample = reader.ReadInt16();
            dataChunkId = new string(reader.ReadChars(4));
            dataSize = reader.ReadInt32();

            // zamknięcie pliku
            reader.Close();
        }

        public bool loadMusic(string spath)
        {
            try
            {

                // otworzenie pliku wav
                var reader = new BinaryReader(File.OpenRead(spath));

                // przejście bezpośredniu do fragmentu pliku, w którym znajdują się dane
                reader.BaseStream.Seek(44, SeekOrigin.Begin);

                // ustawienie SoundBufferDescription dla SecondaryBuffer, który włącza dźwięk
                var buffer = new SoundBufferDescription();
                buffer.Flags = BufferFlags.ControlVolume;
                buffer.BufferBytes = dataSize;
                buffer.Format = new WaveFormat(sampleRate, bitsPerSample, numChannels);
                buffer.AlgorithmFor3D = Guid.Empty;

                // Utworzenie Secondary buffer z powyższymi ustawieniami
                secondarySoundBuffer = new SecondarySoundBuffer(dSound, buffer);

                // załadowanie do pamięci danych z pliku wav
                waveData = reader.ReadBytes(dataSize);

                // zamknięcie pliku wav
                reader.Close();

                // zablokowanie secondary buffera, żeby zapisać na nim dane
                DataStream waveBufferData2;
                var waveBufferData1 = secondarySoundBuffer.Lock(0, dataSize, LockFlags.None, out waveBufferData2);

                // skopiowanie danych z pamięci do secondary buffera
                waveBufferData1.Write(waveData, 0, dataSize);

                // odblokowanie secondary buffera, aby był gotowy do odtwarzania
                secondarySoundBuffer.Unlock(waveBufferData1, waveBufferData2);

            }
            catch
            {
                return false;
            }
            
            return true;
            
        }

        // gettery nagłówka wav
        public string getAudioFormat()
        {
            return audioFormat.ToString();
        }
        public short getBitsPerSample()
        {
            return bitsPerSample;
        }
        public short getBlockAlign()
        {
            return blockAlign;
        }
        public int getBytesPerSecond()
        {
            return bytesPerSecond;
        }
        public string getChunkId()
        {
            return chunkId;
        }
        public int getChunkSize()
        {
            return chunkSize;
        }
        public string getDataChunkId()
        {
            return dataChunkId;
        }
        public int getDataSize()
        {
            return dataSize;
        }
        public string getFormat()
        {
            return format;
        }
        public short getNumChannels()
        {
            return numChannels;
        }
        public int getSampleRate()
        {
            return sampleRate;
        }
        public string getSubChunkId()
        {
            return subChunkId;
        }
        public int getSubChunkSize()
        {
            return subChunkSize;
        }

    }

       
}