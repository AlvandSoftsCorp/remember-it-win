namespace RememberIt
{
    partial class FrmPcToPhone
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblPhoneID = new System.Windows.Forms.Label();
            this.txtPhoneID = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tcContainer = new System.Windows.Forms.TabControl();
            this.tbpConnection = new System.Windows.Forms.TabPage();
            this.pnlConnect = new System.Windows.Forms.Panel();
            this.lblConnectionState = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.tbpTransfer = new System.Windows.Forms.TabPage();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.dgvCards = new System.Windows.Forms.DataGridView();
            this.clmnRow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnSelectPcCard = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmnCard = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnCroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnAction = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnSelectPhCard = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clmnCardPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnCardPathPc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnCardPathPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnManifestDataPc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnManifestDataPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmbPhoneCardGroup = new System.Windows.Forms.ComboBox();
            this.lblPhoneCardGroup = new System.Windows.Forms.Label();
            this.cmbPcCardGroup = new System.Windows.Forms.ComboBox();
            this.lblPcCardGroup = new System.Windows.Forms.Label();
            this.pnlWorkingDir = new System.Windows.Forms.Panel();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.lblCount = new System.Windows.Forms.Label();
            this.lblProgress = new System.Windows.Forms.Label();
            this.flpButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnInitialize = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSendToPhone = new System.Windows.Forms.Button();
            this.btnRemoveFromPhone = new System.Windows.Forms.Button();
            this.btnFetchFromPhone = new System.Windows.Forms.Button();
            this.btnClonePcToPhone = new System.Windows.Forms.Button();
            this.chbxShowDiffs = new System.Windows.Forms.CheckBox();
            this.tbpLog = new System.Windows.Forms.TabPage();
            this.rtxtLog = new System.Windows.Forms.RichTextBox();
            this.tcContainer.SuspendLayout();
            this.tbpConnection.SuspendLayout();
            this.pnlConnect.SuspendLayout();
            this.tbpTransfer.SuspendLayout();
            this.pnlContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCards)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlWorkingDir.SuspendLayout();
            this.flpButtons.SuspendLayout();
            this.tbpLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPhoneID
            // 
            this.lblPhoneID.AutoSize = true;
            this.lblPhoneID.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhoneID.Location = new System.Drawing.Point(19, 17);
            this.lblPhoneID.Name = "lblPhoneID";
            this.lblPhoneID.Size = new System.Drawing.Size(93, 24);
            this.lblPhoneID.TabIndex = 0;
            this.lblPhoneID.Text = "Phone ID:";
            // 
            // txtPhoneID
            // 
            this.txtPhoneID.Font = new System.Drawing.Font("Courier New", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhoneID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtPhoneID.Location = new System.Drawing.Point(19, 48);
            this.txtPhoneID.Name = "txtPhoneID";
            this.txtPhoneID.Size = new System.Drawing.Size(310, 32);
            this.txtPhoneID.TabIndex = 1;
            this.txtPhoneID.Text = "C0-A8-01-B3";
            this.txtPhoneID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPhoneID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPhoneID_KeyDown);
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnConnect.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnConnect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnConnect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.Location = new System.Drawing.Point(19, 86);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(310, 50);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tcContainer
            // 
            this.tcContainer.Controls.Add(this.tbpConnection);
            this.tcContainer.Controls.Add(this.tbpTransfer);
            this.tcContainer.Controls.Add(this.tbpLog);
            this.tcContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcContainer.Location = new System.Drawing.Point(0, 0);
            this.tcContainer.Name = "tcContainer";
            this.tcContainer.SelectedIndex = 0;
            this.tcContainer.Size = new System.Drawing.Size(836, 461);
            this.tcContainer.TabIndex = 3;
            // 
            // tbpConnection
            // 
            this.tbpConnection.BackColor = System.Drawing.Color.LightGray;
            this.tbpConnection.Controls.Add(this.pnlConnect);
            this.tbpConnection.Location = new System.Drawing.Point(4, 22);
            this.tbpConnection.Name = "tbpConnection";
            this.tbpConnection.Padding = new System.Windows.Forms.Padding(3);
            this.tbpConnection.Size = new System.Drawing.Size(828, 435);
            this.tbpConnection.TabIndex = 0;
            this.tbpConnection.Text = "Connection";
            // 
            // pnlConnect
            // 
            this.pnlConnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.pnlConnect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlConnect.Controls.Add(this.lblConnectionState);
            this.pnlConnect.Controls.Add(this.btnDisconnect);
            this.pnlConnect.Controls.Add(this.lblPhoneID);
            this.pnlConnect.Controls.Add(this.btnConnect);
            this.pnlConnect.Controls.Add(this.txtPhoneID);
            this.pnlConnect.Location = new System.Drawing.Point(194, 133);
            this.pnlConnect.Name = "pnlConnect";
            this.pnlConnect.Size = new System.Drawing.Size(350, 213);
            this.pnlConnect.TabIndex = 3;
            // 
            // lblConnectionState
            // 
            this.lblConnectionState.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConnectionState.ForeColor = System.Drawing.Color.Crimson;
            this.lblConnectionState.Location = new System.Drawing.Point(118, 17);
            this.lblConnectionState.Name = "lblConnectionState";
            this.lblConnectionState.Size = new System.Drawing.Size(211, 24);
            this.lblConnectionState.TabIndex = 4;
            this.lblConnectionState.Text = "Not Connected";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.btnDisconnect.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnDisconnect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnDisconnect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnDisconnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDisconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisconnect.Location = new System.Drawing.Point(19, 142);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(310, 50);
            this.btnDisconnect.TabIndex = 3;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = false;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // tbpTransfer
            // 
            this.tbpTransfer.Controls.Add(this.pnlContainer);
            this.tbpTransfer.Controls.Add(this.flpButtons);
            this.tbpTransfer.Location = new System.Drawing.Point(4, 22);
            this.tbpTransfer.Name = "tbpTransfer";
            this.tbpTransfer.Padding = new System.Windows.Forms.Padding(3);
            this.tbpTransfer.Size = new System.Drawing.Size(828, 435);
            this.tbpTransfer.TabIndex = 1;
            this.tbpTransfer.Text = "Transfer";
            this.tbpTransfer.UseVisualStyleBackColor = true;
            // 
            // pnlContainer
            // 
            this.pnlContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.pnlContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContainer.Controls.Add(this.dgvCards);
            this.pnlContainer.Controls.Add(this.tableLayoutPanel1);
            this.pnlContainer.Controls.Add(this.pnlWorkingDir);
            this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContainer.Location = new System.Drawing.Point(191, 3);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(634, 429);
            this.pnlContainer.TabIndex = 2;
            // 
            // dgvCards
            // 
            this.dgvCards.AllowUserToAddRows = false;
            this.dgvCards.AllowUserToDeleteRows = false;
            this.dgvCards.AllowUserToOrderColumns = true;
            this.dgvCards.AllowUserToResizeRows = false;
            this.dgvCards.BackgroundColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Maroon;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCards.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCards.ColumnHeadersHeight = 40;
            this.dgvCards.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvCards.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmnRow,
            this.clmnSelectPcCard,
            this.clmnCard,
            this.clmnCroup,
            this.clmnAction,
            this.clmnSelectPhCard,
            this.clmnCardPhone,
            this.clmnCardPathPc,
            this.clmnCardPathPhone,
            this.clmnManifestDataPc,
            this.clmnManifestDataPhone});
            this.dgvCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCards.Location = new System.Drawing.Point(0, 59);
            this.dgvCards.Name = "dgvCards";
            this.dgvCards.ReadOnly = true;
            this.dgvCards.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCards.Size = new System.Drawing.Size(632, 285);
            this.dgvCards.TabIndex = 12;
            this.dgvCards.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCards_CellDoubleClick);
            // 
            // clmnRow
            // 
            this.clmnRow.HeaderText = "Row";
            this.clmnRow.Name = "clmnRow";
            this.clmnRow.ReadOnly = true;
            this.clmnRow.Visible = false;
            // 
            // clmnSelectPcCard
            // 
            this.clmnSelectPcCard.HeaderText = "";
            this.clmnSelectPcCard.Name = "clmnSelectPcCard";
            this.clmnSelectPcCard.ReadOnly = true;
            this.clmnSelectPcCard.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmnSelectPcCard.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.clmnSelectPcCard.Width = 20;
            // 
            // clmnCard
            // 
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clmnCard.DefaultCellStyle = dataGridViewCellStyle2;
            this.clmnCard.HeaderText = "Card-PC";
            this.clmnCard.Name = "clmnCard";
            this.clmnCard.ReadOnly = true;
            this.clmnCard.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.clmnCard.Width = 150;
            // 
            // clmnCroup
            // 
            this.clmnCroup.HeaderText = "Group";
            this.clmnCroup.Name = "clmnCroup";
            this.clmnCroup.ReadOnly = true;
            this.clmnCroup.Visible = false;
            // 
            // clmnAction
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clmnAction.DefaultCellStyle = dataGridViewCellStyle3;
            this.clmnAction.HeaderText = "Action";
            this.clmnAction.Name = "clmnAction";
            this.clmnAction.ReadOnly = true;
            this.clmnAction.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // clmnSelectPhCard
            // 
            this.clmnSelectPhCard.HeaderText = "";
            this.clmnSelectPhCard.Name = "clmnSelectPhCard";
            this.clmnSelectPhCard.ReadOnly = true;
            this.clmnSelectPhCard.Width = 20;
            // 
            // clmnCardPhone
            // 
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clmnCardPhone.DefaultCellStyle = dataGridViewCellStyle4;
            this.clmnCardPhone.HeaderText = "Card-Phone";
            this.clmnCardPhone.Name = "clmnCardPhone";
            this.clmnCardPhone.ReadOnly = true;
            this.clmnCardPhone.Width = 150;
            // 
            // clmnCardPathPc
            // 
            this.clmnCardPathPc.HeaderText = "Card-Path-PC";
            this.clmnCardPathPc.Name = "clmnCardPathPc";
            this.clmnCardPathPc.ReadOnly = true;
            this.clmnCardPathPc.Visible = false;
            // 
            // clmnCardPathPhone
            // 
            this.clmnCardPathPhone.HeaderText = "Card-Path-Phone";
            this.clmnCardPathPhone.Name = "clmnCardPathPhone";
            this.clmnCardPathPhone.ReadOnly = true;
            this.clmnCardPathPhone.Visible = false;
            // 
            // clmnManifestDataPc
            // 
            this.clmnManifestDataPc.HeaderText = "ManifestDataPc";
            this.clmnManifestDataPc.Name = "clmnManifestDataPc";
            this.clmnManifestDataPc.ReadOnly = true;
            this.clmnManifestDataPc.Visible = false;
            // 
            // clmnManifestDataPhone
            // 
            this.clmnManifestDataPhone.HeaderText = "ManifestDataPhone";
            this.clmnManifestDataPhone.Name = "clmnManifestDataPhone";
            this.clmnManifestDataPhone.ReadOnly = true;
            this.clmnManifestDataPhone.Visible = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.cmbPhoneCardGroup, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblPhoneCardGroup, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cmbPcCardGroup, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblPcCardGroup, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(632, 59);
            this.tableLayoutPanel1.TabIndex = 11;
            // 
            // cmbPhoneCardGroup
            // 
            this.cmbPhoneCardGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbPhoneCardGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPhoneCardGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPhoneCardGroup.FormattingEnabled = true;
            this.cmbPhoneCardGroup.Location = new System.Drawing.Point(319, 31);
            this.cmbPhoneCardGroup.Name = "cmbPhoneCardGroup";
            this.cmbPhoneCardGroup.Size = new System.Drawing.Size(310, 28);
            this.cmbPhoneCardGroup.TabIndex = 12;
            // 
            // lblPhoneCardGroup
            // 
            this.lblPhoneCardGroup.AutoSize = true;
            this.lblPhoneCardGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPhoneCardGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhoneCardGroup.Location = new System.Drawing.Point(319, 0);
            this.lblPhoneCardGroup.Name = "lblPhoneCardGroup";
            this.lblPhoneCardGroup.Size = new System.Drawing.Size(310, 24);
            this.lblPhoneCardGroup.TabIndex = 9;
            this.lblPhoneCardGroup.Text = "Card Group (Phone)";
            // 
            // cmbPcCardGroup
            // 
            this.cmbPcCardGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmbPcCardGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPcCardGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbPcCardGroup.FormattingEnabled = true;
            this.cmbPcCardGroup.Location = new System.Drawing.Point(3, 31);
            this.cmbPcCardGroup.Name = "cmbPcCardGroup";
            this.cmbPcCardGroup.Size = new System.Drawing.Size(310, 28);
            this.cmbPcCardGroup.TabIndex = 8;
            // 
            // lblPcCardGroup
            // 
            this.lblPcCardGroup.AutoSize = true;
            this.lblPcCardGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPcCardGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPcCardGroup.Location = new System.Drawing.Point(3, 0);
            this.lblPcCardGroup.Name = "lblPcCardGroup";
            this.lblPcCardGroup.Size = new System.Drawing.Size(310, 24);
            this.lblPcCardGroup.TabIndex = 1;
            this.lblPcCardGroup.Text = "Card Group (PC)";
            // 
            // pnlWorkingDir
            // 
            this.pnlWorkingDir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.pnlWorkingDir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlWorkingDir.Controls.Add(this.pbProgress);
            this.pnlWorkingDir.Controls.Add(this.lblCount);
            this.pnlWorkingDir.Controls.Add(this.lblProgress);
            this.pnlWorkingDir.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlWorkingDir.Location = new System.Drawing.Point(0, 344);
            this.pnlWorkingDir.Name = "pnlWorkingDir";
            this.pnlWorkingDir.Size = new System.Drawing.Size(632, 83);
            this.pnlWorkingDir.TabIndex = 0;
            // 
            // pbProgress
            // 
            this.pbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbProgress.Location = new System.Drawing.Point(190, 40);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(437, 23);
            this.pbProgress.TabIndex = 7;
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.Location = new System.Drawing.Point(100, 40);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(55, 24);
            this.lblCount.TabIndex = 6;
            this.lblCount.Text = "0 of 0";
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgress.Location = new System.Drawing.Point(4, 40);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(90, 24);
            this.lblProgress.TabIndex = 5;
            this.lblProgress.Text = "Progress:";
            // 
            // flpButtons
            // 
            this.flpButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.flpButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpButtons.Controls.Add(this.btnInitialize);
            this.flpButtons.Controls.Add(this.btnRefresh);
            this.flpButtons.Controls.Add(this.btnSendToPhone);
            this.flpButtons.Controls.Add(this.btnRemoveFromPhone);
            this.flpButtons.Controls.Add(this.btnFetchFromPhone);
            this.flpButtons.Controls.Add(this.btnClonePcToPhone);
            this.flpButtons.Controls.Add(this.chbxShowDiffs);
            this.flpButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.flpButtons.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpButtons.Location = new System.Drawing.Point(3, 3);
            this.flpButtons.Name = "flpButtons";
            this.flpButtons.Size = new System.Drawing.Size(188, 429);
            this.flpButtons.TabIndex = 1;
            // 
            // btnInitialize
            // 
            this.btnInitialize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnInitialize.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnInitialize.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnInitialize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnInitialize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInitialize.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInitialize.ForeColor = System.Drawing.Color.Maroon;
            this.btnInitialize.Location = new System.Drawing.Point(3, 3);
            this.btnInitialize.Name = "btnInitialize";
            this.btnInitialize.Size = new System.Drawing.Size(182, 37);
            this.btnInitialize.TabIndex = 3;
            this.btnInitialize.Text = "Initialize";
            this.btnInitialize.UseVisualStyleBackColor = false;
            this.btnInitialize.Click += new System.EventHandler(this.btnInitialize_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.Maroon;
            this.btnRefresh.Location = new System.Drawing.Point(3, 46);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(182, 37);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSendToPhone
            // 
            this.btnSendToPhone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnSendToPhone.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnSendToPhone.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnSendToPhone.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnSendToPhone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendToPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendToPhone.ForeColor = System.Drawing.Color.Maroon;
            this.btnSendToPhone.Location = new System.Drawing.Point(3, 89);
            this.btnSendToPhone.Name = "btnSendToPhone";
            this.btnSendToPhone.Size = new System.Drawing.Size(182, 37);
            this.btnSendToPhone.TabIndex = 5;
            this.btnSendToPhone.Text = "Send To Phone";
            this.btnSendToPhone.UseVisualStyleBackColor = false;
            this.btnSendToPhone.Click += new System.EventHandler(this.btnSendToPhone_Click);
            // 
            // btnRemoveFromPhone
            // 
            this.btnRemoveFromPhone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnRemoveFromPhone.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnRemoveFromPhone.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnRemoveFromPhone.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnRemoveFromPhone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveFromPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemoveFromPhone.ForeColor = System.Drawing.Color.Maroon;
            this.btnRemoveFromPhone.Location = new System.Drawing.Point(3, 132);
            this.btnRemoveFromPhone.Name = "btnRemoveFromPhone";
            this.btnRemoveFromPhone.Size = new System.Drawing.Size(182, 37);
            this.btnRemoveFromPhone.TabIndex = 6;
            this.btnRemoveFromPhone.Text = "Remove From Phone";
            this.btnRemoveFromPhone.UseVisualStyleBackColor = false;
            this.btnRemoveFromPhone.Click += new System.EventHandler(this.btnRemoveFromPhone_Click);
            // 
            // btnFetchFromPhone
            // 
            this.btnFetchFromPhone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnFetchFromPhone.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnFetchFromPhone.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnFetchFromPhone.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnFetchFromPhone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFetchFromPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFetchFromPhone.ForeColor = System.Drawing.Color.Maroon;
            this.btnFetchFromPhone.Location = new System.Drawing.Point(3, 175);
            this.btnFetchFromPhone.Name = "btnFetchFromPhone";
            this.btnFetchFromPhone.Size = new System.Drawing.Size(182, 37);
            this.btnFetchFromPhone.TabIndex = 7;
            this.btnFetchFromPhone.Text = "Fetch From Phone";
            this.btnFetchFromPhone.UseVisualStyleBackColor = false;
            this.btnFetchFromPhone.Click += new System.EventHandler(this.btnFetchFromPhone_Click);
            // 
            // btnClonePcToPhone
            // 
            this.btnClonePcToPhone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnClonePcToPhone.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.btnClonePcToPhone.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnClonePcToPhone.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnClonePcToPhone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClonePcToPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClonePcToPhone.ForeColor = System.Drawing.Color.Maroon;
            this.btnClonePcToPhone.Location = new System.Drawing.Point(3, 218);
            this.btnClonePcToPhone.Name = "btnClonePcToPhone";
            this.btnClonePcToPhone.Size = new System.Drawing.Size(182, 37);
            this.btnClonePcToPhone.TabIndex = 8;
            this.btnClonePcToPhone.Text = "Clone To Phone";
            this.btnClonePcToPhone.UseVisualStyleBackColor = false;
            this.btnClonePcToPhone.Click += new System.EventHandler(this.btnClonePcToPhone_Click);
            // 
            // chbxShowDiffs
            // 
            this.chbxShowDiffs.FlatAppearance.BorderColor = System.Drawing.Color.Blue;
            this.chbxShowDiffs.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.chbxShowDiffs.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.chbxShowDiffs.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.chbxShowDiffs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chbxShowDiffs.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbxShowDiffs.Location = new System.Drawing.Point(3, 261);
            this.chbxShowDiffs.Name = "chbxShowDiffs";
            this.chbxShowDiffs.Size = new System.Drawing.Size(182, 37);
            this.chbxShowDiffs.TabIndex = 9;
            this.chbxShowDiffs.Text = "Show Only Diffs";
            this.chbxShowDiffs.UseVisualStyleBackColor = true;
            this.chbxShowDiffs.CheckedChanged += new System.EventHandler(this.chbxShowDiffs_CheckedChanged);
            // 
            // tbpLog
            // 
            this.tbpLog.Controls.Add(this.rtxtLog);
            this.tbpLog.Location = new System.Drawing.Point(4, 22);
            this.tbpLog.Name = "tbpLog";
            this.tbpLog.Padding = new System.Windows.Forms.Padding(3);
            this.tbpLog.Size = new System.Drawing.Size(828, 435);
            this.tbpLog.TabIndex = 2;
            this.tbpLog.Text = "Log";
            this.tbpLog.UseVisualStyleBackColor = true;
            // 
            // rtxtLog
            // 
            this.rtxtLog.BackColor = System.Drawing.SystemColors.Info;
            this.rtxtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtLog.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtxtLog.Location = new System.Drawing.Point(3, 3);
            this.rtxtLog.Name = "rtxtLog";
            this.rtxtLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtxtLog.Size = new System.Drawing.Size(822, 429);
            this.rtxtLog.TabIndex = 0;
            this.rtxtLog.Text = "";
            // 
            // FrmPcToPhone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 461);
            this.Controls.Add(this.tcContainer);
            this.Name = "FrmPcToPhone";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PC to Phone";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPcToPhone_FormClosing);
            this.Load += new System.EventHandler(this.FrmPcToPhone_Load);
            this.tcContainer.ResumeLayout(false);
            this.tbpConnection.ResumeLayout(false);
            this.pnlConnect.ResumeLayout(false);
            this.pnlConnect.PerformLayout();
            this.tbpTransfer.ResumeLayout(false);
            this.pnlContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCards)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pnlWorkingDir.ResumeLayout(false);
            this.pnlWorkingDir.PerformLayout();
            this.flpButtons.ResumeLayout(false);
            this.tbpLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPhoneID;
        private System.Windows.Forms.TextBox txtPhoneID;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TabControl tcContainer;
        private System.Windows.Forms.TabPage tbpConnection;
        private System.Windows.Forms.TabPage tbpTransfer;
        private System.Windows.Forms.Panel pnlConnect;
        private System.Windows.Forms.TabPage tbpLog;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.RichTextBox rtxtLog;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.FlowLayoutPanel flpButtons;
        private System.Windows.Forms.Button btnInitialize;
        private System.Windows.Forms.Panel pnlWorkingDir;
        private System.Windows.Forms.Label lblPcCardGroup;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox cmbPhoneCardGroup;
        private System.Windows.Forms.Label lblPhoneCardGroup;
        private System.Windows.Forms.ComboBox cmbPcCardGroup;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSendToPhone;
        private System.Windows.Forms.Button btnRemoveFromPhone;
        private System.Windows.Forms.Button btnFetchFromPhone;
        private System.Windows.Forms.DataGridView dgvCards;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnRow;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmnSelectPcCard;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnCard;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnCroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnAction;
        private System.Windows.Forms.DataGridViewCheckBoxColumn clmnSelectPhCard;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnCardPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnCardPathPc;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnCardPathPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnManifestDataPc;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnManifestDataPhone;
        private System.Windows.Forms.Button btnClonePcToPhone;
        private System.Windows.Forms.CheckBox chbxShowDiffs;
        private System.Windows.Forms.Label lblConnectionState;
    }
}