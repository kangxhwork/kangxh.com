<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="managek8scluster.aspx.cs" Inherits="kangxh.com.container.managek8scluster" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="startK8sCluster" runat="server" OnClick="startK8sCluster_Click" Text="Start K8S Cluster" />
            <asp:Button ID="stopK8sCluster" runat="server" Text="Stop K8S Cluster" />
        </div>
    </form>
</body>
</html>
