<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Fooldal.aspx.cs" Inherits="Fooldal" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" Height="194px" style="margin-bottom: 0px" meta:resourcekey="Panel1Resource1">
    <asp:Label ID="lbMain" runat="server" Text="Fooldal" meta:resourcekey="lbMainResource1"></asp:Label>
       <br /> 
       <br />       
          
         <br />
         <br />
    <asp:Label ID="searchLB" runat="server" Text="Mit keresel?" meta:resourcekey="searchLBResource1"></asp:Label>
        <br />
        <asp:Table ID="Table1" runat="server" ViewStateMode="Disabled" BackColor="Silver" BorderColor="#999999" BorderStyle="Solid" CellPadding="1" CellSpacing="1">
            <asp:TableRow runat="server" >
                <asp:TableCell runat="server" >
                    <asp:TextBox runat="server" ID="esemnynevTB" placeholder="<%$ Resources:String,esemnynevTB_ph %>" ></asp:TextBox>
                      <br />    <br />
                     <asp:TextBox ID="leirasTB" runat="server" placeholder="<%$ Resources:String,leirasTB_ph %>"  ></asp:TextBox>
                           <br />            <br />
                     <asp:TextBox ID="orszagTB" runat="server" placeholder="<%$ Resources:String,orszagTB_ph %>"  ></asp:TextBox>
                        <br />      <br />
                        <asp:TextBox ID="varosTB" runat="server" placeholder="<%$ Resources:String,varosTB_ph %>"  ></asp:TextBox>
                           <br />   <br />   <br />
                     <br />
        <asp:Button ID="keresésBT" runat="server" Text="Keresés" OnClick="Kereses_Click" meta:resourcekey="keresésBTResource1" />
</asp:TableCell>
                <asp:TableCell runat="server" ><asp:Label ID="mettolKIIRLB" runat="server" Text="Mettől?" meta:resourcekey="mettolKIIRLBResource1"></asp:Label>
                     
                     <br />
                     <asp:Label ID="mettolLB" runat="server" meta:resourcekey="mettolLBResource1"></asp:Label>
<asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar>
  
                    <br />
                </asp:TableCell>
                <asp:TableCell runat="server"><asp:Label ID="meddigKIIRLB" runat="server" Text="Meddig?"></asp:Label>

                         <br />
                    <asp:Label ID="meddigLB" runat="server" meta:resourcekey="meddigLBResource1"></asp:Label>
<asp:Calendar ID="Calendar2" runat="server" OnSelectionChanged="Calendar2_SelectionChanged" meta:resourcekey="Calendar2Resource1"></asp:Calendar>

                        <br />
                </asp:TableCell>
            </asp:TableRow>
           
        </asp:Table>
           
           
        <br />
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        <asp:Label ID="probaLB" runat="server" meta:resourcekey="probaLBResource1"></asp:Label>

          
</asp:Panel>
<p>
</p>
</asp:Content>

