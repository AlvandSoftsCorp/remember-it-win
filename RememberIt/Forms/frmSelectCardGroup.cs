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
    public partial class frmSelectCardGroup : Form
    {
        public frmSelectCardGroup()
        {
            InitializeComponent();

            Init();
        }

        string[] GroupPaths = null;
        private void Init()
        {
            this.GroupPaths = Globals.GetPathOfEachGroup();
            if (GroupPaths == null) return;
            if (GroupPaths.Length == 0) return;
            cmbGroups.Items.Clear();

            for (int i = 0; i < GroupPaths.Length; i++)
            {
                string grp_str = Globals.GetLastPartOfDir(GroupPaths[i]);
                cmbGroups.Items.Add(grp_str);
            }
            cmbGroups.SelectedIndex = 0;
        }

        private void chbxCreateNewGroup_CheckedChanged(object sender, EventArgs e)
        {
            gbxCreateNewCardGroup.Enabled = chbxCreateNewGroup.Checked;
        }

        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            if (txtNewGroupName.Text.Trim() == "")
            {
                MessageBox.Show("Invalid Group Name.");
                return;
            }

            string cards_path = Globals.GetCardsPath();
            string grp_path = cards_path +  txtNewGroupName.Text.Trim();

            try
            {
                DirectoryInfo di = Directory.CreateDirectory(grp_path);
                if (di.Exists)
                {
                    MessageBox.Show("Group Created!");
                    this.Init();
                    string grp_name = Globals.GetLastPartOfDir(di.FullName);
                    for (int i = 0; i < cmbGroups.Items.Count; i++)
                    {
                        if (cmbGroups.Items[i].ToString() == grp_name)
                        {
                            cmbGroups.SelectedIndex = i;
                            break;
                        }
                    }
                }
                else MessageBox.Show("Unable to create group");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Invalid Group Name.\r\n"+ex.Message);
            }

        }


        bool is_canceled = true;
        public bool IsCanceled
        {
            get
            {
                return is_canceled;
            }
        }

        public string SelectedGroup
        {
            get
            {
                return cmbGroups.Text;
            }
        }
        public string SelectedGroupPath
        {
            get
            {
                try
                {
                    return this.GroupPaths[cmbGroups.SelectedIndex];
                }
                catch
                {
                    return null;
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            is_canceled = false;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            is_canceled = true;
            Close();
        }
    }
}
