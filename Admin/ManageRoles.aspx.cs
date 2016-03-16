using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EWSD.Admin
{
    public partial class ManageRoles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            comboRoles.Items.Clear();

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT role_name FROM Roles";
                    cmd.Prepare();

                    conn.Open();
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboRoles.Items.Add(reader.GetString(0));
                        }
                    }
                    conn.Close();
                }
            }
        }

        protected void bDeleteRole_Click(object sender, EventArgs e)
        {
            literalAddFailure.Text = "";
            literalAddSuccess.Text = "";
            literalDeleteFailure.Text = "";
            literalDeleteSuccess.Text = "";

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "DELETE FROM roles WHERE role_name = @roleName";
                        cmd.Prepare();

                        cmd.Parameters.AddWithValue("@roleName", comboRoles.SelectedItem.Text);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        literalDeleteSuccess.Text = "Role deletion successful.";
                    }
                    catch (SqlException ex)
                    {
                        literalDeleteFailure.Text = "Role deletion failure. Reason: " + ex.Message;
                    }
                }
            }
        }

        protected void bAddRole_Click(object sender, EventArgs e)
        {
            literalAddFailure.Text = "";
            literalAddSuccess.Text = "";
            literalDeleteFailure.Text = "";
            literalDeleteSuccess.Text = "";

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT role_name FROM roles";
                    cmd.Prepare();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if(String.Equals(fieldNewRole.Text, reader.GetString(0), StringComparison.OrdinalIgnoreCase))
                            {
                                literalAddFailure.Text = "Role addition failed. Reason: Duplicate role name.";
                                return;
                            }
                        }
                    }

                    cmd.CommandText = "SELECT TOP 1 role_id FROM roles ORDER BY role_id DESC";
                    cmd.Prepare();

                    int newRoleId = 0;
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        newRoleId = reader.GetInt32(0) + 1;
                    }

                    cmd.CommandText = "INSERT INTO roles VALUES (@roleId, @roleName)";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@roleId", newRoleId);
                    cmd.Parameters.AddWithValue("@roleName", fieldNewRole.Text);

                    cmd.ExecuteNonQuery();
                    conn.Close();
                    literalAddSuccess.Text = "Role addition successful.";
                }
            }
        }
    }
}