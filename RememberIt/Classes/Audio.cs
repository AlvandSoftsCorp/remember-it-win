using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alvas.Audio;
using System.IO;
using System.Windows.Forms;

namespace RememberIt
{
    public class Audio
    {
        WaveWriter AudioWaveWriter = null;
        RecorderEx AudioRecorder = null;
        string AudioFileName = "";

        public delegate void RecordingIsDoneDelegate(string AudioFileName);
        public event RecordingIsDoneDelegate OnRecordingIsDone = null;


        private bool is_recording = false;
        public bool IsRecording
        {
            get
            {
                return is_recording;
            }
        }

        public void StartAudioRecording(string FileName)
        {
            try
            {
                AudioFileName = FileName+".wav";
                RecorderEx rex = new RecorderEx();
                rex.Open += new EventHandler(rex_Open);
                rex.Data += new RecorderEx.DataEventHandler(rex_Data);
                rex.Close += new EventHandler(rex_Close);
                rex.Format = AudioCompressionManager.GetPcmFormat(1, 16, 44100);
                rex.StartRecord();
                this.is_recording = true;
                AudioRecorder = rex;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Unable to start recording\n" + ex.Message);
                AudioFileName = "";
                AudioRecorder = null;
                this.is_recording = false;
            }
        }

        public void StopRecording()
        {
            if (AudioRecorder == null) return;
            AudioRecorder.StopRecord();
        }

        void rex_Close(object sender, EventArgs e)
        {
            AudioWaveWriter.Close();
            AudioWaveWriter = null;

            string Mp3FileName = AudioFileName.Replace(".wav", ".ria");
            ConvertToMp3(AudioFileName, Mp3FileName);

            if (this.OnRecordingIsDone != null)
            {
                this.OnRecordingIsDone(Mp3FileName);
            }
            AudioFileName = "";
            this.is_recording = false;
        }

        private void ConvertToMp3(string AudioFileName, string Mp3FileName)
        {
            string wavFile = AudioFileName;
            string mp3File = Mp3FileName;

            using (WaveReader wr = new WaveReader(File.OpenRead(wavFile)))
            {
                IntPtr pcmFormat = wr.ReadFormat();
                byte[] pcmData = wr.ReadData();
                wr.Close();

                WaveFormat wf = AudioCompressionManager.GetWaveFormat(pcmFormat);
                if (wf.wFormatTag != AudioCompressionManager.PcmFormatTag)//Decode if not PCM data
                {
                    Decode2Pcm(ref pcmFormat, ref pcmData, ref wf);
                }
                IntPtr webFormat = AudioCompressionManager.GetCompatibleFormat(pcmFormat,
                AudioCompressionManager.MpegLayer3FormatTag);
                byte[] webData = AudioCompressionManager.Convert(pcmFormat, webFormat,
                pcmData, false);
                MemoryStream ms = new MemoryStream();
                using (WaveWriter ww = new WaveWriter(ms,
                AudioCompressionManager.FormatBytes(webFormat)))
                {
                    ww.WriteData(webData);
                    using (WaveReader wr2 = new WaveReader(ms))
                    {
                        using (FileStream fs = File.OpenWrite(mp3File))
                        {
                            wr2.MakeMP3(fs);
                        }
                    }
                }
                File.Delete(AudioFileName);
            }
        }

        void rex_Open(object sender, EventArgs e)
        {
            if (AudioWaveWriter == null)
            {
                AudioWaveWriter = new WaveWriter(
                    File.Create(AudioFileName), 
                    AudioCompressionManager.FormatBytes(((RecorderEx)sender).Format));
            }
        }

        void rex_Data(object sender, DataEventArgs e)
        {
            AudioWaveWriter.WriteData(e.Data);
        }

        private static void Decode2Pcm(ref IntPtr format, ref byte[] data, ref WaveFormat wf)
        {
            IntPtr formatPcm16Bit = IntPtr.Zero;
            byte[] dataPcm16Bit = null;
            AudioCompressionManager.ToPcm16Bit(format, data, ref formatPcm16Bit, ref dataPcm16Bit);
            wf = AudioCompressionManager.GetWaveFormat(formatPcm16Bit);
            format = formatPcm16Bit;
            data = dataPcm16Bit;
        }
    }
}
