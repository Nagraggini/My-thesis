<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Esemeny_letrehozas.aspx.cs" Inherits="_Default" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" Height="248px" meta:resourcekey="Panel1Resource1">
        <asp:Label ID="esemalapLb" runat="server" Text="Esemény alapadatainak megadása:" meta:resourcekey="esemalapLbResource1"></asp:Label>
            <br />
         <asp:Table ID="Table1" runat="server" ViewStateMode="Disabled" BackColor="Silver" BorderColor="#999999" BorderStyle="Solid" CellPadding="1" CellSpacing="1" >
       <asp:TableRow ID="TableRow1" runat="server" >
           <asp:TableCell ID="TableCell1" runat="server" ><asp:TextBox ID="EsemnyneveTB" runat="server" placeholder="<%$ Resources:String,esemnynevTB_ph %>" ></asp:TextBox>

            <br />
                 <br />
               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Ez nem maradhat üresen." ControlToValidate="EsemnyneveTB" ValidationGroup="Kihagyo" ></asp:RequiredFieldValidator>

        
               <br />
               <textarea id="LeirasTA" runat="server" cols="20" rows="3" placeholder="<%$ Resources:String,leirasTB_ph %>"></textarea>

             <br />
            <br />
        <asp:TextBox ID="OrszagTB" runat="server" placeholder="<%$ Resources:String,orszagTB_ph %>"></asp:TextBox>

            <br />
        <asp:TextBox ID="VarosTB" runat="server" placeholder="<%$ Resources:String,varosTB_ph %>"></asp:TextBox>

            <br />
        <asp:TextBox ID="UtcaTB" runat="server" placeholder="<%$ Resources:String,UtcaTB %>"></asp:TextBox>

            <br />
        <asp:TextBox ID="HazszamTB" runat="server" placeholder="<%$ Resources:String,HazszamTB %>" ></asp:TextBox>

            <br />
        <asp:TextBox ID="IranyitoszamTB" runat="server" placeholder="<%$ Resources:String,IranyitoszamTB %>" ></asp:TextBox>

            <br />
               <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
               <cc1:CascadingDropDown ID="CascadingDropDown1" runat="server"
                TargetControlID="AltipusDL"
                Category="Altípus"
                LoadingText="[Altípusok betöltése...]"
                ServicePath="TipusService.asmx"
                ServiceMethod="GetDropDownContents"
                ParentControlID="TipusDL"
                SelectedValue="1" />

        <asp:DropDownList ID="TipusDL" runat="server" EnableViewState="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" meta:resourcekey="TipusDLResource1" ></asp:DropDownList>
             
             <br />
                       

        <asp:DropDownList ID="AltipusDL" runat="server" EnableViewState="true" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" meta:resourcekey="AltipusDLResource1" ></asp:DropDownList>
               <%--OnLoad="DropDownList2_Load"--%>
           <%--AutoPostBack="True"--%>
        <br />
        
          <asp:FileUpload runat="server" ID="flImage" meta:resourcekey="flImageResource1" />

                   <br />  
               </asp:TableCell>
             <asp:TableCell ID="TableCell2" runat="server" >
            <br />   
        <asp:Label ID="MettolLB" runat="server" Text="Mettől?" meta:resourcekey="MettolLBResource1"></asp:Label>

             <br />
        <asp:Label ID="MettolLB_E" runat="server" meta:resourcekey="MettolLB_EResource1"></asp:Label>
<asp:Calendar ID="MettolCD" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" ></asp:Calendar>
     <%--meta:resourcekey="MettolCDResource1"--%>
            <br />
                 </asp:TableCell>
                 <asp:TableCell ID="TableCell3" runat="server" >
                       <br />
                       <br />
        <asp:Label ID="MeddigLB" runat="server" Text="Meddig?" meta:resourcekey="MeddigLBResource1"></asp:Label>

             <br />
        <asp:Label ID="MeddigLB_E" runat="server" meta:resourcekey="MeddigLB_EResource1"></asp:Label>
<asp:Calendar ID="MeddigCD" runat="server" OnSelectionChanged="Calendar2_SelectionChanged" meta:resourcekey="MeddigCDResource1"></asp:Calendar>
    <%--meta:resourcekey="MeddigCDResource1"--%>
            <br />
            <br />                  
       
                 </asp:TableCell>
           </asp:TableRow>
             </asp:Table>
             <br />
            <br />  
        <asp:Button ID="Esemny_letrehoz" runat="server" Text="Esemény létrehozása" ValidationGroup="Kihagyo" OnClick="Esemnyletrehoz_Click" meta:resourcekey="Esemny_letrehozResource1" /> 
        <br />
         <asp:Label ID="lblRes" runat="server" Text=" " meta:resourcekey="lblResResource1"></asp:Label> 
            <br /> 
        <asp:Label ID="ErtesitesLB" runat="server" meta:resourcekey="ErtesitesLBResource1"></asp:Label>
        <br /> 
       
    </asp:Panel>
    <br />
   
  </asp:Content>

