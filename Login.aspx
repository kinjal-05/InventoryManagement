<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="InventoryManagement.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
</head>
<body>
    <h2>Login</h2>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblEmail" runat="server" Text="Email: "></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox><br />
            <asp:Label ID="lblPassword" runat="server" Text="Password: "></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox><br />
            <asp:DropDownList ID="ddlRole" runat="server">
                <asp:ListItem>Supplier</asp:ListItem>
                <asp:ListItem>Consumer</asp:ListItem>
                <asp:ListItem>Manager</asp:ListItem>
            </asp:DropDownList><br />
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlRole" Operator="Equal" Type="Integer" Display="Dynamic" ErrorMessage="Select either from Supplier or Consumer" ValueToCompare="0"></asp:CompareValidator>
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="LoginUser" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="RegisterUser" />
        </div>
    </form>
</body>
</html>
