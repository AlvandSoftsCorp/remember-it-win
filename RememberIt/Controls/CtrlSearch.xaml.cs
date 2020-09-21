using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace RememberIt
{
    /// <summary>
    /// Interaction logic for CtrlSearch.xaml
    /// </summary>
    public partial class CtrlSearch : UserControl
    {
        public MainWindow wndMainWindow = null;
        private List<Card> SearchResults = new List<Card>();
        private string[] CardGroupPaths = null;
        

        public CtrlSearch()
        {
            InitializeComponent();
        }


        private List<Card> FindMatch(string TextToSearch)
        {
            List<Card> matched_cards = new List<Card>();
            string[] card_paths = null;
            if (cmbCardGroups.SelectedIndex==0)
            {
                card_paths = Globals.GetPathOfEachCard();
            }
            else
            {
                card_paths = Globals.GetPathOfEachCard(CardGroupPaths[cmbCardGroups.SelectedIndex - 1]);
            }

            var cards = Globals.GetListOfCards(card_paths);

            foreach (Card card in cards)
            {
                List<string> text_file_list = new List<string>();

                string[] question_text_files = Globals.GetListOfTextFiles(card.QuestionAbsPath);
                string[] answer_text_files = Globals.GetListOfTextFiles(card.AnswerAbsPath);
                string[] reminder_text_files = Globals.GetListOfTextFiles(card.ReminderAbsPath);

                text_file_list.AddRange(question_text_files);
                text_file_list.AddRange(answer_text_files);
                text_file_list.AddRange(reminder_text_files);

                foreach (string file in text_file_list)
                {
                    string file_text = File.ReadAllText(file);
                    if (file_text.IndexOf(TextToSearch) >= 0)
                    {
                        matched_cards.Add(card);
                        break;
                    }
                }
            }
            return matched_cards;
        }

        bool is_initialized = false;
        public void Init()
        {
            if (this.is_initialized) return;

            cmbCardGroups.Items.Clear();
            cmbCardGroups.Items.Add("[ALL]");
            string[] groups = Globals.GetPathOfEachGroup();
            this.CardGroupPaths = groups;
            foreach (string grp_path in groups)
            {
                string grp = Globals.GetLastPartOfDir(grp_path);
                cmbCardGroups.Items.Add(grp);
            }
            cmbCardGroups.SelectedIndex = 0;
            this.is_initialized = true;
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            string txt = txtSearch.Text.Trim();
            var matched_cards = FindMatch(txt);
            SearchResults = matched_cards;
            ctrlCardBrowser.Init(SearchResults);
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btn_Click(null, null);
            }
        }

        private void CtrlExtButton_OnExtButtonClicked(object sender, RoutedEventArgs e)
        {
            CtrlCardBrowser cb = new CtrlCardBrowser();
            cb.Init(SearchResults);
            wndMainWindow.PresentContent(cb);
        }
    }
}
