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
    public partial class ManageCourse : System.Web.UI.Page
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
                        cmd.CommandText = "SELECT * FROM faculty";
                        cmd.Prepare();

                        conn.Open();
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboParentFaculty.Items.Add(reader.GetString(0) + " - " + reader.GetString(1));
                            }
                        }

                        ArrayList arrUnavailableCourses = new ArrayList();

                        //load remove course drop down list
                        cmd.CommandText = "SELECT DISTINCT course_code FROM course INNER JOIN coursework ON course_code = coursework.parent_course";
                        cmd.Prepare();

                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                arrUnavailableCourses.Add(reader.GetString(0));
                            }
                        }

                        cmd.CommandText = "SELECT * FROM course";
                        cmd.Prepare();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            bool isUnavailable;

                            while (reader.Read())
                            {
                                isUnavailable = false;

                                foreach(string str in arrUnavailableCourses)
                                {
                                    if (reader.GetString(0).Equals(str))
                                    {
                                        isUnavailable = true;
                                        break;
                                    }
                                }

                                if (!isUnavailable)
                                {
                                    comboRemovableCourses.Items.Add(reader.GetString(0) + " - " + reader.GetString(1));
                                }
                            }
                        }

                        if(comboRemovableCourses.Items.Count < 1)
                        {
                            comboRemovableCourses.Items.Add("No Removable Courses");
                            bRemoveCourse.Enabled = false;
                        }

                        conn.Close();
                    }
                }
            }
        }

        protected void bAddCourse_Click(object sender, EventArgs e)
        {
            literalActionFailure.Text = "";
            literalActionSuccess.Text = "";

            if (Page.IsValid)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "INSERT INTO course VALUES (@courseCode, @courseTitle, @coursePrefix, @parentFaculty, NULL, NULL)";
                            cmd.Prepare();

                            cmd.Parameters.AddWithValue("@courseCode", fieldCourseCode.Text);
                            cmd.Parameters.AddWithValue("@courseTitle", fieldCourseTitle.Text);
                            cmd.Parameters.AddWithValue("@coursePrefix", fieldCoursePrefix.Text);
                            cmd.Parameters.AddWithValue("@parentFaculty", comboParentFaculty.SelectedItem.Text.Split(" - ".ToCharArray())[0]);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            fieldCourseCode.Text = "";
                            fieldCourseTitle.Text = "";
                            fieldCoursePrefix.Text = "";

                            literalActionSuccess.Text = "Course successfully added. Page will refresh in 3 seconds.";
                            Response.AddHeader("REFRESH", "3;");
                        }
                    }
                }
                catch(SqlException ex)
                {
                    literalActionFailure.Text = "Course addition failed. Reason: " + ex.Message;
                }
            }
        }

        protected void bRemoveCourse_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM course WHERE course_code = @courseCode";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@courseCode", comboRemovableCourses.SelectedItem.Text.Split(" - ".ToCharArray())[0]);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    literalActionSuccess.Text = "Course removal successful. Page will refresh in 3 seconds.";
                    Response.AddHeader("REFRESH", "3;");
                }
            }
        }
    }
}