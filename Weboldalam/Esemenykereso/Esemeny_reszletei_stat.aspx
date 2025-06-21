<%@ Page Language="C#"  AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="Esemeny_reszletei_stat.aspx.cs" Inherits="Esemeny_reszletei_stat" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
     
<asp:Panel ID="Panel1" runat="server" Height="397px" meta:resourcekey="Panel1Resource1">
         <br />
        <br />
        <asp:Label ID="esemenyreszletKIIR" runat="server" Text="Esemény részletei:" BorderStyle="Double" meta:resourcekey="esemenyreszletKIIRResource1"></asp:Label>
      <br />  <br />  
            <asp:Image runat="server" ID="ImagePreview" Height="164px" Width="125px" ImageUrl="~/nopic.jpg" meta:resourcekey="ImagePreviewResource1" />
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
          <asp:Label ID="esemenyhelyKIIR" runat="server" Text="Esemény helye:" Visible="False" meta:resourcekey="esemenyhelyKIIRResource1"  ></asp:Label>
        <br />  <br />    
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
    <%-- eddig hanyan mennek statisztika --%>    
         <asp:Label ID="eddigmennekKIIR" runat="server" Text="Eddig hányan mennek:" meta:resourcekey="eddigmennekKIIRResource1"></asp:Label>
        <br />  <br />       
        <asp:Label ID="eddigmennekLB" runat="server" meta:resourcekey="eddigmennekLBResource1"></asp:Label>
        <br />  <br />       
    <%--ertekeles--%>
    <asp:Label ID="ertekeles_avg_KIIR" runat="server" Text="Értékelések átlaga:" meta:resourcekey="ertekeles_avg_KIIRResource1"></asp:Label>
        <br />  <br />       
        <asp:Label ID="ertekeles_avg_LB" runat="server" meta:resourcekey="ertekeles_avg_LBResource1"></asp:Label>
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
    <%-- diagrammok --%>
    <asp:Table ID="Table1" runat="server" Visible="False" ViewStateMode="Disabled" BackColor="Silver" BorderColor="#999999" BorderStyle="Solid" CellPadding="1" CellSpacing="1">
            <asp:TableRow ID="TableRow1" runat="server" >
                <asp:TableCell ID="TableCell1" runat="server" >

                    <asp:Chart ID="Chart_nem" runat="server"  meta:resourcekey="Chart_nemResource1">        
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea1" ></asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>

                </asp:TableCell>
                <asp:TableCell ID="TableCell2" runat="server" >
                 
                     <asp:Chart ID="Chart_kor" runat="server"  meta:resourcekey="Chart_korResource1">        
                   <ChartAreas>
                   <asp:ChartArea Name="ChartArea2"></asp:ChartArea>
                    </ChartAreas>
                    </asp:Chart>
                     
                </asp:TableCell>               
            </asp:TableRow>

        <asp:TableRow ID="TableRow2" runat="server" >
                <asp:TableCell ID="TableCell3" runat="server" >

                    <asp:Label ID="stat1" runat="server" Text="Az eseményre jelentkezett felhasználók nemeinek aránya." meta:resourcekey="stat1Resource1" ></asp:Label>

                </asp:TableCell>
                <asp:TableCell ID="TableCell4" runat="server" >
                 
                  <asp:Label ID="stat2" runat="server"  Text="Az eseményre jelentkezett felhasználók életkora." meta:resourcekey="stat2Resource1" ></asp:Label>   
                     
                </asp:TableCell>               
            </asp:TableRow>
           <%-- a táblázat alsó fele  --%>

          <asp:TableRow ID="TableRow3" runat="server" >
                <asp:TableCell ID="TableCell5" runat="server" >

                    <asp:Chart ID="Chart_megt" runat="server" meta:resourcekey="Chart_megtResource1">        
                        <ChartAreas>
                            <asp:ChartArea Name="ChartArea3" ></asp:ChartArea>
                        </ChartAreas>
                    </asp:Chart>

                </asp:TableCell>
                <asp:TableCell ID="TableCell6" runat="server" >
                 
                      <asp:Chart ID="Chart_megt2" runat="server" meta:resourcekey="Chart_megt2Resource1">        
                            <ChartAreas>
                                <asp:ChartArea Name="ChartArea4" ></asp:ChartArea>
                            </ChartAreas>
                        </asp:Chart>
                     
                </asp:TableCell>               
            </asp:TableRow>

        <asp:TableRow ID="TableRow4" runat="server" >
                <asp:TableCell ID="TableCell7" runat="server" >

                    <asp:Label ID="stat3" runat="server" Text="Megmutatja, hogy milyen időpontokban (24 órára lebontva)" meta:resourcekey="stat3Resource1" ></asp:Label>
                    <br />
                    <asp:Label ID="Label3_1" runat="server" Text="hány felhasználó változtatta az állapotát, az értékelését" meta:resourcekey="stat3_1Resource1" ></asp:Label>
                    <br />
                    <asp:Label ID="Label3_2" runat="server" Text=", vagy csak megtekintette az eseményt." meta:resourcekey="stat3_2Resource1" ></asp:Label>

                </asp:TableCell>
                <asp:TableCell ID="TableCell8" runat="server" >
                 
                  <asp:Label ID="stat4" runat="server" Text="Megmutatja, hogy mely időpontokban (24 órára lebontva)" meta:resourcekey="stat4Resource1" ></asp:Label>  
                   <br />
                    <asp:Label ID="Label4_1" runat="server" Text="hány felhasználó jelölte, hogy jön az eseményre." meta:resourcekey="stat4_1Resource1" ></asp:Label>  

                     
                </asp:TableCell>               
            </asp:TableRow>
        </asp:Table>
     <br />  <br />     
    
    <asp:Button ID="deleteBT" runat="server" Text="Törlés" OnClick="Del_Click" meta:resourcekey="deleteBTResource1" />
        </asp:Panel>  
</asp:Content>