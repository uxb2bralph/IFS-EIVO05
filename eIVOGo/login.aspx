﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="eIVOGo.login"
    StylesheetTheme="Login" %>

<%@ Register src="Module/UI/CaptchaImg.ascx" tagname="CaptchaImg" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>電子發票系統</title>
<script type="text/javascript" language="javascript">
<!--
//顯示年份//
function show_date(){
	var time=new Date(); //宣告日期物件，儲存目前系統時間
	t_year=time.getFullYear(); //取得今年年分
	if(t_year > 2011){
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
                    <asp:TextBox ID="PID"  Width="100px" Text="" runat="server" ></asp:TextBox>
                </td>
                <td>
                    <span>密碼：</span><br />
                    <asp:TextBox ID="PWD" Width="100px" TextMode="Password" runat="server" ></asp:TextBox>
                    
                </td>
                <td>
                   <asp:ImageButton ID="btnLogin" ImageUrl="images/login_button_up.gif" 
                        runat="server" onmouseover="this.src='images/login_button_over.gif'"  
                        onmouseout="this.src='images/login_button_up.gif'" onclick="btnLogin_Click"  />
                </td>
                <td valign="bottom">
                    <span class="forget_pw">[ <a href="getPassword_email.aspx">忘記密碼</a> ]</span>
                </td>
            </tr>
            </table>
            <table border="0" cellspacing="0" cellpadding="0" class="verifyno" >
             <tr>
                <td>
                     <uc1:CaptchaImg ID="CaptchaImg1" runat="server" />
                </td>
            </tr>
            </table> 
            <table border="0" cellspacing="0" cellpadding="0" class="err01">
            <tr>
                <td>
                     <asp:Label ID="lblMsg" runat="server" ></asp:Label>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <div class="copyright">
        Powered by 
        UXB2B
    </div>
</body>
</html>
