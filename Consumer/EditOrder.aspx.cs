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
    public partial class EditOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int orderID = Convert.ToInt32(Request.QueryString["OrderID"]);
                LoadOrderItems(orderID);
            }
        }

        private void LoadOrderItems(int orderID)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["inventoryConnection"].ConnectionString))
            {
                string query = "SELECT oi.ProductID, p.Name as ProductName, oi.Quantity, oi.Price FROM OrderItems oi INNER JOIN Products p ON oi.ProductID = p.ProductID WHERE oi.OrderID = @OrderID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@OrderID", orderID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvOrderItems.DataSource = dt;
                gvOrderItems.DataBind();
            }
        }

        protected void btnUpdateOrder_Click(object sender, EventArgs e)
        {
            int orderID = Convert.ToInt32(Request.QueryString["OrderID"]);
            decimal totalAmount = 0;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["inventoryConnection"].ConnectionString))
            {
                conn.Open();

                foreach (GridViewRow row in gvOrderItems.Rows)
                {
                    int productID = Convert.ToInt32(row.Cells[0].Text);
                    int quantity = Convert.ToInt32(((TextBox)row.FindControl("txtQuantity")).Text);
                    decimal price = Convert.ToDecimal(row.Cells[2].Text);

                    SqlCommand cmd = new SqlCommand("UPDATE OrderItems SET Quantity = @Quantity WHERE OrderID = @OrderID AND ProductID = @ProductID", conn);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    cmd.Parameters.AddWithValue("@OrderID", orderID);
                    cmd.Parameters.AddWithValue("@ProductID", productID);
                    cmd.ExecuteNonQuery();

                    totalAmount += price * quantity;
                }

                SqlCommand updateOrderTotalCmd = new SqlCommand("UPDATE Orders SET TotalAmount = @TotalAmount WHERE OrderID = @OrderID", conn);
                updateOrderTotalCmd.Parameters.AddWithValue("@TotalAmount", totalAmount);
                updateOrderTotalCmd.Parameters.AddWithValue("@OrderID", orderID);
                updateOrderTotalCmd.ExecuteNonQuery();
            }

            Response.Redirect("ConsumerDashboard.aspx");
        }
    }
}