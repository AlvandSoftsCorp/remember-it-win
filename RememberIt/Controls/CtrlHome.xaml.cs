using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace RememberIt
{
    /// <summary>
    /// Interaction logic for CtrlHome.xaml
    /// </summary>
    public partial class CtrlHome : UserControl
    {
        private enum Faces
        {
            ShowMissedCards = 0,
            ShowPendingCards = 1,
            ShowAllCards = 2
        }
        
        private Faces Face = Faces.ShowMissedCards;
        public MainWindow wndMainWindow = null;
        private ContextMenu mnuMenu = new ContextMenu();
        private CtrlExtButton btnNewCard = null;
        private CtrlExtButton btnSearch = null;
        private CtrlExtButton btnDefaultGroup = null;
        private CtrlExtButton btnCards = null;
        private List<CtrlExtButton> btnCardGroups = new List<CtrlExtButton>();

        private MenuItem mnuShowAllCards = null;
        private MenuItem mnuShowMissedCards = null;
        private MenuItem mnuShowPendingCards = null;

        public CtrlHome()
        {
            InitializeComponent();
            InitMenu();
            Init();

            grdCards.AllowDrop = true;
            grdCards.Drop += new DragEventHandler(grdCards_Drop);
            grdCards.Background = System.Windows.Media.Brushes.Gray;
        }

        void grdCards_Drop(object sender, DragEventArgs drg_event)
        {
            drg_event.Handled = true;
            
            
            // Check that the data being dragged is a set of folders
            if (drg_event.Data.GetDataPresent(DataFormats.FileDrop))
            {

                // Get an array with the filenames of the files being dragged
                string[] paths = (string[])drg_event.Data.GetData(DataFormats.FileDrop);
                List<Card> cards = Globals.GetListOfCards(paths);

                CtrlCardBrowser cb = new CtrlCardBrowser();
                cb.Init(cards);

                wndMainWindow.PresentContent(cb);

                drg_event.Effects = DragDropEffects.Copy;
            }
            else
            {
                drg_event.Effects = DragDropEffects.None;
            }
        }

        private void InitMenu()
        {
            MenuItem mnuRefresh = new MenuItem();
            mnuRefresh.Header = "Refresh";
            mnuRefresh.Click += new RoutedEventHandler(mnuRefresh_Click);
            mnuMenu.Items.Add(mnuRefresh);

            MenuItem mnuOpenCardsRoot = new MenuItem();
            mnuOpenCardsRoot.Header = "Open Cards Folder";
            mnuOpenCardsRoot.Click += new RoutedEventHandler(mnuOpenCardsRoot_Click);
            mnuMenu.Items.Add(mnuOpenCardsRoot);

            MenuItem mnuBuildHtmlDocs = new MenuItem();
            mnuBuildHtmlDocs.Header = "Build HTML Documents";
            mnuBuildHtmlDocs.Click += new RoutedEventHandler(mnuBuildHtmlDocs_Click);
            mnuMenu.Items.Add(mnuBuildHtmlDocs);

            MenuItem mnuRemoveHtmlDocs = new MenuItem();
            mnuRemoveHtmlDocs.Header = "Remove HTML Documents";
            mnuRemoveHtmlDocs.Click += new RoutedEventHandler(mnuRemoveHtmlDocs_Click);
            mnuMenu.Items.Add(mnuRemoveHtmlDocs);

            mnuShowAllCards = new MenuItem();
            mnuShowAllCards.Header = "Show All Cards";
            mnuShowAllCards.Click += new RoutedEventHandler(mnuShowAllCrads_Click);
            mnuMenu.Items.Add(mnuShowAllCards);


            mnuShowMissedCards = new MenuItem();
            mnuShowMissedCards.Header = "Show Missed Cards";
            mnuShowMissedCards.Click += new RoutedEventHandler(mnuShowMissedCrads_Click);
            mnuShowMissedCards.IsChecked = true;
            mnuMenu.Items.Add(mnuShowMissedCards);

            mnuShowPendingCards = new MenuItem();
            mnuShowPendingCards.Header = "Show Pending Cards";
            mnuShowPendingCards.Click += new RoutedEventHandler(mnuShowPendingCards_Click);
            mnuMenu.Items.Add(mnuShowPendingCards);

            MenuItem mnuSendCardsToPhoneCards = new MenuItem();
            mnuSendCardsToPhoneCards.Header = "Send Cards to Phone";
            mnuSendCardsToPhoneCards.Click += new RoutedEventHandler(mnuSendCardsToPhoneCards_Click);
            mnuMenu.Items.Add(mnuSendCardsToPhoneCards);

            MenuItem mnuChangeRoot = new MenuItem();
            mnuChangeRoot.Header = "ChangeRoot";
            mnuChangeRoot.Click += new RoutedEventHandler(mnuChangeRoot_Click);
            mnuMenu.Items.Add(mnuChangeRoot);

            this.ContextMenu = mnuMenu;
        }


        private void Refresh()
        {
            Cleanup();
            Init();
        }

        private void Cleanup()
        {
            while (grdContent.Children.Count > 0)
            {
                grdContent.Children.RemoveAt(0);
            }
            while (grdCards.Children.Count > 0)
            {
                grdCards.Children.RemoveAt(0);
            }
        }

        private int AddExtButton(Grid GridContainer, int ColIndex, string Title, string Detail, string Uri, RoutedEventHandler OnClickEventHandler, object Tag, out CtrlExtButton Button)
        {
            var rd = new RowDefinition();
            rd.Height = GridLength.Auto;
            int grid_row_index = GridContainer.RowDefinitions.Count;
            GridContainer.RowDefinitions.Add(rd);

            CtrlExtButton btn = new CtrlExtButton();
            btn.Title = Title;
            btn.Detail = Detail;
            Image img = new Image();
            BitmapImage img_bitmap = new BitmapImage();
            img_bitmap.BeginInit();
            img_bitmap.UriSource = new Uri(Uri);
            img_bitmap.EndInit();
            img.Source = img_bitmap;
            btn.Image_Source = img_bitmap;
            btn.SetValue(Grid.ColumnProperty, ColIndex);
            btn.SetValue(Grid.RowProperty, grid_row_index);
            btn.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            btn.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            GridContainer.Children.Add(btn);
            btn.Tag = Tag;
            btn.OnExtButtonClicked += OnClickEventHandler;
            Button = btn;
            return grid_row_index;
        }


        private void SetImageToGrid(Grid GridContainer, string Uri, int RowIndex, int ColIndex)
        {
            Image img = new Image();
            BitmapImage btimap_image = new BitmapImage();
            btimap_image.BeginInit();
            btimap_image.UriSource = new Uri(Uri);
            btimap_image.EndInit();
            img.Source = btimap_image;

            img.SetValue(Grid.ColumnProperty, ColIndex);
            img.SetValue(Grid.RowProperty, RowIndex);
            img.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            img.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            GridContainer.Children.Add(img);
        }


        private void Init()
        {
            this.AddExtButton(
                grdContent,
                0,  /*Col Index*/
                "New Card",
                "Add a new Card",
                "pack://application:,,,/pic/plus_64.png",
                OnNewCardClicked,
                null,/*Tag*/
                out btnNewCard);

            this.AddExtButton(
                grdContent,
                0,  /*Col Index*/
                "Search",
                "Search & Filter",
                "pack://application:,,,/pic/search_64.png",
                OnSearchClicked,
                null,/*Tag*/
                out btnSearch
                );

            string def_grp_name = Globals.GetDefaultGroupName();
            this.AddExtButton(
                    grdContent,
                    0,  /*Col Index*/
                    string.Format("Default Group: [{0}]", def_grp_name),
                    "Press to change the default group.",
                    "pack://application:,,,/pic/group_64.png",
                    OnChangeDefaultGroupClicked,
                    null,/*Tag*/
                    out btnDefaultGroup);

            if (this.Face == Faces.ShowMissedCards)
            {
                int cnt = Globals.GetNumberOfMissedCards();
                this.AddExtButton(
                        grdContent,
                        0,  /*Col Index*/
                        string.Format("Missed: [{0}]", cnt),
                        "Total Missed cards",
                        "pack://application:,,,/pic/missed_64.png",
                        OnShowMissedCardsClicked,
                        "[ALL]",
                        out btnCards
                        );

                string[] groups = Globals.GetPathOfEachGroup();
                if (groups != null)
                {
                    foreach (string group_path in groups)
                    {
                        string group_name = Globals.GetLastPartOfDir(group_path);
                        cnt = Globals.GetNumberOfMissedCards(group_path);
                        if (cnt == 0) continue;
                        CtrlExtButton btn_grp = null;
                        int row_index =
                        this.AddExtButton(
                            grdCards,
                            1,  /*Col Index*/
                            string.Format("{0}: [{1}]", group_name, cnt),
                            string.Format("You have {0} cards avilable", cnt),
                            "pack://application:,,,/pic/group_128.png",
                            OnShowMissedCardsClicked,
                            group_path,
                            out btn_grp);
                        btnCardGroups.Add(btn_grp);

                        this.SetImageToGrid(
                            grdCards,
                            "pack://application:,,,/pic/down_right_100.png",
                            row_index,
                            0);
                    }
                }
            }
            else if (this.Face == Faces.ShowPendingCards)
            {
                int cnt = Globals.GetNumberOfPendingCards();
                this.AddExtButton(
                        grdContent,
                        0,  /*Col Index*/
                        string.Format("Pending: [{0}]", cnt),
                        "Total Pending cards",
                        "pack://application:,,,/pic/alarm_64.png",
                        OnShowPendingCardsClicked,
                        "[ALL]",
                        out btnCards
                        );

                string[] groups = Globals.GetPathOfEachGroup();
                if (groups != null)
                {
                    foreach (string group_path in groups)
                    {
                        string group_name = Globals.GetLastPartOfDir(group_path);
                        cnt = Globals.GetNumberOfPendingCards(group_path);
                        if (cnt == 0) continue;
                        CtrlExtButton btn_grp = null;
                        int row_index =
                        this.AddExtButton(
                            grdCards,
                            1,  /*Col Index*/
                            string.Format("{0}: [{1}]", group_name, cnt),
                            string.Format("You have {0} cards avilable", cnt),
                            "pack://application:,,,/pic/group_128.png",
                            OnShowPendingCardsClicked,
                            group_path,
                            out btn_grp);
                        btnCardGroups.Add(btn_grp);

                        this.SetImageToGrid(
                            grdCards,
                            "pack://application:,,,/pic/down_right_100.png",
                            row_index,
                            0);
                    }
                }
            }

            else if (this.Face == Faces.ShowAllCards)
            {
                int cnt = Globals.GetNumberOfAllCards();
                this.AddExtButton(
                        grdContent,
                        0,  /*Col Index*/
                        string.Format("All: [{0}]", cnt),
                        "Total cards",
                        "pack://application:,,,/pic/star_128.png",
                        OnShowAllCardsClicked,
                        "[ALL]",
                        out btnCards
                        );

                string[] groups = Globals.GetPathOfEachGroup();
                if (groups != null)
                {
                    foreach (string group_path in groups)
                    {
                        string group_name = Globals.GetLastPartOfDir(group_path);
                        cnt = Globals.GetNumberOfCards(group_path);
                        if (cnt == 0) continue;
                        CtrlExtButton btn_grp = null;
                        int row_index =
                        this.AddExtButton(
                            grdCards,
                            1,  /*Col Index*/
                            string.Format("{0}: [{1}]", group_name, cnt),
                            string.Format("You have {0} cards avilable", cnt),
                            "pack://application:,,,/pic/group_128.png",
                            OnShowAllCardsClicked,
                            group_path,
                            out btn_grp);
                        btnCardGroups.Add(btn_grp);

                        this.SetImageToGrid(
                            grdCards,
                            "pack://application:,,,/pic/down_right_100.png",
                            row_index,
                            0);
                    }
                }
            }
        }



        void OnShowMissedCardsClicked(object sender, RoutedEventArgs e)
        {
            if (sender is CtrlExtButton)
            {
                CtrlExtButton btn = (CtrlExtButton)sender;

                if (btn.Tag is string)
                {
                    string group_path = btn.Tag.ToString();
                    PresentMissedCards(group_path);
                }

            }
        }


        void OnShowPendingCardsClicked(object sender, RoutedEventArgs e)
        {
            if (sender is CtrlExtButton)
            {
                CtrlExtButton btn = (CtrlExtButton)sender;

                if (btn.Tag is string)
                {
                    string group_path = btn.Tag.ToString();
                    PresentPendingCards(group_path);
                }

            }
        }
        
        
        void OnShowAllCardsClicked(object sender, RoutedEventArgs e)
        {
            if (sender is CtrlExtButton)
            {
                CtrlExtButton btn = (CtrlExtButton)sender;

                if (btn.Tag is string)
                {
                    string group_path = btn.Tag.ToString();
                    PresentAllCards(group_path);
                }

            }
        }

        void PresentMissedCards(string CardGroupPath)
        {
            string[] paths = null;
            if (CardGroupPath == "[ALL]") paths = Globals.GetPathOfEachMissedCard();
            else paths = Globals.GetPathOfEachMissedCard(CardGroupPath);

            var cards = Globals.GetListOfCards(paths);
            CtrlCardBrowser cb = new CtrlCardBrowser();
            cb.Init(cards);
            
            wndMainWindow.PresentContent(cb);
        }
        
        
        void PresentAllCards(string CardGroupPath)
        {
            string[] paths = null;
            if (CardGroupPath == "[ALL]") paths = Globals.GetPathOfEachCard();
            else paths = Globals.GetPathOfEachCard(CardGroupPath);

            var cards = Globals.GetListOfCards(paths);
            CtrlCardBrowser cb = new CtrlCardBrowser();
            cb.Init(cards);
            wndMainWindow.PresentContent(cb);
        }
        
        
        void PresentPendingCards(string CardGroupPath)
        {
            string[] paths = null;
            if (CardGroupPath == "[ALL]") paths = Globals.GetPathOfEachPendingCard();
            else paths = Globals.GetPathOfEachPendingCard(CardGroupPath);

            var cards = Globals.GetListOfCards(paths);
            CtrlCardBrowser cb = new CtrlCardBrowser();
            cb.Init(cards);
            wndMainWindow.PresentContent(cb);
        }


        void OnChangeDefaultGroupClicked(object sender, RoutedEventArgs e)
        {
            frmSelectCardGroup f = new frmSelectCardGroup();
            f.ShowDialog();
            if (f.IsCanceled == true) return;
            Globals.SetDefaultGroup(f.SelectedGroupPath);
            if (sender is CtrlExtButton)
            {
                CtrlExtButton btn = (CtrlExtButton)sender;
                string def_grp_name = Globals.GetDefaultGroupName();
                btn.Title = string.Format("Default Group: [{0}]", def_grp_name);
            }
        }

        public delegate void CardListEventHandler(object sender, string CardGroupPath);
        public event EventHandler OnReq_SetDefaultGroup = null;


        void ctrlCard_OnClose(object sender, EventArgs e)
        {
            wndMainWindow.PopLastScreen();
            this.Refresh();
        }


        private void OnNewCardClicked(object sender, RoutedEventArgs e)
        {
            CtrlCard ctrlCard = new CtrlCard();
            ctrlCard.Init();
            ctrlCard.OnClose += new EventHandler(ctrlCard_OnClose);
            wndMainWindow.PresentContent(ctrlCard);
        }



        private void btnDefaultGroup_Click(object sender, RoutedEventArgs e)
        {
            if (this.OnReq_SetDefaultGroup != null) this.OnReq_SetDefaultGroup(null, null);
            this.Init();
        }

        private void OnSearchClicked(object sender, RoutedEventArgs e)
        {
            CtrlSearch ctrl_search = new CtrlSearch();
            ctrl_search.Init();
            ctrl_search.wndMainWindow = this.wndMainWindow;
            wndMainWindow.PresentContent(ctrl_search);
        }


        void mnuRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.Refresh();
        }


        void mnuOpenCardsRoot_Click(object sender, RoutedEventArgs e)
        {
            string cp = Globals.GetCardsPath();
            Process.Start(cp);
        }


        void mnuBuildHtmlDocs_Click(object sender, RoutedEventArgs e)
        {
            System.Drawing.Rectangle rect = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
            FrmBuildHtmlDocument f = new FrmBuildHtmlDocument();
            f.Top = 0;
            f.Left = 0;
            f.Width = (int)(rect.Width - wndMainWindow.Width);
            f.Height = rect.Height;
            f.ShowDialog();
        }

        void mnuRemoveHtmlDocs_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Remove all HTML documents?", "Warning", MessageBoxButton.YesNoCancel);
            if (res != MessageBoxResult.Yes) return;

            int cnt = 0;
            int fail_cnt = 0;
            string[] all_files = Globals.GetAllFiles(Globals.GetCardsPath());
            for (int i = 0; i < all_files.Length; i++)
            {
                FileInfo fi = new FileInfo(all_files[i]);
                if (fi.Extension.ToLower() == ".html")
                {
                    try
                    {
                        File.Delete(all_files[i]);
                        cnt++;
                    }
                    catch (Exception ex)
                    {
                        fail_cnt++;
                        continue;
                    }
                }
            }

            if (fail_cnt == 0)
            {
                string msg = string.Format("Removed Files: {0}", cnt);
                MessageBox.Show(msg);
            }
            else
            {
                string msg = string.Format("Removed Files: {0}, Unremovable Files: {1}", cnt, fail_cnt);
                MessageBox.Show(msg);
            }
        }



        void mnuShowAllCrads_Click(object sender, RoutedEventArgs e)
        {
            this.Face = Faces.ShowAllCards;
            this.Refresh();

            mnuShowAllCards.IsChecked = true;
            mnuShowMissedCards.IsChecked = false;
            mnuShowPendingCards.IsChecked = false;
        }
        
        
        void mnuShowMissedCrads_Click(object sender, RoutedEventArgs e)
        {
            this.Face = Faces.ShowMissedCards;
            this.Refresh();
            
            mnuShowAllCards.IsChecked = false;
            mnuShowMissedCards.IsChecked = true;
            mnuShowPendingCards.IsChecked = false;
        }


        void mnuShowPendingCards_Click(object sender, RoutedEventArgs e)
        {
            this.Face = Faces.ShowPendingCards;
            this.Refresh();
            
            mnuShowAllCards.IsChecked = false;
            mnuShowMissedCards.IsChecked = false;
            mnuShowPendingCards.IsChecked = true;
        }


        void mnuSendCardsToPhoneCards_Click(object sender, RoutedEventArgs e)
        {
            System.Drawing.Rectangle rect = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
            FrmPcToPhone f = new FrmPcToPhone();
            f.Top = 0;
            f.Left = 0;
            f.Width = (int)(rect.Width - wndMainWindow.Width);
            f.Height = rect.Height;
            f.ShowDialog();
        }

        void mnuChangeRoot_Click(object sender, RoutedEventArgs e)
        {
            FrmChangeRoot f = new FrmChangeRoot();
            f.Root = Globals.Root;
            f.ShowDialog();
            if (f.IsCanceled) return;
            // MessageBox.Show(f.Root);
            Globals.Root = f.Root+"\\";

            this.Refresh();
        }

        

    }
}