using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EWSD.Guest
{
    public partial class ExceptionReport : System.Web.UI.Page
    {
        private string type;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                type = Request.QueryString["type"];

                if (type.Equals("xclcm"))
                {
                    panelXCLCM.Visible = true;
                    literalXCLCM.Text = "Courses Without Pro-Vice Chancellor or Director of Learning and Quality";
                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "SELECT faculty_name FROM faculty WHERE pro_vice_chancellor IS NULL OR director_learning_quality IS NULL";
                            cmd.Prepare();

                            conn.Open();
                            using(SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    ListItem item = new ListItem(reader.GetString(0));
                                    listXCLCM.Items.Add(item);
                                }
                            }
                            conn.Close();
                        }
                    }
                }
                else if (type.Equals("xcmray"))
                {
                    panelXCMRAY.Visible = true;
                    literalXCMRAY.Text = "Courses without CMR for an Academic Year";
                }
                else if (type.Equals("xresp"))
                {
                    panelXRESP.Visible = true;
                    literalXRESP.Text = "CMRs Without Response";

                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "SELECT DISTINCT report_id FROM reports WHERE approved_by IS NULL";
                            cmd.Prepare();

                            ArrayList arrReportId = new ArrayList();

                            conn.Open();
                            using(SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    arrReportId.Add(reader.GetInt32(0));
                                }
                            }

                            foreach(int i in arrReportId)
                            {
                                string course = "";

                                cmd.Parameters.Clear();

                                cmd.CommandText = "SELECT course_title FROM course WHERE course_code IN (" +
                                    "SELECT parent_course FROM coursework WHERE coursework_code IN (" +
                                        "SELECT coursework_code FROM statistic WHERE stat_id IN (" +
                                            "SELECT stat_id FROM reports WHERE report_id = @reportId)))";
                                cmd.Prepare();

                                cmd.Parameters.AddWithValue("@reportId", i);

                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        course = reader.GetString(0);
                                    }
                                }

                                cmd.Parameters.Clear();

                                cmd.CommandText = "SELECT report_date FROM reports WHERE report_id = @reportId";
                                cmd.Prepare();

                                cmd.Parameters.AddWithValue("@reportId", i);

                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        TimeSpan ts = DateTime.Now - reader.GetDateTime(0);
                                        course += " - " + ts.Days + " days since submission.";
                                    }
                                }

                                ListItem item = new ListItem(course);
                                listXRESP.Items.Add(item);
                            }
                        }
                    }
                }
            }
        }

        protected void bSelectAcademicYear_Click(object sender, EventArgs e)
        {
            panelXCMRAYSelectAcademicYear.Visible = false;
            panelXCMRAYBody.Visible = true;

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT faculty_name FROM faculty WHERE faculty_code NOT IN( " +
                        "SELECT registered_faculty FROM course WHERE course_code IN(" +
                            "SELECT parent_course FROM coursework WHERE coursework_code IN(" +
                                "SELECT coursework_code FROM statistic WHERE stat_id IN(" +
                                    "SELECT stat_id FROM reports WHERE academic_year = @acadYear))))";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@acadYear", fieldXCMRAYAcademicYear.Text);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListItem item = new ListItem(reader.GetString(0));
                            listXCMRAY.Items.Add(item);
                        }
                    }
                    conn.Close();
                }
            }
        }
    }
}