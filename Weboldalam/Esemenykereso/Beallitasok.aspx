<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Beallitasok.aspx.cs" Inherits="Beallitasok" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" Height="242px" meta:resourcekey="Panel1Resource1">
    <asp:Label ID="setLB" runat="server" Text="Beállítások" meta:resourcekey="setLBResource1"></asp:Label>
   <%--       <br />
        <br />--%>
  <%--        <asp:Label ID="userLB" runat="server" Text="Felhasználó név:" meta:resourcekey="userLBResource1"></asp:Label>
        <asp:TextBox ID="felhnevTB" runat="server" meta:resourcekey="felhnevTBResource1"></asp:TextBox>--%>
            <br />
        <br />
        <asp:Label ID="pwLB" runat="server" Text="Jelszó:" meta:resourcekey="pwLBResource1"></asp:Label>
        <asp:TextBox ID="passTB" runat="server" TextMode="Password" meta:resourcekey="passTBResource1"></asp:TextBox>
            <br />
        <br />
        <asp:Label ID="nameLB" runat="server" Text="Név:" meta:resourcekey="nameLBResource1"></asp:Label>
        <asp:TextBox ID="nevTB" runat="server" meta:resourcekey="nevTBResource2"></asp:TextBox>
            <br />
        <br />
        <asp:Label ID="birthLB" runat="server" Text="Születési év:" meta:resourcekey="birthLBResource1"></asp:Label>
        <asp:TextBox ID="szulevTB" runat="server" meta:resourcekey="szulevTBResource1"></asp:TextBox>
            <br />
        <br />
        <asp:Label ID="genderLB" runat="server" Text="Nem:" meta:resourcekey="genderLBResource1"></asp:Label>
             <br />  <br />       
        <asp:DropDownList ID="DropDownList1" runat="server" meta:resourcekey="DropDownList1Resource1" >
            <asp:ListItem Text="Nő" Value="0" meta:resourcekey="ListItemResource1" />
            <asp:ListItem Text="Férfi" Value="1" meta:resourcekey="ListItemResource2" />
        </asp:DropDownList>
        <br />
        <asp:Label ID="sojournLB" runat="server" Text="Tartózkodási hely:" meta:resourcekey="sojournLBResource1"></asp:Label>
         <asp:TextBox ID="helyTB" runat="server" meta:resourcekey="helyTBResource1" ></asp:TextBox>
            <br />
        <br />
        <asp:Label ID="eadLB" runat="server" Text="Email cím:" meta:resourcekey="eadLBResource1"></asp:Label>
         <asp:TextBox ID="TextBox7" runat="server" meta:resourcekey="TextBox7Resource1"></asp:TextBox>
         <br />         
         <br />
        <asp:Button ID="SaveBT" runat="server" Text="Módosítások mentése" OnClick="Save_Click" meta:resourcekey="SaveBTResource1"  />
        <br />         
         <br />
        <asp:Label ID="tesztLb" runat="server" meta:resourcekey="tesztLbResource1"></asp:Label>
         <br />         
         <br />
        <asp:Label ID="ErtesitesLb" runat="server" meta:resourcekey="ErtesitesLbResource1"></asp:Label>
</asp:Panel>
</asp:Content>

