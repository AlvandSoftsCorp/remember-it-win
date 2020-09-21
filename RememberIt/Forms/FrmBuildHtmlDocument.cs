using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RememberIt
{
    public partial class FrmBuildHtmlDocument : Form
    {
        public delegate void ProcessInfo(string ProcessInformation);
        public ProcessInfo ProcessInfoHandler = null;

        
        public FrmBuildHtmlDocument()
        {
            InitializeComponent();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            this.FormClosing += new FormClosingEventHandler(FrmBuildHtmlDocument_FormClosing);

            ProcessInfoHandler = new ProcessInfo(Logger);
        }


        void FrmBuildHtmlDocument_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (building_in_process == true)
            {
                MessageBox.Show("Building Documents is in proceess. Plase wait.");
                e.Cancel = true;
            }
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Building Documents finished.");
            building_in_process = false;
        }


        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            Globals.BuildHtmlDocs(ProcessInfoHandler);
        }

        BackgroundWorker bw = new BackgroundWorker();

        bool building_in_process = false;
        private void btnBuild_Click(object sender, EventArgs e)
        {
            if (building_in_process == true)
            {
                MessageBox.Show("Building HTML Documents is already in process now.");
                return;
            }

            txtInfo.Clear();
            building_in_process = true;
            bw.RunWorkerAsync();
        }

        public void Logger(string ProcessInformation)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ProcessInfo(Logger), new object[] { ProcessInformation });
            }
            else
            {
                txtInfo.AppendText(string.Format("[Card]: {0}\r\n", ProcessInformation));
                txtInfo.ScrollToCaret();
            }
        }
    }
}
