using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EWSD.Admin
{
    public partial class ManageAssessment : System.Web.UI.Page
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
                        cmd.CommandText = "SELECT faculty_code, faculty_name FROM faculty";
                        cmd.Prepare();

                        conn.Open();
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ListItem item = new ListItem(reader.GetString(1), reader.GetString(0));
                                comboFaculty.Items.Add(item);
                            }
                        }
                    }
                }
            }
        }

        protected void bSelectFaculty_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT course_code, course_title FROM course WHERE registered_faculty = @faculty";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@faculty", comboFaculty.SelectedValue);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListItem item = new ListItem(reader.GetString(1), reader.GetString(0));
                            comboCourse.Items.Add(item);
                        }
                    }
                    panelSelectFaculty.Visible = false;
                    panelSelectCourse.Visible = true;
                }
            }
        }

        protected void bSelectCourse_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT coursework_code, coursework_title FROM coursework WHERE parent_course = @course";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@course", comboCourse.SelectedValue);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListItem item = new ListItem(reader.GetString(1), reader.GetString(0));
                            comboCoursework.Items.Add(item);
                        }

                        if(comboCoursework.Items.Count < 1)
                        {
                            comboCoursework.Items.Add("No Coursework found.");
                            bSelectCoursework.Enabled = false;
                        }
                    }

                    bool cw1 = false;
                    bool cw2 = false;
                    bool exam = false;

                    cmd.Parameters.Clear();

                    cmd.CommandText = "SELECT coursework_1, coursework_2, exam FROM coursework WHERE coursework_code = @coursework";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@coursework", comboCoursework.SelectedValue);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.GetBoolean(0))
                            {
                                cw1 = true;
                            }

                            if (reader.GetBoolean(1))
                            {
                                cw2 = true;
                            }

                            if (reader.GetBoolean(2))
                            {
                                exam = true;
                            }
                        }
                    }

                    if (cw1)
                    {
                        checkCW1.Checked = true;
                    }

                    if (cw2)
                    {
                        checkCW2.Checked = true;
                    }

                    if (exam)
                    {
                        checkExam.Checked = true;
                    }

                    panelSelectCourse.Visible = false;
                    panelSelectCoursework.Visible = true;
                }
            }
        }

        protected void bSelectCoursework_Click(object sender, EventArgs e)
        {
            panelSelectCoursework.Visible = false;
            panelManageAssessment.Visible = true;
        }

        protected void bUpdateAssessment_Click(object sender, EventArgs e)
        {
            bool cw1 = false;
            bool cw2 = false;
            bool exam = false;

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    if (checkCW1.Checked || checkCW2.Checked || checkExam.Checked)
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "UPDATE coursework SET coursework_1 = @cw1, coursework_2 = @cw2, exam = @exam WHERE coursework_code = @cwCode";
                        cmd.Prepare();

                        cmd.Parameters.AddWithValue("@cwCode", comboCoursework.SelectedValue);
                        cmd.Parameters.AddWithValue("@cw1", checkCW1.Checked);
                        cmd.Parameters.AddWithValue("@cw2", checkCW2.Checked);
                        cmd.Parameters.AddWithValue("@exam", checkExam.Checked);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        literalActionSuccess.Text = "Assessments updated. Page will refresh in 3 seconds.";
                        Response.AddHeader("REFRESH", "3;");
                    }
                    else
                    {
                        literalActionFailure.Text = "Subjects must have assessments.";
                    }
                }
            }
        }
    }
}