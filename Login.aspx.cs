using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
namespace InventoryManagement
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void LoginUser(object sender, EventArgs e)
        {
            SqlConnection connStr = new SqlConnection();
            connStr.ConnectionString = ConfigurationManager.ConnectionStrings["inventoryConnection"].ConnectionString;
   
            string query = "SELECT * FROM Users WHERE Email = @Email AND Password = @Password AND Role=@Role";

            using (connStr)
            {
                SqlCommand cmd = new SqlCommand(query, connStr);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@Role", ddlRole.Text);
                connStr.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Create session for user
                    Session["User"] = reader["Email"].ToString();
                    Session["Role"] = reader["Role"].ToString();

                    // Redirect based on role
                    string role = reader["Role"].ToString();
                    if (role == "Supplier")
                        Response.Redirect("~/Supplier/SupplierDashboard.aspx");
                    else if (role == "Consumer")
                        Response.Redirect("~/Consumer/ConsumerDashboard.aspx");
                    else if (role == "Manager")
                        Response.Redirect("~/Manager/ManagerDashboard.aspx");
                }
            }
        }

        protected void RegisterUser(object sender, EventArgs e)
        {
            Response.Redirect("Registration.aspx");
        }
    }
}