using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Net.Sockets;

namespace RememberIt
{
    public partial class FrmSendCardsToPhone : Form
    {
        RemoteTcpCom com = new RemoteTcpCom();
        List<string> LocalFiles = new List<string>();
        List<string> RemoteFiles = new List<string>();
        string[] FetchList = null;
        int FetchListIndex = 0;
        string IncomingTempFolder = "";

        public FrmSendCardsToPhone()
        {
            InitializeComponent();
        }



        private void btnConnectToPhone_Click(object sender, EventArgs e)
        {
            com.RemoteIpAddress = "192.168.1.179";
            com.PortNoCommand = 50400;
            com.PortNoEvent = 50401;
            com.PortNoDataOutgoing = 50402;
            com.PortNoDataIncomming = 50403;
            com.Owner = this;
            com.OnLog += new RemoteTcpCom.DelegateLog(com_OnLog);
            com.OnFileSentCompeleted += new RemoteTcpCom.DelegateFileSentCompeleted(com_OnFileSentCompeleted);
            com.OnFileReceivedCompeleted += new RemoteTcpCom.DelegateFileReceivedCompleted(com_OnFileReceivedCompeleted);
            com.Init();

            try
            {
                com.Connect();
                txtLog.AppendText("Connected.\r\n");
                txtLog.AppendText("-===============================-\r\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        delegate void OnFileReceivedCompeletedDelegate(string FileName, long FileLength);
        void com_OnFileReceivedCompeleted(string FileName, long FileLength)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new OnFileReceivedCompeletedDelegate(com_OnFileReceivedCompeleted), new object[] { FileName, FileLength });
            }
            else
            {
                FetchListIndex++;
                if (FetchListIndex >= FetchList.Length)
                {
                    FetchListIndex = 0;
                    MessageBox.Show("All Done");
                }
                else
                {
                    com.FetchFile(FetchList[FetchListIndex], IncomingTempFolder);
                }
            }
        }

        void com_OnFileSentCompeleted(string FileName, long FileLength)
        {
            file_indx++;
            //txtLog.AppendText("---------------------------------\r\n");
            Application.DoEvents();
            if (file_indx >= LocalFiles.Count)
            {
                lblCount.Text = "All Done!";
                return;
            }

            lblCount.Text = string.Format("{0} of {1}", file_indx + 1, LocalFiles.Count);

            //txtLog.AppendText(string.Format("Copy: '{0}' to '{1}'\r\n", LocalFiles[file_indx], RemoteFiles[file_indx]));
            com.FileCopyTo(LocalFiles[file_indx], RemoteFiles[file_indx]);
        }

        void com_OnLog(string Log)
        {
            txtLog.AppendText(Log + "\r\n");
        }


        int file_indx = 0;
        private void btnSend_Click(object sender, EventArgs e)
        {
            LocalFiles.Clear();
            RemoteFiles.Clear();

            string sel_card_folder = lbCards.SelectedItem.ToString();
            string[] all_files = Globals.GetFiles(sel_card_folder);
            string root = Globals.GetExecutablePath();

            LocalFiles.AddRange(all_files);

            string remote_file = "";
            for (int i = 0; i < all_files.Length; i++)
            {
                remote_file = all_files[i].Replace(root, "/");
                remote_file = remote_file.Replace("\\", "/");
                RemoteFiles.Add(remote_file);
            }


            file_indx = 0;
            lblCount.Text = string.Format("{0} of {1}", file_indx + 1, LocalFiles.Count);

            try
            {
                //txtLog.AppendText(string.Format("Copy: '{0}' to '{1}'\r\n", LocalFiles[file_indx], RemoteFiles[file_indx]));
                com.FileCopyTo(LocalFiles[file_indx], RemoteFiles[file_indx]);
            }
            catch (Exception ex)
            {
                txtLog.AppendText(ex.Message + "\r\n");
            }
        }

        private void FrmSendCardsToPhone_Load(object sender, EventArgs e)
        {
            string[] all_cards = Globals.GetPathOfEachCard();
            lbCards.Items.AddRange(all_cards);

        }

        private void btnEchoBack_Click(object sender, EventArgs e)
        {
            string resp = com.EchoBack("Hello");
            MessageBox.Show(resp);
        }

        private void btnGetListOfFiles_Click(object sender, EventArgs e)
        {
            this.FetchList = com.GetListOfAllFiles("/Cards/words/C13951114-235935-454/");

            txtLog.AppendText("\r\n");
            txtLog.AppendText("\r\n");
            txtLog.AppendText("-----------------------------------");
            txtLog.AppendText("\r\n");
            
            for (int i = 0; i < FetchList.Length; i++)
            {
                txtLog.AppendText(FetchList[i]+"\r\n");
            }
        }

        private void btnFetchFile_Click(object sender, EventArgs e)
        {
            if (this.FetchList == null) return;
            if (this.FetchList.Length == 0) return;
            string f = FetchList[0];

            IncomingTempFolder = Globals.GetExecutablePath() + "TMP" + Globals.GetDateTimeStamp() + "\\";
            IncomingTempFolder = Globals.CreateFolder(IncomingTempFolder);
            if (IncomingTempFolder == null)
            {
                MessageBox.Show("Unable to create temp folder.");
                return;
            }
            
            txtLog.AppendText("\r\n");
            txtLog.AppendText("---------------------------------");
            txtLog.AppendText("\r\n");
            
            com.FetchFile(f, IncomingTempFolder);
        }
    }
}