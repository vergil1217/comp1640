using EWSD.Domain;
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
    public partial class GuestViewCMR : System.Web.UI.Page
    {
        private string reportId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                reportId = Request.QueryString["reportId"];

                if(reportId != null)
                {
                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = conn;

                            ArrayList arrStats = new ArrayList();

                            cmd.CommandText = "SELECT * FROM statistic WHERE stat_id IN (SELECT stat_id FROM reports WHERE report_id = @reportId)";
                            cmd.Prepare();

                            cmd.Parameters.AddWithValue("@reportId", reportId);

                            conn.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    arrStats.Add(new Statistics(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), Convert.ToDouble(reader.GetDecimal(3)), Convert.ToDouble(reader.GetDecimal(4)), Convert.ToDouble(reader.GetDecimal(5)), Convert.ToDouble(reader.GetDecimal(6)), Convert.ToDouble(reader.GetDecimal(7)), Convert.ToDouble(reader.GetDecimal(8)), Convert.ToDouble(reader.GetDecimal(9)), Convert.ToDouble(reader.GetDecimal(10)), Convert.ToDouble(reader.GetDecimal(11)), Convert.ToDouble(reader.GetDecimal(12)), Convert.ToDouble(reader.GetDecimal(13)), Convert.ToDouble(reader.GetDecimal(14)), Convert.ToDouble(reader.GetDecimal(15))));
                                }
                            }

                            cmd.Parameters.Clear();

                            cmd.CommandText = "SELECT * FROM reports WHERE report_id = @reportId";
                            cmd.Prepare();

                            cmd.Parameters.AddWithValue("@reportId", reportId);

                            Report report = null;

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    report = new Report(reader.GetInt32(0), arrStats, reader.GetInt32(3), reader.GetString(4), reader.GetString(5), ((reader.IsDBNull(6)) ? DateTime.MinValue : reader.GetDateTime(6)), ((reader.IsDBNull(7)) ? null : new Staff(reader.GetInt32(7))), ((reader.IsDBNull(8)) ? null : reader.GetString(8)), ((reader.IsDBNull(9)) ? DateTime.MinValue : reader.GetDateTime(9)), ((reader.IsDBNull(10)) ? null : new Staff(reader.GetInt32(10))));
                                }
                            }

                            Session["report"] = report;

                            cmd.Parameters.Clear();

                            literalAcademicYear.Text = ((Statistics)report.statistics[0]).academicYear.ToString();

                            cmd.CommandText = "SELECT course_title FROM course WHERE course_code IN (" +
                                "SELECT parent_course FROM coursework WHERE coursework_code IN (" +
                                    "SELECT coursework_code FROM statistic WHERE stat_id IN (" +
                                        "SELECT stat_id FROM reports WHERE report_id = @reportId)))";
                            cmd.Prepare();

                            cmd.Parameters.AddWithValue("@reportId", reportId);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    literalCourseTitle.Text = reader.GetString(0);
                                }
                            }

                            cmd.Parameters.Clear();

                            literalStudentCount.Text = report.studentCount.ToString();

                            cmd.CommandText = "SELECT coursework_code, coursework_title FROM coursework WHERE coursework_code IN (" +
                                "SELECT coursework_code FROM statistic WHERE stat_id IN (" +
                                    "SELECT stat_id FROM reports WHERE report_id = @reportId))";
                            cmd.Prepare();

                            cmd.Parameters.AddWithValue("@reportId", reportId);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    literalSubjectList.Text += reader.GetString(0).ToUpper() + " - " + reader.GetString(1) + "<br/>";
                                }
                            }

                            for (int i = 0; i < report.statistics.Count; i++)
                            {
                                Statistics s = ((Statistics)arrStats[i]);
                                switch (i)
                                {
                                    case 0:
                                        fieldCw1.Text = s.courseworkCode.ToUpper();
                                        comboGddCw1.Text = s.courseworkCode.ToUpper();

                                        fieldCw1Mean.Text = s.mean.ToString();
                                        fieldCw1Median.Text = s.median.ToString();
                                        fieldCw1StdDev.Text = s.standardDeviation.ToString();
                                        fieldGddCw1Group1.Text = s.gdGroup1.ToString();
                                        fieldGddCw1Group2.Text = s.gdGroup2.ToString();
                                        fieldGddCw1Group3.Text = s.gdGroup3.ToString();
                                        fieldGddCw1Group4.Text = s.gdGroup4.ToString();
                                        fieldGddCw1Group5.Text = s.gdGroup5.ToString();
                                        fieldGddCw1Group6.Text = s.gdGroup6.ToString();
                                        fieldGddCw1Group7.Text = s.gdGroup7.ToString();
                                        fieldGddCw1Group8.Text = s.gdGroup8.ToString();
                                        fieldGddCw1Group9.Text = s.gdGroup9.ToString();
                                        fieldGddCw1Group10.Text = s.gdGroup10.ToString();
                                        break;
                                    case 1:
                                        fieldCw2.Text = s.courseworkCode.ToUpper();
                                        comboGddCw2.Text = s.courseworkCode.ToUpper();

                                        fieldCw2Mean.Text = s.mean.ToString();
                                        fieldCw2Median.Text = s.median.ToString();
                                        fieldCw2StdDev.Text = s.standardDeviation.ToString();
                                        fieldGddCw2Group1.Text = s.gdGroup1.ToString();
                                        fieldGddCw2Group2.Text = s.gdGroup2.ToString();
                                        fieldGddCw2Group3.Text = s.gdGroup3.ToString();
                                        fieldGddCw2Group4.Text = s.gdGroup4.ToString();
                                        fieldGddCw2Group5.Text = s.gdGroup5.ToString();
                                        fieldGddCw2Group6.Text = s.gdGroup6.ToString();
                                        fieldGddCw2Group7.Text = s.gdGroup7.ToString();
                                        fieldGddCw2Group8.Text = s.gdGroup8.ToString();
                                        fieldGddCw2Group9.Text = s.gdGroup9.ToString();
                                        fieldGddCw2Group10.Text = s.gdGroup10.ToString();
                                        break;
                                    case 2:
                                        fieldCw3.Text = s.courseworkCode.ToUpper();
                                        comboGddCw3.Text = s.courseworkCode.ToUpper();

                                        fieldCw3Mean.Text = s.mean.ToString();
                                        fieldCw3Median.Text = s.median.ToString();
                                        fieldCw3StdDev.Text = s.standardDeviation.ToString();
                                        fieldGddCw3Group1.Text = s.gdGroup1.ToString();
                                        fieldGddCw3Group2.Text = s.gdGroup2.ToString();
                                        fieldGddCw3Group3.Text = s.gdGroup3.ToString();
                                        fieldGddCw3Group4.Text = s.gdGroup4.ToString();
                                        fieldGddCw3Group5.Text = s.gdGroup5.ToString();
                                        fieldGddCw3Group6.Text = s.gdGroup6.ToString();
                                        fieldGddCw3Group7.Text = s.gdGroup7.ToString();
                                        fieldGddCw3Group8.Text = s.gdGroup8.ToString();
                                        fieldGddCw3Group9.Text = s.gdGroup9.ToString();
                                        fieldGddCw3Group10.Text = s.gdGroup10.ToString();
                                        break;
                                }
                            }

                            overallMean.Text = (double.Parse(fieldCw1Mean.Text) + double.Parse(fieldCw2Mean.Text) + double.Parse(fieldCw3Mean.Text)).ToString();
                            overallMedian.Text = (double.Parse(fieldCw1Median.Text) + double.Parse(fieldCw2Median.Text) + double.Parse(fieldCw3Median.Text)).ToString();
                            overallStdDev.Text = (double.Parse(fieldCw1StdDev.Text) + double.Parse(fieldCw2StdDev.Text) + double.Parse(fieldCw3StdDev.Text)).ToString();

                            overallGroup1.Text = (double.Parse(fieldGddCw1Group1.Text) + double.Parse(fieldGddCw2Group1.Text) + double.Parse(fieldGddCw3Group1.Text)).ToString();
                            overallGroup2.Text = (double.Parse(fieldGddCw1Group2.Text) + double.Parse(fieldGddCw2Group2.Text) + double.Parse(fieldGddCw3Group2.Text)).ToString();
                            overallGroup3.Text = (double.Parse(fieldGddCw1Group3.Text) + double.Parse(fieldGddCw2Group3.Text) + double.Parse(fieldGddCw3Group3.Text)).ToString();
                            overallGroup4.Text = (double.Parse(fieldGddCw1Group4.Text) + double.Parse(fieldGddCw2Group4.Text) + double.Parse(fieldGddCw3Group4.Text)).ToString();
                            overallGroup5.Text = (double.Parse(fieldGddCw1Group5.Text) + double.Parse(fieldGddCw2Group5.Text) + double.Parse(fieldGddCw3Group5.Text)).ToString();
                            overallGroup6.Text = (double.Parse(fieldGddCw1Group6.Text) + double.Parse(fieldGddCw2Group6.Text) + double.Parse(fieldGddCw3Group6.Text)).ToString();
                            overallGroup7.Text = (double.Parse(fieldGddCw1Group7.Text) + double.Parse(fieldGddCw2Group7.Text) + double.Parse(fieldGddCw3Group7.Text)).ToString();
                            overallGroup8.Text = (double.Parse(fieldGddCw1Group8.Text) + double.Parse(fieldGddCw2Group8.Text) + double.Parse(fieldGddCw3Group8.Text)).ToString();
                            overallGroup9.Text = (double.Parse(fieldGddCw1Group9.Text) + double.Parse(fieldGddCw2Group9.Text) + double.Parse(fieldGddCw3Group9.Text)).ToString();
                            overallGroup10.Text = (double.Parse(fieldGddCw1Group10.Text) + double.Parse(fieldGddCw2Group10.Text) + double.Parse(fieldGddCw3Group10.Text)).ToString();

                            fieldGeneralComments.Text = report.comments;
                            fieldActionTaken.Text = report.actionTaken;

                            cmd.Parameters.Clear();
                            cmd.CommandText = "SELECT DISTINCT dlt_comment FROM reports WHERE report_id = @reportId";
                            cmd.Prepare();

                            cmd.Parameters.AddWithValue("@reportId", report.reportId);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    if (!reader.IsDBNull(0))
                                    {
                                        fieldDLTComments.Text = reader.GetString(0);
                                        panelDLTComments.Visible = true;
                                    }
                                }
                            }
                            cmd.Parameters.Clear();

                            panelCMRBody.Visible = true;
                        }
                    }
                }
            }
        }
    }
}