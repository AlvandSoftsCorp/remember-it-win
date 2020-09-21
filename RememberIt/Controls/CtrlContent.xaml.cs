using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RememberIt
{
    /// <summary>
    /// Interaction logic for CtrlContent.xaml
    /// </summary>
    public partial class CtrlContent : UserControl
    {
        private ContextMenu mnuContentMenu = new ContextMenu();
        private MenuItem mnuRemoveItem = new MenuItem();
        
        private int ContentNumber
        {
            get
            {
                int n = 0;
                string[] files = Directory.GetFiles(WorkingFolder);
                foreach (string file in files)
                {
                    FileInfo fi = new FileInfo(file);
                    int indx = fi.Name.IndexOf(fi.Extension);
                    if (indx < 0) continue;

                    string ns = fi.Name.Substring(0, indx);
                    try
                    {
                        int m = int.Parse(ns);
                        if (m > n) n = m;
                    }
                    catch
                    {
                        continue;
                    }
                }
                

                return n+1;
            }
        }

        public void Load()
        {
            if (Directory.Exists(WorkingFolder) == false) return;
            string[] content_files = Directory.GetFiles(WorkingFolder);
            foreach (string file in content_files)
            {
                ContentItem ci = new ContentItem(file);
                AddContent(ci);
            }
        }

        public void Unload()
        {
            while (Contents.Count > 0)
            {   
                var rd = (RowDefinition)Contents[0].Tag;
                rd.Height = new GridLength(0);
                Contents.RemoveAt(0);
            }
        }


        public Brush BgColor
        {
            get
            {
                return grdContent.Background;
            }
            set
            {
                grdContent.Background = value;
            }
        }


        public string CardRootFolder = "";  //   Root of the card. Example: \C13951111-224001-335\
        public string WorkingFolder = "";   //   Question, answer or reminder folder.   (\Q\  OR  \A\  or  \R\ folder)
        

        Audio AudioRecorder = null;
        public CtrlContent()
        {
            InitializeComponent();
        }

        bool is_initialized = false;
        public void Init()
        {
            if (this.is_initialized) return;
            grdContent.Drop += new DragEventHandler(grdContent_PreviewDrop);
            AudioRecorder = new Audio();
            AudioRecorder.OnRecordingIsDone += new Audio.RecordingIsDoneDelegate(AudioRecorder_OnRecordingIsDone);

            mnuRemoveItem.Header = "Remove";
            int indx = mnuContentMenu.Items.Add(mnuRemoveItem);
            if (indx != -1)
            {
                mnuRemoveItem.Click += new RoutedEventHandler(RemoveItem_Click);
            }

            is_initialized = true;
        }


        void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            RowDefinition rd = null;
            if (mi.Tag is Control) ListOfContentControls.Remove((Control)mi.Tag);

            if (mi.Tag is CtrlRoundImage) rd = (RowDefinition)((CtrlRoundImage)mi.Tag).Tag;
            else if (mi.Tag is CtrlRoundLabel) rd = (RowDefinition)((CtrlRoundLabel)mi.Tag).Tag;
            else if (mi.Tag is CtrlAudioPlayer)
            {
                CtrlAudioPlayer mp = (CtrlAudioPlayer)mi.Tag;
                mp.CloseStram();
                rd = (RowDefinition)((CtrlAudioPlayer)mi.Tag).Tag;
            }
            else return;
            
            rd.Height = new GridLength(0);
            try
            {
                ContentItem content_item = (ContentItem)rd.Tag;
                string file_name = content_item.ContentFile;
                Globals.DeleteFile(file_name);
                Contents.Remove(content_item);
            }
            catch
            {
                //
            }

        }

        private List<ContentItem> Contents = new List<ContentItem>();
        public List<Control> ListOfContentControls = new List<Control>();

        private void AddContent(ContentItem ContentInstance)
        {
            Contents.Add(ContentInstance);
            string uri = "file:///" + ContentInstance.ContentFile;
            
            var rd = new RowDefinition();
            rd.Height = GridLength.Auto;
            int GridRowIndex = grdContent.RowDefinitions.Count;
            grdContent.RowDefinitions.Add(rd);

            rd.Tag = ContentInstance;
            ContentInstance.Tag = rd;

            switch (ContentInstance.MimeType)
            {
                //case ".bmp":
                //case ".png":
                //case ".jpg":
                case ".rip":
                    CtrlRoundImage ri = new CtrlRoundImage();
                    ListOfContentControls.Add(ri);
                    ri.SetValue(Grid.RowProperty, GridRowIndex);
                    ri.SetValue(Grid.ColumnProperty, 1);
                    ri.PictureImageSource = Globals.LoadImage(ContentInstance.ContentFile);
                    grdContent.Children.Add(ri);
                    ri.Tag = rd;
                    ri.ContextMenu = mnuContentMenu;
                    ri.MouseUp += new MouseButtonEventHandler(ContentItem_MouseUp);
                    ri.MouseDoubleClick += new MouseButtonEventHandler(ImageContent_MouseDoubleClick);
                    break;

                //case ".txt":
                case ".rit":
                    string TextString = File.ReadAllText(ContentInstance.ContentFile);
                    CtrlRoundLabel rl = new CtrlRoundLabel();
                    ListOfContentControls.Add(rl);
                    rl.SetValue(Grid.RowProperty, GridRowIndex);
                    rl.SetValue(Grid.ColumnProperty, 1);
                    rl.LabelText = TextString;
                    grdContent.Children.Add(rl);
                    rl.Tag = rd;
                    rl.ContextMenu = mnuContentMenu;
                    rl.MouseUp += new MouseButtonEventHandler(ContentItem_MouseUp);
                    break;
                //case ".wav":
                //case ".mp3":
                case ".ria":
                    string media_file_name = ContentInstance.ContentFile;
                    CtrlAudioPlayer ap = new CtrlAudioPlayer();
                    ListOfContentControls.Add(ap);
                    ap.SetValue(Grid.RowProperty, GridRowIndex);
                    ap.SetValue(Grid.ColumnProperty, 1);
                    ap.MediaFileName = media_file_name;
                    grdContent.Children.Add(ap);
                    ap.Tag = rd;
                    ap.ContextMenu = mnuContentMenu;
                    ap.MouseUp += new MouseButtonEventHandler(ContentItem_MouseUp);
                    break;
            }
            scScroll.ScrollToBottom();
            //this.Build();
        }

        void ImageContent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is CtrlRoundImage)
            {
                CtrlRoundImage ri = (CtrlRoundImage)sender;
                RowDefinition rd = (RowDefinition)ri.Tag;
                ContentItem ci = (ContentItem)rd.Tag;
                Globals.OpenImageEditor(ci.ContentFile);
            }
        }

        void ContentItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            mnuRemoveItem.Tag = sender;
        }



        void btn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RowDefinition rd = (RowDefinition)((Label)sender).Tag;
            rd.Height = new GridLength(0);
            try
            {
                ContentItem content_item = (ContentItem)rd.Tag;
                string file_name = content_item.ContentFile;
                Globals.DeleteFile(file_name);
                Contents.Remove(content_item);
            }
            catch
            { 
                //
            }
        }



        private void grdContent_PreviewDrop(object sender, DragEventArgs drg_event)
        {
            drg_event.Handled = true;
            // Check that the data being dragged is a file
            if (drg_event.Data.GetDataPresent(DataFormats.FileDrop))
            {
                // Get an array with the filenames of the files being dragged
                string[] files = (string[])drg_event.Data.GetData(DataFormats.FileDrop);

                for (int i = 0; i < files.Length; i++)
                {
                    string file_name = files[i];
                    string file_ext = System.IO.Path.GetExtension(files[i]).ToLower();

                    string supported_ = ".jpg .png .txt .mp3 .mp4 .bmp .wav";
                    int indx = supported_.IndexOf(file_ext);
                    if (indx == -1)
                    {
                        drg_event.Effects = DragDropEffects.None;
                        continue;
                    }

                    try
                    {
                        ContentItem ci = new RememberIt.ContentItem(this.ContentNumber, this.WorkingFolder, file_name);
                        AddContent(ci);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show(ex.Message);
                        drg_event.Effects = DragDropEffects.None;
                        continue;
                    }
                }
                drg_event.Effects = DragDropEffects.Copy;
            }
            else
            {
                drg_event.Effects = DragDropEffects.None;
            }
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (txtAdd.Text.Trim() == "")
            {
                if (AudioRecorder.IsRecording == false)
                {
                    // Note no extention is applied but *.wav is considered at fist.
                    // When recording is finished the wave file is converted to an *.mp3 equivalent.
                    string AudioRecordingFileName = WorkingFolder + this.ContentNumber.ToString().PadLeft(3, '0');
                    AudioRecorder.StartAudioRecording(AudioRecordingFileName);
                    txtAdd.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    AudioRecorder.StopRecording();
                }
            }
            else
            {
                ContentItem ci = ContentItem.ProvideContentFromText(this.ContentNumber, WorkingFolder, txtAdd.Text.Trim());
                AddContent(ci);
                txtAdd.Clear();
            }
        }

        public void DisableOperations()
        {
            btnAdd.IsEnabled = false;
            txtAdd.IsEnabled = false;
        }
        


        private void txtAdd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ContentItem ci = ContentItem.ProvideContentFromText(this.ContentNumber, WorkingFolder, txtAdd.Text.Trim());
                AddContent(ci);
                txtAdd.Clear();
            }
        }



        private void OnPasteCommand(object sender, ExecutedRoutedEventArgs e)
        {
            string str = e.Parameter as string;
            switch (str)
            {
                case "Paste":
                    ContentItem[] c = ContentItem.ProvideContentFromClipboard(this.ContentNumber, WorkingFolder);
                    for (int i = 0; i < c.Length; i++)
                    {
                        AddContent(c[i]);
                    }
                    break;
            }
            e.Handled = true;
        }


        void AudioRecorder_OnRecordingIsDone(string AudioFileName)
        {
            txtAdd.Visibility = System.Windows.Visibility.Visible;
            FileInfo fi = new FileInfo(AudioFileName);
            if (fi.Exists)
            {
                ContentItem ci = new ContentItem(AudioFileName);
                AddContent(ci);
            }
        }


        private void btnOk_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnOk_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AudioRecorder.StopRecording();
        }

        private void foo()
        {
            string folder = Globals.GetExecutablePath() + "C1020\\";
            Directory.CreateDirectory(folder);

            for (int i = 0; i < grdContent.RowDefinitions.Count; i++)
            {
                if (grdContent.RowDefinitions[i].Height != new GridLength(0))
                {
                    ContentItem ci = (ContentItem)grdContent.RowDefinitions[i].Tag;
                    //MessageBox.Show(ci.ContentFile);
                    FileInfo fi = new FileInfo(ci.ContentFile);
                    File.Copy(ci.ContentFile, folder + ((i + 1).ToString()).PadLeft(3, '0') + fi.Extension, true);
                }
            }

            Process.Start(folder);
        }


        private void txtAdd_TextChanged(object sender, TextChangedEventArgs e)
        {
            BitmapImage img_bitmap = new BitmapImage();
            img_bitmap.BeginInit();
            if (txtAdd.Text.Trim() == "")
            {
                img_bitmap.UriSource = new Uri("pack://application:,,,/pic/mic_64.png");
            }
            else
            {
                img_bitmap.UriSource = new Uri("pack://application:,,,/pic/plus_64.png");
            }
            img_bitmap.EndInit();
            imgAddContent.Source = img_bitmap;
        }

    }

    public class ContentItem
    {
        public ContentItem(int ContentNumber, string ContentFolder, string FileName)
        {
            FileInfo fi = new FileInfo(FileName);
            MimeType = fi.Extension;
            string file_name = ContentFolder + ContentNumber.ToString().PadLeft(3,'0') + fi.Extension;
            File.Copy(FileName, file_name);

            if (fi.Extension.ToLower() == ".bmp")
            {
               file_name = Globals.ConvertBmpToPng(file_name);
            }
            this.ContentFile = file_name;
        }

        //public ContentItem(int ContentNumber, string FileName)
        //{
        //    FileInfo fi = new FileInfo(FileName);
        //    MimeType = fi.Extension;
        //    this.ContentFile = FileName;
        //}
        
        
        public ContentItem(string FileName)
        {
            FileInfo fi = new FileInfo(FileName);
            this.MimeType = fi.Extension;
            this.ContentFile = FileName;
        }

        public static ContentItem ProvideContentFromText(int ContentNumber, string ContentFolder, string TextString)
        {
            //string file_name = ContentFolder + ContentNumber.ToString().PadLeft(3, '0') + ".txt";
            string file_name = ContentFolder + ContentNumber.ToString().PadLeft(3, '0') + ".rit";
            File.WriteAllText(file_name, TextString);
            //ContentItem ci = new ContentItem(ContentNumber, file_name);
            ContentItem ci = new ContentItem(file_name);
            return ci;
        }

        public static ContentItem[] ProvideContentFromClipboard(int ContentNumber, string ContentFolder)
        {
            List<ContentItem> content_list = new List<ContentItem>();

            if (Clipboard.ContainsImage() == true)
            {
                System.Windows.Forms.IDataObject clipboardData = System.Windows.Forms.Clipboard.GetDataObject();
                if (clipboardData == null) return null;
                if (clipboardData.GetDataPresent(System.Windows.Forms.DataFormats.Bitmap) == false) return null;
                System.Drawing.Bitmap bitmap = (System.Drawing.Bitmap)clipboardData.GetData(System.Windows.Forms.DataFormats.Bitmap);
                //string file_name = ContentFolder + ContentNumber.ToString().PadLeft(3, '0') + ".png";
                string file_name = ContentFolder + ContentNumber.ToString().PadLeft(3, '0') + ".rip";
                bitmap.Save(file_name, System.Drawing.Imaging.ImageFormat.Png);
                content_list.Add(new ContentItem(file_name));
                return content_list.ToArray();
            }

            if (Clipboard.ContainsText())
            {
                string clipboard_text = Clipboard.GetText().Trim();
                if (Globals.IsValidUri(clipboard_text))
                {
                    string uri_str = clipboard_text;
                    Uri uri = new Uri(uri_str);
                    try
                    {
                        string file_name = System.IO.Path.GetFileName(uri.LocalPath);
                        string file_ext = System.IO.Path.GetExtension(uri.LocalPath).Trim().ToLower();
                        string ext = "";
                        switch (file_ext)
                        {
                            case ".mp3":
                            case ".wav":
                                ext = ".ria";
                                break;

                            case ".bmp":
                            case ".png":
                            case ".jpg":
                                ext = ".rip";
                                break;
                            
                            case ".txt":
                                ext = ".rit";
                                break;
                        }

                        string downloaded_file_name = ContentFolder + ContentNumber.ToString().PadLeft(3, '0') + ext;
                        using (WebClient wc = new WebClient())
                        {
                            //wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                            wc.DownloadFile(uri, downloaded_file_name);
                            //content_list.Add(new ContentItem(ContentNumber, downloaded_file_name));
                            content_list.Add(new ContentItem(downloaded_file_name));
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to follow URL.\n" + ex.Message);
                        return content_list.ToArray();
                    }
                }
                else
                {
                    string text = Clipboard.GetText();
                    //string file_name = ContentFolder + ContentNumber.ToString().PadLeft(3, '0') + ".txt";
                    string file_name = ContentFolder + ContentNumber.ToString().PadLeft(3, '0') + ".rit";
                    File.WriteAllText(file_name, text);
                    //content_list.Add(new ContentItem(ContentNumber, file_name));
                    content_list.Add(new ContentItem(file_name));
                }
                return content_list.ToArray();
            }


            if (Clipboard.ContainsAudio())
            {
                // Not implemented 
                return content_list.ToArray();
            }


            if (Clipboard.ContainsFileDropList())
            {
                StringCollection files = Clipboard.GetFileDropList();
                for (int i = 0; i < files.Count; i++)
                {
                    FileInfo fi = new FileInfo(files[i]);
                    content_list.Add(new ContentItem(ContentNumber, ContentFolder, files[i]));
                }
            }
            return content_list.ToArray();
        }

        public string ContentFile = "";
        public string MimeType = "";
        public object Tag = null;
    }
}