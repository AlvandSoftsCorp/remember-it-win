using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Controls;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Forms;
using System.Diagnostics;

namespace RememberIt
{
    public class Globals
    {

        public static string Root = "";

        public static string GetNewGuid()
        {
            Guid guid = System.Guid.NewGuid();
            return guid.ToString();
        }

        
        public static List<Card> GetListOfCards(string[] ListOfCardPaths)
        {
            if (ListOfCardPaths == null) return null;

            List<Card> card_list = new List<Card>();
            string[] cards = ListOfCardPaths;
            foreach (string card_abs_path in cards)
            {
                if (Card.IsValidCardAbsPath(card_abs_path) == false) continue;
                Card c = new Card(card_abs_path + "\\");
                card_list.Add(c);
            }
            return card_list;
        }
        
        
        public static string[] GetPathOfEachGroup()
        {
            try
            {
                string[] groups = Directory.GetDirectories(Globals.GetCardsPath());
                return groups;
            }
            catch
            {
                return null;
            }
        }


        internal static string[] GetPathOfEachCard()
        {
            List<string> all_cards = new List<string>();
            string[] all_groups = GetPathOfEachGroup();
            if (all_groups == null) return null;

            for (int i = 0; i < all_groups.Length; i++)
            {
                string[] cards_in_group = Globals.GetPathOfEachCard(all_groups[i]);
                all_cards.AddRange(cards_in_group);
            }
            return all_cards.ToArray();
        }


        public static string[] GetPathOfEachCard(string Group)
        {
            try
            {
                string[] cards = Directory.GetDirectories(Group);
                return cards;
            }
            catch
            {
                return null;
            }
        }
        
        
        public static string[] GetPathOfEachCard(string Group, int Stage)
        {
            string[] all_cards = Globals.GetPathOfEachCard(Group);
            List<string> card_list = new List<string>();
            foreach (string c in all_cards)
            {
                try
                {
                    string manifest_file = c + "\\manifest.man";
                    KeyValPair kv = new KeyValPair(';', ':');
                    kv.Load(manifest_file);
                    int stg = int.Parse(kv.GetVal("stage"));
                    if (stg == Stage)
                    {
                        card_list.Add(c);
                    }
                }
                catch
                {
                    continue;
                }
            }
            return card_list.ToArray();
        }


        internal static string[] GetPathOfEachPendingCard()
        {
            string[] all_cards = Globals.GetPathOfEachCard();
            List<string> pending_cards = new List<string>();
            foreach (string c in all_cards)
            {
                try
                {
                    MultiCalendar mc = MultiCalendar.FromDateTime(DateTime.Now);
                    double gdp_now = mc.GetGdp();
                    string manifest_file = c + "\\manifest.man";
                    KeyValPair kv = new KeyValPair(';', ':');
                    kv.Load(manifest_file);
                    double nv_gdp = double.Parse(kv.GetVal("next_visit"));
                    if (nv_gdp >= gdp_now)
                    {
                        pending_cards.Add(c);
                    }
                }
                catch
                {
                    continue;
                }
            }
            return pending_cards.ToArray();
        }
        
        
        internal static string[] GetPathOfEachPendingCard(string Group)
        {
            string[] all_cards = Globals.GetPathOfEachCard(Group);
            List<string> pending_cards = new List<string>();
            foreach (string c in all_cards)
            {
                try
                {
                    MultiCalendar mc = MultiCalendar.FromDateTime(DateTime.Now);
                    double gdp_now = mc.GetGdp();
                    string manifest_file = c + "\\manifest.man";
                    KeyValPair kv = new KeyValPair(';', ':');
                    kv.Load(manifest_file);
                    double nv_gdp = double.Parse(kv.GetVal("next_visit"));
                    if (nv_gdp >= gdp_now)
                    {
                        pending_cards.Add(c);
                    }
                }
                catch
                {
                    continue;
                }
            }
            return pending_cards.ToArray();
        }

        
        internal static string[] GetPathOfEachMissedCard()
        {
            string[] all_cards = Globals.GetPathOfEachCard();
            if (all_cards == null)
            {
                // MessageBox.Show("No cards found.");
                return null;
            }
            List<string> pending_cards = new List<string>();
            foreach (string c in all_cards)
            {
                try
                {
                    MultiCalendar mc = MultiCalendar.FromDateTime(DateTime.Now);
                    double gdp_now = mc.GetGdp();
                    string manifest_file = c + "\\manifest.man";
                    KeyValPair kv = new KeyValPair(';', ':');
                    kv.Load(manifest_file);
                    double nv_gdp = double.Parse(kv.GetVal("next_visit"));
                    if (nv_gdp < gdp_now)
                    {
                        pending_cards.Add(c);
                    }
                }
                catch
                {
                    continue;
                }
            }
            return pending_cards.ToArray();
        }


        internal static string[] GetPathOfEachMissedCard(string GroupPath)
        {
            string[] cards = Globals.GetPathOfEachCard(GroupPath);
            List<string> missed_cards = new List<string>();
            foreach (string c in cards)
            {
                try
                {
                    MultiCalendar mc = MultiCalendar.FromDateTime(DateTime.Now);
                    double gdp_now = mc.GetGdp();
                    string manifest_file = c + "\\manifest.man";
                    KeyValPair kv = new KeyValPair(';', ':');
                    kv.Load(manifest_file);
                    double nv_gdp = double.Parse(kv.GetVal("next_visit"));
                    if (nv_gdp < gdp_now)
                    {
                        missed_cards.Add(c);
                    }
                }
                catch
                {
                    continue;
                }
            }
            return missed_cards.ToArray();
        }
        

        public static int GetNumberOfGroups()
        {
            string[] all_groups = GetPathOfEachGroup();
            if (all_groups != null) return all_groups.Length;
            else return 0;
        }

        
        public static int GetNumberOfCards(string Group)
        {
            string[] all_cards_in_group = GetPathOfEachCard(Group);
            if (all_cards_in_group != null) return all_cards_in_group.Length;
            else return 0;
        }


        public static int GetNumberOfAllCards()
        {
            string[] all_groups = GetPathOfEachGroup();
            if (all_groups == null) return 0;

            int cnt = 0;
            for (int i = 0; i < all_groups.Length; i++)
            {
                cnt += GetNumberOfCards(all_groups[i]);
            }
            return cnt;
        }

        

        
        public static string GetExecutablePath()
        {
            string path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            return path + "\\";
        }

        
        public static string GetCardsPath()
        {
            return Globals.Root + "cards\\";
        }


        public static bool ClearTempFolder()
        {
            string dir = GetExecutablePath();
            dir += "temp\\";
            try
            {
                if (Directory.Exists(dir))
                {
                    Directory.Delete(dir, true);
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        
        public static string CreateNewCard(string CardGroup)
        {
            try
            {
                string dir = GetExecutablePath();
                dir += @"Cards\";
                dir += CardGroup + @"\";
                dir += "C"+GetDateTimeStamp() + @"\";
                if (Directory.Exists(dir) == false)
                {
                    Directory.CreateDirectory(dir);
                }
                string history = string.Format("[{0}] Created. Stage: 0", Globals.GetDateTimeStampSec());
                File.WriteAllText(string.Format("{0}history.txt", dir), history);

                DateTime now = DateTime.Now;
                MultiCalendar mc = new MultiCalendar();
                mc.SetGregDate(now.Year, now.Month, now.Day);
                mc.SetTime(now.Hour, now.Minute, now.Second);
                double gdp = mc.GetGdp();
                
                KeyValPair kvp = new KeyValPair(';',':');
                kvp.Add("next_visit", (gdp + 1).ToString());
                kvp.Add("next_visit_jal", mc.GetJalDate(DateSeparator: "") + "-" + mc.GetTime(TimeSeparator: ""));
                kvp.Add("next_visit_grg", mc.GetGregDate(DateSeparator: "") + "-" + mc.GetTime(TimeSeparator: ""));
                kvp.Add("stage", "0");
                kvp.Add("can_edit", "1");
                kvp.Add("last_modified", gdp.ToString());
                kvp.Add("crc", "0");
                string manifest_data = kvp.GetStrLines();
                
                //manifest_data += string.Format("next_visit={0}\r\n", gdp);
                //manifest_data += string.Format("stage={0}\r\n", 0);
                //manifest_data += string.Format("can_edit={0}\r\n", 1);
                //manifest_data += string.Format("last_modified={0}\r\n", gdp);
                //manifest_data += string.Format("crc={0}\r\n", 0);
                File.WriteAllText(string.Format("{0}manifest.man", dir), manifest_data);
                return dir;
            }
            catch
            {
                return "";
            }
        }

        
        public static string GetDateTimeStamp()
        {
            MultiCalendar mc = new MultiCalendar();
            DateTime dt = DateTime.Now;
            string time_stamp = string.Format("{0}{1}{2}-{3}", Pad2(dt.Hour), Pad2(dt.Minute), Pad2(dt.Second), Pad3(dt.Millisecond));

            mc.SetGregDate(dt.Year, dt.Month, dt.Day);
            string date_time_stamp = mc.GetJalDate().Replace("/", "") + "-" + time_stamp;
            return date_time_stamp;
        }
        
        
        public static string GetDateTimeStampSec()
        {
            MultiCalendar mc = new MultiCalendar();
            DateTime dt = DateTime.Now;
            string time_stamp = string.Format("{0}:{1}:{2}", Pad2(dt.Hour), Pad2(dt.Minute), Pad2(dt.Second));

            mc.SetGregDate(dt.Year, dt.Month, dt.Day);
            string date_time_stamp = mc.GetJalDate()+ "-" + time_stamp;
            return date_time_stamp;
        }

        
        private static string Pad2(int p)
        {
            return p.ToString().PadLeft(2, '0');
        }
        
        
        private static string Pad3(int p)
        {
            return p.ToString().PadLeft(3, '0');
        }

        
        public static bool IsValidUri(string UriString)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(UriString, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return result;
        }

        
        internal static string CreateFolder(string DirPath)
        {
            try
            {
                Directory.CreateDirectory(DirPath);
                return DirPath;
            }
            catch
            {
                return null;
            }
        }

        
        internal static void DeleteFile(string FullFileName)
        {
            try
            {
                File.Delete(FullFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to delete content\n" + ex.Message);
            }
        }

        //internal static System.Windows.Media.ImageSource LoadImage(string ImageFileName)
        //{
        //    System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(ImageFileName);
        //    System.Drawing.Image img = (System.Drawing.Image)img;

        //    return null;  
        //}

        internal static BitmapSource LoadImage(string ImageFileName)
        {
            byte[] imageData = File.ReadAllBytes(ImageFileName);
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                var decoder = BitmapDecoder.Create(ms,
                    BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
                return decoder.Frames[0];
            }
        }

        
        public static string ConvertBmpToJpeg(string BitmapFileName)
        {
            FileInfo fi = new FileInfo(BitmapFileName);
            if (fi.Exists == false) return null;
            try
            {
                byte[] imageData = File.ReadAllBytes(BitmapFileName);
                MemoryStream ms = new MemoryStream(imageData);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                string jpg_file_name = BitmapFileName.Replace(fi.Extension, ".jpg");
                img.Save(jpg_file_name, System.Drawing.Imaging.ImageFormat.Jpeg);
                File.Delete(BitmapFileName);
                return jpg_file_name;
            }
            catch
            {
                return null;
            }

        }

        
        public static string ConvertBmpToPng(string BitmapFileName)
        {
            FileInfo fi = new FileInfo(BitmapFileName);
            if (fi.Exists == false) return null;
            try
            {
                byte[] imageData = File.ReadAllBytes(BitmapFileName);
                MemoryStream ms = new MemoryStream(imageData);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                string png_file_name = BitmapFileName.Replace(fi.Extension, ".png");
                img.Save(png_file_name, System.Drawing.Imaging.ImageFormat.Png);
                File.Delete(BitmapFileName);
                return png_file_name;
            }
            catch
            {
                return null;
            }

        }
        

        internal static int GetNumberOfPendingCards()
        {
            return Globals.GetPathOfEachPendingCard().Length;
        }


        internal static int GetNumberOfMissedCards()
        {
            string[] missed_cards = Globals.GetPathOfEachMissedCard();
            if (missed_cards == null) return 0;
            return missed_cards.Length;
        }

        internal static int GetNumberOfMissedCards(string GroupPath)
        {
            return Globals.GetPathOfEachMissedCard(GroupPath).Length;
        }
        
        
        internal static int GetNumberOfPendingCards(string GroupPath)
        {
            return Globals.GetPathOfEachPendingCard(GroupPath).Length;
        }

        
        internal static string GetDefaultGroupName()
        {
            string gp = GetDefaultGroupPath();
            string gn = Globals.GetLastPartOfDir(gp);
            return gn;
        }


        internal static string GetDefaultGroupPath()
        {
            KeyValPair kv = new KeyValPair(';', ':');
            string settings_file_name = Globals.GetExecutablePath() + "Settings.stg";
            kv.Load(settings_file_name);
            string def_group = kv.GetVal("DefaultGroup");
            return def_group;
        }

        private static int ShouldBuildHtmlDocs = -1;
        public static int GetShuldBuildHtmlDocs()
        {
            if (ShouldBuildHtmlDocs == -1)
            {
                KeyValPair kv = new KeyValPair(';', ':');
                string settings_file_name = Globals.GetExecutablePath() + "Settings.stg";
                kv.Load(settings_file_name);
                string build_html_docs_str = kv.GetVal("BuildHtmlDocs");
                try
                {
                    ShouldBuildHtmlDocs = int.Parse(build_html_docs_str);
                }
                catch
                {
                    //MessageBox.Show("Building HTML documents is disabled.");
                    ShouldBuildHtmlDocs = 0;
                }
            }
            return ShouldBuildHtmlDocs;
        }        
        
        internal static void SetDefaultGroup(string DefaultCardGroupPath)
        {
            if (Directory.Exists(DefaultCardGroupPath) == false) return;

            KeyValPair kv = new KeyValPair(';', ':');
            string settings_file_name = Globals.GetExecutablePath() + "Settings.stg";
            kv.Load(settings_file_name);
            kv.SetVal("DefaultGroup", DefaultCardGroupPath);
            kv.Save(settings_file_name);
        }


        public static string GetLastPartOfDir(string ADirectory)
        {
            try
            {
                string[] parts = ADirectory.Split(new char[] { '\\','/' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0) return null;
                return parts[parts.Length - 1].Trim();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        
        public static string GetDirOfFile(string FullFileName)
        {
            string res = "";
            try
            {
                string[] parts = FullFileName.Split(new char[] { '\\','/' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 0) return null;
                for (int i = 0; i < parts.Length-1; i++)
                {
                    res += parts[i] + "\\";
                }
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        
        public static List<string> GetListOfDefectiveCards()
        {
            List<string> defective_cards = new List<string>();
            string[] all_cards_paths = Globals.GetPathOfEachCard();
            if (all_cards_paths == null) return defective_cards;
 
            foreach (string cp in all_cards_paths)
            {
                string question_dir = cp + "\\Q\\";
                if (Directory.Exists(question_dir) == false)
                {
                    defective_cards.Add(cp);
                    continue;
                }
                string[] question_files = Directory.GetFiles(question_dir);
                if (question_files == null || question_files.Length == 0)
                {
                    defective_cards.Add(cp);
                    continue;
                }
                
                string answer_dir = cp + "\\A\\";
                if (Directory.Exists(answer_dir) == false)
                {
                    defective_cards.Add(cp);
                    continue;
                }
                string[] answer_files = Directory.GetFiles(answer_dir);
                if (answer_files == null || answer_files.Length == 0)
                {
                    defective_cards.Add(cp);
                    continue;
                }
            }
            return defective_cards;
        }


        internal static int GetNumberOfFiles(string Path)
        {
            if (Directory.Exists(Path) == false) return 0;
            string[] files = Directory.GetFiles(Path);
            if (files == null) return 0;
            return files.Length;
        }

        
        internal static void RemoveCard(string DirectoryPath)
        {
            try
            {
                Directory.Delete(DirectoryPath, true);
            }
            catch
            { 
                //
            }

        }

        
        public static void OpenImageEditor(string ImageFile)
        {
            KeyValPair kv = new KeyValPair(';', ':');
            kv.Load(Globals.GetExecutablePath() + "settings.stg");
            string image_editor_path = kv.GetVal("ImageEditor");
            if (File.Exists(image_editor_path) == false) return;
            try
            {
                Process.Start(image_editor_path, ImageFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        internal static string[] GetListOfTextFiles(string DirectoryPath)
        {
            List<string> list_of_text_files = new List<string>();
            try
            {
                string[] files = Directory.GetFiles(DirectoryPath);
                foreach (string file in files)
                {
                    FileInfo fi = new FileInfo(file);
                    if (fi.Extension.ToLower() == ".txt")
                    {
                        list_of_text_files.Add(file);
                    }
                }
            }
            catch 
            { 
                //
            }
            return list_of_text_files.ToArray();
        }


        public static void BuildHtmlDocs(string CardPath, FrmBuildHtmlDocument.ProcessInfo ProcessInfoHandler)
        {
            if (Globals.GetShuldBuildHtmlDocs() == 0) return;
            if (ProcessInfoHandler != null) ProcessInfoHandler(CardPath);

            Build(CardPath, "Q");
            Build(CardPath, "A");
            Build(CardPath, "R");
            Build(CardPath, "H");
        }



        private static byte[] QuestionTemplate = global::RememberIt.Properties.Resources.Question;
        private static byte[] AnswerTemplate = global::RememberIt.Properties.Resources.Answer;
        private static byte[] ReminderTemplate = global::RememberIt.Properties.Resources.Reminder;
        private static byte[] HistoryTemplate = global::RememberIt.Properties.Resources.History;
        private static byte[] GroupTemplate = global::RememberIt.Properties.Resources.GroupTemplate;
        private static byte[] ButtonTemplate = global::RememberIt.Properties.Resources.ButtonTemplate;
        private static byte[] RememberItTemplate = global::RememberIt.Properties.Resources.RememberIt;
        
        public static void Build(string CardPath, string DocName)
        {
            Card c = new Card(CardPath);
            string[] file_names = null; // c.GetQuestionFilesNames();
            byte[] file_bytes = null;

            if (DocName == "Q")
            {
                file_bytes = QuestionTemplate;
                file_names = c.GetQuestionFilesNames();
            }
            else if (DocName == "A")
            {
                file_bytes = AnswerTemplate;
                file_names = c.GetAnswerFilesNames();
            }
            else if (DocName == "R")
            {
                file_bytes = ReminderTemplate;
                file_names = c.GetReminderFileNames();
            }
            else if (DocName == "H")
            {
                try
                {
                    file_bytes = HistoryTemplate;
                    string[] history_lines = File.ReadAllLines(CardPath + "/history.txt");
                    string history = "";
                    foreach (string line in history_lines)
                    {
                        history += line + "<br />";
                    }

                    string history_doc = ASCIIEncoding.ASCII.GetString(file_bytes);
                    history_doc = history_doc.Replace("$home", GetButton("Home", 100, 30, "../../RememberIt.html"));
                    history_doc = history_doc.Replace("$group", GetButton(c.GetGroupName(), 100, 30, "../Group.html"));
                    history_doc = history_doc.Replace("$card_content", history);
                    file_bytes = ASCIIEncoding.ASCII.GetBytes(history_doc);

                    if (file_bytes.Length <= 3) return;
                    file_bytes[0] = 0xEF;
                    file_bytes[1] = 0xBB;
                    file_bytes[2] = 0xBF;
                    File.WriteAllBytes(CardPath + "/H.html", file_bytes);
                }
                catch
                {
                    //
                }

                return;
            }


            if (file_names == null) return;


            string doc_str = ASCIIEncoding.ASCII.GetString(file_bytes);

            string content_tags = "";
            string str = "";
            foreach (string file in file_names)
            {
                FileInfo fi = new FileInfo(file);
                switch (fi.Extension.ToLower())
                {
                    case ".txt":
                        str = File.ReadAllText(file);
                        content_tags += HtmlDocBuilder.GetParagraphTag(str);
                        break;
                    case ".mp3":
                    case ".wav":
                        content_tags += HtmlDocBuilder.GetAudioTag(DocName + "/" + fi.Name, "audio/mpeg");
                        break;
                    case ".jpg":
                    case ".png":
                    case ".bmp":
                        content_tags += HtmlDocBuilder.GetImageTag(DocName + "/" + fi.Name, "Image", 100);
                        break;
                }
            }
            doc_str = doc_str.Replace("$home", GetButton("Home", 100, 30, "../../RememberIt.html"));
            doc_str = doc_str.Replace("$group", GetButton(c.GetGroupName(), 100, 30, "../Group.html"));
            doc_str = doc_str.Replace("$card_content", content_tags);
            file_bytes = ASCIIEncoding.ASCII.GetBytes(doc_str);

            if (file_bytes.Length <= 3) return;
            file_bytes[0] = 0xEF;
            file_bytes[1] = 0xBB;
            file_bytes[2] = 0xBF;
            File.WriteAllBytes(CardPath + "/"+DocName + ".html", file_bytes);
        }


        internal static void BuildHtmlDocs(FrmBuildHtmlDocument.ProcessInfo ProcessInfoHandler)
        {
            string[] all_cards_paths = Globals.GetPathOfEachCard();
            foreach (string card_path in all_cards_paths)
            {
                try
                {
                    BuildHtmlDocs(card_path, ProcessInfoHandler);
                }
                catch
                {
                    continue;
                }
            }

            string[] all_groups = Globals.GetPathOfEachGroup();
            if (all_groups != null)
            {

                foreach (string group in all_groups)
                {
                    try
                    {
                        BuildHtmlGroupInfo(group, ProcessInfoHandler);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            BuildRootDocument(ProcessInfoHandler);

        }

        private static void BuildRootDocument(FrmBuildHtmlDocument.ProcessInfo ProcessInfoHandler)
        {
            if (ProcessInfoHandler != null) ProcessInfoHandler("RememberIt.html");

            string root_path = Globals.GetCardsPath();
            string[] group_paths = Globals.GetPathOfEachGroup();
            if (group_paths == null) return;

            byte[] file_bytes = RememberItTemplate;
            string card_group_doc = ASCIIEncoding.ASCII.GetString(file_bytes);
            card_group_doc = card_group_doc.Replace("$groups", Globals.GetButtonsForGroups());

            file_bytes = ASCIIEncoding.ASCII.GetBytes(card_group_doc);

            if (file_bytes.Length <= 3) return;
            file_bytes[0] = 0xEF;
            file_bytes[1] = 0xBB;
            file_bytes[2] = 0xBF;
            string html_file_name = root_path + "/RememberIt.html";
            File.WriteAllBytes(html_file_name, file_bytes);

            Process.Start(html_file_name);
        }


        private static string GetButton(string ButtonText, int Width, int Height, string Href)
        {
            byte[] bytes = ButtonTemplate;
            if (bytes == null) return "";
            if (bytes.Length <= 3) return "";
            string btn = ASCIIEncoding.ASCII.GetString(bytes, 3, bytes.Length - 3);
            btn = btn.Replace("$width", Width.ToString());
            btn = btn.Replace("$height", Height.ToString());
            btn = btn.Replace("$href", Href);
            btn = btn.Replace("$button_text", ButtonText);
            return btn;
        }


        private static void BuildHtmlGroupInfo(string group, FrmBuildHtmlDocument.ProcessInfo ProcessInfoHandler)
        {
            if (ProcessInfoHandler != null) ProcessInfoHandler(group);

            string[] card_paths = Globals.GetPathOfEachCard(group);
            if (card_paths == null) return;

            byte[] file_bytes = GroupTemplate;
            string card_group_doc = ASCIIEncoding.ASCII.GetString(file_bytes);
            card_group_doc = card_group_doc.Replace("$home", GetButton("Home",100,30,"../RememberIt.html"));
            card_group_doc = card_group_doc.Replace("$group_name", Globals.GetLastPartOfDir(group));
            card_group_doc = card_group_doc.Replace("$missed_cards", GetButtonsForMissedCards(group));
            card_group_doc = card_group_doc.Replace("$pending_cards", GetButtonsForPendingCards(group));
            card_group_doc = card_group_doc.Replace("$stage_0", GetButtonsForStage(group, 0));
            card_group_doc = card_group_doc.Replace("$stage_1", GetButtonsForStage(group, 1));
            card_group_doc = card_group_doc.Replace("$stage_2", GetButtonsForStage(group, 2));
            card_group_doc = card_group_doc.Replace("$stage_3", GetButtonsForStage(group, 3));
            card_group_doc = card_group_doc.Replace("$stage_4", GetButtonsForStage(group, 4));
            card_group_doc = card_group_doc.Replace("$stage_5", GetButtonsForStage(group, 5));
            file_bytes = ASCIIEncoding.ASCII.GetBytes(card_group_doc);

            if (file_bytes.Length <= 3) return;
            file_bytes[0] = 0xEF;
            file_bytes[1] = 0xBB;
            file_bytes[2] = 0xBF;
            File.WriteAllBytes(group + "/Group.html", file_bytes);


            //foreach (string cp in card_paths)
            //{
            //    try
            //    {
                    
            //    }
            //    catch
            //    {
            //        continue;
            //    }
            //}

        }
        private static string GetButtonsForMissedCards(string CardGroup)
        {
            string buttons = "";
            string[] missed_cards = Globals.GetPathOfEachMissedCard(CardGroup);
            if (missed_cards == null) return "";
            var cards = Globals.GetListOfCards(missed_cards);
            foreach (Card card in cards)
            {
                string btn_text = Globals.GetLastPartOfDir(card.CardAbsPath);
                string href = string.Format("{0}/Q.html", btn_text);
                buttons += GetButton(btn_text, 200, 30, href);
                buttons += "<br />";
                buttons += "\r\n";
            }
            return buttons;
        }

        private static string GetButtonsForPendingCards(string CardGroup)
        {
            string buttons = "";
            string[] pending_cards = Globals.GetPathOfEachPendingCard(CardGroup);
            if (pending_cards == null) return "";
            var cards = Globals.GetListOfCards(pending_cards);
            foreach (Card card in cards)
            {
                string btn_text = Globals.GetLastPartOfDir(card.CardAbsPath);
                string href = string.Format("{0}/Q.html", btn_text);
                buttons += GetButton(btn_text, 200, 30, href);
                buttons += "<br />";
                buttons += "\r\n";
            }
            return buttons;
        }

        
        private static string GetButtonsForGroups()
        {
            string buttons = "";
            string[] groups = Globals.GetPathOfEachGroup();
            if (groups == null) return "";
            foreach (string group in groups)
            {
                string btn_text = Globals.GetLastPartOfDir(group);
                string href = string.Format("{0}/Group.html", btn_text);
                buttons += GetButton(btn_text, 200, 30, href);
                buttons += "<br />";
                buttons += "\r\n";
            }
            return buttons;
        }
        
        
        private static string GetButtonsForStage(string CardGroup, int Stage)
        {
            string buttons = "";
            string[] pending_cards = Globals.GetPathOfEachCard(CardGroup, Stage);
            if (pending_cards == null) return "";
            var cards = Globals.GetListOfCards(pending_cards);
            foreach (Card card in cards)
            {
                string btn_text = Globals.GetLastPartOfDir(card.CardAbsPath);
                string href = string.Format("{0}/Q.html", btn_text);
                buttons += GetButton(btn_text, 200, 30, href);
                buttons += "<br />";
                buttons += "\r\n";
            }
            return buttons;
        }

        internal static string GetDirectoryOfFile(string RemoteDestinationFileName)
        {
            if (RemoteDestinationFileName == null) return "";
            if (RemoteDestinationFileName == "") return "";
            RemoteDestinationFileName.Replace("\\", "/");

            string[] parts = RemoteDestinationFileName.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0) return "";

            string path = "";

            for (int i = 0; i < parts.Length-1; i++)
            {
                path += parts[i]+"/";
            }
            return path;
        }

        internal static string[] GetFiles(string Dir)
        {
            List<string> files = new List<string>();
            if (Directory.Exists(Dir) == false) return null;

            files.AddRange(Directory.GetFiles(Dir));

            string[] sub_folders = Directory.GetDirectories(Dir);
            foreach (string dir in sub_folders)
            {
                files.AddRange(Directory.GetFiles(dir));
            }
            return files.ToArray();
        }
        
        
        internal static string[] GetAllFiles(string Dir)
        {
            List<string> files = new List<string>();
            if (Directory.Exists(Dir) == false) return null;
            string absolute_dir = Dir;
            
            Stack<string> stack = new Stack<string>();
            stack.Push(absolute_dir);

            string dir = "";
            while (stack.Count > 0)
            {
                 dir = stack.Pop();
                 files.AddRange(Directory.GetFiles(dir));
                 string[] sub_dirs = Directory.GetDirectories(dir);
                 foreach (string sub in sub_dirs)
                 {
                     stack.Push(sub);
                 }
            }
            return files.ToArray();
        }

        public static byte HexToByte(string HexValue)
        {
            try
            {
                byte b = byte.Parse(HexValue.ToUpper(), System.Globalization.NumberStyles.HexNumber);
                return b;
            }
            catch
            {
                throw new Exception("Invalid hex byte");
            }
        }

        public static string ByteToHex(byte AByte)
        {
             return string.Format("{0:X}", AByte);
        }

        internal static bool DeleteDirectory(string ADir)
        {
            try
            {
                if (Directory.Exists(ADir) == false) return true;
                Directory.Delete(ADir, true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        internal static string GetGroup(string RemoteFullFileName)
        {
            try
            {
                string group = GetRelPath(RemoteFullFileName);
                group = group.Replace("/Cards/", "");
                int indx = group.IndexOf('/');
                group = group.Substring(0, indx);
                return group;
            }
            catch
            {
                return "TempGroup";
            }
        }

        internal static string GetRelPath(string CardPath)
        {
            string path = CardPath.Trim();
            path = path.Replace('\\', '/');
            int indx = path.IndexOf("/Cards/", StringComparison.InvariantCultureIgnoreCase);
            if (indx == -1) return CardPath;
            path = path.Substring(indx);
            return path;
        }
    }
}