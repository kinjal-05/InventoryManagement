<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="InventoryManagement.Default" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inventory Management</title>
</head>
<body>
     <form id="form1" runat="server">
        <h2>Available Products</h2>
        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="Login_Click" Width="116px" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnRegister" runat="server" Text="Registration" OnClick="Register_Click" Width="201px" />
        &nbsp;<div>

            <asp:GridView ID="ProductGrid" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Product Name" />
                    <asp:BoundField DataField="Price" HeaderText="Price" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="Color" HeaderText="Color" />
                    <asp:BoundField DataField="Size" HeaderText="Size" />
                    <asp:ImageField DataImageUrlField="Image" HeaderText="Image" />
                </Columns>
            </asp:GridView>

        </div>
    </form>
</body>
</html>

