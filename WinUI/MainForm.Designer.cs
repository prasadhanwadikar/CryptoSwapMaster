namespace CryptoSwapMaster.WinUI
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tcSections = new System.Windows.Forms.TabControl();
            this.tpDashboard = new System.Windows.Forms.TabPage();
            this.splitDashboard = new System.Windows.Forms.SplitContainer();
            this.dgBalance = new System.Windows.Forms.DataGridView();
            this.pnlOrders = new System.Windows.Forms.Panel();
            this.pnlOpenOrders = new System.Windows.Forms.Panel();
            this.dgOpenOrders = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.poolDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baseAssetDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baseQtyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExecutedBaseQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quoteAssetDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expectedQuoteQtyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receivedQuoteQtyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusMsgDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastModifiedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Action = new System.Windows.Forms.DataGridViewButtonColumn();
            this.openOrdersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pnlNewOrder = new System.Windows.Forms.Panel();
            this.tableOpenOrders = new System.Windows.Forms.TableLayoutPanel();
            this.cbPool = new System.Windows.Forms.ComboBox();
            this.tbQuoteQty = new System.Windows.Forms.TextBox();
            this.lblAction = new System.Windows.Forms.Label();
            this.lblQuoteQty = new System.Windows.Forms.Label();
            this.lblPool = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblBaseQty = new System.Windows.Forms.Label();
            this.lblQuoteAsset = new System.Windows.Forms.Label();
            this.tbBaseQty = new System.Windows.Forms.TextBox();
            this.cbQuoteAsset = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.pnlBaseAssetInfo = new System.Windows.Forms.Panel();
            this.btnChangeBotStatus = new System.Windows.Forms.Button();
            this.lblOpenOrders = new System.Windows.Forms.Label();
            this.lblFreeBalValue = new System.Windows.Forms.Label();
            this.lblFreeBal = new System.Windows.Forms.Label();
            this.cbBaseAsset = new System.Windows.Forms.ComboBox();
            this.lblBaseAsset = new System.Windows.Forms.Label();
            this.tpOrdersHistory = new System.Windows.Forms.TabPage();
            this.pnlOrdersHistory = new System.Windows.Forms.Panel();
            this.dgOrdersHistory = new System.Windows.Forms.DataGridView();
            this.baseAssetDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.poolDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.baseQtyDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quoteAssetDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expectedQuoteQtyDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.receivedQuoteQtyDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusMsgDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.createdDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastModifiedDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ordersHistoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.pnlFilters = new System.Windows.Forms.Panel();
            this.cbOrderStatus = new System.Windows.Forms.ComboBox();
            this.lblOrderStatus = new System.Windows.Forms.Label();
            this.cbBaseAsset2 = new System.Windows.Forms.ComboBox();
            this.lblBaseAsset2 = new System.Windows.Forms.Label();
            this.btnRefreshOrdersHistory = new System.Windows.Forms.Button();
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
            this.pnlStatusBar = new System.Windows.Forms.Panel();
            this.pnlTabControl = new System.Windows.Forms.Panel();
            this.tcSections.SuspendLayout();
            this.tpDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitDashboard)).BeginInit();
            this.splitDashboard.Panel1.SuspendLayout();
            this.splitDashboard.Panel2.SuspendLayout();
            this.splitDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgBalance)).BeginInit();
            this.pnlOrders.SuspendLayout();
            this.pnlOpenOrders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOpenOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.openOrdersBindingSource)).BeginInit();
            this.pnlNewOrder.SuspendLayout();
            this.tableOpenOrders.SuspendLayout();
            this.pnlBaseAssetInfo.SuspendLayout();
            this.tpOrdersHistory.SuspendLayout();
            this.pnlOrdersHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOrdersHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ordersHistoryBindingSource)).BeginInit();
            this.pnlFilters.SuspendLayout();
            this.tpSettings.SuspendLayout();
            this.pnlTabControl.SuspendLayout();
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
            this.tcSections.Size = new System.Drawing.Size(1233, 608);
            this.tcSections.TabIndex = 0;
            // 
            // tpDashboard
            // 
            this.tpDashboard.Controls.Add(this.splitDashboard);
            this.tpDashboard.Location = new System.Drawing.Point(4, 44);
            this.tpDashboard.Margin = new System.Windows.Forms.Padding(0);
            this.tpDashboard.Name = "tpDashboard";
            this.tpDashboard.Padding = new System.Windows.Forms.Padding(2);
            this.tpDashboard.Size = new System.Drawing.Size(1225, 560);
            this.tpDashboard.TabIndex = 0;
            this.tpDashboard.Text = "Dashboard";
            this.tpDashboard.UseVisualStyleBackColor = true;
            // 
            // splitDashboard
            // 
            this.splitDashboard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitDashboard.Location = new System.Drawing.Point(2, 2);
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
            this.splitDashboard.Panel2.Controls.Add(this.pnlOrders);
            this.splitDashboard.Panel2.Controls.Add(this.pnlBaseAssetInfo);
            this.splitDashboard.Panel2.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.splitDashboard.Size = new System.Drawing.Size(1221, 556);
            this.splitDashboard.SplitterDistance = 157;
            this.splitDashboard.SplitterWidth = 1;
            this.splitDashboard.TabIndex = 10;
            // 
            // dgBalance
            // 
            this.dgBalance.AllowUserToAddRows = false;
            this.dgBalance.AllowUserToDeleteRows = false;
            this.dgBalance.AllowUserToOrderColumns = true;
            this.dgBalance.AllowUserToResizeRows = false;
            this.dgBalance.BackgroundColor = System.Drawing.Color.White;
            this.dgBalance.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgBalance.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgBalance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgBalance.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgBalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgBalance.GridColor = System.Drawing.Color.White;
            this.dgBalance.Location = new System.Drawing.Point(0, 0);
            this.dgBalance.Margin = new System.Windows.Forms.Padding(4);
            this.dgBalance.MultiSelect = false;
            this.dgBalance.Name = "dgBalance";
            this.dgBalance.ReadOnly = true;
            this.dgBalance.RowHeadersVisible = false;
            this.dgBalance.RowTemplate.Height = 28;
            this.dgBalance.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgBalance.Size = new System.Drawing.Size(1219, 155);
            this.dgBalance.TabIndex = 7;
            // 
            // pnlOrders
            // 
            this.pnlOrders.AutoScroll = true;
            this.pnlOrders.Controls.Add(this.pnlOpenOrders);
            this.pnlOrders.Controls.Add(this.pnlNewOrder);
            this.pnlOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOrders.Location = new System.Drawing.Point(5, 79);
            this.pnlOrders.Name = "pnlOrders";
            this.pnlOrders.Size = new System.Drawing.Size(1214, 317);
            this.pnlOrders.TabIndex = 11;
            // 
            // pnlOpenOrders
            // 
            this.pnlOpenOrders.Controls.Add(this.dgOpenOrders);
            this.pnlOpenOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOpenOrders.Location = new System.Drawing.Point(0, 66);
            this.pnlOpenOrders.Name = "pnlOpenOrders";
            this.pnlOpenOrders.Size = new System.Drawing.Size(1214, 251);
            this.pnlOpenOrders.TabIndex = 13;
            // 
            // dgOpenOrders
            // 
            this.dgOpenOrders.AllowUserToAddRows = false;
            this.dgOpenOrders.AllowUserToDeleteRows = false;
            this.dgOpenOrders.AllowUserToResizeColumns = false;
            this.dgOpenOrders.AllowUserToResizeRows = false;
            this.dgOpenOrders.AutoGenerateColumns = false;
            this.dgOpenOrders.BackgroundColor = System.Drawing.Color.White;
            this.dgOpenOrders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgOpenOrders.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgOpenOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgOpenOrders.ColumnHeadersVisible = false;
            this.dgOpenOrders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.userIdDataGridViewTextBoxColumn,
            this.poolDataGridViewTextBoxColumn,
            this.dataGridViewTextBoxColumn2,
            this.baseAssetDataGridViewTextBoxColumn,
            this.baseQtyDataGridViewTextBoxColumn,
            this.ExecutedBaseQty,
            this.quoteAssetDataGridViewTextBoxColumn,
            this.expectedQuoteQtyDataGridViewTextBoxColumn,
            this.receivedQuoteQtyDataGridViewTextBoxColumn,
            this.statusDataGridViewTextBoxColumn,
            this.statusMsgDataGridViewTextBoxColumn,
            this.createdDataGridViewTextBoxColumn,
            this.lastModifiedDataGridViewTextBoxColumn,
            this.Action});
            this.dgOpenOrders.DataSource = this.openOrdersBindingSource;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgOpenOrders.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgOpenOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgOpenOrders.GridColor = System.Drawing.Color.White;
            this.dgOpenOrders.Location = new System.Drawing.Point(0, 0);
            this.dgOpenOrders.Margin = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.dgOpenOrders.MultiSelect = false;
            this.dgOpenOrders.Name = "dgOpenOrders";
            this.dgOpenOrders.ReadOnly = true;
            this.dgOpenOrders.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dgOpenOrders.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgOpenOrders.RowTemplate.Height = 28;
            this.dgOpenOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgOpenOrders.Size = new System.Drawing.Size(1214, 251);
            this.dgOpenOrders.TabIndex = 8;
            this.dgOpenOrders.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgOpenOrders_CellClick);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            this.idDataGridViewTextBoxColumn.Visible = false;
            // 
            // userIdDataGridViewTextBoxColumn
            // 
            this.userIdDataGridViewTextBoxColumn.DataPropertyName = "UserId";
            this.userIdDataGridViewTextBoxColumn.HeaderText = "UserId";
            this.userIdDataGridViewTextBoxColumn.Name = "userIdDataGridViewTextBoxColumn";
            this.userIdDataGridViewTextBoxColumn.ReadOnly = true;
            this.userIdDataGridViewTextBoxColumn.Visible = false;
            // 
            // poolDataGridViewTextBoxColumn
            // 
            this.poolDataGridViewTextBoxColumn.DataPropertyName = "Pool";
            this.poolDataGridViewTextBoxColumn.HeaderText = "Pool";
            this.poolDataGridViewTextBoxColumn.MinimumWidth = 122;
            this.poolDataGridViewTextBoxColumn.Name = "poolDataGridViewTextBoxColumn";
            this.poolDataGridViewTextBoxColumn.ReadOnly = true;
            this.poolDataGridViewTextBoxColumn.Width = 122;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Type";
            this.dataGridViewTextBoxColumn2.HeaderText = "Type";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 151;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 151;
            // 
            // baseAssetDataGridViewTextBoxColumn
            // 
            this.baseAssetDataGridViewTextBoxColumn.DataPropertyName = "BaseAsset";
            this.baseAssetDataGridViewTextBoxColumn.HeaderText = "BaseAsset";
            this.baseAssetDataGridViewTextBoxColumn.Name = "baseAssetDataGridViewTextBoxColumn";
            this.baseAssetDataGridViewTextBoxColumn.ReadOnly = true;
            this.baseAssetDataGridViewTextBoxColumn.Visible = false;
            // 
            // baseQtyDataGridViewTextBoxColumn
            // 
            this.baseQtyDataGridViewTextBoxColumn.DataPropertyName = "BaseQty";
            this.baseQtyDataGridViewTextBoxColumn.HeaderText = "Base Qty";
            this.baseQtyDataGridViewTextBoxColumn.MinimumWidth = 181;
            this.baseQtyDataGridViewTextBoxColumn.Name = "baseQtyDataGridViewTextBoxColumn";
            this.baseQtyDataGridViewTextBoxColumn.ReadOnly = true;
            this.baseQtyDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.baseQtyDataGridViewTextBoxColumn.Width = 181;
            // 
            // ExecutedBaseQty
            // 
            this.ExecutedBaseQty.DataPropertyName = "ExecutedBaseQty";
            this.ExecutedBaseQty.HeaderText = "Executed Base Qty";
            this.ExecutedBaseQty.Name = "ExecutedBaseQty";
            this.ExecutedBaseQty.ReadOnly = true;
            this.ExecutedBaseQty.Visible = false;
            // 
            // quoteAssetDataGridViewTextBoxColumn
            // 
            this.quoteAssetDataGridViewTextBoxColumn.DataPropertyName = "QuoteAsset";
            this.quoteAssetDataGridViewTextBoxColumn.HeaderText = "Quote Asset";
            this.quoteAssetDataGridViewTextBoxColumn.MinimumWidth = 151;
            this.quoteAssetDataGridViewTextBoxColumn.Name = "quoteAssetDataGridViewTextBoxColumn";
            this.quoteAssetDataGridViewTextBoxColumn.ReadOnly = true;
            this.quoteAssetDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.quoteAssetDataGridViewTextBoxColumn.Width = 151;
            // 
            // expectedQuoteQtyDataGridViewTextBoxColumn
            // 
            this.expectedQuoteQtyDataGridViewTextBoxColumn.DataPropertyName = "ExpectedQuoteQty";
            this.expectedQuoteQtyDataGridViewTextBoxColumn.HeaderText = "Quote Qty";
            this.expectedQuoteQtyDataGridViewTextBoxColumn.MinimumWidth = 181;
            this.expectedQuoteQtyDataGridViewTextBoxColumn.Name = "expectedQuoteQtyDataGridViewTextBoxColumn";
            this.expectedQuoteQtyDataGridViewTextBoxColumn.ReadOnly = true;
            this.expectedQuoteQtyDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.expectedQuoteQtyDataGridViewTextBoxColumn.Width = 181;
            // 
            // receivedQuoteQtyDataGridViewTextBoxColumn
            // 
            this.receivedQuoteQtyDataGridViewTextBoxColumn.DataPropertyName = "ReceivedQuoteQty";
            this.receivedQuoteQtyDataGridViewTextBoxColumn.HeaderText = "ReceivedQuoteQty";
            this.receivedQuoteQtyDataGridViewTextBoxColumn.Name = "receivedQuoteQtyDataGridViewTextBoxColumn";
            this.receivedQuoteQtyDataGridViewTextBoxColumn.ReadOnly = true;
            this.receivedQuoteQtyDataGridViewTextBoxColumn.Visible = false;
            // 
            // statusDataGridViewTextBoxColumn
            // 
            this.statusDataGridViewTextBoxColumn.DataPropertyName = "Status";
            this.statusDataGridViewTextBoxColumn.HeaderText = "Status";
            this.statusDataGridViewTextBoxColumn.Name = "statusDataGridViewTextBoxColumn";
            this.statusDataGridViewTextBoxColumn.ReadOnly = true;
            this.statusDataGridViewTextBoxColumn.Visible = false;
            // 
            // statusMsgDataGridViewTextBoxColumn
            // 
            this.statusMsgDataGridViewTextBoxColumn.DataPropertyName = "StatusMsg";
            this.statusMsgDataGridViewTextBoxColumn.HeaderText = "StatusMsg";
            this.statusMsgDataGridViewTextBoxColumn.Name = "statusMsgDataGridViewTextBoxColumn";
            this.statusMsgDataGridViewTextBoxColumn.ReadOnly = true;
            this.statusMsgDataGridViewTextBoxColumn.Visible = false;
            // 
            // createdDataGridViewTextBoxColumn
            // 
            this.createdDataGridViewTextBoxColumn.DataPropertyName = "Created";
            this.createdDataGridViewTextBoxColumn.HeaderText = "Created";
            this.createdDataGridViewTextBoxColumn.Name = "createdDataGridViewTextBoxColumn";
            this.createdDataGridViewTextBoxColumn.ReadOnly = true;
            this.createdDataGridViewTextBoxColumn.Visible = false;
            // 
            // lastModifiedDataGridViewTextBoxColumn
            // 
            this.lastModifiedDataGridViewTextBoxColumn.DataPropertyName = "LastModified";
            this.lastModifiedDataGridViewTextBoxColumn.HeaderText = "LastModified";
            this.lastModifiedDataGridViewTextBoxColumn.Name = "lastModifiedDataGridViewTextBoxColumn";
            this.lastModifiedDataGridViewTextBoxColumn.ReadOnly = true;
            this.lastModifiedDataGridViewTextBoxColumn.Visible = false;
            // 
            // Action
            // 
            this.Action.DataPropertyName = "Id";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            this.Action.DefaultCellStyle = dataGridViewCellStyle2;
            this.Action.HeaderText = "Action";
            this.Action.MinimumWidth = 116;
            this.Action.Name = "Action";
            this.Action.ReadOnly = true;
            this.Action.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Action.Text = "Remove";
            this.Action.UseColumnTextForButtonValue = true;
            this.Action.Width = 116;
            // 
            // openOrdersBindingSource
            // 
            this.openOrdersBindingSource.DataSource = typeof(CryptoSwapMaster.Data.Entities.Order);
            // 
            // pnlNewOrder
            // 
            this.pnlNewOrder.Controls.Add(this.tableOpenOrders);
            this.pnlNewOrder.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNewOrder.Location = new System.Drawing.Point(0, 0);
            this.pnlNewOrder.Name = "pnlNewOrder";
            this.pnlNewOrder.Size = new System.Drawing.Size(1214, 66);
            this.pnlNewOrder.TabIndex = 5;
            // 
            // tableOpenOrders
            // 
            this.tableOpenOrders.AutoSize = true;
            this.tableOpenOrders.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableOpenOrders.ColumnCount = 6;
            this.tableOpenOrders.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableOpenOrders.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableOpenOrders.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableOpenOrders.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableOpenOrders.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tableOpenOrders.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tableOpenOrders.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableOpenOrders.Controls.Add(this.cbPool, 0, 1);
            this.tableOpenOrders.Controls.Add(this.tbQuoteQty, 4, 1);
            this.tableOpenOrders.Controls.Add(this.lblAction, 5, 0);
            this.tableOpenOrders.Controls.Add(this.lblQuoteQty, 4, 0);
            this.tableOpenOrders.Controls.Add(this.lblPool, 0, 0);
            this.tableOpenOrders.Controls.Add(this.btnAdd, 5, 1);
            this.tableOpenOrders.Controls.Add(this.lblBaseQty, 2, 0);
            this.tableOpenOrders.Controls.Add(this.lblQuoteAsset, 3, 0);
            this.tableOpenOrders.Controls.Add(this.tbBaseQty, 2, 1);
            this.tableOpenOrders.Controls.Add(this.cbQuoteAsset, 3, 1);
            this.tableOpenOrders.Controls.Add(this.lblType, 1, 0);
            this.tableOpenOrders.Controls.Add(this.cbType, 1, 1);
            this.tableOpenOrders.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableOpenOrders.Location = new System.Drawing.Point(0, 0);
            this.tableOpenOrders.Margin = new System.Windows.Forms.Padding(0);
            this.tableOpenOrders.Name = "tableOpenOrders";
            this.tableOpenOrders.RowCount = 2;
            this.tableOpenOrders.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableOpenOrders.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableOpenOrders.Size = new System.Drawing.Size(903, 66);
            this.tableOpenOrders.TabIndex = 6;
            // 
            // cbPool
            // 
            this.cbPool.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbPool.BackColor = System.Drawing.Color.White;
            this.cbPool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPool.FormattingEnabled = true;
            this.cbPool.Location = new System.Drawing.Point(4, 32);
            this.cbPool.Name = "cbPool";
            this.cbPool.Size = new System.Drawing.Size(114, 33);
            this.cbPool.TabIndex = 2;
            this.cbPool.SelectedIndexChanged += new System.EventHandler(this.cbPool_SelectedIndexChanged);
            // 
            // tbQuoteQty
            // 
            this.tbQuoteQty.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbQuoteQty.Location = new System.Drawing.Point(608, 31);
            this.tbQuoteQty.Name = "tbQuoteQty";
            this.tbQuoteQty.Size = new System.Drawing.Size(174, 30);
            this.tbQuoteQty.TabIndex = 6;
            this.tbQuoteQty.Leave += new System.EventHandler(this.tbQuoteQty_Leave);
            // 
            // lblAction
            // 
            this.lblAction.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblAction.AutoSize = true;
            this.lblAction.Location = new System.Drawing.Point(789, 1);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(67, 25);
            this.lblAction.TabIndex = 0;
            this.lblAction.Text = "Action";
            this.lblAction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblQuoteQty
            // 
            this.lblQuoteQty.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblQuoteQty.AutoSize = true;
            this.lblQuoteQty.Location = new System.Drawing.Point(608, 1);
            this.lblQuoteQty.Name = "lblQuoteQty";
            this.lblQuoteQty.Size = new System.Drawing.Size(102, 25);
            this.lblQuoteQty.TabIndex = 0;
            this.lblQuoteQty.Text = "Quote Qty";
            this.lblQuoteQty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPool
            // 
            this.lblPool.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPool.AutoSize = true;
            this.lblPool.Location = new System.Drawing.Point(4, 1);
            this.lblPool.Name = "lblPool";
            this.lblPool.Size = new System.Drawing.Size(51, 25);
            this.lblPool.TabIndex = 0;
            this.lblPool.Text = "Pool";
            this.lblPool.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAdd
            // 
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAdd.Location = new System.Drawing.Point(789, 30);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(110, 32);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Add";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblBaseQty
            // 
            this.lblBaseQty.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblBaseQty.AutoSize = true;
            this.lblBaseQty.Location = new System.Drawing.Point(276, 1);
            this.lblBaseQty.Name = "lblBaseQty";
            this.lblBaseQty.Size = new System.Drawing.Size(93, 25);
            this.lblBaseQty.TabIndex = 0;
            this.lblBaseQty.Text = "Base Qty";
            this.lblBaseQty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblQuoteAsset
            // 
            this.lblQuoteAsset.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblQuoteAsset.AutoSize = true;
            this.lblQuoteAsset.Location = new System.Drawing.Point(457, 1);
            this.lblQuoteAsset.Name = "lblQuoteAsset";
            this.lblQuoteAsset.Size = new System.Drawing.Size(121, 25);
            this.lblQuoteAsset.TabIndex = 0;
            this.lblQuoteAsset.Text = "Quote Asset";
            this.lblQuoteAsset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbBaseQty
            // 
            this.tbBaseQty.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbBaseQty.Location = new System.Drawing.Point(276, 31);
            this.tbBaseQty.Name = "tbBaseQty";
            this.tbBaseQty.Size = new System.Drawing.Size(174, 30);
            this.tbBaseQty.TabIndex = 4;
            this.tbBaseQty.Leave += new System.EventHandler(this.tbBaseQty_Leave);
            // 
            // cbQuoteAsset
            // 
            this.cbQuoteAsset.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbQuoteAsset.BackColor = System.Drawing.Color.White;
            this.cbQuoteAsset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQuoteAsset.FormattingEnabled = true;
            this.cbQuoteAsset.Location = new System.Drawing.Point(457, 32);
            this.cbQuoteAsset.Name = "cbQuoteAsset";
            this.cbQuoteAsset.Size = new System.Drawing.Size(144, 33);
            this.cbQuoteAsset.TabIndex = 5;
            this.cbQuoteAsset.SelectedIndexChanged += new System.EventHandler(this.cbQuoteAsset_SelectedIndexChanged);
            // 
            // lblType
            // 
            this.lblType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(125, 1);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(57, 25);
            this.lblType.TabIndex = 8;
            this.lblType.Text = "Type";
            this.lblType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbType
            // 
            this.cbType.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbType.BackColor = System.Drawing.Color.White;
            this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[] {
            "Limit",
            "Stop Loss"});
            this.cbType.Location = new System.Drawing.Point(125, 32);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(144, 33);
            this.cbType.TabIndex = 9;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            // 
            // pnlBaseAssetInfo
            // 
            this.pnlBaseAssetInfo.Controls.Add(this.btnChangeBotStatus);
            this.pnlBaseAssetInfo.Controls.Add(this.lblOpenOrders);
            this.pnlBaseAssetInfo.Controls.Add(this.lblFreeBalValue);
            this.pnlBaseAssetInfo.Controls.Add(this.lblFreeBal);
            this.pnlBaseAssetInfo.Controls.Add(this.cbBaseAsset);
            this.pnlBaseAssetInfo.Controls.Add(this.lblBaseAsset);
            this.pnlBaseAssetInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBaseAssetInfo.Location = new System.Drawing.Point(5, 0);
            this.pnlBaseAssetInfo.Name = "pnlBaseAssetInfo";
            this.pnlBaseAssetInfo.Size = new System.Drawing.Size(1214, 79);
            this.pnlBaseAssetInfo.TabIndex = 9;
            // 
            // btnChangeBotStatus
            // 
            this.btnChangeBotStatus.Enabled = false;
            this.btnChangeBotStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangeBotStatus.ForeColor = System.Drawing.Color.Blue;
            this.btnChangeBotStatus.Location = new System.Drawing.Point(730, 20);
            this.btnChangeBotStatus.Margin = new System.Windows.Forms.Padding(4);
            this.btnChangeBotStatus.Name = "btnChangeBotStatus";
            this.btnChangeBotStatus.Size = new System.Drawing.Size(172, 42);
            this.btnChangeBotStatus.TabIndex = 21;
            this.btnChangeBotStatus.Text = "BOT: Loading ...";
            this.btnChangeBotStatus.UseVisualStyleBackColor = true;
            this.btnChangeBotStatus.Click += new System.EventHandler(this.btnChangeBotStatus_Click);
            // 
            // lblOpenOrders
            // 
            this.lblOpenOrders.AutoSize = true;
            this.lblOpenOrders.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpenOrders.Location = new System.Drawing.Point(-1, 6);
            this.lblOpenOrders.Name = "lblOpenOrders";
            this.lblOpenOrders.Size = new System.Drawing.Size(137, 26);
            this.lblOpenOrders.TabIndex = 0;
            this.lblOpenOrders.Text = "Open Orders";
            // 
            // lblFreeBalValue
            // 
            this.lblFreeBalValue.AutoSize = true;
            this.lblFreeBalValue.Location = new System.Drawing.Point(335, 46);
            this.lblFreeBalValue.Name = "lblFreeBalValue";
            this.lblFreeBalValue.Size = new System.Drawing.Size(39, 25);
            this.lblFreeBalValue.TabIndex = 0;
            this.lblFreeBalValue.Text = "0.0";
            // 
            // lblFreeBal
            // 
            this.lblFreeBal.AutoSize = true;
            this.lblFreeBal.Location = new System.Drawing.Point(241, 46);
            this.lblFreeBal.Name = "lblFreeBal";
            this.lblFreeBal.Size = new System.Drawing.Size(139, 25);
            this.lblFreeBal.TabIndex = 0;
            this.lblFreeBal.Text = "Free Balance: ";
            // 
            // cbBaseAsset
            // 
            this.cbBaseAsset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBaseAsset.DropDownWidth = 100;
            this.cbBaseAsset.FormattingEnabled = true;
            this.cbBaseAsset.Location = new System.Drawing.Point(86, 43);
            this.cbBaseAsset.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.cbBaseAsset.Name = "cbBaseAsset";
            this.cbBaseAsset.Size = new System.Drawing.Size(135, 33);
            this.cbBaseAsset.TabIndex = 1;
            this.cbBaseAsset.SelectedIndexChanged += new System.EventHandler(this.cbBaseAsset_SelectedIndexChanged);
            // 
            // lblBaseAsset
            // 
            this.lblBaseAsset.AutoSize = true;
            this.lblBaseAsset.Location = new System.Drawing.Point(1, 46);
            this.lblBaseAsset.Name = "lblBaseAsset";
            this.lblBaseAsset.Size = new System.Drawing.Size(123, 25);
            this.lblBaseAsset.TabIndex = 0;
            this.lblBaseAsset.Text = "Base Asset: ";
            // 
            // tpOrdersHistory
            // 
            this.tpOrdersHistory.Controls.Add(this.pnlOrdersHistory);
            this.tpOrdersHistory.Controls.Add(this.pnlFilters);
            this.tpOrdersHistory.Location = new System.Drawing.Point(4, 44);
            this.tpOrdersHistory.Margin = new System.Windows.Forms.Padding(0);
            this.tpOrdersHistory.Name = "tpOrdersHistory";
            this.tpOrdersHistory.Padding = new System.Windows.Forms.Padding(1);
            this.tpOrdersHistory.Size = new System.Drawing.Size(1225, 560);
            this.tpOrdersHistory.TabIndex = 1;
            this.tpOrdersHistory.Text = "Orders History";
            this.tpOrdersHistory.UseVisualStyleBackColor = true;
            // 
            // pnlOrdersHistory
            // 
            this.pnlOrdersHistory.Controls.Add(this.dgOrdersHistory);
            this.pnlOrdersHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOrdersHistory.Location = new System.Drawing.Point(1, 59);
            this.pnlOrdersHistory.Name = "pnlOrdersHistory";
            this.pnlOrdersHistory.Size = new System.Drawing.Size(1223, 500);
            this.pnlOrdersHistory.TabIndex = 1;
            // 
            // dgOrdersHistory
            // 
            this.dgOrdersHistory.AllowUserToAddRows = false;
            this.dgOrdersHistory.AllowUserToDeleteRows = false;
            this.dgOrdersHistory.AllowUserToResizeRows = false;
            this.dgOrdersHistory.AutoGenerateColumns = false;
            this.dgOrdersHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgOrdersHistory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgOrdersHistory.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            this.dgOrdersHistory.ColumnHeadersHeight = 30;
            this.dgOrdersHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgOrdersHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.baseAssetDataGridViewTextBoxColumn1,
            this.poolDataGridViewTextBoxColumn1,
            this.Type,
            this.baseQtyDataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn1,
            this.quoteAssetDataGridViewTextBoxColumn1,
            this.expectedQuoteQtyDataGridViewTextBoxColumn1,
            this.receivedQuoteQtyDataGridViewTextBoxColumn1,
            this.statusDataGridViewTextBoxColumn1,
            this.statusMsgDataGridViewTextBoxColumn1,
            this.createdDataGridViewTextBoxColumn1,
            this.lastModifiedDataGridViewTextBoxColumn1});
            this.dgOrdersHistory.DataSource = this.ordersHistoryBindingSource;
            this.dgOrdersHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgOrdersHistory.Location = new System.Drawing.Point(0, 0);
            this.dgOrdersHistory.MultiSelect = false;
            this.dgOrdersHistory.Name = "dgOrdersHistory";
            this.dgOrdersHistory.RowHeadersVisible = false;
            this.dgOrdersHistory.RowTemplate.Height = 28;
            this.dgOrdersHistory.Size = new System.Drawing.Size(1223, 500);
            this.dgOrdersHistory.TabIndex = 0;
            // 
            // baseAssetDataGridViewTextBoxColumn1
            // 
            this.baseAssetDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.baseAssetDataGridViewTextBoxColumn1.DataPropertyName = "BaseAsset";
            this.baseAssetDataGridViewTextBoxColumn1.Frozen = true;
            this.baseAssetDataGridViewTextBoxColumn1.HeaderText = "Base Asset";
            this.baseAssetDataGridViewTextBoxColumn1.Name = "baseAssetDataGridViewTextBoxColumn1";
            this.baseAssetDataGridViewTextBoxColumn1.ReadOnly = true;
            this.baseAssetDataGridViewTextBoxColumn1.Width = 148;
            // 
            // poolDataGridViewTextBoxColumn1
            // 
            this.poolDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.poolDataGridViewTextBoxColumn1.DataPropertyName = "Pool";
            this.poolDataGridViewTextBoxColumn1.Frozen = true;
            this.poolDataGridViewTextBoxColumn1.HeaderText = "Pool";
            this.poolDataGridViewTextBoxColumn1.Name = "poolDataGridViewTextBoxColumn1";
            this.poolDataGridViewTextBoxColumn1.ReadOnly = true;
            this.poolDataGridViewTextBoxColumn1.Width = 87;
            // 
            // Type
            // 
            this.Type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Type.DataPropertyName = "Type";
            this.Type.Frozen = true;
            this.Type.HeaderText = "Type";
            this.Type.MinimumWidth = 50;
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            this.Type.Width = 93;
            // 
            // baseQtyDataGridViewTextBoxColumn1
            // 
            this.baseQtyDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.baseQtyDataGridViewTextBoxColumn1.DataPropertyName = "BaseQty";
            this.baseQtyDataGridViewTextBoxColumn1.HeaderText = "Base Qty";
            this.baseQtyDataGridViewTextBoxColumn1.Name = "baseQtyDataGridViewTextBoxColumn1";
            this.baseQtyDataGridViewTextBoxColumn1.ReadOnly = true;
            this.baseQtyDataGridViewTextBoxColumn1.Width = 129;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.dataGridViewTextBoxColumn1.DataPropertyName = "ExecutedBaseQty";
            this.dataGridViewTextBoxColumn1.HeaderText = "Executed Base Qty";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 216;
            // 
            // quoteAssetDataGridViewTextBoxColumn1
            // 
            this.quoteAssetDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.quoteAssetDataGridViewTextBoxColumn1.DataPropertyName = "QuoteAsset";
            this.quoteAssetDataGridViewTextBoxColumn1.HeaderText = "Quote Asset";
            this.quoteAssetDataGridViewTextBoxColumn1.Name = "quoteAssetDataGridViewTextBoxColumn1";
            this.quoteAssetDataGridViewTextBoxColumn1.ReadOnly = true;
            this.quoteAssetDataGridViewTextBoxColumn1.Width = 157;
            // 
            // expectedQuoteQtyDataGridViewTextBoxColumn1
            // 
            this.expectedQuoteQtyDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.expectedQuoteQtyDataGridViewTextBoxColumn1.DataPropertyName = "ExpectedQuoteQty";
            this.expectedQuoteQtyDataGridViewTextBoxColumn1.HeaderText = "Expected Quote Qty";
            this.expectedQuoteQtyDataGridViewTextBoxColumn1.Name = "expectedQuoteQtyDataGridViewTextBoxColumn1";
            this.expectedQuoteQtyDataGridViewTextBoxColumn1.ReadOnly = true;
            this.expectedQuoteQtyDataGridViewTextBoxColumn1.Width = 225;
            // 
            // receivedQuoteQtyDataGridViewTextBoxColumn1
            // 
            this.receivedQuoteQtyDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.receivedQuoteQtyDataGridViewTextBoxColumn1.DataPropertyName = "ReceivedQuoteQty";
            this.receivedQuoteQtyDataGridViewTextBoxColumn1.HeaderText = "Received Quote Qty";
            this.receivedQuoteQtyDataGridViewTextBoxColumn1.Name = "receivedQuoteQtyDataGridViewTextBoxColumn1";
            this.receivedQuoteQtyDataGridViewTextBoxColumn1.ReadOnly = true;
            this.receivedQuoteQtyDataGridViewTextBoxColumn1.Width = 224;
            // 
            // statusDataGridViewTextBoxColumn1
            // 
            this.statusDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.statusDataGridViewTextBoxColumn1.DataPropertyName = "Status";
            this.statusDataGridViewTextBoxColumn1.HeaderText = "Status";
            this.statusDataGridViewTextBoxColumn1.Name = "statusDataGridViewTextBoxColumn1";
            this.statusDataGridViewTextBoxColumn1.ReadOnly = true;
            this.statusDataGridViewTextBoxColumn1.Width = 104;
            // 
            // statusMsgDataGridViewTextBoxColumn1
            // 
            this.statusMsgDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.statusMsgDataGridViewTextBoxColumn1.DataPropertyName = "StatusMsg";
            this.statusMsgDataGridViewTextBoxColumn1.HeaderText = "Reason if cancelled by bot";
            this.statusMsgDataGridViewTextBoxColumn1.Name = "statusMsgDataGridViewTextBoxColumn1";
            this.statusMsgDataGridViewTextBoxColumn1.ReadOnly = true;
            this.statusMsgDataGridViewTextBoxColumn1.Width = 275;
            // 
            // createdDataGridViewTextBoxColumn1
            // 
            this.createdDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.createdDataGridViewTextBoxColumn1.DataPropertyName = "Created";
            this.createdDataGridViewTextBoxColumn1.HeaderText = "Created";
            this.createdDataGridViewTextBoxColumn1.Name = "createdDataGridViewTextBoxColumn1";
            this.createdDataGridViewTextBoxColumn1.ReadOnly = true;
            this.createdDataGridViewTextBoxColumn1.Visible = false;
            this.createdDataGridViewTextBoxColumn1.Width = 102;
            // 
            // lastModifiedDataGridViewTextBoxColumn1
            // 
            this.lastModifiedDataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.lastModifiedDataGridViewTextBoxColumn1.DataPropertyName = "LastModified";
            this.lastModifiedDataGridViewTextBoxColumn1.HeaderText = "LastModified";
            this.lastModifiedDataGridViewTextBoxColumn1.Name = "lastModifiedDataGridViewTextBoxColumn1";
            this.lastModifiedDataGridViewTextBoxColumn1.ReadOnly = true;
            this.lastModifiedDataGridViewTextBoxColumn1.Visible = false;
            this.lastModifiedDataGridViewTextBoxColumn1.Width = 136;
            // 
            // ordersHistoryBindingSource
            // 
            this.ordersHistoryBindingSource.DataSource = typeof(CryptoSwapMaster.Data.Entities.Order);
            // 
            // pnlFilters
            // 
            this.pnlFilters.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlFilters.Controls.Add(this.cbOrderStatus);
            this.pnlFilters.Controls.Add(this.lblOrderStatus);
            this.pnlFilters.Controls.Add(this.cbBaseAsset2);
            this.pnlFilters.Controls.Add(this.lblBaseAsset2);
            this.pnlFilters.Controls.Add(this.btnRefreshOrdersHistory);
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilters.Location = new System.Drawing.Point(1, 1);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Size = new System.Drawing.Size(1223, 58);
            this.pnlFilters.TabIndex = 0;
            // 
            // cbOrderStatus
            // 
            this.cbOrderStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrderStatus.DropDownWidth = 100;
            this.cbOrderStatus.FormattingEnabled = true;
            this.cbOrderStatus.Items.AddRange(new object[] {
            "All",
            "Open",
            "InProcess",
            "Cancelled",
            "Completed"});
            this.cbOrderStatus.Location = new System.Drawing.Point(298, 13);
            this.cbOrderStatus.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.cbOrderStatus.Name = "cbOrderStatus";
            this.cbOrderStatus.Size = new System.Drawing.Size(142, 33);
            this.cbOrderStatus.TabIndex = 4;
            // 
            // lblOrderStatus
            // 
            this.lblOrderStatus.AutoSize = true;
            this.lblOrderStatus.Location = new System.Drawing.Point(246, 16);
            this.lblOrderStatus.Name = "lblOrderStatus";
            this.lblOrderStatus.Size = new System.Drawing.Size(79, 25);
            this.lblOrderStatus.TabIndex = 3;
            this.lblOrderStatus.Text = "Status: ";
            // 
            // cbBaseAsset2
            // 
            this.cbBaseAsset2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBaseAsset2.DropDownWidth = 100;
            this.cbBaseAsset2.FormattingEnabled = true;
            this.cbBaseAsset2.Items.AddRange(new object[] {
            "All"});
            this.cbBaseAsset2.Location = new System.Drawing.Point(88, 13);
            this.cbBaseAsset2.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.cbBaseAsset2.Name = "cbBaseAsset2";
            this.cbBaseAsset2.Size = new System.Drawing.Size(135, 33);
            this.cbBaseAsset2.TabIndex = 2;
            // 
            // lblBaseAsset2
            // 
            this.lblBaseAsset2.AutoSize = true;
            this.lblBaseAsset2.Location = new System.Drawing.Point(5, 16);
            this.lblBaseAsset2.Name = "lblBaseAsset2";
            this.lblBaseAsset2.Size = new System.Drawing.Size(123, 25);
            this.lblBaseAsset2.TabIndex = 1;
            this.lblBaseAsset2.Text = "Base Asset: ";
            // 
            // btnRefreshOrdersHistory
            // 
            this.btnRefreshOrdersHistory.Location = new System.Drawing.Point(489, 11);
            this.btnRefreshOrdersHistory.Name = "btnRefreshOrdersHistory";
            this.btnRefreshOrdersHistory.Size = new System.Drawing.Size(89, 28);
            this.btnRefreshOrdersHistory.TabIndex = 0;
            this.btnRefreshOrdersHistory.Text = "Refresh";
            this.btnRefreshOrdersHistory.UseVisualStyleBackColor = true;
            this.btnRefreshOrdersHistory.Click += new System.EventHandler(this.btnRefreshOrdersHistory_Click);
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
            this.tpSettings.Size = new System.Drawing.Size(1225, 560);
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
            this.clbQuoteAssets.Size = new System.Drawing.Size(1100, 354);
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
            this.lblQuoteAssets.Location = new System.Drawing.Point(17, 156);
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
            // pnlStatusBar
            // 
            this.pnlStatusBar.BackColor = System.Drawing.Color.White;
            this.pnlStatusBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatusBar.Location = new System.Drawing.Point(0, 608);
            this.pnlStatusBar.Name = "pnlStatusBar";
            this.pnlStatusBar.Size = new System.Drawing.Size(1233, 43);
            this.pnlStatusBar.TabIndex = 6;
            // 
            // pnlTabControl
            // 
            this.pnlTabControl.Controls.Add(this.tcSections);
            this.pnlTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTabControl.Location = new System.Drawing.Point(0, 0);
            this.pnlTabControl.Name = "pnlTabControl";
            this.pnlTabControl.Size = new System.Drawing.Size(1233, 608);
            this.pnlTabControl.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1233, 651);
            this.Controls.Add(this.pnlTabControl);
            this.Controls.Add(this.pnlStatusBar);
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
            this.pnlOrders.ResumeLayout(false);
            this.pnlOpenOrders.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgOpenOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.openOrdersBindingSource)).EndInit();
            this.pnlNewOrder.ResumeLayout(false);
            this.pnlNewOrder.PerformLayout();
            this.tableOpenOrders.ResumeLayout(false);
            this.tableOpenOrders.PerformLayout();
            this.pnlBaseAssetInfo.ResumeLayout(false);
            this.pnlBaseAssetInfo.PerformLayout();
            this.tpOrdersHistory.ResumeLayout(false);
            this.pnlOrdersHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgOrdersHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ordersHistoryBindingSource)).EndInit();
            this.pnlFilters.ResumeLayout(false);
            this.pnlFilters.PerformLayout();
            this.tpSettings.ResumeLayout(false);
            this.tpSettings.PerformLayout();
            this.pnlTabControl.ResumeLayout(false);
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
        private System.Windows.Forms.Panel pnlNewOrder;
        private System.Windows.Forms.TableLayoutPanel tableOpenOrders;
        private System.Windows.Forms.Label lblPool;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.Label lblQuoteQty;
        private System.Windows.Forms.Label lblQuoteAsset;
        private System.Windows.Forms.Label lblBaseQty;
        private System.Windows.Forms.ComboBox cbPool;
        private System.Windows.Forms.TextBox tbQuoteQty;
        private System.Windows.Forms.ComboBox cbQuoteAsset;
        private System.Windows.Forms.TextBox tbBaseQty;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dgOpenOrders;
        private System.Windows.Forms.BindingSource openOrdersBindingSource;
        private System.Windows.Forms.Panel pnlOrders;
        private System.Windows.Forms.Panel pnlBaseAssetInfo;
        private System.Windows.Forms.Label lblBaseAsset;
        private System.Windows.Forms.Panel pnlOpenOrders;
        private System.Windows.Forms.Label lblFreeBalValue;
        private System.Windows.Forms.Label lblFreeBal;
        private System.Windows.Forms.ComboBox cbBaseAsset;
        private System.Windows.Forms.Label lblOpenOrders;
        private System.Windows.Forms.Panel pnlStatusBar;
        private System.Windows.Forms.Panel pnlTabControl;
        private System.Windows.Forms.Panel pnlOrdersHistory;
        private System.Windows.Forms.DataGridView dgOrdersHistory;
        private System.Windows.Forms.BindingSource ordersHistoryBindingSource;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Button btnRefreshOrdersHistory;
        private System.Windows.Forms.Button btnChangeBotStatus;
        private System.Windows.Forms.ComboBox cbOrderStatus;
        private System.Windows.Forms.Label lblOrderStatus;
        private System.Windows.Forms.ComboBox cbBaseAsset2;
        private System.Windows.Forms.Label lblBaseAsset2;
        private System.Windows.Forms.DataGridViewTextBoxColumn baseAssetDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn poolDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn groupDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn baseQtyDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn quoteAssetDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn expectedQuoteQtyDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn receivedQuoteQtyDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusMsgDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastModifiedDataGridViewTextBoxColumn1;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cbType;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn userIdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn poolDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn baseAssetDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn baseQtyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExecutedBaseQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn quoteAssetDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn expectedQuoteQtyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn receivedQuoteQtyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusMsgDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastModifiedDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn Action;
    }
}