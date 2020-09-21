using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace RememberIt
{
    public class Card
    {
        public double NextVisitGdp = 0;
        public int Stage = 0;
        public int CanEdit = 0;
        public double LastModifiedGdp = 0;
        public uint Crc = 0;
        public string HistoryData = "";
        public string HistoryAbsFileName = "";
        public string CardAbsPath = "";
        public string ManifestAbsFileName = "";
        public string ManifestData = "";
        public string QuestionAbsPath = "";
        public string AnswerAbsPath = "";
        public string ReminderAbsPath = "";
        public string CardGroupAbsPath = "";

        public Card()
        {

        }

        public Card(string CardAbsPath)
        {
            this.CardAbsPath = CardAbsPath;
            this.CardGroupAbsPath = GetGroupPath();
            this.ManifestAbsFileName = CardAbsPath + "\\manifest.man";
            this.HistoryAbsFileName = CardAbsPath + "\\history.txt";
            this.QuestionAbsPath = CardAbsPath + "\\Q\\";
            this.AnswerAbsPath = CardAbsPath + "\\A\\";
            this.ReminderAbsPath = CardAbsPath + "\\R\\";

            this.NextVisitGdp = GetNextVisit();
            this.Stage = GetStage();
            this.HistoryData = GetHistoryData();
            this.ManifestData = GetMainfestData();
        }

        public bool UpdateManifestFile()
        {
            try
            {
                KeyValPair kvp = new KeyValPair(';', ':');
                kvp.Add("next_visit", this.NextVisitGdp.ToString());
                kvp.Add("stage", this.Stage.ToString());
                kvp.Add("can_edit", this.CanEdit.ToString());
                kvp.Add("last_modified", this.LastModifiedGdp.ToString());
                kvp.Add("crc", this.Crc.ToString());
                return kvp.Save(this.ManifestAbsFileName);
            }
            catch
            {
                return false;
            }
        }

        private string GetHistoryData()
        {
            try
            {
                return  File.ReadAllText(this.HistoryAbsFileName);
            }
            catch
            {
                return "";
            }
        }


        private string GetMainfestData()
        {
            try
            {
                return File.ReadAllText(this.ManifestAbsFileName);
            }
            catch
            {
                return "";
            }
        }


        private int GetStage()
        {
            try
            {
                KeyValPair manifest = new KeyValPair(';', ':');
                manifest.Load(this.ManifestAbsFileName);
                string stg = manifest.GetVal("stage");
                int stage = int.Parse(stg);
                return stage;
            }
            catch
            {
                return 0;
            }
        }


        private double GetNextVisit()
        {
            try
            {
                KeyValPair manifest = new KeyValPair(';', ':');
                manifest.Load(this.ManifestAbsFileName);
                string nv = manifest.GetVal("next_visit");
                double next_visit = double.Parse(nv);
                return next_visit;
            }
            catch
            {
                return 0;
            }
        }

        public string[] GetQuestionFilesNames()
        {
            if (Directory.Exists(this.QuestionAbsPath) == false) return null;
            try
            {
                return Directory.GetFiles(this.QuestionAbsPath);
            }
            catch
            {
                return null;
            }
        }

        public string[] GetAnswerFilesNames()
        {
            if (Directory.Exists(this.AnswerAbsPath) == false) return null;
            try
            {
                return Directory.GetFiles(this.AnswerAbsPath);
            }
            catch
            {
                return null;
            }
        }

        public string[] GetReminderFileNames()
        {
            if (Directory.Exists(this.ReminderAbsPath) == false) return null;
            try
            {
                return Directory.GetFiles(this.ReminderAbsPath);
            }
            catch
            {
                return null;
            }
        }

        public string GetGroupPath()
        {
            string[] parts = this.CardAbsPath.Split(new char[]{'/','\\'});
            try
            {
                string group_path = "";
                for (int i = 0; i < parts.Length-1; i++)
                {
                    group_path += parts[i]+"\\";
                }
                return group_path;
            }
            catch (Exception)
            {

                return "";
            }
        }

        public string GetGroupName()
        {
            string gp = GetGroupPath();
            try
            {
                return Globals.GetLastPartOfDir(gp);
            }
            catch (Exception)
            {
                return "";
            }
        }

        internal static bool IsValidCardAbsPath(string CardAbsPath)
        {
            if (Directory.Exists(CardAbsPath) == false) return false;
            if (Directory.Exists(CardAbsPath + @"\Q") == false) return false;
            if (Directory.Exists(CardAbsPath + @"\A") == false) return false;
            if (Directory.Exists(CardAbsPath + @"\R") == false) return false;
            if (File.Exists(CardAbsPath + @"\manifest.man") == false) return false;
            if (File.Exists(CardAbsPath + @"\history.txt") == false) return false;
            if (Directory.GetFiles(CardAbsPath + @"\Q\").Length == 0) return false;
            if (Directory.GetFiles(CardAbsPath + @"\A\").Length == 0) return false;

            return true;
        }
    }
}
