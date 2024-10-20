<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManagerDashboard.aspx.cs" Inherits="InventoryManagement.Manager" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manager Dashboard</title>
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
        <h2>Manager Dashboard</h2>
        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>

        <!-- Manager Profile Section -->
        <h3>Edit Profile</h3>
        <div class="profile-section">
            <asp:Label ID="lblManagerName" runat="server" Text="Supplier Name:"></asp:Label>
            <asp:TextBox ID="txtManagerName" runat="server" ReadOnly="True"></asp:TextBox>
            <br />
            <asp:Label ID="lblManagerEmail" runat="server" Text="Supplier Email:"></asp:Label>
            <asp:TextBox ID="txtManagerEmail" runat="server" ReadOnly="True"></asp:TextBox>
            <br />
            <asp:Button ID="btnUpdateProfile" runat="server" Text="Update Profile" OnClick="btnUpdateProfile_Click" />
            <br />
            <asp:Label ID="lblProfileMessage" runat="server" ForeColor="Green"></asp:Label>
        </div>

        <!-- Dashboard: Products and Orders -->
        <div class="dashboard">
            <!-- Products Section -->
            <div class="section">
                <h3>All Products</h3>
                <asp:DropDownList ID="ddlSortProducts" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSortProducts_SelectedIndexChanged">
                    <asp:ListItem Text="Sort by Name" Value="Name" />
                    <asp:ListItem Text="Sort by Price" Value="Price" />
                    <asp:ListItem Text="Sort by Date Added" Value="DateAdded" />
                </asp:DropDownList>

                <asp:Repeater ID="rptProducts" runat="server">
                    <HeaderTemplate>
                        <table class="table">
                            <tr>
                                <th>Product Name</th>
                                <th>Price</th>
                                <th>Date Added</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Name") %></td>
                            <td><%# Eval("Price", "{0:C}") %></td>
                            <td><%# Eval("DateAdded", "{0:yyyy-MM-dd}") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>

            <!-- Orders Section -->
            <div class="section">
                <h3>All Orders</h3>
                <asp:DropDownList ID="ddlSortOrders" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSortOrders_SelectedIndexChanged">
                    <asp:ListItem Text="Sort by Order Date" Value="OrderDate" />
                    <asp:ListItem Text="Sort by Consumer Email" Value="ConsumerEmail" />
                </asp:DropDownList>

                <asp:Repeater ID="rptOrders" runat="server">
                    <HeaderTemplate>
                        <table class="table">
                            <tr>
                                <th>Order Date</th>
                                <th>Consumer Email</th>
                                <th>Status</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("OrderDate", "{0:yyyy-MM-dd}") %></td>
                            <td><%# Eval("ConsumerEmail") %></td>
                            <td><%# Eval("Status") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </div>
    </form>
</body>
</html>
