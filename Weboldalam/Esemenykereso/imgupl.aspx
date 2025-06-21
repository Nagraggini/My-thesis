<%@ Page Language="C#" AutoEventWireup="true" CodeFile="imgupl.aspx.cs" Inherits="namoona_imgupl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload Image</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br/>Select Image<br/>
        <asp:FileUpload runat="server" ID="flImage" />
<asp:Button runat="server" ValidationGroup="Details" ID="btnSubmit" Text="Upload" onclick="btnSubmit_Click" />
       <br />  <br />
    
            <asp:Image runat="server" ID="ImagePreview" Height="164px" Width="125px" />
        <br />  <br />
        <asp:Label ID="lblRes" runat="server" Text=" "></asp:Label>
       <%-- <asp:Image ID="picone" ImageUrl="ImgHandler.ashx?esemenyID=8" runat="server" />--%>
    </div>
    </form>
</body>
</html>
