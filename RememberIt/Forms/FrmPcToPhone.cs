using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RememberIt
{
    public partial class FrmPcToPhone : Form
    {
        RemoteTcpCom com = new RemoteTcpCom();
        List<string> LocalFiles = new List<string>();
        List<string> RemoteFiles = new List<string>();
        List<string> FetchList = new List<string>();
        int FetchListIndex = 0;
        string IncomingTempFolder = "";
        int file_indx = 0;


        public FrmPcToPhone()
        {
            InitializeComponent();

            cmbPcCardGroup.Items.Clear();
            cmbPcCardGroup.Items.Add("[ALL]");
            cmbPcCardGroup.SelectedIndex = 0;

            cmbPhoneCardGroup.Items.Clear();
            cmbPhoneCardGroup.Items.Add("[ALL]");
            cmbPhoneCardGroup.SelectedIndex = 0;

        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            pnlConnect.Left = pnlConnect.Parent.Width / 2 - pnlConnect.Width / 2;
            pnlConnect.Top = pnlConnect.Parent.Height / 2 - pnlConnect.Height / 2;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            string id_str = txtPhoneID.Text.Trim();
            string ip_address = GetIpAddress(id_str);
            if (ip_address == null)
            {
                MessageBox.Show("Invalid ID");
                dgvCards.Rows.Clear();
                return;
            }

            com.RemoteIpAddress = ip_address;
            com.PortNoCommand = 50400;
            com.PortNoEvent = 50401;
            com.PortNoDataOutgoing = 50402;
            com.PortNoDataIncomming = 50403;
            com.Owner = this;
            if (com.Initialized == false)
            {
                com.OnLog += new RemoteTcpCom.DelegateLog(com_OnLog);
                com.OnFileSentCompeleted += new RemoteTcpCom.DelegateFileSentCompeleted(com_OnFileSentCompeleted);
                com.OnFileReceivedCompeleted += new RemoteTcpCom.DelegateFileReceivedCompleted(com_OnFileReceivedCompeleted);
            }
            com.Init();

            try
            {
                lblConnectionState.Text = "Connecting...";
                Application.DoEvents();
                com.Connect();
                lblConnectionState.Text = "Connected.";
                lblConnectionState.ForeColor = Color.DarkGreen;

                rtxtLog.AppendText("Connected.\r\n");
                rtxtLog.AppendText("-===============================-\r\n");
            }
            catch (Exception ex)
            {
                lblConnectionState.Text = "Not Connected";
                lblConnectionState.ForeColor = Color.Crimson;
                MessageBox.Show(ex.Message);
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
                pbProgress.Value = 0;
                return;
            }

            lblCount.Text = string.Format("{0} of {1}", file_indx + 1, LocalFiles.Count);
            pbProgress.Maximum = LocalFiles.Count;
            pbProgress.Value = file_indx + 1;

            //txtLog.AppendText(string.Format("Copy: '{0}' to '{1}'\r\n", LocalFiles[file_indx], RemoteFiles[file_indx]));
            com.FileCopyTo(LocalFiles[file_indx], RemoteFiles[file_indx]);
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
                lblCount.Text = string.Format("{0} of {1}", FetchListIndex + 1, FetchList.Count);
                pbProgress.Maximum = FetchList.Count;
                pbProgress.Value = FetchListIndex + 1;

                FetchListIndex++;

                if (FetchListIndex >= FetchList.Count)
                {
                    FetchListIndex = 0;
                    lblCount.Text = "Done.";
                    pbProgress.Value = 0;
                }
                else
                {
                    com.FetchFile(FetchList[FetchListIndex]);
                }
            }
        }


        void com_OnLog(string Log)
        {
            rtxtLog.AppendText(Log + "\r\n");
        }


        private string GetIpAddress(string id_str)
        {
            string[] parts = id_str.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 4) return "";
            byte[] bytes = new byte[4];
            try
            {
                bytes[0] = Globals.HexToByte(parts[0]);
                bytes[1] = Globals.HexToByte(parts[1]);
                bytes[2] = Globals.HexToByte(parts[2]);
                bytes[3] = Globals.HexToByte(parts[3]);
                return string.Format("{0}.{1}.{2}.{3}", bytes[0], bytes[1], bytes[2], bytes[3]);

            }
            catch
            {
                return null;
            }
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                com.Disconnect();
                
                lblConnectionState.ForeColor = Color.Crimson;
                lblConnectionState.Text = "Not Connected";

            }
            catch(Exception ex)
            {
                MessageBox.Show("Press Leave button on your device");
            }
        }

        private void FrmPcToPhone_Load(object sender, EventArgs e)
        {
            //string def_group_path = Globals.GetDefaultGroupPath();
            //if (Directory.Exists(def_group_path))
            //{
            //    txtWorkingDir.Text = def_group_path;
            //}
            //else
            //{
            //    txtWorkingDir.Text = Application.StartupPath;
            //}
        }

        private void btnInitialize_Click(object sender, EventArgs e)
        {
            FetchPhoneCardGroups();
            GetPcCardGroups();
        }

        private void GetPcCardGroups()
        {
            string[] groups = Globals.GetPathOfEachGroup();
            cmbPcCardGroup.Items.Clear();
            cmbPcCardGroup.Items.Add("[ALL]");
            foreach (string g in groups)
            {
                cmbPcCardGroup.Items.Add(Globals.GetLastPartOfDir(g));
            }
            cmbPcCardGroup.SelectedIndex = 0;
        }

        private void FetchPhoneCardGroups()
        {
            string[] groups = com.GetListOfCardGroups();
            cmbPhoneCardGroup.Items.Clear();
            cmbPhoneCardGroup.Items.Add("[ALL]");
            foreach (string g in groups)
            {
                cmbPhoneCardGroup.Items.Add(Globals.GetLastPartOfDir(g));
            }
            cmbPhoneCardGroup.SelectedIndex = 0;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dgvCards.Rows.Clear();
            string[] Phone_cards = com.GetListOfCards(cmbPhoneCardGroup.SelectedItem.ToString());
            string[] Pc_cards =null;
            int row = 0;

            foreach (string phone_side_card in Phone_cards)
            {
                int indx = dgvCards.Rows.Add(1);
                dgvCards.Rows[indx].Cells[clmnRow.Index].Value = ++row;
                dgvCards.Rows[indx].Cells[clmnSelectPcCard.Index].Value = false;
                dgvCards.Rows[indx].Cells[clmnCard.Index].Value = "";
                dgvCards.Rows[indx].Cells[clmnAction.Index].Value = "";
                dgvCards.Rows[indx].Cells[clmnSelectPhCard.Index].Value = false;
                dgvCards.Rows[indx].Cells[clmnCardPhone.Index].Value = Globals.GetLastPartOfDir(phone_side_card);
                dgvCards.Rows[indx].Cells[clmnCardPathPc.Index].Value = "";
                dgvCards.Rows[indx].Cells[clmnCardPathPhone.Index].Value = phone_side_card;
                dgvCards.Rows[indx].Cells[clmnCroup.Index].Value = "";
                dgvCards.Rows[indx].Cells[clmnManifestDataPc.Index].Value = "";
                dgvCards.Rows[indx].Cells[clmnManifestDataPhone.Index].Value = "";

            }


            if (cmbPcCardGroup.SelectedIndex == 0)
            {
                List<string> all_cards = new List<string>();
                string[] groups = Globals.GetPathOfEachGroup();
                foreach (string group in groups)
                {
                    string[] cards = Globals.GetPathOfEachCard(group);
                    foreach (string card in cards)
                    {
                        all_cards.Add(card);
                    }
                }
                Pc_cards = all_cards.ToArray();
            }
            else
            {
                string group = group = Globals.GetCardsPath() + "\\" + cmbPcCardGroup.SelectedItem.ToString();
                Pc_cards = Globals.GetPathOfEachCard(group);
            }

            if (Phone_cards != null)
            {

                if (Pc_cards!=null) foreach (string card_dir_path in Pc_cards)
                {
                    string card_name = Globals.GetLastPartOfDir(card_dir_path);
                    int idx = Lookup(card_name, dgvCards);
                    if (idx == -1)
                    {
                        int indx = dgvCards.Rows.Add(1);
                        dgvCards.Rows[indx].Cells[clmnRow.Index].Value = ++row;
                        dgvCards.Rows[indx].Cells[clmnSelectPcCard.Index].Value = false;
                        dgvCards.Rows[indx].Cells[clmnCard.Index].Value = card_name;
                        dgvCards.Rows[indx].Cells[clmnAction.Index].Value = "";
                        dgvCards.Rows[indx].Cells[clmnSelectPhCard.Index].Value = false;
                        dgvCards.Rows[indx].Cells[clmnCardPhone.Index].Value = "";
                        dgvCards.Rows[indx].Cells[clmnCardPathPc.Index].Value = card_dir_path;
                        dgvCards.Rows[indx].Cells[clmnCardPathPhone.Index].Value = "";
                        dgvCards.Rows[indx].Cells[clmnCroup.Index].Value = "";
                        dgvCards.Rows[indx].Cells[clmnManifestDataPc.Index].Value = "";
                        dgvCards.Rows[indx].Cells[clmnManifestDataPhone.Index].Value = "";

                    }
                    else
                    {
                        dgvCards.Rows[idx].Cells[clmnCard.Index].Value = card_name;
                        dgvCards.Rows[idx].Cells[clmnCardPathPc.Index].Value = card_dir_path;
                    }
                }
            }


            // Extract manifest data
            for (int i = 0; i < dgvCards.Rows.Count; i++)
            {
                
                // Extract manifest data of phone-side cards
                string card_path_phone = (string)dgvCards.Rows[i].Cells[clmnCardPathPhone.Index].Value;
                string manifest_phone = "";
                if (card_path_phone != "")
                {
                    manifest_phone = com.GetManifestData(card_path_phone);
                    dgvCards.Rows[i].Cells[clmnManifestDataPhone.Index].Value = manifest_phone;
                }
                

                // Extract manifest data of pc-side cards
                string card_path_pc = (string)dgvCards.Rows[i].Cells[clmnCardPathPc.Index].Value;
                string manifest_pc = "";
                if (card_path_pc != "")
                {
                    manifest_pc = File.ReadAllText(card_path_pc+"/manifest.man");
                    KeyValPair kvp = new KeyValPair('\n','=');
                    kvp.Fill(manifest_pc);
                    manifest_pc = kvp.GetString(';',':');
                    dgvCards.Rows[i].Cells[clmnManifestDataPc.Index].Value = manifest_pc;
                }
            }


            // Define proper action for transfering cards to/from pc from/to phone.
            for (int i = 0; i < dgvCards.Rows.Count; i++)
            {
                string manifest_phone = (string)dgvCards.Rows[i].Cells[clmnManifestDataPhone.Index].Value;
                string manifest_pc = (string)dgvCards.Rows[i].Cells[clmnManifestDataPc.Index].Value;

                if (manifest_phone == "" && manifest_pc == "") continue;
                else if (manifest_phone == "") dgvCards.Rows[i].Cells[clmnAction.Index].Value = "--->";
                else if (manifest_pc == "") dgvCards.Rows[i].Cells[clmnAction.Index].Value = "<---";
                else
                {
                    KeyValPair kvp_phone = new KeyValPair(';', ':');
                    kvp_phone.Fill(manifest_phone);

                    KeyValPair kvp_pc = new KeyValPair(';', ':');
                    kvp_pc.Fill(manifest_pc);


                    double ph_last_modified = 0; 
                    double pc_last_modified = 0;
                    try
                    {
                       ph_last_modified = Double.Parse(kvp_phone.GetVal("last_modified"));
                       pc_last_modified = Double.Parse(kvp_pc.GetVal("last_modified"));
                    }
                    catch
                    {
                        dgvCards.Rows[i].Cells[clmnAction.Index].Value = "!";
                        continue;
                    }

                    if (pc_last_modified == ph_last_modified) dgvCards.Rows[i].Cells[clmnAction.Index].Value = "<--->";
                    else if (pc_last_modified > ph_last_modified) dgvCards.Rows[i].Cells[clmnAction.Index].Value = "--->";
                    else dgvCards.Rows[i].Cells[clmnAction.Index].Value = "<---";
                }

            }

            if (chbxShowDiffs.Checked) ShowOnlyDiffs();
        }

        private int Lookup(string CardName, DataGridView dgvDataGrid)
        {
            for (int i = 0; i < dgvDataGrid.Rows.Count; i++)
            {
                if ((string)(dgvDataGrid.Rows[i].Cells[clmnCardPhone.Index].Value) == CardName.Trim()) return i;
            }
            return -1;
        }

        private void btnSendToPhone_Click(object sender, EventArgs e)
        {
            if (dgvCards.SelectedRows.Count == 0) return;

            LocalFiles.Clear();
            RemoteFiles.Clear();

            List<string> pc_side_cards = new List<string>();
            for (int i = 0; i < dgvCards.SelectedRows.Count; i++)
            {
                pc_side_cards.Add((string)dgvCards.SelectedRows[i].Cells[clmnCardPathPc.Index].Value);
            }

            foreach (string card in pc_side_cards)
            {
                string[] all_files = Globals.GetFiles(card);
                if (all_files == null) continue;
                string root = Globals.GetExecutablePath();
                LocalFiles.AddRange(all_files);
                
                string remote_file = "";
                for (int i = 0; i < all_files.Length; i++)
                {
                    remote_file = all_files[i].Replace(root, "/");
                    remote_file = remote_file.Replace("\\", "/");
                    RemoteFiles.Add(remote_file);
                }
            }

            file_indx = 0;
            lblCount.Text = string.Format("{0} of {1}", file_indx + 1, LocalFiles.Count);
            pbProgress.Maximum = LocalFiles.Count;
            pbProgress.Value = file_indx + 1;
            try
            {
                //txtLog.AppendText(string.Format("Copy: '{0}' to '{1}'\r\n", LocalFiles[file_indx], RemoteFiles[file_indx]));
                com.FileCopyTo(LocalFiles[file_indx], RemoteFiles[file_indx]);
            }
            catch (Exception ex)
            {
                // txtLog.AppendText(ex.Message + "\r\n");
            }
        }

        private void btnRemoveFromPhone_Click(object sender, EventArgs e)
        {
            if (dgvCards.SelectedRows== null) return;
            if (dgvCards.SelectedRows.Count == 0) return;

            string card_path = "";
            for (int i = 0; i < dgvCards.SelectedRows.Count; i++)
            {
                card_path = (string)dgvCards.SelectedRows[i].Cells[clmnCardPathPhone.Index].Value;
                if (card_path == null) continue;
                com.RemoveCard(card_path);
            }

        }

        private void btnFetchFromPhone_Click(object sender, EventArgs e)
        {
            if (dgvCards.SelectedRows == null) return;
            if (dgvCards.SelectedRows.Count == 0) return;
            this.FetchList.Clear();
            this.FetchListIndex = 0;

            List<string> phone_card_paths = new List<string>();
            List<string> pc_card_paths = new List<string>();

            string phone_card_path = "";
            string pc_card_path = "";
            for (int i = 0; i < dgvCards.SelectedRows.Count; i++)
            {
                phone_card_path = (string)dgvCards.SelectedRows[i].Cells[clmnCardPathPhone.Index].Value;
                if (phone_card_path != null) phone_card_paths.Add(phone_card_path);

                pc_card_path = (string)dgvCards.SelectedRows[i].Cells[clmnCardPathPc.Index].Value;
                if (pc_card_path != null) pc_card_paths.Add(pc_card_path);


            }

            foreach (string card in pc_card_paths)
            {
                if (Globals.DeleteDirectory(card) == false)
                {
                    rtxtLog.AppendText(string.Format("Unable to remove directory '{0}'\r\n", card));
                }
            }


            foreach (string card in phone_card_paths)
            {
                string rel_path = Globals.GetRelPath(card);
                string[] files = com.GetListOfAllFiles(rel_path);
                this.FetchList.AddRange(files);
            }
            if (this.FetchList.Count == 0) return;
            com.FetchFile(this.FetchList[0]);
        }

        private void dgvCards_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // PC Side cards only:
            if (e.ColumnIndex != clmnCard.Index) return;

            string card_root = (string)dgvCards.Rows[e.RowIndex].Cells[clmnCardPathPc.Index].Value;

            WndCardBrowser w = new WndCardBrowser();
            w.CardRootFolder = card_root;
            w.Init();
            w.ShowDialog();
        }

        private void btnClonePcToPhone_Click(object sender, EventArgs e)
        {
            string[] pc_side_groups = Globals.GetPathOfEachGroup();
            string group = "";
            foreach (string g in pc_side_groups)
            {
                group = Globals.GetLastPartOfDir(g);
                com.DirCreate("Cards/"+group);
            }

        }



        private void ShowAll()
        {
            for (int i = 0; i < dgvCards.Rows.Count; i++)
            {
                dgvCards.Rows[i].Visible = true;
            }
        }

        private void ShowOnlyDiffs()
        {
            for (int i = 0; i < dgvCards.Rows.Count; i++)
            {
                string action = dgvCards.Rows[i].Cells[clmnAction.Index].Value.ToString();
                if (action == "<--->")
                {
                    dgvCards.Rows[i].Visible = false;
                }
            }
        }

        private void chbxShowDiffs_CheckedChanged(object sender, EventArgs e)
        {
            if (chbxShowDiffs.Checked)
            {
                ShowOnlyDiffs();
            }
            else
            {
                ShowAll();
            }
        }

        private void FrmPcToPhone_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (com == null) return;
            com.Disconnect();
        }

        private void txtPhoneID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnConnect_Click(null, null);
            }
        }
    }
}