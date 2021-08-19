<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>New event</title>
    </head>
<body class="modal">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellspacing="4" cellpadding="0">
            <tr>
                <td align="right"></td>
                <td>
                    <div class="header">Edit Event</div>
                    <asp:LinkButton ID="LinkButtonDelete" runat="server" OnClick="LinkButtonDelete_Click">Delete</asp:LinkButton>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td align="right">Cliente:</td>
                <td><asp:Label ID="nombre_cliente" runat="server" Text=""  ForeColor="#3333FF" Font-Italic="True"  ></asp:Label></td>
            </tr>
                   <tr>
                <td align="right">Servicio:</td>
                <td><asp:Label ID="servicio" runat="server" Text="" ReadOnly="True" ForeColor="#3333FF" Font-Italic="True"  ></asp:Label></td>
            </tr>
                  <tr>
                <td align="right">Fecha Inicial:</td>
                   <td><asp:TextBox CssClass="form-control" ID="fecha_inicial" runat="server"></asp:TextBox> </td> 
            </tr>
            <tr>
                <td align="right">Fecha Final:</td>
                <td><asp:TextBox ID="fecha_final" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">Descripcion:</td>
                <td><asp:TextBox ID="descripcion" runat="server" TextMode="MultiLine"></asp:TextBox></td>
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
                <td align="right"></td>
                <td>
                    <asp:Button ID="ButtonOK" runat="server" CssClass="btn btn-success" OnClick="ButtonOK_Click" Text="  OK  " />
                    <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" OnClick="ButtonCancel_Click" />
                </td>
            </tr>
        </table>
        
        </div>
    </form>
</body>
</html>
