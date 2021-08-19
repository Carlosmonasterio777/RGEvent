<%@ Page Language="C#" AutoEventWireup="true" CodeFile="New.aspx.cs" Inherits="New" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>New event</title>
    <link href='css/main.css' type="text/css" rel="stylesheet" /> 
</head>
<body class="dialog">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellspacing="4" cellpadding="0">
            <tr>
                <td align="right"></td>
                <td>
                    <div class="header">New Event</div>
                </td>
            </tr>
            <tr>
                <td align="right">Fecha Inicial:</td>
                <td><asp:TextBox ID="fecha_inicial" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">Fecha Final:</td>
                <td><asp:TextBox ID="fecha_final" runat="server" Enabled="False" ReadOnly="True"></asp:TextBox></td>
            </tr>
                     <tr>
                <td align="right">Cantidad en horas:</td>
                <td><asp:TextBox ID="cantidad_hora" runat="server" type="number" min="1"  AutoPostBack="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">Descripcion:</td>
                <td><asp:TextBox ID="descripcion" runat="server"></asp:TextBox></td>
            </tr>
              <tr>
                <td align="right">Cliente:</td>
                <td><asp:TextBox ID="id_cliente" runat="server"></asp:TextBox></td>
            </tr>
              <tr>
                <td align="right">Usuario:</td>
                <td><asp:TextBox ID="id_usuario" runat="server"></asp:TextBox></td>
            </tr>
              <tr>
                <td align="right">Subtotal:</td>
                <td><asp:TextBox ID="subtotal" runat="server"></asp:TextBox></td>
            </tr>
              <tr>
                <td align="right">Total:</td>
                <td><asp:TextBox ID="total" runat="server"></asp:TextBox></td>
            </tr>
              <tr>
                <td align="right">Servicio:</td>
                <td><asp:TextBox ID="id_servicio" runat="server"></asp:TextBox></td>
            </tr>
    
            <tr>
                <td align="right"></td>
                <td>
                    <asp:Button ID="ButtonOK" runat="server" OnClick="ButtonOK_Click" Text="OK" />
                    <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" OnClick="ButtonCancel_Click" />
                </td>
            </tr>
        </table>
        
        </div>
    </form>
</body>
</html>
