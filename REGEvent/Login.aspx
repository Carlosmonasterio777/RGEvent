<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login_" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="css/User.css" rel="stylesheet" type="text/css">
    <title></title>
</head>
<body>
   <div class="login">
	<h1>Login</h1>
    <form runat="server">
                <asp:Label ID="Label1" runat="server" Text="User" ForeColor="White"></asp:Label>
  <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                        <asp:Label ID="Label2" runat="server" Text="Password"  ForeColor="White"></asp:Label>
            <asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
                       
        <asp:Button ID="Button1" runat="server" Text="Login" class="btn btn-primary btn-block btn-large" OnClick="Button1_Click" />

    </form>
</div>

</body>
</html>
