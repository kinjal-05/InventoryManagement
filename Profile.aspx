<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="InventoryManagement.Profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Profile</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Edit Profile</h2>
            <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>
            <br />
            <asp:Label ID="lblUsername" runat="server" Text="Username: "></asp:Label>
            <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" ReadOnly="True"></asp:TextBox>
            <br />
            <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required" ForeColor="Red"></asp:RequiredFieldValidator>
            .<br />
            <asp:Label ID="lblRole" runat="server" Text="Role: "></asp:Label>
            <asp:TextBox ID="txtRole" runat="server" ReadOnly="True"></asp:TextBox>
            <br />
            <asp:Label ID="lblPhone" Text="Phone" runat="server"></asp:Label>
            <asp:TextBox ID="txtPhone" runat="server" MaxLength="10" ToolTip="Enter you Phone number"></asp:TextBox>
            
            <br />

            <asp:Label ID="lblAddress" Text="Address" runat="server"></asp:Label>
            <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="btnSave" runat="server" Text="Save Changes" OnClick="btnSave_Click" />
            <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" />
        </div>
    </form>
</body>
</html>
