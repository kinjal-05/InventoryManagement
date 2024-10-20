<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierDashboard.aspx.cs" Inherits="InventoryManagement.Supplier" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Supplier Dashboard</title>
    <link rel="stylesheet" type="text/css" href="Styles/dashboard.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div id="dashboard">
            <!-- Sidebar for Navigation -->
            <nav id="sidebar">
                <ul>
                    <li><a href="#addProduct">Add Product</a></li>
                    <li><a href="#viewProducts">View My Products</a></li>
                    <li><a href="#searchProduct">Search & Sort Products</a></li>
                    <li><a href="#editProfile">Edit Profile</a></li>
                </ul>
            </nav>

            <!-- Main Content Section -->
            <div id="mainContent">
                <!-- Add and Manage Products -->
                <section id="addProduct">
                    <h2>Add or Manage Products</h2>
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>
                    <br />
                    <asp:Label ID="lblProductName" runat="server" Text="Product Name:"></asp:Label>
                    <asp:TextBox ID="txtProductName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvProductName" runat="server" ControlToValidate="txtProductName" ErrorMessage="Product Name is required" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
                    <asp:Label ID="lblProductPrice" runat="server" Text="Product Price:"></asp:Label>
                    <asp:TextBox ID="txtProductPrice" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvProductPrice" runat="server" ControlToValidate="txtProductPrice" ErrorMessage="Product Price is required" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
                    <asp:Label ID="lblProductDate" runat="server" Text="Product Date:"></asp:Label>
                    <asp:TextBox ID="txtProductDate" runat="server" TextMode="Date"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvProductDate" runat="server" ControlToValidate="txtProductDate" ErrorMessage="Product Date is required" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
                    <asp:Button ID="btnAddProduct" runat="server" Text="Add Product" OnClick="btnAddProduct_Click" />
                    <asp:Button ID="btnUpdateProduct" runat="server" Text="Update Product" OnClick="btnUpdateProduct_Click" />
                    <asp:Button ID="btnDeleteProduct" runat="server" Text="Delete Product" OnClick="btnDeleteProduct_Click" />
                </section>

                <!-- View All Products by Supplier -->
                <section id="viewProducts">
                    <h2>My Products</h2>
                    <asp:GridView ID="gvMyProducts" runat="server" AutoGenerateColumns="True"></asp:GridView>
                </section>

                <!-- Search and Sort Products -->
                <section id="searchProduct">
                    <h2>Search & Sort Products</h2>
                    <asp:Label ID="lblSearchName" runat="server" Text="Search by Name:"></asp:Label>
                    <asp:TextBox ID="txtSearchName" runat="server"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblSearchDate" runat="server" Text="Search by Date:"></asp:Label>
                    <asp:TextBox ID="txtSearchDate" runat="server" TextMode="Date"></asp:TextBox>
                    <br />
                    <asp:DropDownList ID="ddlSort" runat="server">
                        <asp:ListItem Text="Sort by Name" Value="Name"></asp:ListItem>
                        <asp:ListItem Text="Sort by Date" Value="DateAdded"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnSearchProduct" runat="server" Text="Search" OnClick="btnSearchProduct_Click" />
                    <asp:GridView ID="gvSearchResults" runat="server" AutoGenerateColumns="True"></asp:GridView>
                </section>

                <!-- Edit Supplier Profile -->
                <section id="editProfile">
                    <h2>Edit Profile</h2>
                    <asp:Label ID="lblSupplierName" runat="server" Text="Supplier Name:"></asp:Label>
                    <asp:TextBox ID="txtSupplierName" runat="server" ReadOnly="True"></asp:TextBox>
                    <br />
                    <asp:Label ID="lblSupplierEmail" runat="server" Text="Supplier Email:"></asp:Label>
                    <asp:TextBox ID="txtSupplierEmail" runat="server" ReadOnly="True"></asp:TextBox>
                    <br />
                    <asp:Button ID="btnUpdateProfile" runat="server" Text="Update Profile" OnClick="btnUpdateProfile_Click" />
                    <asp:Label ID="lblProfileMessage" runat="server" ForeColor="Green"></asp:Label>
                </section>
            </div>
        </div>
    </form>
</body>
</html>
