<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%--  --%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
    </style>
    <link href="Login.css" rel="stylesheet"  type="text/css"/>
</asp:Content>

   
  
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    

    <asp:Panel ID="Panel1" runat="server" Height="250px" DefaultButton="btLog" meta:resourcekey="Panel1Resource1"> 

        <asp:Label ID="lbLogin" runat="server" Text="Bejelentkezés"  BorderStyle="Double" meta:resourcekey="lbLoginResource1" ></asp:Label>
        <br />
       
       <table style="width:45%">
  <tr>
    <td class="auto-style1"><asp:Label ID="user" runat="server" Text="Felhasználó név:" meta:resourcekey="userResource1"></asp:Label></td>
    <td class="auto-style1">  <asp:TextBox ID="felhnevTB" runat="server" meta:resourcekey="felhnevTBResource1"></asp:TextBox></td>
    <td class="auto-style1">       
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Adja meg a felhasználó nevét!" ControlToValidate="felhnevTB"  ValidationGroup="Kihagyo"  ></asp:RequiredFieldValidator>
    </td>
  </tr>
  <tr>
    <td class="auto-style1"><asp:Label ID="pw" runat="server" Text="Jelszó:" meta:resourcekey="pwResource1"></asp:Label></td>
     <td class="auto-style1"> <asp:TextBox ID="passTB" runat="server" TextMode="Password" meta:resourcekey="passTBResource1" ></asp:TextBox></td>
    <td class="auto-style1">
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Adja meg a jelszavát!" ControlToValidate="passTB"  ValidationGroup="Kihagyo" ></asp:RequiredFieldValidator>
    </td>
  </tr>
  <tr>
    <td></td>
    <td><asp:Button ID="btLog" runat="server" Text="Bejelentkezés"  ValidationGroup="Kihagyo" OnClick="Login_Click"  meta:resourcekey="btLogResource1"  /></td> 
    <td>
        </td>
  </tr>
</table>
        <asp:Button ID="btRegto" runat="server" Text="Regisztráció"  CausesValidation="False" OnClick="Register_Click" meta:resourcekey="btRegtoResource1"/>
          <br />    <br />
        <asp:Label ID="teszt_lb" runat="server" meta:resourcekey="teszt_lbResource1"></asp:Label>
        <br />    <br />
        <asp:Label ID="lbfiz" runat="server" Text="Fizetett hírdetések:"  BorderStyle="Double" meta:resourcekey="lbfizResource1"></asp:Label>
         <br />    <br />
         <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
       
    </asp:Panel>
</asp:Content>

