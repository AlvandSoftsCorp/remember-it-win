using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace RememberIt
{
    /// <summary>
    /// Interaction logic for CtrlAudioPlayer.xaml
    /// </summary>
    public partial class CtrlAudioPlayer : UserControl
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = null;
        
        public CtrlAudioPlayer()
        {
            InitializeComponent();

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0,100);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                sldSlider.Value = Player.Position.Ticks;
                sldSlider.Minimum = 0;
                sldSlider.Maximum = Player.NaturalDuration.TimeSpan.Ticks;

            }
            catch { }
        }

        public string MediaFileName = "";
        private MediaPlayer Player = new MediaPlayer();

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(MediaFileName))
            {
                try
                {
                    //--------
                    //MemoryStream packStream = new MemoryStream();
                    //Uri packUri = new Uri("bla:");
                    //Uri packPartUri = new Uri("/MemoryResource", UriKind.Relative);
                    //PackagePart packPart = pack.CreatePart(packPartUri, "Media/MemoryResource");
                    //packPart.GetStream().Write(bytes, 0, bytes.Length);
                    //var inMemoryUri = PackUriHelper.Create(packUri, packPart.Uri);


                    //  mediaElement1.LoadedBehavior = MediaState.Manual;
                    //  mediaElement1.Source = inMemoryUri;
                    //  mediaElement1.Play();                    
                    
                    //--------                    
                    Player.Open(new Uri("file:///" + MediaFileName));
                    Player.Play();

                    dispatcherTimer.Start();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void sldSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                Player.Position = new TimeSpan((long)sldSlider.Value);
            }
            catch
            { 
            
            }
        }

        private void btnPlay_Checked(object sender, RoutedEventArgs e)
        {

        }

        internal void CloseStram()
        {
            if (Player!=null) Player.Close();
        }
    }
}
