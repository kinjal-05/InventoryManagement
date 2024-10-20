using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace InventoryManagement
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

        }
        protected void RegisterUser(object sender, EventArgs e)
        {
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rajvi\OneDrive\Documents\sem_5\WAD\InventoryManagement\App_Data\InventoryDatabase.mdf;Integrated Security=True" ;
            string query = "INSERT INTO Users (Username,Email, Password, Role,Phone,Address) VALUES (@Username,@Email, @Password, @Role,@Phone,@Address)";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                cmd.Parameters.AddWithValue("@Role", ddlRole.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text);
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            Response.Redirect("Login.aspx");
        }

        protected void LoginUser(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}