<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="webcam.aspx.cs" Inherits="kangxh.com.html5.IaaS.webcam" %>

<!DOCTYPE html>
<!-- https://www.html5rocks.com/en/tutorials/getusermedia/intro/#toc-screenshot -->
<!-- https://github.com/Azure/azure-storage-node#azure-storage-javascript-client-library-for-browsers -->


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Save Block blob via SAS</title>
</head>
<body>
    <div id="screenshot" style="text-align:center;">
        <video class="videostream" autoplay></video>
        <img id="screenshot-img">
        <br />
        <button class="capture-button">Start Camera</button>
        <button id="screenshot-button" disabled>Take Screenshot</button>
        <button id="saveBlob-button" disabled>Save Image to Blob</button>
    </div>

    <script src="azure-storage.blob.js"></script>
    <script src="webcam.js"></script>

    <form id="blobInfoForm" runat="server">
        <asp:Label ID="blobAccountLabel" runat="server" Text="Storage Account Uri"></asp:Label>
    <div id="blobInfo">
        <asp:TextBox ID="blobAccountTextbox" runat="server" Height="25px" Width="1137px"></asp:TextBox>
        <br />
        <asp:Label ID="sasTokenLabel" runat="server" Text="Storage Account SAS Token"></asp:Label>
        <br />
        <asp:TextBox ID="sasTokenTextBox" runat="server" Height="25px" Width="1137px"></asp:TextBox>
    </div>
    </form>
</body>
</html>
