namespace RememberIt
{
    partial class frmDefectiveCards
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvDefections = new System.Windows.Forms.DataGridView();
            this.clmnRow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmnCardName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnCardPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnQFilesCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnAnswerFileCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnReminderFileCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnSelectNone = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDefections)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDefections
            // 
            this.dgvDefections.AllowUserToAddRows = false;
            this.dgvDefections.AllowUserToDeleteRows = false;
            this.dgvDefections.AllowUserToOrderColumns = true;
            this.dgvDefections.AllowUserToResizeRows = false;
            this.dgvDefections.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDefections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDefections.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmnRow,
            this.clmnSelect,
            this.clmnCardName,
            this.clmnCardPath,
            this.clmnQFilesCount,
            this.clmnAnswerFileCount,
            this.clmnReminderFileCount});
            this.dgvDefections.Location = new System.Drawing.Point(13, 12);
            this.dgvDefections.MultiSelect = false;
            this.dgvDefections.Name = "dgvDefections";
            this.dgvDefections.ReadOnly = true;
            this.dgvDefections.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDefections.Size = new System.Drawing.Size(475, 290);
            this.dgvDefections.TabIndex = 1;
            this.dgvDefections.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDefections_CellClick);
            this.dgvDefections.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDefections_CellDoubleClick);
            this.dgvDefections.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvDefections_KeyDown);
            // 
            // clmnRow
            // 
            this.clmnRow.HeaderText = "Row";
            this.clmnRow.Name = "clmnRow";
            this.clmnRow.ReadOnly = true;
            this.clmnRow.Width = 30;
            // 
            // clmnSelect
            // 
            this.clmnSelect.HeaderText = "Select";
            this.clmnSelect.Name = "clmnSelect";
            this.clmnSelect.ReadOnly = true;
            this.clmnSelect.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmnSelect.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.clmnSelect.Width = 50;
            // 
            // clmnCardName
            // 
            this.clmnCardName.HeaderText = "Card Name";
            this.clmnCardName.Name = "clmnCardName";
            this.clmnCardName.ReadOnly = true;
            this.clmnCardName.Width = 200;
            // 
            // clmnCardPath
            // 
            this.clmnCardPath.HeaderText = "CardPath";
            this.clmnCardPath.Name = "clmnCardPath";
            this.clmnCardPath.ReadOnly = true;
            this.clmnCardPath.Visible = false;
            // 
            // clmnQFilesCount
            // 
            this.clmnQFilesCount.HeaderText = "Q#";
            this.clmnQFilesCount.Name = "clmnQFilesCount";
            this.clmnQFilesCount.ReadOnly = true;
            this.clmnQFilesCount.Width = 40;
            // 
            // clmnAnswerFileCount
            // 
            this.clmnAnswerFileCount.HeaderText = "A#";
            this.clmnAnswerFileCount.Name = "clmnAnswerFileCount";
            this.clmnAnswerFileCount.ReadOnly = true;
            this.clmnAnswerFileCount.Width = 40;
            // 
            // clmnReminderFileCount
            // 
            this.clmnReminderFileCount.HeaderText = "R#";
            this.clmnReminderFileCount.Name = "clmnReminderFileCount";
            this.clmnReminderFileCount.ReadOnly = true;
            this.clmnReminderFileCount.Width = 40;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectAll.Location = new System.Drawing.Point(13, 308);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 35);
            this.btnSelectAll.TabIndex = 2;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnSelectNone
            // 
            this.btnSelectNone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSelectNone.Location = new System.Drawing.Point(94, 308);
            this.btnSelectNone.Name = "btnSelectNone";
            this.btnSelectNone.Size = new System.Drawing.Size(75, 35);
            this.btnSelectNone.TabIndex = 3;
            this.btnSelectNone.Text = "Select None";
            this.btnSelectNone.UseVisualStyleBackColor = true;
            this.btnSelectNone.Click += new System.EventHandler(this.btnSelectNone_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.Location = new System.Drawing.Point(175, 308);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 35);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemove.Location = new System.Drawing.Point(256, 308);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(126, 35);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.Text = "Remove Selected";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // frmDefectiveCards
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 346);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnSelectNone);
            this.Controls.Add(this.btnSelectAll);
            this.Controls.Add(this.dgvDefections);
            this.Name = "frmDefectiveCards";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Defective Cards";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDefections)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDefections;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnRow;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmnSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnCardName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnCardPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnQFilesCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnAnswerFileCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnReminderFileCount;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnSelectNone;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnRemove;
    }
}