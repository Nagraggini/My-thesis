
<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<%--<!DOCTYPE html>--%>

<%--<html xmlns="http://www.w3.org/1999/xhtml">--%>
<%--<head id="Head1" runat="server">
    <title></title>
</head>
<body>--%>
  <%--<form id="form1" runat="server">--%>
    <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
         </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <table style="width:45%">
  <tr>
    <td class="auto-style1"><asp:Label ID="lbnev" runat="server" Text="Név:" meta:resourcekey="lbnevResource1"></asp:Label></td>
    <td class="auto-style1">  <asp:TextBox ID="nevTB" runat="server" meta:resourcekey="nevTBResource1"></asp:TextBox></td>
    <td class="auto-style1">       
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Ez nem maradhat üresen." ControlToValidate="nevTB"  ValidationGroup="Kihagyo" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
    </td>
  </tr>
  <tr>
    <td class="auto-style1"><asp:Label ID="lbfelhnev" runat="server" Text="Felhasználó név:" meta:resourcekey="lbfelhnevResource1"></asp:Label></td>
     <td class="auto-style1"> <asp:TextBox ID="felhnevTB" runat="server" meta:resourcekey="felhnevTBResource1"></asp:TextBox></td>
    <td class="auto-style1">
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Ez nem maradhat üresen." ControlToValidate="felhnevTB" ValidationGroup="Kihagyo" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
    </td>
  </tr>
  <tr>
   <td class="auto-style1"><asp:Label ID="lbpw" runat="server" Text="Jelszó:" meta:resourcekey="lbpwResource1"></asp:Label></td>
    <td class="auto-style1"> <asp:TextBox ID="passwordTB" runat="server" TextMode="Password" meta:resourcekey="passwordTBResource1"></asp:TextBox></td>
    
        <td class="auto-style1">
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Ez nem maradhat üresen." ControlToValidate="passwordTB"  ValidationGroup="Kihagyo" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
    </td>
        
  </tr>
</table>

           
        <asp:Button ID="btReg" runat="server" Text="Regisztráció"  ValidationGroup="Kihagyo" OnClick="RegisterIn_Click" meta:resourcekey="btRegResource1" />
                         <br />
                         <br />
        <asp:Button ID="btBack" runat="server" Text="Vissza a Bejelentkezéshez" CausesValidation="False" OnClick="Back_Click" meta:resourcekey="btBackResource1" />
                          <br />
                         <br />
         <asp:Label ID="teszt_lb" runat="server" meta:resourcekey="teszt_lbResource1"></asp:Label>
    </div>
       </asp:Content>
    <%--</form>--%>
<%--</body>
</html>--%>



