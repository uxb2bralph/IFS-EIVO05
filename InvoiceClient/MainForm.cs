using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

using InvoiceClient.Agent;
using InvoiceClient.Properties;
using Model.Schema.EIVO;
using InvoiceClient.Helper;

namespace InvoiceClient
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            SystemConfigTab.VisibleChanged += new EventHandler(SystemConfigTab_VisibleChanged);
            WelfareTab.VisibleChanged += new EventHandler(WelfareTab_VisibleChanged);
            ServiceConfigTab.VisibleChanged += new EventHandler(ServiceConfigTab_VisibleChanged);
        }

        private bool initializeActivation()
        {
            String actKey = Microsoft.VisualBasic.Interaction.InputBox("新輸入識別代碼:", "啟用系統");
            if (!String.IsNullOrEmpty(actKey) && InvoiceClient.Helper.AppSigner.ResetCertificate(actKey))
            {
                MessageBox.Show("連線服務已啟用!!", "啟用系統", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            return false;
        }

        void ServiceConfigTab_VisibleChanged(object sender, EventArgs e)
        {
            if (ServiceConfigTab.Visible)
            {
                refreshServiceConfig();
                cbAutoInvService.Checked = Settings.Default.IsAutoInvService;
                AutoInvServiceInterval.Text = Settings.Default.AutoInvServiceInterval.ToString();
            }
        }

        private void refreshServiceConfig()
        {
            InvoiceClientTransferManager.ResetServiceController();
            btnInstall.Enabled = InvoiceClientTransferManager.ServiceInstance == null;
            btnUninstall.Enabled = !btnInstall.Enabled;

            if (InvoiceClientTransferManager.ServiceInstance != null)
            {
                btnStop.Enabled = InvoiceClientTransferManager.ServiceInstance.Status == ServiceControllerStatus.Running;
                btnRun.Enabled = !btnStop.Enabled;
            }
            else
            {
                btnStop.Enabled = false;
                btnRun.Enabled = false;
            }
        }

        void WelfareTab_VisibleChanged(object sender, EventArgs e)
        {
            if (WelfareTab.Visible)
            {
                cbAutoWelfare.Checked = Settings.Default.IsAutoWelfare;
                AutoWelfareInterval.Text = Settings.Default.AutoWelfareInterval.ToString();
            }
        }

        void SystemConfigTab_VisibleChanged(object sender, EventArgs e)
        {
            if (SystemConfigTab.Visible)
            {
                ServerUrl.Text = Settings.Default.InvoiceClient_WS_Invoice_eInvoiceService;
                InvoiceTxnPath.Text = Settings.Default.InvoiceTxnPath;
                SignerSubjectName.Text = Settings.Default.SignerSubjectName;
                SignerKeyPassword.Text = Settings.Default.SignerKeyPassword;
                SignerCspName.Text = Settings.Default.SignerCspName;
                if (String.IsNullOrEmpty(Settings.Default.SellerReceiptNo))
                {
                    var item = (new InvoiceServerInspector()).GetRegisterdMember();
                    if (item != null)
                    {
                        Settings.Default["SellerReceiptNo"] = item.ReceiptNo;
                    }
                }
                SellerReceiptNo.Text = Settings.Default.SellerReceiptNo;
            }
        }

        private void miClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //this.ShowInTaskbar = false;
            this.Text = Settings.Default.AppTitle;
            notifyIcon.Visible = true;

            if (AppSigner.SignerCertificate == null && String.IsNullOrEmpty(Settings.Default.ActivationKey))
            {
                if (!initializeActivation())
                {
                    MessageBox.Show("無法建立識別資料!!", "啟用失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }
            }

            InvoiceClientTransferManager.SetAutoUpdateWelfareAgency();
            InvoiceClientTransferManager.SetAutoInvoiceService();
            if (Settings.Default.DisableWelfareTab)
                tabControl1.TabPages.Remove(WelfareTab);
            if (Settings.Default.DisableInvoiceCenterTab)
                tabControl1.TabPages.Remove(InvoiceCenterTab);
            if (Settings.Default.DisableEIVOPlatformTab)
                tabControl1.TabPages.Remove(EIVOPlatformTab);
            if (Settings.Default.DisableB2CInvoiceCenterTab)
                tabControl1.TabPages.Remove(SystemReportTab);
            if (Settings.Default.DisablePhysicalChannelTab)
                tabControl1.TabPages.Remove(PhysicalChannelTab);
            if (Settings.Default.DisableReceiptUploadTab)
                tabControl1.TabPages.Remove(ReceiptTab);
            if (Settings.Default.DisableCsvInvoiceCenterTab)
                tabControl1.TabPages.Remove(CsvInvoiceTab);
            if (Settings.Default.DisableCheckedStatementTab)
                tabControl1.TabPages.Remove(CheckedStatementConfigTab);

            InvoiceClientTransferManager.StartUp(Settings.Default.InvoiceTxnPath);

//            this.Hide();
        }

        private void miRestore_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.ShowInTaskbar = false;
                this.Hide();
            }
        }

        private void btnServerUrl_Click(object sender, EventArgs e)
        {
            Settings.Default["InvoiceClient_WS_Invoice_eInvoiceService"] = ServerUrl.Text;
            MessageBox.Show("設定完成!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
        }

        private void btnTxnPath_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath =  Settings.Default.InvoiceTxnPath;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Settings.Default["InvoiceTxnPath"] = dialog.SelectedPath;
                    MessageBox.Show("設定完成!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnSignerSubjectName_Click(object sender, EventArgs e)
        {
            Settings.Default["SignerSubjectName"] = SignerSubjectName.Text;
            MessageBox.Show("設定完成!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCspName_Click(object sender, EventArgs e)
        {
            Settings.Default["SignerCspName"] = SignerCspName.Text;
            MessageBox.Show("設定完成!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSignerKeyPass_Click(object sender, EventArgs e)
        {
            Settings.Default["SignerKeyPassword"] = SignerKeyPassword.Text;
            MessageBox.Show("設定完成!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnReceiptNo_Click(object sender, EventArgs e)
        {
            Settings.Default["SellerReceiptNo"] = SellerReceiptNo.Text;
            MessageBox.Show("設定完成!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnAutoWelfare_Click(object sender, EventArgs e)
        {
            int interval;
            if (int.TryParse(AutoWelfareInterval.Text, out interval))
            {
                Settings.Default["AutoWelfareInterval"] = interval;
                InvoiceClientTransferManager.SetAutoUpdateWelfareAgency();
                MessageBox.Show("設定完成!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("請輸入整數的間隔數值!!", "自動更新社福機購資料", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                AutoWelfareInterval.Focus();
            }
        }

        private void btnUpdateWelfare_Click(object sender, EventArgs e)
        {
            SocialWelfareAgenciesRoot welfareItem = InvoiceClientTransferManager.UpdateWelfareAgency();
            if (welfareItem != null)
            {
                WelfareInfo.Text = String.Join("//--------------------------------\r\n", welfareItem.SocialWelfareAgencies
                    .Select(a => String.Format("機構代碼：{0}\r\n統一編號：{1}\r\n機構名稱：{2}\r\n地　　址：{3}\r\n連絡電話：{4}\r\n電子郵件：{5}\r\n",
                        a.Code, a.Ban, a.Name, a.Address, a.TEL, a.Email)).ToArray());
                MessageBox.Show("更新完成!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                WelfareInfo.Text = String.Empty;
                MessageBox.Show("尚未發現新的社福機構資料!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cbAutoWelfare_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default["IsAutoWelfare"] = cbAutoWelfare.Checked;
            InvoiceClientTransferManager.SetAutoUpdateWelfareAgency();
            MessageBox.Show("設定完成!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            Program.Install(false, new String[0]);
            refreshServiceConfig();
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            Program.Install(true, new String[0]);
            refreshServiceConfig();
        }

        private void btnGetWelfareAgency_Click(object sender, EventArgs e)
        {
            SocialWelfareAgenciesRoot welfareItem = InvoiceClientTransferManager.GetWelfareAgency();
            if (welfareItem != null)
            {
                WelfareInfo.Text = String.Join("//--------------------------------\r\n", welfareItem.SocialWelfareAgencies
                    .Select(a => String.Format("機構代碼：{0}\r\n統一編號：{1}\r\n機構名稱：{2}\r\n地　　址：{3}\r\n連絡電話：{4}\r\n電子郵件：{5}\r\n",
                        a.Code, a.Ban, a.Name, a.Address, a.TEL, a.Email)).ToArray());
                MessageBox.Show("取得社福機構資料完成!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                WelfareInfo.Text = String.Empty;
                MessageBox.Show("尚未指定社福機構資料!!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            InvoiceClientTransferManager.ServiceInstance.Start();
            btnRun.Enabled = false;
            btnStop.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            InvoiceClientTransferManager.ServiceInstance.Stop();
            btnRun.Enabled = true;
            btnStop.Enabled = false;
        }

        private void cbAutoInvService_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default["IsAutoInvService"] = cbAutoInvService.Checked;
            InvoiceClientTransferManager.SetAutoInvoiceService();
            MessageBox.Show("設定完成!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnAutoInvService_Click(object sender, EventArgs e)
        {
            int interval;
            if (int.TryParse(AutoInvServiceInterval.Text, out interval))
            {
                Settings.Default["AutoInvServiceInterval"] = interval;
                InvoiceClientTransferManager.SetAutoInvoiceService();
                MessageBox.Show("設定完成!!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("請輸入整數的間隔數值!!", "自動下載電子發票資料", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                AutoInvServiceInterval.Focus();
            }

        }

        private void btnInvService_Click(object sender, EventArgs e)
        {
            List<String> pathInfo = InvoiceClientTransferManager.ExceuteInvoiceService();
            if (pathInfo.Count>0)
            {
                if (MessageBox.Show("有新的發票資料下載完成!\r\n是否開啟資料夾檢視?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    foreach (var item in pathInfo)
                    {
                        Win32.Shell.ShellExecute(IntPtr.Zero, "explore", item, null, null, Win32.User.SW_SHOW);
                    }
                }
            }
            else
            {
                MessageBox.Show("沒有新的發票資料!!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void miActivate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("請確定是否以新的識別代碼建立連線服務?", "啟用系統", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                String actKey = Microsoft.VisualBasic.Interaction.InputBox("新輸入識別代碼:", "啟用系統");
                if (!String.IsNullOrEmpty(actKey) && InvoiceClient.Helper.AppSigner.ResetCertificate(actKey))
                {
                    MessageBox.Show("連線服務已啟用!!", "啟用系統", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
