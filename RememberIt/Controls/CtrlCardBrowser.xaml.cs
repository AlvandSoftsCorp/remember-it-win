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
    /// Interaction logic for CtrlCardBrowser.xaml
    /// </summary>
    public partial class CtrlCardBrowser : UserControl //, IActivity
    {
        string SingleSpecifiedCardFolder = "";
        List<Card> Cards = null;
        int CurrentCardIndex = -1;

        private ContextMenu mnuMenu = new ContextMenu();

        public void DisableOperations()
        {
            btnCorrectAnswer.IsEnabled = false;
            btnWrongAnswer.IsEnabled = false;
            ctrlContent.DisableOperations();
        }

        public CtrlCardBrowser()
        {
            InitializeComponent();
            InitMenu();
        }

        private void InitMenu()
        {
            MenuItem mnuRemoveCard = new MenuItem();
            mnuRemoveCard.Header = "Remove Card";
            mnuRemoveCard.Click += new RoutedEventHandler(mnuRemoveCard_Click);
            mnuMenu.Items.Add(mnuRemoveCard);
            this.ContextMenu = mnuMenu;
        }

        void mnuRemoveCard_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentCardIndex == -1) return;
            if (Cards == null) return;
            if (Cards.Count == 0) return;
            Card c = Cards[CurrentCardIndex];
            if (c == null) return;
            string path = c.CardAbsPath;
            if (Directory.Exists(path) == false) return;

            MessageBoxResult mr = MessageBox.Show("Remove Card?", "Warning", MessageBoxButton.YesNoCancel);
            if (mr != MessageBoxResult.Yes) return;

            bool done = false;
            try
            {
                Directory.Delete(path, true);
                done = (Directory.Exists(path) == false);
            }
            catch
            {
                done = false;
            }
            if (done)
            {
                MessageBox.Show("Card removed!");
                Cards.RemoveAt(CurrentCardIndex);
                if (CurrentCardIndex >= Cards.Count) CurrentCardIndex--;
                this.Present();
            }
            else
            {
                MessageBox.Show("Unable to remove card.");
            }
        }


        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            ctrlContent.Init();
        }

        private void Present()
        {
            if (Cards.Count == 0)
            {
                lblCardNumber.Content = "0 of 0";
                ShowNothing();
            }
            else lblCardNumber.Content = string.Format("{0} of {1}", CurrentCardIndex + 1, Cards.Count);
            
            switch (this.CurrentFace)
            {
                case Faces.QuestionFace:
                    ShowQuestion();
                    break;
                case Faces.AnswerFace:
                    ShowAnswer();
                    break;
                case Faces.ReminderFace:
                    ShowReminder();
                    break;
            }
        }

        private void ShowNothing()
        {
            ctrlContent.Unload();
        }

        public enum Faces
        {
            QuestionFace,
            AnswerFace,
            ReminderFace
        }

        public Faces CurrentFace = Faces.QuestionFace;



        private void PrevCard()
        {
            if (Cards.Count == 0) return;

            this.CurrentCardIndex--;
            if (this.CurrentCardIndex == -1) this.CurrentCardIndex = Cards.Count - 1;
            CurrentFace = Faces.QuestionFace;
            Present();
        }

        private void NextCard()
        {
            if (Cards.Count == 0) return;


            this.CurrentCardIndex++;
            if (this.CurrentCardIndex == Cards.Count) this.CurrentCardIndex = 0;
            CurrentFace = Faces.QuestionFace;
            Present();
        }

        private void ShowReminder()
        {
            if (CurrentCardIndex < 0) return;
            if (Cards.Count <= 0) return;

            Card card = Cards[CurrentCardIndex];
            lblTitle.Content = "[Reminder]";
            grdButtons.Visibility = System.Windows.Visibility.Hidden;

            if (ctrlContent.Visibility == System.Windows.Visibility.Visible)
            {
                ctrlContent.Unload();
                ctrlContent.WorkingFolder = card.ReminderAbsPath;
                ctrlContent.Load();
            }
        }

        private void ShowQuestion()
        {
            if (CurrentCardIndex < 0) return;
            if (Cards.Count <= 0) return;

            Card card = Cards[CurrentCardIndex];
            lblTitle.Content = "[Question]";
            grdButtons.Visibility = System.Windows.Visibility.Hidden;

            if (ctrlContent.Visibility == System.Windows.Visibility.Visible)
            {
                ctrlContent.Unload();
                ctrlContent.WorkingFolder = card.QuestionAbsPath;
                ctrlContent.Load();
            }
        }

        private void ShowAnswer()
        {
            if (CurrentCardIndex < 0) return;
            if (Cards.Count <= 0) return;

            Card card = Cards[CurrentCardIndex];
            lblTitle.Content = "[Answer]";
            grdButtons.Visibility = System.Windows.Visibility.Visible;

            if (ctrlContent.Visibility == System.Windows.Visibility.Visible)
            {
                ctrlContent.Unload();
                ctrlContent.WorkingFolder = card.AnswerAbsPath;
                ctrlContent.Load();
            }
        }

        public enum InitCondition
        { 
            AllCards,
            PendingCards,
            MissedCards,
            SingleSpecifiedCard
        }

        public bool Init(InitCondition Condition)
        {
            CurrentFace = Faces.QuestionFace;
            bool is_ok = CheckCardsFolder();
            if (is_ok == false)
            {
                MessageBox.Show("Initialization failed");
            }

            switch (Condition)
            {
                case InitCondition.AllCards:
                    this.Cards = Globals.GetListOfCards(Globals.GetPathOfEachCard());
                    break;
                case InitCondition.PendingCards:
                    this.Cards = Globals.GetListOfCards(Globals.GetPathOfEachPendingCard());
                    break;
                case InitCondition.MissedCards:
                    this.Cards = Globals.GetListOfCards(Globals.GetPathOfEachMissedCard());
                    break;
                case InitCondition.SingleSpecifiedCard:
                    this.Cards = Globals.GetListOfCards(new string[] { this.SingleSpecifiedCardFolder });
                    break;
            }
            this.CurrentCardIndex = 0;
            Present();
            return is_ok;
        }
        
        
        public bool Init(List<Card> GivenCards)
        {
            CurrentFace = Faces.QuestionFace;
            bool is_ok = CheckCardsFolder();
            if (is_ok == false)
            {
                MessageBox.Show("Initialization failed");
            }
            this.Cards = GivenCards;
            this.CurrentCardIndex = 0;
            Present();
            return is_ok;
        }
        
        


        private bool CheckCardsFolder()
        {

            string cards_path = Globals.GetCardsPath();
            if (Directory.Exists(cards_path) == true) return true;
            else
            {
                try
                {
                    MessageBox.Show("Unable to locate cards folder. Trying to create it.");
                    DirectoryInfo inf = Directory.CreateDirectory(cards_path);
                    if (inf.Exists) return true;
                    else return false;
                }
                catch
                {
                    MessageBox.Show("Unable to create 'cards' folder!");
                    return false;
                }
            }
        }

        private void btnCorrectAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentCardIndex == -1) return;
            Card c = Cards[CurrentCardIndex];
            try
            {
                KeyValPair kv = new KeyValPair(';', ':');
                kv.Load(c.ManifestAbsFileName);
                int stage = Convert.ToInt32(kv.GetVal("stage"));
                stage++;
                kv.SetVal("stage", stage.ToString());
                MultiCalendar mc = MultiCalendar.FromDateTime(DateTime.Now);
                double gdp = mc.GetGdp();
                gdp += Math.Pow(2, stage);
                mc.SetGdp(gdp);
                kv.SetVal("next_visit", gdp.ToString());
                kv.Save(c.ManifestAbsFileName);
                string msg = string.Format("Stage -> {0}\r\nNext -> {1}", stage, mc.GetJalDate());

                string history = string.Format("\r\n[{0}] Correct Answer. Stage: {1}", Globals.GetDateTimeStampSec(), stage); 
                File.AppendAllText(c.HistoryAbsFileName, history);

                MessageBox.Show(msg);

                this.Cards.RemoveAt(CurrentCardIndex);
                Present();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to access manifest file\r\n" + ex.Message);
                System.Diagnostics.Process.Start(c.CardAbsPath);
            }
        }

        
        private void btnWrongAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentCardIndex == -1) return;
            Card c = Cards[CurrentCardIndex];
            try
            {
                KeyValPair kv = new KeyValPair(';', ':');
                kv.Load(c.ManifestAbsFileName);
                int stage = Convert.ToInt32(kv.GetVal("stage"));
                stage = 0;
                kv.SetVal("stage", stage.ToString());
                MultiCalendar mc = MultiCalendar.FromDateTime(DateTime.Now);
                double gdp = mc.GetGdp();
                gdp += Math.Pow(2, stage);
                mc.SetGdp(gdp);
                kv.SetVal("next_visit", gdp.ToString());
                kv.Save(c.ManifestAbsFileName);
                string msg = string.Format("Stage -> {0}\r\nNext -> {1}", stage, mc.GetJalDate());

                string history = string.Format("\r\n[{0}] Wrong Answer. Stage: {1}", Globals.GetDateTimeStampSec(), stage);
                File.AppendAllText(c.HistoryAbsFileName, history);
                
                MessageBox.Show(msg);
                
                this.Cards.RemoveAt(CurrentCardIndex);
                Present();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to access manifest file\r\n" + ex.Message);
                System.Diagnostics.Process.Start(c.CardAbsPath);
            }

        }

        private void OnKeyboardCmd(object sender, ExecutedRoutedEventArgs e)
        {
            string str = e.Parameter as string;
            switch (str)
            {
                case "NextCard":
                    NextCard();
                    break;
                case "PrevCard":
                    PrevCard();
                    break;

                    if (this.CurrentFace == Faces.QuestionFace)
                    {
                        this.CurrentFace = Faces.AnswerFace;
                        Present();
                    }
                    else if (this.CurrentFace == Faces.ReminderFace)
                    {
                        this.CurrentFace = Faces.QuestionFace;
                        Present();
                    }
                    break;

                case "Down":
                    if (this.CurrentFace == Faces.QuestionFace)
                    {
                        this.CurrentFace = Faces.AnswerFace;
                        Present();
                    }
                    else if (this.CurrentFace == Faces.ReminderFace)
                    {
                        this.CurrentFace = Faces.QuestionFace;
                        Present();
                    }
                    break;

                case "Up":
                    if (this.CurrentFace == Faces.QuestionFace)
                    {
                        this.CurrentFace = Faces.ReminderFace;
                        Present();
                    }
                    else if (this.CurrentFace == Faces.AnswerFace)
                    {
                        this.CurrentFace = Faces.QuestionFace;
                        Present();
                    }
                    break;
            }
            e.Handled = true;
        }

        internal void SetCardRootFolder(string CardRootFolder)
        {
            this.ctrlContent.CardRootFolder = CardRootFolder;
            SingleSpecifiedCardFolder = CardRootFolder;

        }
    }
}
