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
    public partial class StatisticReport : System.Web.UI.Page
    {
        private string type;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                type = Request.QueryString["type"];

                if (type.Equals("sccmrfay"))
                {
                    panelSCCMRFAY.Visible = true;
                    literalSCCMRFAY.Text = "Faculties with Completed CMR for Each Academic year";

                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "SELECT faculty_code, faculty_name FROM faculty";
                            cmd.Prepare();

                            ArrayList arrFaculties = new ArrayList();

                            conn.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    ListItem item = new ListItem(reader.GetString(1), reader.GetString(0));
                                    arrFaculties.Add(item);
                                }
                            }

                            foreach(ListItem s in arrFaculties)
                            {
                                string faculties = "";

                                cmd.Parameters.Clear();
                                cmd.CommandText = "SELECT COUNT(DISTINCT report_id) FROM reports WHERE stat_id IN (" +
                                    "SELECT stat_id FROM statistic WHERE coursework_code IN (" +
                                        "SELECT coursework_code FROM coursework WHERE parent_course IN (" +
                                            "SELECT course_code FROM course WHERE registered_faculty = (" +
                                                "SELECT faculty_code FROM faculty WHERE faculty_code = @facultyCode)))) AND dlt IS NOT NULL";
                                cmd.Prepare();

                                cmd.Parameters.AddWithValue("@facultyCode", s.Value);

                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        faculties = s.Text + " - " + reader.GetInt32(0) + " completed CMRs";
                                        ListItem item = new ListItem(faculties);
                                        listSCCMRFAY.Items.Add(item);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (type.Equals("spscmrfay"))
                {
                    panelSPSCMRFAY.Visible = true;
                    literalSPSCMRFAY.Text = "Percentage of completed CMR for each Faculty for any Academic Year";

                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "SELECT faculty_code, faculty_name FROM faculty";
                            cmd.Prepare();

                            ArrayList arrFaculties = new ArrayList();

                            conn.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    ListItem item = new ListItem(reader.GetString(1), reader.GetString(0));
                                    arrFaculties.Add(item);
                                }
                            }

                            foreach (ListItem s in arrFaculties)
                            {
                                string faculties = "";

                                cmd.Parameters.Clear();
                                cmd.CommandText = "SELECT COUNT(DISTINCT report_id) FROM reports WHERE stat_id IN (" +
                                    "SELECT stat_id FROM statistic WHERE coursework_code IN (" +
                                        "SELECT coursework_code FROM coursework WHERE parent_course IN (" +
                                            "SELECT course_code FROM course WHERE registered_faculty = (" +
                                                "SELECT faculty_code FROM faculty WHERE faculty_code = @facultyCode)))) AND dlt IS NOT NULL";
                                cmd.Prepare();

                                cmd.Parameters.AddWithValue("@facultyCode", s.Value);

                                double completedCount = 0;
                                double totalCount = 0;

                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        completedCount = reader.GetInt32(0);
                                    }
                                }

                                cmd.Parameters.Clear();
                                cmd.CommandText = "SELECT COUNT(DISTINCT report_id) FROM reports WHERE stat_id IN (" +
                                    "SELECT stat_id FROM statistic WHERE coursework_code IN (" +
                                        "SELECT coursework_code FROM coursework WHERE parent_course IN (" +
                                            "SELECT course_code FROM course WHERE registered_faculty = (" +
                                                "SELECT faculty_code FROM faculty WHERE faculty_code = @facultyCode))))";
                                cmd.Prepare();

                                cmd.Parameters.AddWithValue("@facultyCode", s.Value);

                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        totalCount = reader.GetInt32(0);
                                    }
                                }

                                if(totalCount < 1)
                                {
                                    totalCount = 1;
                                }
                                double percentage = completedCount / totalCount * 100;

                                faculties = s.Text + " - " + percentage + "% completed CMRs";
                                ListItem item = new ListItem(faculties);
                                listSPSCMRFAY.Items.Add(item);
                            }
                        }
                    }
                }
                else if (type.Equals("spscmrr"))
                {
                    panelSPSCMRR.Visible = true;
                    literalSPSCMRR.Text = "Percentage of CMR with Responses for each Faculty";

                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "SELECT faculty_code, faculty_name FROM faculty";
                            cmd.Prepare();

                            ArrayList arrFaculties = new ArrayList();

                            conn.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    ListItem item = new ListItem(reader.GetString(1), reader.GetString(0));
                                    arrFaculties.Add(item);
                                }
                            }

                            foreach (ListItem s in arrFaculties)
                            {
                                string faculties = "";

                                cmd.Parameters.Clear();
                                cmd.CommandText = "SELECT COUNT(DISTINCT report_id) FROM reports WHERE stat_id IN (" +
                                    "SELECT stat_id FROM statistic WHERE coursework_code IN (" +
                                        "SELECT coursework_code FROM coursework WHERE parent_course IN (" +
                                            "SELECT course_code FROM course WHERE registered_faculty = (" +
                                                "SELECT faculty_code FROM faculty WHERE faculty_code = @facultyCode)))) AND approved_by IS NOT NULL";
                                cmd.Prepare();

                                cmd.Parameters.AddWithValue("@facultyCode", s.Value);

                                double respondedCount = 0;
                                double totalCount = 0;

                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        respondedCount = reader.GetInt32(0);
                                    }
                                }

                                cmd.Parameters.Clear();
                                cmd.CommandText = "SELECT COUNT(DISTINCT report_id) FROM reports WHERE stat_id IN (" +
                                    "SELECT stat_id FROM statistic WHERE coursework_code IN (" +
                                        "SELECT coursework_code FROM coursework WHERE parent_course IN (" +
                                            "SELECT course_code FROM course WHERE registered_faculty = (" +
                                                "SELECT faculty_code FROM faculty WHERE faculty_code = @facultyCode))))";
                                cmd.Prepare();

                                cmd.Parameters.AddWithValue("@facultyCode", s.Value);

                                using (SqlDataReader reader = cmd.ExecuteReader())
                                {
                                    if (reader.Read())
                                    {
                                        totalCount = reader.GetInt32(0);
                                    }
                                }

                                if (totalCount < 1)
                                {
                                    totalCount = 1;
                                }
                                double percentage = respondedCount / totalCount * 100;

                                faculties = s.Text + " - " + percentage + "% responded CMRs";
                                ListItem item = new ListItem(faculties);
                                listSPSCMRR.Items.Add(item);
                            }
                        }
                    }
                }
            }
        }
    }
}