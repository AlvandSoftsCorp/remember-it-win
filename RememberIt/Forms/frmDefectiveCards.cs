using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace RememberIt
{
    public partial class frmDefectiveCards : Form
    {
        public frmDefectiveCards()
        {
            InitializeComponent();
            Diagnose();
        }

        public void Diagnose()
        {
            dgvDefections.Rows.Clear();
            List<string> defective_cards = Globals.GetListOfDefectiveCards();
            int row = 0;
            foreach (string cp in defective_cards)
            {
                row++;
                string card_name = Globals.GetLastPartOfDir(cp);
                int q_cnt = Globals.GetNumberOfFiles(cp + "\\Q");
                int a_cnt = Globals.GetNumberOfFiles(cp + "\\A");
                int r_cnt = Globals.GetNumberOfFiles(cp + "\\R");
                dgvDefections.Rows.Add(row, false, card_name, cp, q_cnt, a_cnt, r_cnt);
            }
        }

        private void dgvDefections_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string path = dgvDefections.Rows[e.RowIndex].Cells[clmnCardPath.Index].Value.ToString();
            Process.Start(path);
        }


        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvDefections.Rows.Count; i++)
            {
                dgvDefections.Rows[i].Cells[clmnSelect.Index].Value = true;
            }
        }

        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvDefections.Rows.Count; i++)
            {
                dgvDefections.Rows[i].Cells[clmnSelect.Index].Value = false;
            }

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Diagnose();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Delete selected cards?", "Warning", MessageBoxButtons.YesNoCancel);
            if (dr != System.Windows.Forms.DialogResult.Yes) return;
            
            for (int i = 0; i < dgvDefections.Rows.Count; i++)
            {
                if ((bool)dgvDefections.Rows[i].Cells[clmnSelect.Index].Value)
                {
                    string cp = dgvDefections.Rows[i].Cells[clmnCardPath.Index].Value.ToString();
                    Globals.RemoveCard(cp);                    
                }
            }
            Diagnose();
        }


        private void dgvDefections_KeyDown(object sender, KeyEventArgs e)
        {
            if (dgvDefections.Rows.Count == 0) return;
            if (dgvDefections.SelectedRows.Count == 0) return;


            if (e.KeyCode == Keys.Space)
            {
                bypass = true;
                bool chk = (bool)dgvDefections.SelectedRows[0].Cells[clmnSelect.Index].Value;
                dgvDefections.SelectedRows[0].Cells[clmnSelect.Index].Value = !chk;
            }
        }

        bool bypass = false;
        private void dgvDefections_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (bypass)
            {
                bypass = false;
                return;
            }
            if (e.ColumnIndex != clmnSelect.Index) return;
            bool chk = (bool)dgvDefections.Rows[e.RowIndex].Cells[clmnSelect.Index].Value;
            dgvDefections.Rows[e.RowIndex].Cells[clmnSelect.Index].Value = !chk;
        }
    }
}
