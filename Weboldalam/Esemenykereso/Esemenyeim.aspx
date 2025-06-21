<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Esemenyeim.aspx.cs" Inherits="Esemenyeim" UICulture="auto" enableSessionState="true" culture="auto" meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:Label ID="felh_neveLB" runat="server" Text="Bejelentkezve mint: " meta:resourcekey="felh_neveLBResource1"></asp:Label>
           <br />   <br />
        <asp:Label ID="esemenyeimLB" runat="server" Text="<%$ Resources:String,esemenyeimLB %>" meta:resourcekey="esemenyeimLBResource1"></asp:Label>  
    <br />   <br />
     <asp:Label ID="tesztLb" runat="server" meta:resourcekey="tesztLbResource1"></asp:Label>
    <br />
    <asp:Label ID="ownLB" runat="server" Text="Saját eseményeid:" BorderStyle="Double" meta:resourcekey="ownLBResource1"></asp:Label>
      <br />   <br />
         <asp:Label ID="tesztLb2" runat="server" meta:resourcekey="tesztLb2Resource1"></asp:Label>
     <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>  
      <br />   <br />
         <asp:Label ID="partLB" runat="server" Text="Események amiken részt veszel:" BorderStyle="Double" meta:resourcekey="partLBResource1"></asp:Label>
      <br />   <br />          
    <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
      <br />   <br />
    <asp:Label ID="tesztLb1" runat="server" meta:resourcekey="tesztLb1Resource1"></asp:Label>
   <asp:Panel ID="Panel3" runat="server" Height="236px" meta:resourcekey="Panel3Resource1">
        </asp:Panel>
    
</asp:Content>

    


