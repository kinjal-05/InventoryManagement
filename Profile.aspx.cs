using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace InventoryManagement
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["User"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadProfile();
            }

        }
        private void LoadProfile()
        {
            string email = Session["User"].ToString();
            string role = Session["Role"].ToString();
            //string password = Session["Password"].ToString();
            //string username = Session["Username"].ToString();
            //string address = Session["Address"].ToString();
            //string phone = Session["Phone"].ToString();
            //string connectionString = "your_connection_string";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["inventoryConnection"].ConnectionString))
            {
                string query = "SELECT Username,Email, Password,Role,Phone,Address FROM Users WHERE Email = @Email AND Role=@Role";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Role", role);
                //cmd.Parameters.AddWithValue("@Username", username);
                //cmd.Parameters.AddWithValue("@Password", password);
                //cmd.Parameters.AddWithValue("@Phone", phone);
                //cmd.Parameters.AddWithValue("@Address", address);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtUsername.Text = reader["Username"].ToString();
                    txtEmail.Text = reader["Email"].ToString();
                    txtPassword.Text = reader["Password"].ToString();
                    txtRole.Text = reader["Role"].ToString();
                    txtPhone.Text = reader["Phone"].ToString();
                    txtAddress.Text = reader["Address"].ToString();
                }
                conn.Close();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string email = Session["User"].ToString();
            string role = Session["Role"].ToString();
            string password = txtPassword.Text;
            string username = txtUsername.Text;
            string phone = txtPhone.Text;
            string address = txtAddress.Text;
            
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["inventoryConnection"].ConnectionString))
            {
                string query = "UPDATE Users SET Username=@Username,Password = @Password,Phone=@Phone,Address=@Address WHERE Email = @Email AND Role=@Role";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@Address", address);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                lblMessage.Text = "Profile updated successfully.";
            }
            Response.Redirect("~/Manager/ManagerDashboard.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}