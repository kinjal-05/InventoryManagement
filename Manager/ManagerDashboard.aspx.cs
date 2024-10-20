using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace InventoryManagement
{
    public partial class Manager : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Role"] == null || Session["Role"].ToString() != "Manager")
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadProducts();
                LoadOrders();
                LoadProfile();
            }
        }
        private void LoadProfile()
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
                    txtManagerName.Text = reader["Username"].ToString();
                    txtManagerEmail.Text = reader["Email"].ToString();

                }
                conn.Close();
            }
        }

        protected void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Profile.aspx");
        }

        private void LoadProducts(string sortBy = "Name")
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["inventoryConnection"].ConnectionString))
            {
                con.Open();
                
                string sql = $"SELECT Name, Price, DateAdded FROM Products ORDER BY {sortBy}";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                rptProducts.DataSource = dt;
                rptProducts.DataBind();
            }
        }

        private void LoadOrders(string sortBy = "OrderDate")
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["inventoryConnection"].ConnectionString))
            {
                con.Open();
                string sql = $"SELECT o.OrderDate, u.Email AS ConsumerEmail, o.Status FROM Orders o JOIN Users u ON o.ConsumerID = u.UserID ORDER BY {sortBy}";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                rptOrders.DataSource = dt;
                rptOrders.DataBind();
            }
        }

        protected void ddlSortProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadProducts(ddlSortProducts.SelectedValue);
        }

        protected void ddlSortOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadOrders(ddlSortOrders.SelectedValue);
        }

        
    }
}