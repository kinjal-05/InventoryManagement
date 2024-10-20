<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="InventoryManagement.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Register</h2>
        <div>
            <asp:Label ID="lblUsername" runat="server" Text="Username: "></asp:Label>
            <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox><br />
            <asp:Label ID="lblEmail" Text="Email" runat="server"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" ControlToValidate="txtEmail" ErrorMessage="Email is required" runat="server" ForeColor="Red" /><br />
            <asp:Label ID="lblPassword" runat="server" Text="Password: "></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox><br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPassword" ErrorMessage="Password is required" runat="server" ForeColor="Red" />
            <asp:Label ID="lblRole" runat="server" Text="Role: "></asp:Label>
            <asp:DropDownList ID="ddlRole" runat="server">
                <asp:ListItem Value="0">Select your role.</asp:ListItem>
                <asp:ListItem Value="1">Supplier</asp:ListItem>
                <asp:ListItem Value="2">Consumer</asp:ListItem>
            </asp:DropDownList>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlRole" Operator="Equal" Type="Integer" Display="Dynamic" ErrorMessage="Select either from Supplier or Consumer" ValueToCompare="0"></asp:CompareValidator>
            <br />
            
            <asp:Label ID="lblPhone" Text="Phone" runat="server"></asp:Label>
            <asp:TextBox ID="txtPhone" runat="server" MaxLength="10" ToolTip="Enter you Phone number"></asp:TextBox>
            

            <br />

            <asp:Label ID="lblAddress" Text="Address" runat="server"></asp:Label>
            <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
            
            
            <br />
            
            <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="RegisterUser" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="LoginUser" />
        </div>
    </form>
</body>
</html>
