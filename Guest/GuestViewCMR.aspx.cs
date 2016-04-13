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
                                    arrStats.Add(new Statistics(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetInt32(3), Convert.ToDouble(reader.GetDecimal(4)), Convert.ToDouble(reader.GetDecimal(5)), Convert.ToDouble(reader.GetDecimal(6)), Convert.ToDouble(reader.GetDecimal(7)), Convert.ToDouble(reader.GetDecimal(8)), Convert.ToDouble(reader.GetDecimal(9)), Convert.ToDouble(reader.GetDecimal(10)), Convert.ToDouble(reader.GetDecimal(11)), Convert.ToDouble(reader.GetDecimal(12)), Convert.ToDouble(reader.GetDecimal(13)), Convert.ToDouble(reader.GetDecimal(14)), Convert.ToDouble(reader.GetDecimal(15)), Convert.ToDouble(reader.GetDecimal(16))));
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

                            cmd.CommandText = "SELECT coursework_title FROM coursework WHERE coursework_code IN (" +
                                "SELECT coursework_code FROM statistic WHERE stat_id IN (" +
                                    "SELECT stat_id FROM reports WHERE report_id = @reportId))";
                            cmd.Prepare();

                            cmd.Parameters.AddWithValue("@reportId", reportId);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    literalCourseworkTitle.Text = reader.GetString(0);
                                }
                            }

                            cmd.Parameters.Clear();

                            literalStudentCount.Text = report.studentCount.ToString();

                            cmd.CommandText = "SELECT coursework_1, coursework_2, exam FROM coursework WHERE coursework_code IN (" +
                                "SELECT coursework_code FROM statistic WHERE stat_id IN (" +
                                    "SELECT stat_id FROM reports WHERE report_id = @reportId))";
                            cmd.Prepare();

                            cmd.Parameters.AddWithValue("@reportId", reportId);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    if (reader.GetBoolean(0))
                                    {
                                        literalAssessmentList.Text += "Coursework 1<br/>";
                                    }

                                    if (reader.GetBoolean(1))
                                    {
                                        literalAssessmentList.Text += "Coursework 2<br/>";
                                    }

                                    if (reader.GetBoolean(2))
                                    {
                                        literalAssessmentList.Text += "Exam<br/>";
                                    }
                                }
                            }

                            double totalOverallMean, totalOverallMedian, totalOverallStdDev, totalOverallGroup1, totalOverallGroup2, totalOverallGroup3, totalOverallGroup4, totalOverallGroup5, totalOverallGroup6, totalOverallGroup7, totalOverallGroup8, totalOverallGroup9, totalOverallGroup10;
                            totalOverallMean = totalOverallMedian = totalOverallStdDev = totalOverallGroup1 = totalOverallGroup2 = totalOverallGroup3 = totalOverallGroup4 = totalOverallGroup5 = totalOverallGroup6 = totalOverallGroup7 = totalOverallGroup8 = totalOverallGroup9 = totalOverallGroup10 = 0;

                            foreach (Statistics s in arrStats)
                            {
                                totalOverallMean += s.mean;
                                totalOverallMedian += s.median;
                                totalOverallStdDev += s.standardDeviation;
                                totalOverallGroup1 += s.gdGroup1;
                                totalOverallGroup2 += s.gdGroup2;
                                totalOverallGroup3 += s.gdGroup3;
                                totalOverallGroup4 += s.gdGroup4;
                                totalOverallGroup5 += s.gdGroup5;
                                totalOverallGroup6 += s.gdGroup6;
                                totalOverallGroup7 += s.gdGroup7;
                                totalOverallGroup8 += s.gdGroup8;
                                totalOverallGroup9 += s.gdGroup9;
                                totalOverallGroup10 += s.gdGroup10;

                                switch (s.assessmentType)
                                {
                                    case 1:
                                        rowGddCw1.Visible = true;
                                        rowStatCw1.Visible = true;

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
                                    case 2:
                                        rowGddCw2.Visible = true;
                                        rowStatCw2.Visible = true;

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
                                    case 3:
                                        rowGddExam.Visible = true;
                                        rowStatExam.Visible = true;

                                        fieldExamMean.Text = s.mean.ToString();
                                        fieldExamMedian.Text = s.median.ToString();
                                        fieldExamStdDev.Text = s.standardDeviation.ToString();
                                        fieldGddExamGroup1.Text = s.gdGroup1.ToString();
                                        fieldGddExamGroup2.Text = s.gdGroup2.ToString();
                                        fieldGddExamGroup3.Text = s.gdGroup3.ToString();
                                        fieldGddExamGroup4.Text = s.gdGroup4.ToString();
                                        fieldGddExamGroup5.Text = s.gdGroup5.ToString();
                                        fieldGddExamGroup6.Text = s.gdGroup6.ToString();
                                        fieldGddExamGroup7.Text = s.gdGroup7.ToString();
                                        fieldGddExamGroup8.Text = s.gdGroup8.ToString();
                                        fieldGddExamGroup9.Text = s.gdGroup9.ToString();
                                        fieldGddExamGroup10.Text = s.gdGroup10.ToString();
                                        break;
                                }
                            }

                            overallMean.Text = totalOverallMean.ToString();
                            overallMedian.Text = totalOverallMedian.ToString();
                            overallStdDev.Text = totalOverallStdDev.ToString();

                            overallGroup1.Text = totalOverallGroup1.ToString();
                            overallGroup2.Text = totalOverallGroup2.ToString();
                            overallGroup3.Text = totalOverallGroup3.ToString();
                            overallGroup4.Text = totalOverallGroup4.ToString();
                            overallGroup5.Text = totalOverallGroup5.ToString();
                            overallGroup6.Text = totalOverallGroup6.ToString();
                            overallGroup7.Text = totalOverallGroup7.ToString();
                            overallGroup8.Text = totalOverallGroup8.ToString();
                            overallGroup9.Text = totalOverallGroup9.ToString();
                            overallGroup10.Text = totalOverallGroup10.ToString();

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