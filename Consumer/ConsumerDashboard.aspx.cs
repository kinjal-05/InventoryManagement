using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace InventoryManagement.Consumer
{
    public partial class ConsumerDashboard : System.Web.UI.Page
    {
        private DataTable cartTable;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["User"] == null || Session["Role"].ToString() != "Consumer")
                {
                    Response.Redirect("~/Login.aspx");
                }
                LoadConsumerProfile();
                LoadProducts();
                LoadOrders();
                InitializeCart();
            }

        }
        protected void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Profile.aspx");
        }
        private void LoadConsumerProfile()
        {
            string email = Session["User"].ToString();
            string role = Session["Role"].ToString();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["inventoryConnection"].ConnectionString))
            {
                string query = "SELECT Username, Email FROM Users WHERE Email = @Email AND Role=@Role";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Role", role);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtConsumerName.Text = reader["Username"].ToString();
                    txtConsumerEmail.Text = reader["Email"].ToString();

                }
                conn.Close();
            }
        }

        private void LoadProducts()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["inventoryConnection"].ConnectionString))
            {
                string query = "SELECT * FROM Products";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvProducts.DataSource = dt;
                gvProducts.DataBind();
            }
        }

        private void LoadOrders()
        {
            string consumerEmail = Session["User"].ToString();
            int consumerID = -1;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["inventoryConnection"].ConnectionString))
            {
                conn.Open();
                string query2 = "SELECT UserID FROM Users WHERE Email = @Email";
                SqlCommand cmd2 = new SqlCommand(query2, conn);
                cmd2.Parameters.AddWithValue("@Email", consumerEmail);
                object result = cmd2.ExecuteScalar();
                conn.Close();
                // If result is not null, set supplierID
                if (result != null)
                {
                    consumerID = Convert.ToInt32(result);
                }
                string query = "SELECT * FROM Orders WHERE ConsumerID = @ConsumerID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ConsumerID", consumerID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvOrders.DataSource = dt;
                gvOrders.DataBind();
            }
        }

        private void InitializeCart()
        {
            cartTable = new DataTable();
            cartTable.Columns.Add("ProductID");
            cartTable.Columns.Add("Name");
            cartTable.Columns.Add("Quantity");
            cartTable.Columns.Add("Price");
            cartTable.Columns.Add("Total");
            ViewState["Cart"] = cartTable;
        }

        protected void gvProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "AddToCart")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int productID = Convert.ToInt32(gvProducts.DataKeys[rowIndex].Value);
                string name = gvProducts.Rows[rowIndex].Cells[1].Text;
                decimal price = Convert.ToDecimal(gvProducts.Rows[rowIndex].Cells[2].Text);

                cartTable = ViewState["Cart"] as DataTable;
                DataRow row = cartTable.NewRow();
                row["ProductID"] = productID;
                row["Name"] = name;
                row["Quantity"] = 1;
                row["Price"] = price;
                row["Total"] = price;
                cartTable.Rows.Add(row);

                gvCart.DataSource = cartTable;
                gvCart.DataBind();
            }
        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {

            decimal totalAmount = 0;
            string consumerEmail = Session["User"].ToString();
            int consumerID = -1;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["inventoryConnection"].ConnectionString))
            {
                conn.Open();
                string query2 = "SELECT UserID FROM Users WHERE Email = @Email";
                SqlCommand cmd2 = new SqlCommand(query2, conn);
                cmd2.Parameters.AddWithValue("@Email", consumerEmail);
                object result = cmd2.ExecuteScalar();
                conn.Close();
                // If result is not null, set supplierID
                if (result != null)
                {
                    consumerID = Convert.ToInt32(result);
                }
                SqlCommand cmd = new SqlCommand("INSERT INTO Orders (ConsumerID, OrderDate, TotalAmount, Status) OUTPUT INSERTED.OrderID VALUES (@ConsumerID, @OrderDate, @TotalAmount, @Status)", conn);
                cmd.Parameters.AddWithValue("@ConsumerID", consumerID);
                cmd.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@TotalAmount", totalAmount);  // Calculate from cart
                cmd.Parameters.AddWithValue("@Status", "Pending");

                conn.Open();
                int orderID = (int)cmd.ExecuteScalar();

                foreach (GridViewRow row in gvCart.Rows)
                {
                    int productID = Convert.ToInt32(row.Cells[0].Text);
                    int quantity = Convert.ToInt32(row.Cells[2].Text);
                    decimal price = Convert.ToDecimal(row.Cells[3].Text);

                    SqlCommand orderItemCmd = new SqlCommand("INSERT INTO OrderItems (OrderID, ProductID, Quantity, Price) VALUES (@OrderID, @ProductID, @Quantity, @Price)", conn);
                    orderItemCmd.Parameters.AddWithValue("@OrderID", orderID);
                    orderItemCmd.Parameters.AddWithValue("@ProductID", productID);
                    orderItemCmd.Parameters.AddWithValue("@Quantity", quantity);
                    orderItemCmd.Parameters.AddWithValue("@Price", price);
                    orderItemCmd.ExecuteNonQuery();

                    totalAmount += price * quantity;
                }

                SqlCommand updateOrderTotalCmd = new SqlCommand("UPDATE Orders SET TotalAmount = @TotalAmount WHERE OrderID = @OrderID", conn);
                updateOrderTotalCmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                updateOrderTotalCmd.Parameters.AddWithValue("@OrderID", orderID);
                updateOrderTotalCmd.ExecuteNonQuery();
            }

            //cartTable.Clear();
            gvCart.DataBind();
            LoadOrders();
        }

        protected void gvOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex;
            int orderID=(-1);
            if (int.TryParse(e.CommandArgument.ToString(), out rowIndex))
            {
                orderID = Convert.ToInt32(gvOrders.DataKeys[rowIndex].Value);
            }

            if (e.CommandName == "EditOrder")
            {
                Response.Redirect($"EditOrder.aspx?OrderID={orderID}");
            }
            else if (e.CommandName == "DeleteOrder")
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["inventoryConnection"].ConnectionString))
                {
                    SqlCommand cmd2 = new SqlCommand("DELETE OrderItems FROM OrderItems JOIN Orders ON Orders.OrderID = OrderItems.OrderID WHERE Orders.OrderID = @OrderID",conn);
                    
                    cmd2.Parameters.AddWithValue("@OrderID", orderID);
                    conn.Open();
                    cmd2.ExecuteNonQuery();
                    conn.Close();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Orders WHERE OrderID = @OrderID", conn);
                    cmd.Parameters.AddWithValue("@OrderID", orderID);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    
                }
                LoadOrders();
            }
            else if (e.CommandName == "DownloadOrder")
            {
                DownloadOrder(orderID);
                // Code for downloading the order as a file (CSV, PDF, etc.)
            }
        }
        private void DownloadOrder(int orderID)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["inventoryConnection"].ConnectionString))
            {
                string query = @"SELECT o.OrderID, o.TotalAmount, oi.Quantity, oi.Price, p.Name 
                         FROM Orders o
                         INNER JOIN OrderItems oi ON o.OrderID = oi.OrderID
                         INNER JOIN Products p ON oi.ProductID = p.ProductID
                         WHERE o.OrderID = @OrderID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OrderID", orderID);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    Response.Clear();
                    Response.ContentType = "text/plain";
                    Response.AddHeader("content-disposition", $"attachment;filename=Order_{orderID}.txt");

                    Response.Write("Order ID: " + orderID + "\n");
                    Response.Write("Product Name\tQuantity\tPrice\tTotal\n");

                    decimal totalAmount = 0;
                    while (reader.Read())
                    {
                        string productName = reader["Name"].ToString();
                        int quantity = Convert.ToInt32(reader["Quantity"]);
                        decimal price = Convert.ToDecimal(reader["Price"]);
                        decimal total = price * quantity;

                        Response.Write($"{productName}\t{quantity}\t{price}\t{total}\n");
                        totalAmount += total;
                    }

                    Response.Write("\nTotal Amount: " + totalAmount);
                    Response.End();
                }
            }
        }

    }
}