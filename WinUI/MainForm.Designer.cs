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
            this.label1 = new System.Windows.Forms.Label();
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
            this.splitDashboard.Panel2.Controls.Add(this.label1);
            this.splitDashboard.Size = new System.Drawing.Size(1223, 601);
            this.splitDashboard.SplitterDistance = 215;
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
            this.dgBalance.Size = new System.Drawing.Size(1221, 213);
            this.dgBalance.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(136, 123);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(860, 46);
            this.label1.TabIndex = 0;
            this.label1.Text = "Open Orders for selected asset from above grid";
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
            this.splitDashboard.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitDashboard)).EndInit();
            this.splitDashboard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgBalance)).EndInit();
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
        private System.Windows.Forms.Label label1;
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
    }
}