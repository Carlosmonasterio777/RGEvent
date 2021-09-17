<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Edit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>New event</title>
    </head>

      <style>
    * {
  font-family: Helvetica;
  font-weight: 400;
  font-size: 16px;
   color: #bdc3c7; 
}

    .textbox
    {
color: black
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
  height: 30px;
  border-radius: 5px;
  box-sizing: border-box;
  box-shadow: 1px 1px 1px rgba(0,0,0,.01);
  border: 0;
  border-bottom: 5px solid rgba(0,0,0,.12);
  line-height: 30px;
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


</style>
<body class="modal">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellspacing="4" cellpadding="0">
            <tr>
                <td align="right"></td>
                <td>
                    <div class="header">Editar Evento</div>
                    <asp:LinkButton ID="LinkButtonDelete" runat="server" OnClick="LinkButtonDelete_Click">Eliminar</asp:LinkButton>
                    <br />
                     <asp:LinkButton ID="Cancel" runat="server" OnClick="LinkButtonCancel_Click">Cancelar</asp:LinkButton>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td align="right">Cliente:</td>
                <td><asp:Label ID="nombre_cliente" runat="server" Text=""   Font-Italic="True"  ></asp:Label></td>
            </tr>
                   <tr>
                <td align="right">Servicio:</td>
                <td><asp:Label ID="servicio"  runat="server" Text="" ReadOnly="True"  Font-Italic="True"  ></asp:Label></td>
            </tr>
                    <tr>
                <td align="right">Cantidad en horas/unidades:</td>
                <td><asp:TextBox ID="cantidad" CssClass="textbox" type="number" min="1" AutoPostBack="true" runat="server"></asp:TextBox></td>
            </tr>
                  <tr>
                <td align="right">Fecha Inicial:</td>
                   <td><asp:TextBox CssClass="textbox" ID="fecha_inicial" runat="server"></asp:TextBox> </td> 
            </tr>
           
            <tr>
                <td align="right">Fecha Final:</td>
                <td><asp:TextBox ID="fecha_final" CssClass="textbox" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">Descripcion:</td>
                <td><asp:TextBox ID="descripcion" CssClass="textbox" runat="server" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
              
          
              <tr>
                <td align="right">Precio:</td>
                <td><asp:TextBox ID="subtotal"  CssClass="textbox" ReadOnly="true"  runat="server"></asp:TextBox></td>
            </tr>
              <tr>
                <td align="right">Total:</td>
                <td><asp:TextBox ID="total" CssClass="textbox" ReadOnly="true" runat="server"></asp:TextBox></td>
            </tr>
                 
            <tr>
                <td align="right"></td>
                <td>
                    <asp:Button ID="ButtonOK" runat="server"  CssClass="button" OnClick="ButtonOK_Click" Text="  OK  " />
                    <asp:Button ID="ButtonCancel" runat="server"  CssClass="button" Text="Cancel" OnClick="ButtonCancel_Click" />
                </td>
            </tr>
        </table>
        
        </div>
    </form>
</body>
</html>
