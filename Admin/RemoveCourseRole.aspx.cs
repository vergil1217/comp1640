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
    public partial class RemoveCourseRole : System.Web.UI.Page
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
                        cmd.CommandText = "SELECT * FROM faculty";
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
                    cmd.CommandText = "SELECT * FROM course WHERE registered_faculty = @faculty AND " + getRemovingRole() + " IS NOT NULL";
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
                            comboCourse.Items.Add("No courses");
                            bSelectCourse.Enabled = false;
                        }
                    }
                    panelSelectFaculty.Visible = false;
                    panelSelectCourse.Visible = true;
                }
            }
        }

        protected void bSelectCourse_Click(object sender, EventArgs e)
        {
            literalActionSuccess.Text = "";
            literalWarning.Text = "";

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM course WHERE course_code = @course";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@course", comboCourse.SelectedItem.Value);

                    conn.Open();
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        role = Request.QueryString["role"];
                        if (role.Equals("cl"))
                        {
                           fieldStaffId.Text = reader.GetInt32(4).ToString();
                        }
                        else if (role.Equals("cm"))
                        {
                            fieldStaffId.Text = reader.GetInt32(5).ToString();
                        }

                    }

                    cmd.Parameters.Clear();

                    cmd.CommandText = "SELECT * FROM staff WHERE staff_id = @staffId";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@staffId", fieldStaffId.Text);

                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            literalCurrentStaff.Text = reader.GetString(3) + " " + reader.GetString(4);
                        }
                    }

                    panelSelectCourse.Visible = false;
                    panelSelectStaff.Visible = true;
                }
            }
        }

        protected void bRemoveStaff_Click(object sender, EventArgs e)
        {
            literalActionSuccess.Text = "";
            literalWarning.Text = "";

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE course SET " + getRemovingRole() + " = NULL WHERE course_code = @course";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@course", comboCourse.SelectedItem.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    literalCurrentStaff.Text = "";

                    literalActionSuccess.Text = labelRole.Text + " succesfully removed. Page will refresh in 3 seconds.";
                    Response.AddHeader("REFRESH", "3;");
                }
            }
        }

        private string getRemovingRole()
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
    }
}