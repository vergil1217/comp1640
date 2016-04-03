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
    public partial class RemoveFacultyRole : System.Web.UI.Page
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
                            cmd.CommandText = "SELECT faculty_code, faculty_name FROM faculty WHERE pro_vice_chancellor IS NOT NULL;";
                        }
                        else if (role.Equals("dlt"))
                        {
                            cmd.CommandText = "SELECT faculty_code, faculty_name FROM faculty WHERE director_learning_quality IS NOT NULL;";
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
                        }
                        conn.Close();

                        if (comboFaculties.Items.Count < 1)
                        {
                            comboFaculties.Items.Add("No faculties");
                            bSelectFaculty.Enabled = false;
                        }
                        
                    }
                }
            }
        }

        protected void bSelectFaculty_Click(object sender, EventArgs e)
        {
            role = Request.QueryString["role"];
            string facultyCode = comboFaculties.SelectedItem.Value;

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    conn.Open();

                    cmd.CommandText = "SELECT * FROM faculty WHERE faculty.faculty_code = @facultyCode";
                    cmd.Parameters.Add("@facultyCode", SqlDbType.VarChar, 50).Value = facultyCode;
                    cmd.Prepare();

                    int staffId = 0;
                    
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        if (role.Equals("pvc"))
                        {
                            staffId = reader.GetInt32(2);
                        }
                        else if (role.Equals("dlt"))
                        {
                            staffId = reader.GetInt32(3);
                        }
                    }
                    fieldStaffId.Text = staffId.ToString();

                    cmd.Parameters.Clear();

                    cmd.CommandText = "SELECT * FROM staff WHERE staff.staff_id = @staffId";
                    cmd.Parameters.Add("@staffId", SqlDbType.Int).Value = staffId;
                    cmd.Prepare();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        literalConfirmFaculty.Text = comboFaculties.SelectedItem.Text;
                        literalStaffName.Text = reader.GetString(3) + " " + reader.GetString(4);
                    }
                }
            }

            panelSelectFaculty.Visible = false;
            panelRemoveStaff.Visible = true;
        }

        protected void bConfirmRemoveStaff_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    role = Request.QueryString["role"];
                    string facultyCode = comboFaculties.SelectedItem.Value;

                    cmd.Connection = conn;
                    if (role.Equals("pvc"))
                    {
                        cmd.CommandText = "UPDATE faculty SET pro_vice_chancellor = NULL WHERE faculty_code = @facultyCode";
                    }
                    else if (role.Equals("dlt"))
                    {
                        cmd.CommandText = "UPDATE faculty SET director_learning_quality = NULL WHERE faculty_code = @facultyCode";
                    }
                    cmd.Parameters.Add("@facultyCode", SqlDbType.VarChar, 50).Value = facultyCode;
                    conn.Open();
                    cmd.Prepare();
                    
                    cmd.ExecuteNonQuery();
                    literalActionSuccess.Text = labelRole.Text + " successfully removed. You will be redirected to the Admin Homepage in 3 seconds.";
                    Response.AddHeader("REFRESH", "3;URL=AdminHome.aspx");
                }
            }
        }
    }
}