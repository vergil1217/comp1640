using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EWSD.Admin
{
    public partial class AssignFacultyRole : System.Web.UI.Page
    {
        private string role;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                role = Request.QueryString["role"];

                if (role.Equals("pvc"))
                {
                    labelRole.Text = "Pro-Vice Chancellor";
                }
                else if (role.Equals("dlt"))
                {
                    labelRole.Text = "Director of Learning & Quality";
                }

                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        if (role.Equals("pvc"))
                        {
                            cmd.CommandText = "SELECT * FROM faculty WHERE pro_vice_chancellor IS NULL";
                        }
                        else if (role.Equals("dlt"))
                        {
                            cmd.CommandText = "SELECT * FROM faculty WHERE director_learning_quality IS NULL";
                        }
                        cmd.Prepare();

                        conn.Open();
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ListItem item = new ListItem(reader.GetString(1), reader.GetString(0));
                                comboFaculties.Items.Add(item);
                            }
                            if(comboFaculties.Items.Count < 1)
                            {
                                comboFaculties.Items.Add("No available faculties");
                                bSelectFaculty.Enabled = false;
                            }
                        }

                        cmd.CommandText = "SELECT * FROM staff WHERE user_role = @role";
                        if (role.Equals("pvc"))
                        {
                            cmd.Parameters.Add("@role", SqlDbType.Int, 50).Value = 4;
                        }
                        else if (role.Equals("dlt"))
                        {
                            cmd.Parameters.Add("@role", SqlDbType.Int, 50).Value = 3;
                        }

                        cmd.Prepare();

                        using (SqlDataReader reader = cmd.ExecuteReader())
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
                            comboStaff.Items.Add("No available staff");
                            literalWarning.Text = "Warning: No available staff!";
                            bSelectStaff.Enabled = false;
                        }
                    }
                }
            }
        }

        protected void bSelectStaff_Click(object sender, EventArgs e)
        {
            role = Request.QueryString["role"];

            literalWarning.Text = "";
            literalActionSuccess.Text = "";

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string facultyCode = comboFaculties.SelectedItem.Value;
                    string staffId = comboStaff.SelectedItem.Value;

                    cmd.Connection = conn;
                    if (role.Equals("pvc"))
                    {
                        cmd.CommandText = "UPDATE faculty SET pro_vice_chancellor = @staffId WHERE faculty_code = @facultyCode";
                    }
                    else if (role.Equals("dlt"))
                    {
                        cmd.CommandText = "UPDATE faculty SET director_learning_quality = @staffId WHERE faculty_code = @facultyCode";
                    }
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@staffId", staffId);
                    cmd.Parameters.AddWithValue("@facultyCode", facultyCode);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    literalActionSuccess.Text = labelRole.Text + " successfully assigned. Page will refresh in 3 seconds.";
                    Response.AddHeader("REFRESH", "3;");
                }
            }
        }
    }
}