<%@ Page Language="C#" AutoEventWireup="true" Inherits="eIVOGo.login" StylesheetTheme="Login" %>

<%@ Register Src="Module/UI/CaptchaImg.ascx" TagName="CaptchaImg" TagPrefix="uc1" %>
<%@ Register src="Module/UI/Announcement.ascx" tagname="Announcement" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>集團加值中心</title>
    <script type="text/javascript" language="javascript">
<!--
        //顯示年份//
        function show_date() {
            var time = new Date(); //宣告日期物件，儲存目前系統時間
            t_year = time.getFullYear(); //取得今年年分
            if (t_year > 2011) {
                document.write(" - " + t_year);
            }
        }
-->
    </script>
</head>
<body>
    <div class="login">
        <form id="form1" runat="server">
        <table border="0" cellspacing="0" cellpadding="0" class="sign_in">
            <tr>
                <td>
                    <span>帳號：</span><br />
                    <asp:TextBox ID="PID" Width="100px" Text="" runat="server"></asp:TextBox>
                </td>
                <td>
                    <span>密碼：</span><br />
                    <asp:TextBox ID="PWD" Width="100px" TextMode="Password" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:ImageButton ID="btnLogin" ImageUrl="images/login_button_up.gif" runat="server"
                        onmouseover="this.src='images/login_button_over.gif'" onmouseout="this.src='images/login_button_up.gif'"
                        OnClick="btnLogin_Click" />
                </td>
                <td valign="bottom">
                    <span class="forget_pw">[ <a href="getPassword_email.aspx">忘記密碼</a> ]</span>
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="0" class="verifyno">
            <tr>
                <td>
                    <uc1:CaptchaImg ID="CaptchaImg1" runat="server" />
                </td>
            </tr>
        </table>
        <table border="0" cellspacing="0" cellpadding="0" class="err01">
            <tr>
                <td>
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <div class="run">
<!--跑馬燈開始-->
  <marquee class="notice" scrollamount="2">
  <uc2:Announcement ID="MaintainAnnouncement" runat="server" />
  </marquee>
<!--跑馬燈結束-->
</div>
    <div class="copyright">
        Powered by UXB2B
    </div>
</body>
</html>
<script runat="server">
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (eIVOCenter.Properties.Settings.Default.UseSSL)
        {
            String url = Request.Url.OriginalString;
            if (!url.StartsWith("https", StringComparison.CurrentCultureIgnoreCase))
            {
                Response.Redirect("https" + url.Substring(4));
            }
        }
    }
</script>
