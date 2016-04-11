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
    public partial class AssignCourseRole : System.Web.UI.Page
    {
        private string role;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                role = Request.QueryString["role"];

                if (role.Equals("cl"))
                {
                    labelRole.Text = "Course Leader";
                }
                else if (role.Equals("cm"))
                {
                    labelRole.Text = "Course Moderator";
                }

                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT * FROM staff WHERE user_role = (SELECT role_id FROM roles WHERE role_name = @roleName)";
                        cmd.Prepare();

                        cmd.Parameters.AddWithValue("@roleName", role);

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            int counter = 0;

                            while (reader.Read())
                            {
                                counter++;
                            }

                            if(counter < 1)
                            {
                                literalWarning.Text = "Warning: No staff available!";
                                bSelectFaculty.Enabled = false;
                            }
                        }

                        cmd.Parameters.Clear();

                        cmd.CommandText = "SELECT * FROM faculty";
                        cmd.Prepare();
                        
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ListItem item = new ListItem(reader.GetString(1), reader.GetString(0));
                                comboFaculties.Items.Add(item);
                            }
                        }
                    }
                }
            }
        }

        protected void bSelectFaculty_Click(object sender, EventArgs e)
        {
            literalActionSuccess.Text = "";
            literalWarning.Text = "";

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM course WHERE registered_faculty = @faculty AND " + getAssigningRole() + " IS NULL";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@faculty", comboFaculties.SelectedItem.Value);

                    conn.Open();
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListItem item = new ListItem(reader.GetString(1), reader.GetString(0));
                            comboCourse.Items.Add(item);
                        }

                        if(comboCourse.Items.Count < 1)
                        {
                            comboCourse.Items.Add("No course.");
                            bSelectCourse.Enabled = false;
                        }
                    }

                    panelSelectCourse.Visible = true;
                    panelSelectFaculty.Visible = false;
                }
            }
        }

        protected void bSelectCourse_Click(object sender, EventArgs e)
        {
            role = Request.QueryString["role"];

            literalActionSuccess.Text = "";
            literalWarning.Text = "";

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM staff WHERE user_role = (SELECT role_id FROM roles WHERE role_name = @roleName)";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@roleName", role);

                    conn.Open();
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListItem item = new ListItem(reader.GetString(3) + " " + reader.GetString(4), reader.GetInt32(0).ToString());
                            comboStaff.Items.Add(item);
                        }
                    }

                    cmd.Parameters.Clear();

                    panelSelectStaff.Visible = true;
                    panelSelectCourse.Visible = false;
                }
            }
        }

        protected void bSelectStaff_Click(object sender, EventArgs e)
        {
            literalActionSuccess.Text = "";
            literalWarning.Text = "";

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE course SET " + getAssigningRole() + " = @staffId WHERE course_code = @courseCode";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@staffId", comboStaff.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@courseCode", comboCourse.SelectedItem.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    literalActionSuccess.Text = labelRole.Text + " successfully assigned. Page will refresh in 3 seconds.";
                    Response.AddHeader("REFRESH", "3;");
                }
            }
        }

        private string getAssigningRole()
        {
            role = Request.QueryString["role"];
            string retString = "";

            if (role.Equals("cl"))
            {
                retString = "course_leader";
            }
            else if (role.Equals("cm"))
            {
                retString = "course_moderator";
            }
            return retString;
        }

        private string getStaffRole()
        {
            role = Request.QueryString["role"];
            string retString = "";

            if (role.Equals("cl"))
            {
                retString = "1";
            }
            else if (role.Equals("cm"))
            {
                retString = "2";
            }
            return retString;
        }
    }
}