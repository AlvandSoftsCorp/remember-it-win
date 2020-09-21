namespace RememberIt
{
    partial class FrmSendCardsToPhone
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
            this.btnConnectToPhone = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.lbCards = new System.Windows.Forms.ListBox();
            this.spContainer = new System.Windows.Forms.SplitContainer();
            this.btnEchoBack = new System.Windows.Forms.Button();
            this.lblCount = new System.Windows.Forms.Label();
            this.btnGetListOfFiles = new System.Windows.Forms.Button();
            this.btnFetchFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.spContainer)).BeginInit();
            this.spContainer.Panel1.SuspendLayout();
            this.spContainer.Panel2.SuspendLayout();
            this.spContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnectToPhone
            // 
            this.btnConnectToPhone.Location = new System.Drawing.Point(12, 12);
            this.btnConnectToPhone.Name = "btnConnectToPhone";
            this.btnConnectToPhone.Size = new System.Drawing.Size(115, 43);
            this.btnConnectToPhone.TabIndex = 0;
            this.btnConnectToPhone.Text = "Connect";
            this.btnConnectToPhone.UseVisualStyleBackColor = true;
            this.btnConnectToPhone.Click += new System.EventHandler(this.btnConnectToPhone_Click);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(734, 12);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(115, 43);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(190)))));
            this.txtLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtLog.Location = new System.Drawing.Point(0, 0);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(842, 374);
            this.txtLog.TabIndex = 3;
            // 
            // lbCards
            // 
            this.lbCards.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lbCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCards.FormattingEnabled = true;
            this.lbCards.Location = new System.Drawing.Point(0, 0);
            this.lbCards.Name = "lbCards";
            this.lbCards.Size = new System.Drawing.Size(842, 71);
            this.lbCards.TabIndex = 4;
            // 
            // spContainer
            // 
            this.spContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.spContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.spContainer.Location = new System.Drawing.Point(7, 61);
            this.spContainer.Name = "spContainer";
            this.spContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spContainer.Panel1
            // 
            this.spContainer.Panel1.Controls.Add(this.lbCards);
            // 
            // spContainer.Panel2
            // 
            this.spContainer.Panel2.Controls.Add(this.txtLog);
            this.spContainer.Size = new System.Drawing.Size(846, 457);
            this.spContainer.SplitterDistance = 75;
            this.spContainer.TabIndex = 5;
            // 
            // btnEchoBack
            // 
            this.btnEchoBack.Location = new System.Drawing.Point(133, 12);
            this.btnEchoBack.Name = "btnEchoBack";
            this.btnEchoBack.Size = new System.Drawing.Size(146, 42);
            this.btnEchoBack.TabIndex = 6;
            this.btnEchoBack.Text = "Echo Back";
            this.btnEchoBack.UseVisualStyleBackColor = true;
            this.btnEchoBack.Click += new System.EventHandler(this.btnEchoBack_Click);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.ForeColor = System.Drawing.Color.Blue;
            this.lblCount.Location = new System.Drawing.Point(576, 18);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(72, 25);
            this.lblCount.TabIndex = 7;
            this.lblCount.Text = "0 of 0";
            // 
            // btnGetListOfFiles
            // 
            this.btnGetListOfFiles.Location = new System.Drawing.Point(285, 13);
            this.btnGetListOfFiles.Name = "btnGetListOfFiles";
            this.btnGetListOfFiles.Size = new System.Drawing.Size(146, 42);
            this.btnGetListOfFiles.TabIndex = 8;
            this.btnGetListOfFiles.Text = "Get List Of Files";
            this.btnGetListOfFiles.UseVisualStyleBackColor = true;
            this.btnGetListOfFiles.Click += new System.EventHandler(this.btnGetListOfFiles_Click);
            // 
            // btnFetchFile
            // 
            this.btnFetchFile.Location = new System.Drawing.Point(437, 13);
            this.btnFetchFile.Name = "btnFetchFile";
            this.btnFetchFile.Size = new System.Drawing.Size(99, 42);
            this.btnFetchFile.TabIndex = 9;
            this.btnFetchFile.Text = "Fetch";
            this.btnFetchFile.UseVisualStyleBackColor = true;
            this.btnFetchFile.Click += new System.EventHandler(this.btnFetchFile_Click);
            // 
            // FrmSendCardsToPhone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 520);
            this.Controls.Add(this.btnFetchFile);
            this.Controls.Add(this.btnGetListOfFiles);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.btnEchoBack);
            this.Controls.Add(this.spContainer);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnConnectToPhone);
            this.Name = "FrmSendCardsToPhone";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Send Cards to Phone";
            this.Load += new System.EventHandler(this.FrmSendCardsToPhone_Load);
            this.spContainer.Panel1.ResumeLayout(false);
            this.spContainer.Panel2.ResumeLayout(false);
            this.spContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spContainer)).EndInit();
            this.spContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnConnectToPhone;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.ListBox lbCards;
        private System.Windows.Forms.SplitContainer spContainer;
        private System.Windows.Forms.Button btnEchoBack;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Button btnGetListOfFiles;
        private System.Windows.Forms.Button btnFetchFile;
    }
}