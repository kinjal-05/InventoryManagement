<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsumerDashboard.aspx.cs" Inherits="InventoryManagement.Consumer.ConsumerDashboard" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Consumer Dashboard</title>
    <style>
        .dashboard {
            display: flex;
            justify-content: space-around;
        }

        .section {
            width: 45%;
        }

        .table {
            width: 100%;
            border-collapse: collapse;
        }

        .table th, .table td {
            border: 1px solid #ccc;
            padding: 10px;
            text-align: left;
        }

        .profile-section {
            margin: 20px 0;
        }

        .error {
            color: red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Consumer Dashboard</h2>

        <!-- Profile Section -->
        <h3>Edit Profile</h3>
        <div class="profile-section">
            <asp:Label ID="lblConsumerName" runat="server" Text="Supplier Name:"></asp:Label>
            <asp:TextBox ID="txtConsumerName" runat="server" ReadOnly="True"></asp:TextBox>
            <br />
            <asp:Label ID="lblConsumerEmail" runat="server" Text="Supplier Email:"></asp:Label>
            <asp:TextBox ID="txtConsumerEmail" runat="server" ReadOnly="True"></asp:TextBox>
            <br />
            <asp:Button ID="btnUpdateProfile" runat="server" Text="Update Profile" OnClick="btnUpdateProfile_Click" />
        </div>
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>

        <h2>Product List</h2>
        <asp:GridView ID="gvProducts" runat="server"  DataKeyNames="ProductID" OnRowCommand="gvProducts_RowCommand">
            <Columns>
                <asp:BoundField DataField="ProductID" HeaderText="Product ID" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Price" HeaderText="Price" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity Available" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
                <asp:ButtonField Text="Add to Cart" CommandName="AddToCart" ButtonType="Button" />
            </Columns>
        </asp:GridView>

        <h2>My Orders</h2>
        <asp:GridView ID="gvOrders" runat="server" DataKeyNames="OrderID" AutoGenerateColumns="False" OnRowCommand="gvOrders_RowCommand">
            <Columns>
                <asp:BoundField DataField="OrderID" HeaderText="Order ID" />
                <asp:BoundField DataField="OrderDate" HeaderText="Order Date" />
                <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:ButtonField runat="server" Text="Edit" CommandName="EditOrder" ButtonType="Button"  />
                <asp:ButtonField Text="Delete" CommandName="DeleteOrder" ButtonType="Button" />
                <asp:ButtonField Text="Download" CommandName="DownloadOrder" ButtonType="Button" />
                
            </Columns>
        </asp:GridView>

        <h2>Cart</h2>
        <asp:GridView ID="gvCart" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="ProductID" HeaderText="Product ID" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                <asp:BoundField DataField="Price" HeaderText="Price" />
                <asp:BoundField DataField="Total" HeaderText="Total" />
            </Columns>
        </asp:GridView>
        <asp:Button ID="btnPlaceOrder" runat="server" Text="Place Order" OnClick="btnPlaceOrder_Click" />
    </form>
</body>

</html>
