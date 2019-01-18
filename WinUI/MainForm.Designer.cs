namespace UI
{
    partial class MainForm
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
            this.tcSections = new System.Windows.Forms.TabControl();
            this.tpDashboard = new System.Windows.Forms.TabPage();
            this.splitDashboard = new System.Windows.Forms.SplitContainer();
            this.dgBalance = new System.Windows.Forms.DataGridView();
            this.pnlOpenOrders = new System.Windows.Forms.Panel();
            this.tableOpenOrders = new System.Windows.Forms.TableLayoutPanel();
            this.cbPool = new System.Windows.Forms.ComboBox();
            this.tbQuoteQty = new System.Windows.Forms.TextBox();
            this.cbGroup = new System.Windows.Forms.ComboBox();
            this.lblAction = new System.Windows.Forms.Label();
            this.lblQuoteQty = new System.Windows.Forms.Label();
            this.lblGroup = new System.Windows.Forms.Label();
            this.lblPool = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblQuoteAsset = new System.Windows.Forms.Label();
            this.lblBaseAsset = new System.Windows.Forms.Label();
            this.cbQuoteAsset = new System.Windows.Forms.ComboBox();
            this.tbBaseQty = new System.Windows.Forms.TextBox();
            this.pnlStatusBar = new System.Windows.Forms.Panel();
            this.pnlBaseAssetInfo = new System.Windows.Forms.Panel();
            this.lblOpenOrders = new System.Windows.Forms.Label();
            this.tpOrdersHistory = new System.Windows.Forms.TabPage();
            this.tpSettings = new System.Windows.Forms.TabPage();
            this.lblIPValue = new System.Windows.Forms.Label();
            this.lblIP = new System.Windows.Forms.Label();
            this.clbQuoteAssets = new System.Windows.Forms.CheckedListBox();
            this.lblBinance = new System.Windows.Forms.Label();
            this.lblExchange = new System.Windows.Forms.Label();
            this.lblQuoteAssets = new System.Windows.Forms.Label();
            this.btnCancelSettings = new System.Windows.Forms.Button();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.txtSecretKey = new System.Windows.Forms.TextBox();
            this.txtApiKey = new System.Windows.Forms.TextBox();
            this.lblSecretKey = new System.Windows.Forms.Label();
            this.lblApiKey = new System.Windows.Forms.Label();
            this.tcSections.SuspendLayout();
            this.tpDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitDashboard)).BeginInit();
            this.splitDashboard.Panel1.SuspendLayout();
            this.splitDashboard.Panel2.SuspendLayout();
            this.splitDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBalance)).BeginInit();
            this.pnlOpenOrders.SuspendLayout();
            this.tableOpenOrders.SuspendLayout();
            this.pnlBaseAssetInfo.SuspendLayout();
            this.tpSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcSections
            // 
            this.tcSections.Controls.Add(this.tpDashboard);
            this.tcSections.Controls.Add(this.tpOrdersHistory);
            this.tcSections.Controls.Add(this.tpSettings);
            this.tcSections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSections.Location = new System.Drawing.Point(0, 0);
            this.tcSections.Margin = new System.Windows.Forms.Padding(0);
            this.tcSections.Name = "tcSections";
            this.tcSections.Padding = new System.Drawing.Point(50, 8);
            this.tcSections.SelectedIndex = 0;
            this.tcSections.Size = new System.Drawing.Size(1233, 651);
            this.tcSections.TabIndex = 0;
            // 
            // tpDashboard
            // 
            this.tpDashboard.Controls.Add(this.splitDashboard);
            this.tpDashboard.Location = new System.Drawing.Point(4, 44);
            this.tpDashboard.Margin = new System.Windows.Forms.Padding(0);
            this.tpDashboard.Name = "tpDashboard";
            this.tpDashboard.Padding = new System.Windows.Forms.Padding(1);
            this.tpDashboard.Size = new System.Drawing.Size(1225, 603);
            this.tpDashboard.TabIndex = 0;
            this.tpDashboard.Text = "Dashboard";
            this.tpDashboard.UseVisualStyleBackColor = true;
            // 
            // splitDashboard
            // 
            this.splitDashboard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitDashboard.Location = new System.Drawing.Point(1, 1);
            this.splitDashboard.Margin = new System.Windows.Forms.Padding(0);
            this.splitDashboard.Name = "splitDashboard";
            this.splitDashboard.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitDashboard.Panel1
            // 
            this.splitDashboard.Panel1.Controls.Add(this.dgBalance);
            // 
            // splitDashboard.Panel2
            // 
            this.splitDashboard.Panel2.Controls.Add(this.pnlOpenOrders);
            this.splitDashboard.Panel2.Controls.Add(this.pnlBaseAssetInfo);
            this.splitDashboard.Size = new System.Drawing.Size(1223, 601);
            this.splitDashboard.SplitterDistance = 100;
            this.splitDashboard.SplitterWidth = 1;
            this.splitDashboard.TabIndex = 10;
            // 
            // dgBalance
            // 
            this.dgBalance.AllowUserToAddRows = false;
            this.dgBalance.AllowUserToDeleteRows = false;
            this.dgBalance.AllowUserToOrderColumns = true;
            this.dgBalance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgBalance.Location = new System.Drawing.Point(0, 0);
            this.dgBalance.Margin = new System.Windows.Forms.Padding(4);
            this.dgBalance.Name = "dgBalance";
            this.dgBalance.ReadOnly = true;
            this.dgBalance.RowTemplate.Height = 28;
            this.dgBalance.Size = new System.Drawing.Size(1221, 98);
            this.dgBalance.TabIndex = 7;
            // 
            // pnlOpenOrders
            // 
            this.pnlOpenOrders.Controls.Add(this.tableOpenOrders);
            this.pnlOpenOrders.Controls.Add(this.pnlStatusBar);
            this.pnlOpenOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOpenOrders.Location = new System.Drawing.Point(0, 33);
            this.pnlOpenOrders.Name = "pnlOpenOrders";
            this.pnlOpenOrders.Size = new System.Drawing.Size(1221, 465);
            this.pnlOpenOrders.TabIndex = 5;
            // 
            // tableOpenOrders
            // 
            this.tableOpenOrders.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableOpenOrders.ColumnCount = 6;
            this.tableOpenOrders.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableOpenOrders.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableOpenOrders.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tableOpenOrders.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableOpenOrders.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22F));
            this.tableOpenOrders.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12F));
            this.tableOpenOrders.Controls.Add(this.cbPool, 0, 1);
            this.tableOpenOrders.Controls.Add(this.tbQuoteQty, 4, 1);
            this.tableOpenOrders.Controls.Add(this.cbGroup, 1, 1);
            this.tableOpenOrders.Controls.Add(this.lblAction, 5, 0);
            this.tableOpenOrders.Controls.Add(this.lblQuoteQty, 4, 0);
            this.tableOpenOrders.Controls.Add(this.lblGroup, 1, 0);
            this.tableOpenOrders.Controls.Add(this.lblPool, 0, 0);
            this.tableOpenOrders.Controls.Add(this.btnAdd, 5, 1);
            this.tableOpenOrders.Controls.Add(this.lblQuoteAsset, 2, 0);
            this.tableOpenOrders.Controls.Add(this.lblBaseAsset, 3, 0);
            this.tableOpenOrders.Controls.Add(this.cbQuoteAsset, 2, 1);
            this.tableOpenOrders.Controls.Add(this.tbBaseQty, 3, 1);
            this.tableOpenOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableOpenOrders.Location = new System.Drawing.Point(0, 0);
            this.tableOpenOrders.Name = "tableOpenOrders";
            this.tableOpenOrders.RowCount = 3;
            this.tableOpenOrders.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableOpenOrders.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableOpenOrders.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableOpenOrders.Size = new System.Drawing.Size(1221, 422);
            this.tableOpenOrders.TabIndex = 6;
            // 
            // cbPool
            // 
            this.cbPool.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbPool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPool.FormattingEnabled = true;
            this.cbPool.Location = new System.Drawing.Point(13, 48);
            this.cbPool.Name = "cbPool";
            this.cbPool.Size = new System.Drawing.Size(121, 33);
            this.cbPool.TabIndex = 12;
            this.cbPool.SelectedIndexChanged += new System.EventHandler(this.cbPool_SelectedIndexChanged);
            // 
            // tbQuoteQty
            // 
            this.tbQuoteQty.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbQuoteQty.Location = new System.Drawing.Point(831, 47);
            this.tbQuoteQty.Name = "tbQuoteQty";
            this.tbQuoteQty.Size = new System.Drawing.Size(213, 30);
            this.tbQuoteQty.TabIndex = 10;
            this.tbQuoteQty.Leave += new System.EventHandler(this.tbQuoteQty_Leave);
            // 
            // cbGroup
            // 
            this.cbGroup.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGroup.FormattingEnabled = true;
            this.cbGroup.Location = new System.Drawing.Point(159, 48);
            this.cbGroup.Name = "cbGroup";
            this.cbGroup.Size = new System.Drawing.Size(121, 33);
            this.cbGroup.TabIndex = 7;
            this.cbGroup.SelectedIndexChanged += new System.EventHandler(this.cbGroup_SelectedIndexChanged);
            // 
            // lblAction
            // 
            this.lblAction.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblAction.AutoSize = true;
            this.lblAction.Location = new System.Drawing.Point(1112, 8);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(67, 25);
            this.lblAction.TabIndex = 5;
            this.lblAction.Text = "Action";
            this.lblAction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblQuoteQty
            // 
            this.lblQuoteQty.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblQuoteQty.AutoSize = true;
            this.lblQuoteQty.Location = new System.Drawing.Point(886, 8);
            this.lblQuoteQty.Name = "lblQuoteQty";
            this.lblQuoteQty.Size = new System.Drawing.Size(102, 25);
            this.lblQuoteQty.TabIndex = 4;
            this.lblQuoteQty.Text = "Quote Qty";
            this.lblQuoteQty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGroup
            // 
            this.lblGroup.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(186, 8);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(66, 25);
            this.lblGroup.TabIndex = 1;
            this.lblGroup.Text = "Group";
            this.lblGroup.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPool
            // 
            this.lblPool.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblPool.AutoSize = true;
            this.lblPool.Location = new System.Drawing.Point(48, 8);
            this.lblPool.Name = "lblPool";
            this.lblPool.Size = new System.Drawing.Size(51, 25);
            this.lblPool.TabIndex = 0;
            this.lblPool.Text = "Pool";
            this.lblPool.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAdd.Location = new System.Drawing.Point(1128, 46);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(36, 31);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblQuoteAsset
            // 
            this.lblQuoteAsset.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblQuoteAsset.AutoSize = true;
            this.lblQuoteAsset.Location = new System.Drawing.Point(366, 8);
            this.lblQuoteAsset.Name = "lblQuoteAsset";
            this.lblQuoteAsset.Size = new System.Drawing.Size(121, 25);
            this.lblQuoteAsset.TabIndex = 3;
            this.lblQuoteAsset.Text = "Quote Asset";
            this.lblQuoteAsset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBaseAsset
            // 
            this.lblBaseAsset.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblBaseAsset.AutoSize = true;
            this.lblBaseAsset.Location = new System.Drawing.Point(635, 8);
            this.lblBaseAsset.Name = "lblBaseAsset";
            this.lblBaseAsset.Size = new System.Drawing.Size(93, 25);
            this.lblBaseAsset.TabIndex = 2;
            this.lblBaseAsset.Text = "Base Qty";
            this.lblBaseAsset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbQuoteAsset
            // 
            this.cbQuoteAsset.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbQuoteAsset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQuoteAsset.FormattingEnabled = true;
            this.cbQuoteAsset.Location = new System.Drawing.Point(326, 48);
            this.cbQuoteAsset.Name = "cbQuoteAsset";
            this.cbQuoteAsset.Size = new System.Drawing.Size(200, 33);
            this.cbQuoteAsset.TabIndex = 8;
            this.cbQuoteAsset.SelectedIndexChanged += new System.EventHandler(this.cbQuoteAsset_SelectedIndexChanged);
            // 
            // tbBaseQty
            // 
            this.tbBaseQty.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbBaseQty.Location = new System.Drawing.Point(575, 47);
            this.tbBaseQty.Name = "tbBaseQty";
            this.tbBaseQty.Size = new System.Drawing.Size(213, 30);
            this.tbBaseQty.TabIndex = 9;
            this.tbBaseQty.Leave += new System.EventHandler(this.tbBaseQty_Leave);
            // 
            // pnlStatusBar
            // 
            this.pnlStatusBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatusBar.Location = new System.Drawing.Point(0, 422);
            this.pnlStatusBar.Name = "pnlStatusBar";
            this.pnlStatusBar.Size = new System.Drawing.Size(1221, 43);
            this.pnlStatusBar.TabIndex = 5;
            // 
            // pnlBaseAssetInfo
            // 
            this.pnlBaseAssetInfo.Controls.Add(this.lblOpenOrders);
            this.pnlBaseAssetInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBaseAssetInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlBaseAssetInfo.Name = "pnlBaseAssetInfo";
            this.pnlBaseAssetInfo.Size = new System.Drawing.Size(1221, 33);
            this.pnlBaseAssetInfo.TabIndex = 4;
            // 
            // lblOpenOrders
            // 
            this.lblOpenOrders.AutoSize = true;
            this.lblOpenOrders.Location = new System.Drawing.Point(8, 6);
            this.lblOpenOrders.Name = "lblOpenOrders";
            this.lblOpenOrders.Size = new System.Drawing.Size(132, 25);
            this.lblOpenOrders.TabIndex = 2;
            this.lblOpenOrders.Text = "Open Orders:";
            // 
            // tpOrdersHistory
            // 
            this.tpOrdersHistory.Location = new System.Drawing.Point(4, 44);
            this.tpOrdersHistory.Margin = new System.Windows.Forms.Padding(0);
            this.tpOrdersHistory.Name = "tpOrdersHistory";
            this.tpOrdersHistory.Padding = new System.Windows.Forms.Padding(1);
            this.tpOrdersHistory.Size = new System.Drawing.Size(1225, 603);
            this.tpOrdersHistory.TabIndex = 1;
            this.tpOrdersHistory.Text = "Orders History";
            this.tpOrdersHistory.UseVisualStyleBackColor = true;
            // 
            // tpSettings
            // 
            this.tpSettings.Controls.Add(this.lblIPValue);
            this.tpSettings.Controls.Add(this.lblIP);
            this.tpSettings.Controls.Add(this.clbQuoteAssets);
            this.tpSettings.Controls.Add(this.lblBinance);
            this.tpSettings.Controls.Add(this.lblExchange);
            this.tpSettings.Controls.Add(this.lblQuoteAssets);
            this.tpSettings.Controls.Add(this.btnCancelSettings);
            this.tpSettings.Controls.Add(this.btnSaveSettings);
            this.tpSettings.Controls.Add(this.txtSecretKey);
            this.tpSettings.Controls.Add(this.txtApiKey);
            this.tpSettings.Controls.Add(this.lblSecretKey);
            this.tpSettings.Controls.Add(this.lblApiKey);
            this.tpSettings.Location = new System.Drawing.Point(4, 44);
            this.tpSettings.Margin = new System.Windows.Forms.Padding(0);
            this.tpSettings.Name = "tpSettings";
            this.tpSettings.Padding = new System.Windows.Forms.Padding(1);
            this.tpSettings.Size = new System.Drawing.Size(1225, 603);
            this.tpSettings.TabIndex = 2;
            this.tpSettings.Text = "Settings";
            this.tpSettings.UseVisualStyleBackColor = true;
            // 
            // lblIPValue
            // 
            this.lblIPValue.AutoSize = true;
            this.lblIPValue.Location = new System.Drawing.Point(139, 45);
            this.lblIPValue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIPValue.Name = "lblIPValue";
            this.lblIPValue.Size = new System.Drawing.Size(59, 25);
            this.lblIPValue.TabIndex = 18;
            this.lblIPValue.Text = "value";
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Location = new System.Drawing.Point(17, 45);
            this.lblIP.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(108, 25);
            this.lblIP.TabIndex = 17;
            this.lblIP.Text = "IP Address";
            // 
            // clbQuoteAssets
            // 
            this.clbQuoteAssets.ForeColor = System.Drawing.SystemColors.WindowText;
            this.clbQuoteAssets.FormattingEnabled = true;
            this.clbQuoteAssets.Location = new System.Drawing.Point(21, 181);
            this.clbQuoteAssets.MultiColumn = true;
            this.clbQuoteAssets.Name = "clbQuoteAssets";
            this.clbQuoteAssets.Size = new System.Drawing.Size(1122, 354);
            this.clbQuoteAssets.Sorted = true;
            this.clbQuoteAssets.TabIndex = 16;
            // 
            // lblBinance
            // 
            this.lblBinance.AutoSize = true;
            this.lblBinance.Location = new System.Drawing.Point(139, 12);
            this.lblBinance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBinance.Name = "lblBinance";
            this.lblBinance.Size = new System.Drawing.Size(83, 25);
            this.lblBinance.TabIndex = 13;
            this.lblBinance.Text = "Binance";
            // 
            // lblExchange
            // 
            this.lblExchange.AutoSize = true;
            this.lblExchange.Location = new System.Drawing.Point(17, 12);
            this.lblExchange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExchange.Name = "lblExchange";
            this.lblExchange.Size = new System.Drawing.Size(100, 25);
            this.lblExchange.TabIndex = 12;
            this.lblExchange.Text = "Exchange";
            // 
            // lblQuoteAssets
            // 
            this.lblQuoteAssets.AutoSize = true;
            this.lblQuoteAssets.Location = new System.Drawing.Point(17, 158);
            this.lblQuoteAssets.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQuoteAssets.Name = "lblQuoteAssets";
            this.lblQuoteAssets.Size = new System.Drawing.Size(131, 25);
            this.lblQuoteAssets.TabIndex = 11;
            this.lblQuoteAssets.Text = "Quote Assets";
            // 
            // btnCancelSettings
            // 
            this.btnCancelSettings.Location = new System.Drawing.Point(159, 549);
            this.btnCancelSettings.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelSettings.Name = "btnCancelSettings";
            this.btnCancelSettings.Size = new System.Drawing.Size(100, 31);
            this.btnCancelSettings.TabIndex = 9;
            this.btnCancelSettings.Text = "Cancel";
            this.btnCancelSettings.UseVisualStyleBackColor = true;
            this.btnCancelSettings.Click += new System.EventHandler(this.btnCancelSettings_Click);
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Location = new System.Drawing.Point(19, 549);
            this.btnSaveSettings.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(100, 31);
            this.btnSaveSettings.TabIndex = 8;
            this.btnSaveSettings.Text = "Save";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // txtSecretKey
            // 
            this.txtSecretKey.Location = new System.Drawing.Point(144, 111);
            this.txtSecretKey.Margin = new System.Windows.Forms.Padding(4);
            this.txtSecretKey.Name = "txtSecretKey";
            this.txtSecretKey.Size = new System.Drawing.Size(556, 30);
            this.txtSecretKey.TabIndex = 3;
            // 
            // txtApiKey
            // 
            this.txtApiKey.Location = new System.Drawing.Point(144, 74);
            this.txtApiKey.Margin = new System.Windows.Forms.Padding(4);
            this.txtApiKey.Name = "txtApiKey";
            this.txtApiKey.Size = new System.Drawing.Size(556, 30);
            this.txtApiKey.TabIndex = 2;
            // 
            // lblSecretKey
            // 
            this.lblSecretKey.AutoSize = true;
            this.lblSecretKey.Location = new System.Drawing.Point(17, 114);
            this.lblSecretKey.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSecretKey.Name = "lblSecretKey";
            this.lblSecretKey.Size = new System.Drawing.Size(109, 25);
            this.lblSecretKey.TabIndex = 1;
            this.lblSecretKey.Text = "Secret Key";
            // 
            // lblApiKey
            // 
            this.lblApiKey.AutoSize = true;
            this.lblApiKey.Location = new System.Drawing.Point(17, 77);
            this.lblApiKey.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblApiKey.Name = "lblApiKey";
            this.lblApiKey.Size = new System.Drawing.Size(81, 25);
            this.lblApiKey.TabIndex = 0;
            this.lblApiKey.Text = "Api Key";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1233, 651);
            this.Controls.Add(this.tcSections);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.Text = "CryptoSwapMaster";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tcSections.ResumeLayout(false);
            this.tpDashboard.ResumeLayout(false);
            this.splitDashboard.Panel1.ResumeLayout(false);
            this.splitDashboard.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitDashboard)).EndInit();
            this.splitDashboard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgBalance)).EndInit();
            this.pnlOpenOrders.ResumeLayout(false);
            this.tableOpenOrders.ResumeLayout(false);
            this.tableOpenOrders.PerformLayout();
            this.pnlBaseAssetInfo.ResumeLayout(false);
            this.pnlBaseAssetInfo.PerformLayout();
            this.tpSettings.ResumeLayout(false);
            this.tpSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcSections;
        private System.Windows.Forms.TabPage tpDashboard;
        private System.Windows.Forms.TabPage tpOrdersHistory;
        private System.Windows.Forms.TabPage tpSettings;
        private System.Windows.Forms.SplitContainer splitDashboard;
        private System.Windows.Forms.DataGridView dgBalance;
        private System.Windows.Forms.Label lblQuoteAssets;
        private System.Windows.Forms.Button btnCancelSettings;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.TextBox txtSecretKey;
        private System.Windows.Forms.TextBox txtApiKey;
        private System.Windows.Forms.Label lblSecretKey;
        private System.Windows.Forms.Label lblApiKey;
        private System.Windows.Forms.Label lblExchange;
        private System.Windows.Forms.Label lblBinance;
        private System.Windows.Forms.CheckedListBox clbQuoteAssets;
        private System.Windows.Forms.Label lblIPValue;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Panel pnlOpenOrders;
        private System.Windows.Forms.Panel pnlBaseAssetInfo;
        private System.Windows.Forms.Panel pnlStatusBar;
        private System.Windows.Forms.TableLayoutPanel tableOpenOrders;
        private System.Windows.Forms.Label lblOpenOrders;
        private System.Windows.Forms.Label lblPool;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.Label lblQuoteQty;
        private System.Windows.Forms.Label lblQuoteAsset;
        private System.Windows.Forms.Label lblBaseAsset;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.ComboBox cbPool;
        private System.Windows.Forms.TextBox tbQuoteQty;
        private System.Windows.Forms.ComboBox cbGroup;
        private System.Windows.Forms.ComboBox cbQuoteAsset;
        private System.Windows.Forms.TextBox tbBaseQty;
        private System.Windows.Forms.Button btnAdd;
    }
}