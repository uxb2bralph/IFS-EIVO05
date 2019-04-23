namespace InvoiceClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.miClose = new System.Windows.Forms.ToolStripMenuItem();
            this.miActivate = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.SystemConfigTab = new System.Windows.Forms.TabPage();
            this.btnReceiptNo = new System.Windows.Forms.Button();
            this.SellerReceiptNo = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnSignerKeyPass = new System.Windows.Forms.Button();
            this.SignerKeyPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCspName = new System.Windows.Forms.Button();
            this.SignerCspName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSignerSubjectName = new System.Windows.Forms.Button();
            this.SignerSubjectName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnTxnPath = new System.Windows.Forms.Button();
            this.InvoiceTxnPath = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnServerUrl = new System.Windows.Forms.Button();
            this.ServerUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SystemReportTab = new System.Windows.Forms.TabPage();
            this.b2cPlatform = new InvoiceClient.MainContent.B2CInvoiceCenterConfig();
            this.WelfareTab = new System.Windows.Forms.TabPage();
            this.btnGetWelfareAgency = new System.Windows.Forms.Button();
            this.WelfareInfo = new System.Windows.Forms.TextBox();
            this.btnUpdateWelfare = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.btnAutoWelfare = new System.Windows.Forms.Button();
            this.AutoWelfareInterval = new System.Windows.Forms.TextBox();
            this.cbAutoWelfare = new System.Windows.Forms.CheckBox();
            this.ServiceConfigTab = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnInvService = new System.Windows.Forms.Button();
            this.btnAutoInvService = new System.Windows.Forms.Button();
            this.AutoInvServiceInterval = new System.Windows.Forms.TextBox();
            this.cbAutoInvService = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUninstall = new System.Windows.Forms.Button();
            this.btnInstall = new System.Windows.Forms.Button();
            this.InvoiceCenterTab = new System.Windows.Forms.TabPage();
            this.centerConfig = new InvoiceClient.MainContent.InvoiceCenterConfig();
            this.EIVOPlatformTab = new System.Windows.Forms.TabPage();
            this.eivoPlatform = new InvoiceClient.MainContent.EIVOPlatformConfig();
            this.PhysicalChannelTab = new System.Windows.Forms.TabPage();
            this.channelConfig = new InvoiceClient.MainContent.PhysicalChannelConfig();
            this.ReceiptTab = new System.Windows.Forms.TabPage();
            this.receiptConfig = new InvoiceClient.MainContent.ReceiptConfig();
            this.CsvInvoiceTab = new System.Windows.Forms.TabPage();
            this.csvInvoiceConfig = new InvoiceClient.MainContent.CsvInvoiceCenterConfig();
            this.CheckedStatementConfigTab = new System.Windows.Forms.TabPage();
            this.checkedStatementConfig = new InvoiceClient.MainContent.CheckedStatementConfig();
            this.invoiceClientServiceController = new System.ServiceProcess.ServiceController();
            this.contextMenu.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SystemConfigTab.SuspendLayout();
            this.SystemReportTab.SuspendLayout();
            this.WelfareTab.SuspendLayout();
            this.ServiceConfigTab.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.InvoiceCenterTab.SuspendLayout();
            this.EIVOPlatformTab.SuspendLayout();
            this.PhysicalChannelTab.SuspendLayout();
            this.ReceiptTab.SuspendLayout();
            this.CsvInvoiceTab.SuspendLayout();
            this.CheckedStatementConfigTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "應用程式執行中";
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miRestore,
            this.miClose,
            this.miActivate});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(153, 88);
            // 
            // miRestore
            // 
            this.miRestore.Name = "miRestore";
            this.miRestore.Size = new System.Drawing.Size(152, 28);
            this.miRestore.Text = "還原";
            this.miRestore.Click += new System.EventHandler(this.miRestore_Click);
            // 
            // miClose
            // 
            this.miClose.Name = "miClose";
            this.miClose.Size = new System.Drawing.Size(152, 28);
            this.miClose.Text = "結束";
            this.miClose.Click += new System.EventHandler(this.miClose_Click);
            // 
            // miActivate
            // 
            this.miActivate.Name = "miActivate";
            this.miActivate.Size = new System.Drawing.Size(152, 28);
            this.miActivate.Text = "重新啟用";
            this.miActivate.Click += new System.EventHandler(this.miActivate_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.SystemConfigTab);
            this.tabControl1.Controls.Add(this.SystemReportTab);
            this.tabControl1.Controls.Add(this.WelfareTab);
            this.tabControl1.Controls.Add(this.ServiceConfigTab);
            this.tabControl1.Controls.Add(this.InvoiceCenterTab);
            this.tabControl1.Controls.Add(this.EIVOPlatformTab);
            this.tabControl1.Controls.Add(this.PhysicalChannelTab);
            this.tabControl1.Controls.Add(this.ReceiptTab);
            this.tabControl1.Controls.Add(this.CsvInvoiceTab);
            this.tabControl1.Controls.Add(this.CheckedStatementConfigTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(746, 489);
            this.tabControl1.TabIndex = 1;
            // 
            // SystemConfigTab
            // 
            this.SystemConfigTab.Controls.Add(this.btnReceiptNo);
            this.SystemConfigTab.Controls.Add(this.SellerReceiptNo);
            this.SystemConfigTab.Controls.Add(this.label7);
            this.SystemConfigTab.Controls.Add(this.btnSignerKeyPass);
            this.SystemConfigTab.Controls.Add(this.SignerKeyPassword);
            this.SystemConfigTab.Controls.Add(this.label5);
            this.SystemConfigTab.Controls.Add(this.btnCspName);
            this.SystemConfigTab.Controls.Add(this.SignerCspName);
            this.SystemConfigTab.Controls.Add(this.label4);
            this.SystemConfigTab.Controls.Add(this.btnSignerSubjectName);
            this.SystemConfigTab.Controls.Add(this.SignerSubjectName);
            this.SystemConfigTab.Controls.Add(this.label3);
            this.SystemConfigTab.Controls.Add(this.btnTxnPath);
            this.SystemConfigTab.Controls.Add(this.InvoiceTxnPath);
            this.SystemConfigTab.Controls.Add(this.label2);
            this.SystemConfigTab.Controls.Add(this.btnServerUrl);
            this.SystemConfigTab.Controls.Add(this.ServerUrl);
            this.SystemConfigTab.Controls.Add(this.label1);
            this.SystemConfigTab.Location = new System.Drawing.Point(4, 28);
            this.SystemConfigTab.Name = "SystemConfigTab";
            this.SystemConfigTab.Padding = new System.Windows.Forms.Padding(3);
            this.SystemConfigTab.Size = new System.Drawing.Size(738, 457);
            this.SystemConfigTab.TabIndex = 0;
            this.SystemConfigTab.Text = "系統設定";
            this.SystemConfigTab.UseVisualStyleBackColor = true;
            // 
            // btnReceiptNo
            // 
            this.btnReceiptNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnReceiptNo.Location = new System.Drawing.Point(596, 381);
            this.btnReceiptNo.Name = "btnReceiptNo";
            this.btnReceiptNo.Size = new System.Drawing.Size(84, 39);
            this.btnReceiptNo.TabIndex = 17;
            this.btnReceiptNo.Text = "確定";
            this.btnReceiptNo.UseVisualStyleBackColor = true;
            this.btnReceiptNo.Click += new System.EventHandler(this.btnReceiptNo_Click);
            // 
            // SellerReceiptNo
            // 
            this.SellerReceiptNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SellerReceiptNo.Location = new System.Drawing.Point(0, 387);
            this.SellerReceiptNo.Name = "SellerReceiptNo";
            this.SellerReceiptNo.Size = new System.Drawing.Size(590, 35);
            this.SellerReceiptNo.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(0, 356);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(181, 29);
            this.label7.TabIndex = 15;
            this.label7.Text = "商家統一編號：";
            // 
            // btnSignerKeyPass
            // 
            this.btnSignerKeyPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSignerKeyPass.Location = new System.Drawing.Point(596, 308);
            this.btnSignerKeyPass.Name = "btnSignerKeyPass";
            this.btnSignerKeyPass.Size = new System.Drawing.Size(84, 39);
            this.btnSignerKeyPass.TabIndex = 14;
            this.btnSignerKeyPass.Text = "確定";
            this.btnSignerKeyPass.UseVisualStyleBackColor = true;
            this.btnSignerKeyPass.Click += new System.EventHandler(this.btnSignerKeyPass_Click);
            // 
            // SignerKeyPassword
            // 
            this.SignerKeyPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SignerKeyPassword.Location = new System.Drawing.Point(0, 314);
            this.SignerKeyPassword.Name = "SignerKeyPassword";
            this.SignerKeyPassword.PasswordChar = '*';
            this.SignerKeyPassword.Size = new System.Drawing.Size(590, 35);
            this.SignerKeyPassword.TabIndex = 13;
            this.SignerKeyPassword.UseSystemPasswordChar = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(0, 282);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(200, 29);
            this.label5.TabIndex = 12;
            this.label5.Text = "憑證PIN CODE：";
            // 
            // btnCspName
            // 
            this.btnCspName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnCspName.Location = new System.Drawing.Point(596, 235);
            this.btnCspName.Name = "btnCspName";
            this.btnCspName.Size = new System.Drawing.Size(84, 39);
            this.btnCspName.TabIndex = 11;
            this.btnCspName.Text = "確定";
            this.btnCspName.UseVisualStyleBackColor = true;
            this.btnCspName.Click += new System.EventHandler(this.btnCspName_Click);
            // 
            // SignerCspName
            // 
            this.SignerCspName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SignerCspName.Location = new System.Drawing.Point(0, 241);
            this.SignerCspName.Name = "SignerCspName";
            this.SignerCspName.Size = new System.Drawing.Size(590, 35);
            this.SignerCspName.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(0, 209);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(182, 29);
            this.label4.TabIndex = 9;
            this.label4.Text = "憑證CSP名稱：";
            // 
            // btnSignerSubjectName
            // 
            this.btnSignerSubjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnSignerSubjectName.Location = new System.Drawing.Point(596, 166);
            this.btnSignerSubjectName.Name = "btnSignerSubjectName";
            this.btnSignerSubjectName.Size = new System.Drawing.Size(84, 39);
            this.btnSignerSubjectName.TabIndex = 8;
            this.btnSignerSubjectName.Text = "確定";
            this.btnSignerSubjectName.UseVisualStyleBackColor = true;
            this.btnSignerSubjectName.Click += new System.EventHandler(this.btnSignerSubjectName_Click);
            // 
            // SignerSubjectName
            // 
            this.SignerSubjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SignerSubjectName.Location = new System.Drawing.Point(0, 172);
            this.SignerSubjectName.Name = "SignerSubjectName";
            this.SignerSubjectName.Size = new System.Drawing.Size(590, 35);
            this.SignerSubjectName.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(0, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(229, 29);
            this.label3.TabIndex = 6;
            this.label3.Text = "簽章憑證主體名稱：";
            // 
            // btnTxnPath
            // 
            this.btnTxnPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnTxnPath.Location = new System.Drawing.Point(596, 98);
            this.btnTxnPath.Name = "btnTxnPath";
            this.btnTxnPath.Size = new System.Drawing.Size(84, 39);
            this.btnTxnPath.TabIndex = 5;
            this.btnTxnPath.Text = "設定";
            this.btnTxnPath.UseVisualStyleBackColor = true;
            this.btnTxnPath.Click += new System.EventHandler(this.btnTxnPath_Click);
            // 
            // InvoiceTxnPath
            // 
            this.InvoiceTxnPath.AutoSize = true;
            this.InvoiceTxnPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.InvoiceTxnPath.Location = new System.Drawing.Point(0, 100);
            this.InvoiceTxnPath.Name = "InvoiceTxnPath";
            this.InvoiceTxnPath.Size = new System.Drawing.Size(170, 29);
            this.InvoiceTxnPath.TabIndex = 4;
            this.InvoiceTxnPath.Text = "C:\\InvoiceTXN";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(0, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(253, 29);
            this.label2.TabIndex = 3;
            this.label2.Text = "電子發票交易資料夾：";
            // 
            // btnServerUrl
            // 
            this.btnServerUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnServerUrl.Location = new System.Drawing.Point(596, 26);
            this.btnServerUrl.Name = "btnServerUrl";
            this.btnServerUrl.Size = new System.Drawing.Size(84, 39);
            this.btnServerUrl.TabIndex = 2;
            this.btnServerUrl.Text = "確定";
            this.btnServerUrl.UseVisualStyleBackColor = true;
            this.btnServerUrl.Click += new System.EventHandler(this.btnServerUrl_Click);
            // 
            // ServerUrl
            // 
            this.ServerUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ServerUrl.Location = new System.Drawing.Point(0, 32);
            this.ServerUrl.Name = "ServerUrl";
            this.ServerUrl.Size = new System.Drawing.Size(590, 35);
            this.ServerUrl.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "伺服端服務網址：";
            // 
            // SystemReportTab
            // 
            this.SystemReportTab.Controls.Add(this.b2cPlatform);
            this.SystemReportTab.Location = new System.Drawing.Point(4, 28);
            this.SystemReportTab.Name = "SystemReportTab";
            this.SystemReportTab.Padding = new System.Windows.Forms.Padding(3);
            this.SystemReportTab.Size = new System.Drawing.Size(738, 457);
            this.SystemReportTab.TabIndex = 1;
            this.SystemReportTab.Text = "工作記錄";
            this.SystemReportTab.UseVisualStyleBackColor = true;
            // 
            // b2cPlatform
            // 
            this.b2cPlatform.Dock = System.Windows.Forms.DockStyle.Fill;
            this.b2cPlatform.Location = new System.Drawing.Point(3, 3);
            this.b2cPlatform.Name = "b2cPlatform";
            this.b2cPlatform.Size = new System.Drawing.Size(732, 451);
            this.b2cPlatform.TabIndex = 0;
            // 
            // WelfareTab
            // 
            this.WelfareTab.Controls.Add(this.btnGetWelfareAgency);
            this.WelfareTab.Controls.Add(this.WelfareInfo);
            this.WelfareTab.Controls.Add(this.btnUpdateWelfare);
            this.WelfareTab.Controls.Add(this.label6);
            this.WelfareTab.Controls.Add(this.btnAutoWelfare);
            this.WelfareTab.Controls.Add(this.AutoWelfareInterval);
            this.WelfareTab.Controls.Add(this.cbAutoWelfare);
            this.WelfareTab.Location = new System.Drawing.Point(4, 28);
            this.WelfareTab.Name = "WelfareTab";
            this.WelfareTab.Padding = new System.Windows.Forms.Padding(3);
            this.WelfareTab.Size = new System.Drawing.Size(738, 457);
            this.WelfareTab.TabIndex = 2;
            this.WelfareTab.Text = "社福機構";
            this.WelfareTab.UseVisualStyleBackColor = true;
            // 
            // btnGetWelfareAgency
            // 
            this.btnGetWelfareAgency.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnGetWelfareAgency.Location = new System.Drawing.Point(546, 101);
            this.btnGetWelfareAgency.Name = "btnGetWelfareAgency";
            this.btnGetWelfareAgency.Size = new System.Drawing.Size(135, 39);
            this.btnGetWelfareAgency.TabIndex = 10;
            this.btnGetWelfareAgency.Text = "重新下載";
            this.btnGetWelfareAgency.UseVisualStyleBackColor = true;
            this.btnGetWelfareAgency.Click += new System.EventHandler(this.btnGetWelfareAgency_Click);
            // 
            // WelfareInfo
            // 
            this.WelfareInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.WelfareInfo.Location = new System.Drawing.Point(0, 79);
            this.WelfareInfo.Multiline = true;
            this.WelfareInfo.Name = "WelfareInfo";
            this.WelfareInfo.ReadOnly = true;
            this.WelfareInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.WelfareInfo.Size = new System.Drawing.Size(483, 337);
            this.WelfareInfo.TabIndex = 9;
            // 
            // btnUpdateWelfare
            // 
            this.btnUpdateWelfare.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnUpdateWelfare.Location = new System.Drawing.Point(546, 51);
            this.btnUpdateWelfare.Name = "btnUpdateWelfare";
            this.btnUpdateWelfare.Size = new System.Drawing.Size(135, 39);
            this.btnUpdateWelfare.TabIndex = 8;
            this.btnUpdateWelfare.Text = "檢查更新";
            this.btnUpdateWelfare.UseVisualStyleBackColor = true;
            this.btnUpdateWelfare.Click += new System.EventHandler(this.btnUpdateWelfare_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(0, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(277, 29);
            this.label6.TabIndex = 7;
            this.label6.Text = "發票捐贈社福機購資料：";
            // 
            // btnAutoWelfare
            // 
            this.btnAutoWelfare.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAutoWelfare.Location = new System.Drawing.Point(596, 0);
            this.btnAutoWelfare.Name = "btnAutoWelfare";
            this.btnAutoWelfare.Size = new System.Drawing.Size(84, 39);
            this.btnAutoWelfare.TabIndex = 4;
            this.btnAutoWelfare.Text = "確定";
            this.btnAutoWelfare.UseVisualStyleBackColor = true;
            this.btnAutoWelfare.Click += new System.EventHandler(this.btnAutoWelfare_Click);
            // 
            // AutoWelfareInterval
            // 
            this.AutoWelfareInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.AutoWelfareInterval.Location = new System.Drawing.Point(403, 0);
            this.AutoWelfareInterval.Name = "AutoWelfareInterval";
            this.AutoWelfareInterval.Size = new System.Drawing.Size(87, 35);
            this.AutoWelfareInterval.TabIndex = 3;
            // 
            // cbAutoWelfare
            // 
            this.cbAutoWelfare.AutoSize = true;
            this.cbAutoWelfare.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbAutoWelfare.Location = new System.Drawing.Point(0, 0);
            this.cbAutoWelfare.Name = "cbAutoWelfare";
            this.cbAutoWelfare.Size = new System.Drawing.Size(438, 33);
            this.cbAutoWelfare.TabIndex = 0;
            this.cbAutoWelfare.Text = "自動更新社福機購資料，間隔(分鐘)：";
            this.cbAutoWelfare.UseVisualStyleBackColor = true;
            this.cbAutoWelfare.CheckedChanged += new System.EventHandler(this.cbAutoWelfare_CheckedChanged);
            // 
            // ServiceConfigTab
            // 
            this.ServiceConfigTab.Controls.Add(this.groupBox3);
            this.ServiceConfigTab.Controls.Add(this.groupBox2);
            this.ServiceConfigTab.Controls.Add(this.groupBox1);
            this.ServiceConfigTab.Location = new System.Drawing.Point(4, 28);
            this.ServiceConfigTab.Name = "ServiceConfigTab";
            this.ServiceConfigTab.Padding = new System.Windows.Forms.Padding(3);
            this.ServiceConfigTab.Size = new System.Drawing.Size(738, 457);
            this.ServiceConfigTab.TabIndex = 3;
            this.ServiceConfigTab.Text = "服務設定";
            this.ServiceConfigTab.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.AutoSize = true;
            this.groupBox3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox3.Controls.Add(this.btnInvService);
            this.groupBox3.Controls.Add(this.btnAutoInvService);
            this.groupBox3.Controls.Add(this.AutoInvServiceInterval);
            this.groupBox3.Controls.Add(this.cbAutoInvService);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox3.Location = new System.Drawing.Point(3, 293);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(732, 155);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "電子發票作業";
            // 
            // btnInvService
            // 
            this.btnInvService.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnInvService.Location = new System.Drawing.Point(548, 87);
            this.btnInvService.Name = "btnInvService";
            this.btnInvService.Size = new System.Drawing.Size(135, 39);
            this.btnInvService.TabIndex = 16;
            this.btnInvService.Text = "立即下載";
            this.btnInvService.UseVisualStyleBackColor = true;
            this.btnInvService.Click += new System.EventHandler(this.btnInvService_Click);
            // 
            // btnAutoInvService
            // 
            this.btnAutoInvService.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnAutoInvService.Location = new System.Drawing.Point(598, 34);
            this.btnAutoInvService.Name = "btnAutoInvService";
            this.btnAutoInvService.Size = new System.Drawing.Size(84, 39);
            this.btnAutoInvService.TabIndex = 15;
            this.btnAutoInvService.Text = "確定";
            this.btnAutoInvService.UseVisualStyleBackColor = true;
            this.btnAutoInvService.Click += new System.EventHandler(this.btnAutoInvService_Click);
            // 
            // AutoInvServiceInterval
            // 
            this.AutoInvServiceInterval.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.AutoInvServiceInterval.Location = new System.Drawing.Point(405, 34);
            this.AutoInvServiceInterval.Name = "AutoInvServiceInterval";
            this.AutoInvServiceInterval.Size = new System.Drawing.Size(87, 35);
            this.AutoInvServiceInterval.TabIndex = 14;
            // 
            // cbAutoInvService
            // 
            this.cbAutoInvService.AutoSize = true;
            this.cbAutoInvService.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.cbAutoInvService.Location = new System.Drawing.Point(2, 34);
            this.cbAutoInvService.Name = "cbAutoInvService";
            this.cbAutoInvService.Size = new System.Drawing.Size(438, 33);
            this.cbAutoInvService.TabIndex = 13;
            this.cbAutoInvService.Text = "自動下載電子發票資料，間隔(分鐘)：";
            this.cbAutoInvService.UseVisualStyleBackColor = true;
            this.cbAutoInvService.CheckedChanged += new System.EventHandler(this.cbAutoInvService_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.Controls.Add(this.btnStop);
            this.groupBox2.Controls.Add(this.btnRun);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox2.Location = new System.Drawing.Point(3, 148);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(732, 145);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "啟用／停用";
            // 
            // btnStop
            // 
            this.btnStop.AutoSize = true;
            this.btnStop.Enabled = false;
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnStop.Location = new System.Drawing.Point(0, 72);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(323, 44);
            this.btnStop.TabIndex = 12;
            this.btnStop.Text = "停止電子發票用戶端服務";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnRun
            // 
            this.btnRun.AutoSize = true;
            this.btnRun.Enabled = false;
            this.btnRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnRun.Location = new System.Drawing.Point(0, 26);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(323, 44);
            this.btnRun.TabIndex = 11;
            this.btnRun.Text = "執行電子發票用戶端服務";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.btnUninstall);
            this.groupBox1.Controls.Add(this.btnInstall);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(732, 145);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "安裝／移除";
            // 
            // btnUninstall
            // 
            this.btnUninstall.AutoSize = true;
            this.btnUninstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnUninstall.Location = new System.Drawing.Point(0, 72);
            this.btnUninstall.Name = "btnUninstall";
            this.btnUninstall.Size = new System.Drawing.Size(272, 44);
            this.btnUninstall.TabIndex = 12;
            this.btnUninstall.Text = "從Windows服務移除";
            this.btnUninstall.UseVisualStyleBackColor = true;
            this.btnUninstall.Click += new System.EventHandler(this.btnUninstall_Click);
            // 
            // btnInstall
            // 
            this.btnInstall.AutoSize = true;
            this.btnInstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnInstall.Location = new System.Drawing.Point(0, 26);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(272, 44);
            this.btnInstall.TabIndex = 11;
            this.btnInstall.Text = "安裝至Windows服務";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // InvoiceCenterTab
            // 
            this.InvoiceCenterTab.Controls.Add(this.centerConfig);
            this.InvoiceCenterTab.Location = new System.Drawing.Point(4, 28);
            this.InvoiceCenterTab.Name = "InvoiceCenterTab";
            this.InvoiceCenterTab.Padding = new System.Windows.Forms.Padding(3);
            this.InvoiceCenterTab.Size = new System.Drawing.Size(738, 457);
            this.InvoiceCenterTab.TabIndex = 4;
            this.InvoiceCenterTab.Text = "集團加值中心";
            this.InvoiceCenterTab.UseVisualStyleBackColor = true;
            // 
            // centerConfig
            // 
            this.centerConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.centerConfig.Location = new System.Drawing.Point(3, 3);
            this.centerConfig.Name = "centerConfig";
            this.centerConfig.Size = new System.Drawing.Size(732, 451);
            this.centerConfig.TabIndex = 0;
            // 
            // EIVOPlatformTab
            // 
            this.EIVOPlatformTab.Controls.Add(this.eivoPlatform);
            this.EIVOPlatformTab.Location = new System.Drawing.Point(4, 28);
            this.EIVOPlatformTab.Name = "EIVOPlatformTab";
            this.EIVOPlatformTab.Padding = new System.Windows.Forms.Padding(3);
            this.EIVOPlatformTab.Size = new System.Drawing.Size(738, 457);
            this.EIVOPlatformTab.TabIndex = 5;
            this.EIVOPlatformTab.Text = "電子發票平台";
            this.EIVOPlatformTab.UseVisualStyleBackColor = true;
            // 
            // eivoPlatform
            // 
            this.eivoPlatform.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eivoPlatform.Location = new System.Drawing.Point(3, 3);
            this.eivoPlatform.Name = "eivoPlatform";
            this.eivoPlatform.Size = new System.Drawing.Size(732, 451);
            this.eivoPlatform.TabIndex = 0;
            // 
            // PhysicalChannelTab
            // 
            this.PhysicalChannelTab.Controls.Add(this.channelConfig);
            this.PhysicalChannelTab.Location = new System.Drawing.Point(4, 28);
            this.PhysicalChannelTab.Name = "PhysicalChannelTab";
            this.PhysicalChannelTab.Padding = new System.Windows.Forms.Padding(3);
            this.PhysicalChannelTab.Size = new System.Drawing.Size(738, 457);
            this.PhysicalChannelTab.TabIndex = 6;
            this.PhysicalChannelTab.Text = "實體通路電子發票";
            this.PhysicalChannelTab.UseVisualStyleBackColor = true;
            // 
            // channelConfig
            // 
            this.channelConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.channelConfig.Location = new System.Drawing.Point(3, 3);
            this.channelConfig.Name = "channelConfig";
            this.channelConfig.Size = new System.Drawing.Size(732, 451);
            this.channelConfig.TabIndex = 0;
            // 
            // ReceiptTab
            // 
            this.ReceiptTab.Controls.Add(this.receiptConfig);
            this.ReceiptTab.Location = new System.Drawing.Point(4, 28);
            this.ReceiptTab.Name = "ReceiptTab";
            this.ReceiptTab.Padding = new System.Windows.Forms.Padding(3);
            this.ReceiptTab.Size = new System.Drawing.Size(738, 457);
            this.ReceiptTab.TabIndex = 7;
            this.ReceiptTab.Text = "收據服務";
            this.ReceiptTab.UseVisualStyleBackColor = true;
            // 
            // receiptConfig
            // 
            this.receiptConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.receiptConfig.Location = new System.Drawing.Point(3, 3);
            this.receiptConfig.Name = "receiptConfig";
            this.receiptConfig.Size = new System.Drawing.Size(732, 451);
            this.receiptConfig.TabIndex = 0;
            // 
            // CsvInvoiceTab
            // 
            this.CsvInvoiceTab.Controls.Add(this.csvInvoiceConfig);
            this.CsvInvoiceTab.Location = new System.Drawing.Point(4, 28);
            this.CsvInvoiceTab.Name = "CsvInvoiceTab";
            this.CsvInvoiceTab.Padding = new System.Windows.Forms.Padding(3);
            this.CsvInvoiceTab.Size = new System.Drawing.Size(738, 457);
            this.CsvInvoiceTab.TabIndex = 8;
            this.CsvInvoiceTab.Text = "CSV電子發票";
            this.CsvInvoiceTab.UseVisualStyleBackColor = true;
            // 
            // csvInvoiceConfig
            // 
            this.csvInvoiceConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.csvInvoiceConfig.Location = new System.Drawing.Point(3, 3);
            this.csvInvoiceConfig.Name = "csvInvoiceConfig";
            this.csvInvoiceConfig.Size = new System.Drawing.Size(732, 451);
            this.csvInvoiceConfig.TabIndex = 0;
            // 
            // CheckedStatementConfigTab
            // 
            this.CheckedStatementConfigTab.Controls.Add(this.checkedStatementConfig);
            this.CheckedStatementConfigTab.Location = new System.Drawing.Point(4, 28);
            this.CheckedStatementConfigTab.Name = "CheckedStatementConfigTab";
            this.CheckedStatementConfigTab.Padding = new System.Windows.Forms.Padding(3);
            this.CheckedStatementConfigTab.Size = new System.Drawing.Size(738, 457);
            this.CheckedStatementConfigTab.TabIndex = 9;
            this.CheckedStatementConfigTab.Text = "對帳單";
            this.CheckedStatementConfigTab.UseVisualStyleBackColor = true;
            // 
            // checkedStatementConfig
            // 
            this.checkedStatementConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedStatementConfig.Location = new System.Drawing.Point(3, 3);
            this.checkedStatementConfig.Name = "checkedStatementConfig";
            this.checkedStatementConfig.Size = new System.Drawing.Size(732, 451);
            this.checkedStatementConfig.TabIndex = 0;
            // 
            // invoiceClientServiceController
            // 
            this.invoiceClientServiceController.ServiceName = Properties.Settings.Default.ServiceName;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(746, 489);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "電子發票－集團加值中心(SOGO)";
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenu.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.SystemConfigTab.ResumeLayout(false);
            this.SystemConfigTab.PerformLayout();
            this.SystemReportTab.ResumeLayout(false);
            this.WelfareTab.ResumeLayout(false);
            this.WelfareTab.PerformLayout();
            this.ServiceConfigTab.ResumeLayout(false);
            this.ServiceConfigTab.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.InvoiceCenterTab.ResumeLayout(false);
            this.EIVOPlatformTab.ResumeLayout(false);
            this.PhysicalChannelTab.ResumeLayout(false);
            this.ReceiptTab.ResumeLayout(false);
            this.CsvInvoiceTab.ResumeLayout(false);
            this.CheckedStatementConfigTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem miClose;
        private System.Windows.Forms.ToolStripMenuItem miRestore;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage SystemConfigTab;
        private System.Windows.Forms.TabPage SystemReportTab;
        private System.Windows.Forms.Button btnServerUrl;
        private System.Windows.Forms.TextBox ServerUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label InvoiceTxnPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnTxnPath;
        private System.Windows.Forms.Button btnSignerSubjectName;
        private System.Windows.Forms.TextBox SignerSubjectName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnCspName;
        private System.Windows.Forms.TextBox SignerCspName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSignerKeyPass;
        private System.Windows.Forms.TextBox SignerKeyPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabPage WelfareTab;
        private System.Windows.Forms.Button btnUpdateWelfare;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnAutoWelfare;
        private System.Windows.Forms.TextBox AutoWelfareInterval;
        private System.Windows.Forms.CheckBox cbAutoWelfare;
        private System.Windows.Forms.Button btnReceiptNo;
        private System.Windows.Forms.TextBox SellerReceiptNo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox WelfareInfo;
        private System.Windows.Forms.TabPage ServiceConfigTab;
        private System.Windows.Forms.Button btnGetWelfareAgency;
        private System.ServiceProcess.ServiceController invoiceClientServiceController;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnUninstall;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnAutoInvService;
        private System.Windows.Forms.TextBox AutoInvServiceInterval;
        private System.Windows.Forms.CheckBox cbAutoInvService;
        private System.Windows.Forms.Button btnInvService;
        private System.Windows.Forms.TabPage InvoiceCenterTab;
        private MainContent.InvoiceCenterConfig centerConfig;
        private System.Windows.Forms.TabPage EIVOPlatformTab;
        private MainContent.EIVOPlatformConfig eivoPlatform;
        private MainContent.B2CInvoiceCenterConfig b2cPlatform;
        private System.Windows.Forms.TabPage PhysicalChannelTab;
        private MainContent.PhysicalChannelConfig channelConfig;
        private System.Windows.Forms.TabPage ReceiptTab;
        private MainContent.ReceiptConfig receiptConfig;
        private System.Windows.Forms.TabPage CsvInvoiceTab;
        private MainContent.CsvInvoiceCenterConfig csvInvoiceConfig;
        private System.Windows.Forms.TabPage CheckedStatementConfigTab;
        private MainContent.CheckedStatementConfig checkedStatementConfig;
        private System.Windows.Forms.ToolStripMenuItem miActivate;
    }
}

