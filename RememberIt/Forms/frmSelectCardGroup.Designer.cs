namespace RememberIt
{
    partial class frmSelectCardGroup
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
            this.cmbGroups = new System.Windows.Forms.ComboBox();
            this.lblCardGroup = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtNewGroupName = new System.Windows.Forms.TextBox();
            this.btnNewGroup = new System.Windows.Forms.Button();
            this.chbxCreateNewGroup = new System.Windows.Forms.CheckBox();
            this.gbxCreateNewCardGroup = new System.Windows.Forms.GroupBox();
            this.gbxCreateNewCardGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbGroups
            // 
            this.cmbGroups.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbGroups.BackColor = System.Drawing.SystemColors.Info;
            this.cmbGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroups.FormattingEnabled = true;
            this.cmbGroups.Location = new System.Drawing.Point(113, 19);
            this.cmbGroups.Name = "cmbGroups";
            this.cmbGroups.Size = new System.Drawing.Size(202, 21);
            this.cmbGroups.TabIndex = 1;
            // 
            // lblCardGroup
            // 
            this.lblCardGroup.AutoSize = true;
            this.lblCardGroup.Location = new System.Drawing.Point(12, 22);
            this.lblCardGroup.Name = "lblCardGroup";
            this.lblCardGroup.Size = new System.Drawing.Size(61, 13);
            this.lblCardGroup.TabIndex = 0;
            this.lblCardGroup.Text = "Card Group";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(240, 191);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 39);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(159, 191);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 39);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtNewGroupName
            // 
            this.txtNewGroupName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewGroupName.BackColor = System.Drawing.SystemColors.Info;
            this.txtNewGroupName.ForeColor = System.Drawing.Color.Maroon;
            this.txtNewGroupName.Location = new System.Drawing.Point(6, 19);
            this.txtNewGroupName.Name = "txtNewGroupName";
            this.txtNewGroupName.Size = new System.Drawing.Size(291, 20);
            this.txtNewGroupName.TabIndex = 0;
            // 
            // btnNewGroup
            // 
            this.btnNewGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNewGroup.Location = new System.Drawing.Point(202, 45);
            this.btnNewGroup.Name = "btnNewGroup";
            this.btnNewGroup.Size = new System.Drawing.Size(95, 38);
            this.btnNewGroup.TabIndex = 1;
            this.btnNewGroup.Text = "Create";
            this.btnNewGroup.UseVisualStyleBackColor = true;
            this.btnNewGroup.Click += new System.EventHandler(this.btnNewGroup_Click);
            // 
            // chbxCreateNewGroup
            // 
            this.chbxCreateNewGroup.AutoSize = true;
            this.chbxCreateNewGroup.Location = new System.Drawing.Point(15, 54);
            this.chbxCreateNewGroup.Name = "chbxCreateNewGroup";
            this.chbxCreateNewGroup.Size = new System.Drawing.Size(82, 17);
            this.chbxCreateNewGroup.TabIndex = 2;
            this.chbxCreateNewGroup.Text = "Create New";
            this.chbxCreateNewGroup.UseVisualStyleBackColor = true;
            this.chbxCreateNewGroup.CheckedChanged += new System.EventHandler(this.chbxCreateNewGroup_CheckedChanged);
            // 
            // gbxCreateNewCardGroup
            // 
            this.gbxCreateNewCardGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxCreateNewCardGroup.Controls.Add(this.txtNewGroupName);
            this.gbxCreateNewCardGroup.Controls.Add(this.btnNewGroup);
            this.gbxCreateNewCardGroup.Enabled = false;
            this.gbxCreateNewCardGroup.Location = new System.Drawing.Point(12, 77);
            this.gbxCreateNewCardGroup.Name = "gbxCreateNewCardGroup";
            this.gbxCreateNewCardGroup.Size = new System.Drawing.Size(303, 100);
            this.gbxCreateNewCardGroup.TabIndex = 3;
            this.gbxCreateNewCardGroup.TabStop = false;
            this.gbxCreateNewCardGroup.Text = "Create New Group";
            // 
            // frmSelectCardGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 242);
            this.Controls.Add(this.gbxCreateNewCardGroup);
            this.Controls.Add(this.chbxCreateNewGroup);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblCardGroup);
            this.Controls.Add(this.cmbGroups);
            this.Name = "frmSelectCardGroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Card Group";
            this.TopMost = true;
            this.gbxCreateNewCardGroup.ResumeLayout(false);
            this.gbxCreateNewCardGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbGroups;
        private System.Windows.Forms.Label lblCardGroup;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtNewGroupName;
        private System.Windows.Forms.Button btnNewGroup;
        private System.Windows.Forms.CheckBox chbxCreateNewGroup;
        private System.Windows.Forms.GroupBox gbxCreateNewCardGroup;
    }
}