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
    public partial class FrmChangeRoot : Form
    {
        public FrmChangeRoot()
        {
            InitializeComponent();
            txtRootPath.AllowDrop = true;
            txtRootPath.DragDrop += new DragEventHandler(txtRootPath_DragDrop);
            txtRootPath.DragOver += new DragEventHandler(txtRootPath_DragOver);
        }

        void txtRootPath_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        void txtRootPath_DragDrop(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
            
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files == null) return;
            if (files.Length != 1) return;
            string dir = files[0];
            if (Directory.Exists(dir)==false) return;
            txtRootPath.Text = dir;
            
        }

        public string Root
        {
            get
            {
                return txtRootPath.Text.Trim();
            }
            set
            {
                txtRootPath.Text = value.Trim();
            }
        }


        private bool is_canceled = true;
        public bool IsCanceled
        {
            get
            {
                return this.is_canceled;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.is_canceled = false;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.is_canceled = true;
            this.Close();
        }

        private void FrmChangeRoot_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.is_canceled == true)
            {
                //
            }
            else
            {
                if (Directory.Exists(txtRootPath.Text.Trim()) == false)
                {
                    MessageBox.Show("Directory does not exist!");
                    e.Cancel = true;
                }
            }
        }

        private void btnBrows_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.SelectedPath = txtRootPath.Text.Trim();
            DialogResult dr =  fb.ShowDialog();
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                txtRootPath.Text = fb.SelectedPath;
            }
        }
    }
}
