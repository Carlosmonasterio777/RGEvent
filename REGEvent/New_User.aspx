<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="New_User.aspx.cs" Inherits="_Default" MasterPageFile="~/Site.master" Title="Event Calendar with Day/Week/Month Views" %>
<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript" src="js/modal.js"></script>
	<script type="text/javascript" src="js/jquery-1.9.1.min.js"></script>
    <link href='css/main.css' type="text/css" rel="stylesheet" /> 

</asp:Content>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <style>
    * {
  font-family: Helvetica;
  font-weight: 400;
  font-size: 16px;
  color: #bdc3c7;
}

body {
  background-color: #2c3e50;
}

.container {
  max-width: 200px;
  margin: 20px ;
}

h2 {
  width: 100%;
  margin: 1em 0 .5em 1.2em;
  padding: 0;
  color: rgba(255,255,255,.2)
}

a {
  text-decoration: none;
     font-size: 18px;
  color: #ecf0f1;
   overflow: hidden;
}

a:hover {
  color: #bdc3c7;
}

.form {
  width: 100%;
  background-color: rgba(255,255,255,.2);
  text-align: center;
  padding: 1.25em;
  border-radius: 5px;
}

input {
  margin: 0 0 .4em 0;
  width: 100%;
  height: 40px;
  border-radius: 5px;
  box-sizing: border-box;
  box-shadow: 1px 1px 1px rgba(0,0,0,.01);
  border: 0;
  border-bottom: 5px solid rgba(0,0,0,.12);
  line-height: 40px;
}

[type='text'] {
  padding-left: 1em;
}

.button {
  color: white;
  background-color: #2980b9;
  font-weight: 600;
  margin-top:10px;
}

[type='text']:focus {
  border: 0;
  border-bottom: 5px solid #2980b9;
  color: black;
  outline: none;
}

.ddlstyle
{
color:rgb(33,33,00);
Font-Family:Helvetica;
font-size:12px;
vertical-align :middle;
  background-color: white;
  color:black;
 
}


* {
    padding: 0;
    margin: 0;
}
body {
    font: 11px Tahoma;
}

h1 {
    font: bold 32px Times;
    color: #666;
    text-align: center;
    padding: 10px 0;
}


#container {
    width: 700px;
    margin:60px;
}

.mGrid {
    width: 70%;
    background-color: #fff;
    margin: -300px 0 10px 300px;
    border: solid 1px #525252;
    border-collapse: collapse;
}

    .mGrid td {
        padding: 2px;
        border: solid 1px #c1c1c1;
        color: #717171;
    }

    .mGrid th {
        padding: 4px 2px;
        color: #fff;
        background: #2980b9 url(grd_head.png) repeat-x top;
        border-left: solid 1px #525252;
        font-size: 0.9em;
    }

    .mGrid .alt {
        background: #fcfcfc url(grd_alt.png) repeat-x top;
    }

    .mGrid .pgr {
        background: #424242 url(grd_pgr.png) repeat-x top;
    }

        .mGrid .pgr table {
            margin: 5px 0;
        }

        .mGrid .pgr td {
            border-width: 0;
            padding: 0 6px;
            border-left: solid 1px #666;
            font-weight: bold;
            color: #fff;
            line-height: 12px;
        }

        .mGrid .pgr a {
            color: #666;
            text-decoration: none;
        }

            .mGrid .pgr a:hover {
                color: #000;
                text-decoration: none;
            }
</style>

 <div class='container'>
  <h2><a>Registrar Nuevo Usuario</a></h2>
  <div class='form'>
      <asp:Label ID="Label1" runat="server" Text="Nickname"></asp:Label>
<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
      <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <asp:Label ID="Label3" runat="server" Text="Rol"></asp:Label>
      <br />
      <asp:DropDownList ID="DropDownList1" CssClass="ddlstyle form" runat="server"></asp:DropDownList>
 <br />
       <asp:Button runat="server" CssClass="button" Text="Guardar" OnClick="Unnamed1_Click"> </asp:Button>
  </div>

    
    </div>
    <asp:GridView ID="GridView1" CssClass="mGrid" runat="server">
    </asp:GridView>
</asp:Content>