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
    public partial class ManageStaffRoles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT * FROM staff WHERE user_role <> 5 OR user_role IS NULL";
                        cmd.Prepare();

                        conn.Open();
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ListItem item = new ListItem(reader.GetString(3) + " " + reader.GetString(4), reader.GetInt32(0).ToString());
                                comboStaff.Items.Add(item);
                            }
                        }

                        conn.Close();
                        if(comboStaff.Items.Count < 1)
                        {
                            comboStaff.Items.Add("No Staffs");
                        }
                    }
                }
            }
        }

        protected void bSelectStaff_Click(object sender, EventArgs e)
        {
            panelSelectStaff.Visible = false;
            panelAssignRoleToStaff.Visible = true;
            
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM staff WHERE staff_id = @staffId";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@staffId", int.Parse(comboStaff.SelectedItem.Value));

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            labelStaffName.Text = reader.GetString(3) + " " + reader.GetString(4);
                            if (!reader.IsDBNull(10))
                            {
                                fieldUserRole.Text = reader.GetByte(10).ToString();
                            }
                        }
                    }

                    cmd.Parameters.Clear();

                    cmd.CommandText = "SELECT * FROM roles";
                    cmd.Prepare();
                    
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListItem item = new ListItem(reader.GetString(1), reader.GetByte(0).ToString());
                            if (!fieldUserRole.Text.Equals(""))
                            {
                                if (reader.GetByte(0) == int.Parse(fieldUserRole.Text))
                                {
                                    item.Selected = true;
                                }
                            }
                            comboRole.Items.Add(item);
                        }
                    }
                }
            }
            
        }

        protected void bAssignRole_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE staff SET user_role = @role WHERE staff_id = @staffId";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@role", int.Parse(comboRole.SelectedItem.Value.ToString()));
                    cmd.Parameters.AddWithValue("@staffId", int.Parse(comboStaff.SelectedItem.Value));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    literalActionSuccess.Text = "Role assignment successful. Page will refresh in 3 seconds.";
                    Response.AddHeader("REFRESH", "3;");
                }
            }
        }
    }
}