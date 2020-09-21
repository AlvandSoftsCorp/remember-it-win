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
using System.Diagnostics;

namespace RememberIt
{
    /// <summary>
    /// Interaction logic for CtrlCard.xaml
    /// </summary>
    public partial class CtrlCard : UserControl
    {

        string CardFolder = "";
        public CtrlCard()
        {
            InitializeComponent();
        }

        public event EventHandler OnClose = null;

        public void Init()
        {
            ctrlQuestion.BgColor = new SolidColorBrush(Color.FromRgb(195, 210, 230));
            ctrlAnswer.BgColor = new SolidColorBrush(Color.FromRgb(170, 250, 175));
            ctrlReminder.BgColor = new SolidColorBrush(Color.FromRgb(225, 195, 220));

            string GroupName = Globals.GetDefaultGroupName();
            lblDefaultCardGroupName.Content = string.Format("Group: {0}", GroupName);

            
            // Creates a temporary folder and its necessary files and folders for a card.
            // If user decides to save the card, the folder will be persistent. Else the themporary
            // folder will be removed.
            // Note: manifest file is created here.
            CardFolder = Globals.CreateNewCard(GroupName);

            ctrlQuestion.CardRootFolder = CardFolder;
            ctrlQuestion.WorkingFolder = Globals.CreateFolder(CardFolder + "Q\\");
            ctrlAnswer.WorkingFolder = Globals.CreateFolder(CardFolder + "A\\");
            ctrlAnswer.CardRootFolder = CardFolder;
            ctrlReminder.WorkingFolder = Globals.CreateFolder(CardFolder + "R\\");
            ctrlReminder.CardRootFolder = CardFolder;
        }



        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ctrlQuestion.Init();
            ctrlAnswer.Init();
            ctrlReminder.Init();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            FrmBuildHtmlDocument.ProcessInfo ProcessInfoHandler = null;
            Globals.BuildHtmlDocs(this.CardFolder, ProcessInfoHandler);
            CreateSnapshot();

            string q_dir = ctrlQuestion.WorkingFolder;
            if (Directory.Exists(q_dir) == false)
            {
                MessageBox.Show(string.Format("Unable to save content because directory '{0}' is missing.", q_dir));
                return;
            }
            if (Directory.GetFiles(q_dir).Length == 0)
            {
                MessageBox.Show(string.Format("Unable to save content because 'Question' content is empty."));
                tcCard.SelectedIndex = 0;
                return;
            }
            
            
            string a_dir = ctrlAnswer.WorkingFolder;
            if (Directory.Exists(a_dir) == false)
            {
                MessageBox.Show(string.Format("Unable to save content because directory '{0}' is missing.", a_dir));
                return;
            }
            if (Directory.GetFiles(a_dir).Length == 0)
            {
                MessageBox.Show(string.Format("Unable to save content because 'Answer' content is empty."));
                tcCard.SelectedIndex = 1;
                return;
            }


            this.save_is_pressed = true;
            this.external_ask_to_discard_is_in_process = false;
            if (this.OnClose != null) this.OnClose(null, null);
        }


        private void CreateSnapshot()
        {
            return;
            //if (ctrlQuestion.ListOfContentControls.Count == 0) return;
            //Control ctrl = ctrlQuestion.ListOfContentControls[0];
            //string snapshot_file_name = ctrlQuestion.CardRootFolder + "Snapshot.png";
            //FileStream stream = new FileStream(snapshot_file_name, FileMode.Create);
            //RenderTargetBitmap rtb = new RenderTargetBitmap((int)ctrl.ActualWidth, (int)ctrl.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            //rtb.Render(ctrl);
            //PngBitmapEncoder png = new PngBitmapEncoder();
            //png.Frames.Add(BitmapFrame.Create(rtb));
            //png.Save(stream);
            //stream.Close();
        }

        private void btnDiscard_Click(object sender, RoutedEventArgs e)
        {
            if (ShouldDeleteCurrenCard())
            {
                DeleteCard();
                save_is_pressed = false;
                this.external_ask_to_discard_is_in_process = false;
                if (this.OnClose != null) this.OnClose(null, null);
            }
            else
            { 
                // Just do nothing
            }
        }

        public bool DeleteCard()
        {
            try
            {
                Directory.Delete(this.CardFolder, true);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to remove card.\n" + ex.Message);
                return false;
            }
        }



        bool external_ask_to_discard_is_in_process = true;
        public bool ExternalAskToDiscardIsInProcess
        {
            get
            {
                return external_ask_to_discard_is_in_process;
            }
        }


        public void ExternalAskToDiscard()
        {
            if (ShouldDeleteCurrenCard())
            {
                DeleteCard();
                save_is_pressed = false;
                allow_close = true;
                external_ask_to_discard_is_in_process = false;
                if (this.OnClose != null) this.OnClose(null, null);
            }
            else
            {
                allow_close = false;
            }
        }

        internal bool ShouldDeleteCurrenCard()
        {
            int n = Globals.GetNumberOfFiles(ctrlQuestion.WorkingFolder);
            int m = Globals.GetNumberOfFiles(ctrlAnswer.WorkingFolder);
            if (n == 0 && m == 0) return true;

            
            MessageBoxResult dr = MessageBox.Show("Delete current card?", "Warning", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
            if (dr != MessageBoxResult.Yes)
            {
                return false;
            }
            else // Delete card:
            {
                return true;
            }
        }


        private bool allow_close = false;
        public bool AllowClose
        {
            get
            {
                return allow_close;
            }
        }

        private bool save_is_pressed = true;
        public bool SaveIsPressed
        {
            get
            {
                return save_is_pressed;
            }
        }
        
    }
}
