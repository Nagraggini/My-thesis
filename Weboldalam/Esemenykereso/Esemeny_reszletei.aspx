<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Esemeny_reszletei.aspx.cs" Inherits="Esemeny_reszletei" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" Height="397px" meta:resourcekey="Panel1Resource1">
         <br />
        <br />
        <asp:Label ID="esemenyreszletKIIR" runat="server" Text="Esemény részletei:" BorderStyle="Double" meta:resourcekey="esemenyreszletKIIRResource1"></asp:Label>
      <br />  <br />  
            <asp:Image runat="server" ID="ImagePreview" Height="164px" Width="200px"  ImageUrl="~/nopic.jpg" meta:resourcekey="ImagePreviewResource1"/>
        <br />  <br />  
         <asp:Label ID="esemenyletreKIIR" runat="server" Text="Esemény létrehozó:" meta:resourcekey="esemenyletreKIIRResource1"></asp:Label>
         <br />  <br />       
          <asp:Label ID="esemnyletrehozoLB" runat="server" Text="Label" meta:resourcekey="esemnyletrehozoLBResource1"></asp:Label>
           <br />  <br />       
         <asp:Label ID="EsemnyneveKIIR" runat="server" Text="Esemény neve:" meta:resourcekey="EsemnyneveKIIRResource1"></asp:Label>
           <br />  <br />          
        <asp:Label ID="EsemenyneveLB" runat="server" Text="Label" meta:resourcekey="EsemenyneveLBResource1"></asp:Label>
           <br />  <br />       
         <asp:Label ID="esemenyleirKIIR" runat="server" Text="Esemény leírása:" Visible="False" meta:resourcekey="esemenyleirKIIRResource1"></asp:Label>
              <br />  <br />       
        <asp:ListBox ID="LeirasLB" runat="server" Visible="False" meta:resourcekey="LeirasLBResource1"></asp:ListBox>
        <br />  <br />       
        <asp:Label ID="mettolKIIR" runat="server" Text="Mettől:" Visible="False" meta:resourcekey="mettolKIIRResource1"></asp:Label>
              <br />  <br />       
        <asp:Label ID="mettolLB" runat="server" Text="Label" Visible="False" meta:resourcekey="mettolLBResource1"></asp:Label>
           <br />  <br />       
         <asp:Label ID="meddigKIIR" runat="server" Text="Meddig:" Visible="False" meta:resourcekey="meddigKIIRResource1"></asp:Label>
              <br />  <br />       
        <asp:Label ID="meddigLB" runat="server" Text="Label" Visible="False" meta:resourcekey="meddigLBResource1"></asp:Label>
          <br />  <br />       
          <asp:Label ID="esemenyhelyKIIR" runat="server" Text="Esemény helye:" Visible="False" meta:resourcekey="esemenyhelyKIIRResource1" ></asp:Label>
          <br /> <br />  
         <asp:Label ID="orszagKIIR" runat="server" Text="Ország:" Visible="False" meta:resourcekey="orszagKIIRResource1"></asp:Label>
              <br />
        <asp:Label ID="orszagLB" runat="server" Text="Label" Visible="False" meta:resourcekey="orszagLBResource1"></asp:Label>
           <br />  
         <asp:Label ID="varosKIIR" runat="server" Text="Város:" Visible="False" meta:resourcekey="varosKIIRResource1"></asp:Label>
              <br />     
        <asp:Label ID="varosLB" runat="server" Text="Label" Visible="False" meta:resourcekey="varosLBResource1"></asp:Label>
          <br />    
         <asp:Label ID="utcaKIIR" runat="server" Text="Utca:" Visible="False" meta:resourcekey="utcaKIIRResource1"></asp:Label>
            <br />    
        <asp:Label ID="utcaLB" runat="server" Text="Label" Visible="False" meta:resourcekey="utcaLBResource1"></asp:Label>
           <br />    
         <asp:Label ID="hazszamKIIR" runat="server" Text="Házszám:" Visible="False" meta:resourcekey="hazszamKIIRResource1"></asp:Label>
           <br /> 
        <asp:Label ID="hazszamLB" runat="server" Text="Label" Visible="False" meta:resourcekey="hazszamLBResource1"></asp:Label>
          <br />      
         <asp:Label ID="iranyszKIIR" runat="server" Text="Írányító szám:" Visible="False" meta:resourcekey="iranyszKIIRResource1"></asp:Label>
           <br />     
        <asp:Label ID="iranyszLB" runat="server" Text="Label" Visible="False" meta:resourcekey="iranyszLBResource1"></asp:Label>
          <br />       
         <asp:Label ID="tipusKIIR" runat="server" Text="Típusa:" meta:resourcekey="tipusKIIRResource1"></asp:Label>
           <br />  <br />       
        <asp:Label ID="tipusLB" runat="server" Text="Label" meta:resourcekey="tipusLBResource1"></asp:Label>
          <br />  <br />       
         <asp:Label ID="altipusKIIR" runat="server" Text="Altípusa:" meta:resourcekey="altipusKIIRResource1"></asp:Label>
        <br />  <br />       
        <asp:Label ID="altipusLB" runat="server" Text="Label" meta:resourcekey="altipusLBResource1"></asp:Label>
          <br />  <br />       
         <asp:Label ID="eddigmennekKIIR" runat="server" Text="Eddig hányan mennek:" meta:resourcekey="eddigmennekKIIRResource1"></asp:Label>
        <br />  <br />       
        <asp:Label ID="eddigmennekLB" runat="server" meta:resourcekey="eddigmennekLBResource1"></asp:Label>
        <br />  <br />       
        <asp:Label ID="allapotKIIR" runat="server" Text="Állapot jelölése:" meta:resourcekey="allapotKIIRResource1"></asp:Label>
               <br />  <br />       
        <asp:DropDownList ID="allapotDL" runat="server" meta:resourcekey="allapotDLResource1" >
              <asp:ListItem Text="Gondolkodok" Value="1" meta:resourcekey="ListItemResource1" />
              <asp:ListItem Text="Ott leszek" Value="2" meta:resourcekey="ListItemResource2" />
              <asp:ListItem Text="Nem megyek" Value="3" meta:resourcekey="ListItemResource3" />        
           </asp:DropDownList>

     <asp:Label ID="allapot_ott_volt" runat="server" Text="Részt vett" Visible="false" meta:resourcekey="allapot_ott_volt_Resource1"></asp:Label>

        <asp:Button ID="allapotmentBT" runat="server" Text="Mentés" OnClick="Save_Click" meta:resourcekey="allapotmentBTResource1" />
          <asp:Label ID="tesztLb" runat="server" meta:resourcekey="tesztLbResource1"></asp:Label>

        <br />  <br />  
         <asp:DropDownList ID="ertekelesDL" runat="server" meta:resourcekey="ertekelesDLResource1" Visible="False" >
              <asp:ListItem Text="1" Value="1" />
              <asp:ListItem Text="2" Value="2" />
              <asp:ListItem Text="3" Value="3" />
              <asp:ListItem Text="4" Value="4" />
              <asp:ListItem Text="5" Value="5" />
            </asp:DropDownList>
        
        <asp:Button ID="ertekelesmentBT" runat="server" Text="Értékelés mentése" Visible="False" meta:resourcekey="ertekelesmentBTResource1" OnClick="Save_Click2" />
    </asp:Panel>
    
    <br />
</asp:Content>

