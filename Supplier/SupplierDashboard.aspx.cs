using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace InventoryManagement
{
    public partial class Supplier : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["User"] == null || Session["Role"].ToString() != "Supplier")
                {
                    Response.Redirect("~/Login.aspx");
                }
                LoadSupplierProfile();
                LoadMyProducts();
            }
        }

        private void LoadSupplierProfile()
        {
            string email = Session["User"].ToString();
            string role = Session["Role"].ToString();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["inventoryConnection"].ConnectionString))
            {
                string query = "SELECT Username, Email FROM Users WHERE Email = @Email AND Role=@Role";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Role",role);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtSupplierName.Text = reader["Username"].ToString();
                    txtSupplierEmail.Text = reader["Email"].ToString();
                    
                }
                conn.Close();
            }
        }

        protected void btnUpdateProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Profile.aspx");
            
        }

        protected void btnAddProduct_Click(object sender, EventArgs e)
        {
            string name = txtProductName.Text;
            decimal price = Convert.ToDecimal(txtProductPrice.Text);
            DateTime date = Convert.ToDateTime(txtProductDate.Text);
            string supplierEmail = Session["User"].ToString();
            int supplierID = -1;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["inventoryConnection"].ConnectionString))
            {
                conn.Open();
                string query2 = "SELECT UserID FROM Users WHERE Email = @Email";
                SqlCommand cmd2 = new SqlCommand(query2, conn);
                cmd2.Parameters.AddWithValue("@Email", supplierEmail);
                object result = cmd2.ExecuteScalar();
                conn.Close();
                // If result is not null, set supplierID
                if (result != null)
                {
                    supplierID = Convert.ToInt32(result);
                }

                string query = "INSERT INTO Products (Name, Price, DateAdded, SupplierID) VALUES (@Name, @Price, @DateAdded, @SupplierID)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@DateAdded", date);
                cmd.Parameters.AddWithValue("@SupplierID", supplierID);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                lblMessage.Text = "Product added successfully.";
                LoadMyProducts();
            }
        }

        protected void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            string name = txtProductName.Text;
            decimal price = Convert.ToDecimal(txtProductPrice.Text);
            string supplierEmail = Session["User"].ToString();
            int supplierID = -1;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["inventoryConnection"].ConnectionString))
            {
                string query2 = "SELECT UserID FROM Users WHERE Email = @Email";
                conn.Open();
                SqlCommand cmd2 = new SqlCommand(query2, conn);
                cmd2.Parameters.AddWithValue("@Email", supplierEmail);
                object result = cmd2.ExecuteScalar();
                conn.Close();
                // If result is not null, set supplierID
                if (result != null)
                {
                    supplierID = Convert.ToInt32(result);
                }

                string query = "UPDATE Products SET Price = @Price WHERE Name = @Name AND SupplierID = @SupplierID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@SupplierID", supplierID);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                lblMessage.Text = "Product updated successfully.";
                LoadMyProducts();
            }
        }

        protected void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            string name = txtProductName.Text;
            string supplierEmail = Session["User"].ToString();
            int supplierID = -1;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["inventoryConnection"].ConnectionString))
            {
                string query2 = "SELECT UserID FROM Users WHERE Email = @Email";
                conn.Open();
                SqlCommand cmd2 = new SqlCommand(query2, conn);
                cmd2.Parameters.AddWithValue("@Email", supplierEmail);
                object result = cmd2.ExecuteScalar();
                conn.Close();

                // If result is not null, set supplierID
                if (result != null)
                {
                    supplierID = Convert.ToInt32(result);
                }

                string query = "DELETE FROM Products WHERE Name = @Name AND SupplierID = @SupplierID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@SupplierID", supplierID);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                lblMessage.Text = "Product deleted successfully.";
                LoadMyProducts();
            }
        }

        private void LoadMyProducts()
        {
            string supplierEmail = Session["User"].ToString();
            int supplierID = -1;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["inventoryConnection"].ConnectionString))
            {
                conn.Open();
                string query2 = "SELECT UserID FROM Users WHERE Email = @Email";
                SqlCommand cmd2 = new SqlCommand(query2, conn);
                cmd2.Parameters.AddWithValue("@Email", supplierEmail);
                object result = cmd2.ExecuteScalar();

                // If result is not null, set supplierID
                if (result != null)
                {
                    supplierID = Convert.ToInt32(result);
                }
                
                string query = "SELECT * FROM Products WHERE SupplierID = @SupplierID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SupplierID", supplierID);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                gvMyProducts.DataSource = dt;
                gvMyProducts.DataBind();
            }
        }

        protected void btnSearchProduct_Click(object sender, EventArgs e)
        {
            string searchName = txtSearchName.Text;
            DateTime searchDate = string.IsNullOrEmpty(txtSearchDate.Text) ? DateTime.MinValue : Convert.ToDateTime(txtSearchDate.Text);
            string supplierEmail = Session["User"].ToString();
            string sortOrder = ddlSort.SelectedValue; 
            int supplierID = -1;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["inventoryConnection"].ConnectionString))
            {
                string query2 = "SELECT UserID FROM Users WHERE Email = @Email";
                conn.Open();
                SqlCommand cmd2 = new SqlCommand(query2, conn);
                cmd2.Parameters.AddWithValue("@Email", supplierEmail);
                object result = cmd2.ExecuteScalar();
                conn.Close();

                // If result is not null, set supplierID
                if (result != null)
                {
                    supplierID = Convert.ToInt32(result);
                }

                string query = "SELECT * FROM Products WHERE SupplierID = @SupplierID";
                if (!string.IsNullOrEmpty(searchName))
                {
                    query += " AND Name LIKE '%' + @Name + '%'";
                }
                if (searchDate != DateTime.MinValue)
                {
                    query += " AND DateAdded = @DateAdded";
                }
                query += " ORDER BY " + sortOrder;

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SupplierID", supplierID);
                if (!string.IsNullOrEmpty(searchName))
                {
                    cmd.Parameters.AddWithValue("@Name", searchName);
                }
                if (searchDate != DateTime.MinValue)
                {
                    cmd.Parameters.AddWithValue("@DateAdded", searchDate);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                gvSearchResults.DataSource = dt;
                gvSearchResults.DataBind();
            }
        }
    }
        
}