using EWSD.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EWSD.Management
{
    public partial class FeedbackCMR : System.Web.UI.Page
    {
        private string reportId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                reportId = Request.QueryString["reportId"];

                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        if (reportId == null)
                        {
                            Staff courseHead = null;

                            cmd.Connection = conn;
                            cmd.CommandText = "SELECT * FROM staff WHERE username = @uid";
                            cmd.Prepare();

                            cmd.Parameters.AddWithValue("@uid", HttpContext.Current.User.Identity.Name);

                            conn.Open();
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                reader.Read();
                                courseHead = new Staff(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetDateTime(8), reader.GetDateTime(9), reader.GetByte(10));
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
                        else
                        {
                            panelSelectFaculty.Visible = false;
                            ArrayList arrStats = new ArrayList();

                            cmd.Connection = conn;
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

                            cmd.Parameters.AddWithValue("@reportId", report.reportId);

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

                            cmd.Parameters.AddWithValue("@reportId", report.reportId);

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
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
                                        fieldDLTComments.Enabled = false;
                                        bSubmitDLTComment.Enabled = false;
                                        bSubmitDLTComment.Text = "Feedback Given";
                                    }
                                }
                            }
                            cmd.Parameters.Clear();

                            literalWarning.Text = "Deadline for feedback: " + report.reportDate.AddDays(14).ToShortDateString();

                            if (DateTime.Now.CompareTo(report.reportDate.AddDays(14)) > 0)
                            {
                                fieldDLTComments.Enabled = false;
                                bSubmitDLTComment.Enabled = false;
                                bSubmitDLTComment.Text = "Deadline Passed";
                            }

                            panelSelectReportId.Visible = false;
                            panelCMRBody.Visible = true;
                            panelDLTComments.Visible = true;
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
                    cmd.CommandText = "SELECT DISTINCT academic_year FROM reports WHERE stat_id IN (" +
                        "SELECT stat_id FROM statistic WHERE coursework_code IN (" +
                            "SELECT coursework_code FROM coursework WHERE parent_course IN (" +
                                "SELECT course_code FROM course WHERE registered_faculty = (" +
                                    "SELECT faculty_code FROM faculty WHERE faculty_code = @facultyCode))));";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@facultyCode", comboFaculties.SelectedValue);

                    conn.Open();
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListItem item = new ListItem(reader.GetInt32(0).ToString(), reader.GetInt32(0).ToString());
                            comboAcademicYear.Items.Add(item);
                        }

                        if(comboAcademicYear.Items.Count < 1)
                        {
                            comboAcademicYear.Items.Add("No academic session available");
                            bSelectAcademicYear.Enabled = false;
                        }

                        panelSelectFaculty.Visible = false;
                        panelSelectAcademicYear.Visible = true;
                    }
                }
            }
        }

        protected void bSelectAcademicYear_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT DISTINCT report_id FROM reports WHERE stat_id IN (" +
                        "SELECT stat_id FROM statistic WHERE coursework_code IN (" +
                            "SELECT coursework_code FROM coursework WHERE parent_course IN (" +
                                "SELECT course_code FROM course WHERE registered_faculty = @facultyCode))) " +
                                    "AND academic_year = @acadYear and approved_by IS NOT NULL";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@facultyCode", comboFaculties.SelectedValue);
                    cmd.Parameters.AddWithValue("@acadYear", comboAcademicYear.SelectedValue);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListItem item = new ListItem(reader.GetInt32(0).ToString(), reader.GetInt32(0).ToString());
                            comboReportId.Items.Add(item);
                        }

                        if (comboReportId.Items.Count < 1)
                        {
                            comboReportId.Items.Add("No reports available");
                            bSelectReport.Enabled = false;
                        }

                        panelSelectAcademicYear.Visible = false;
                        panelSelectReportId.Visible = true;
                    }
                }
            }
        }

        protected void bSelectReport_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    ArrayList arrStats = new ArrayList();

                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM statistic WHERE stat_id IN (SELECT stat_id FROM reports WHERE report_id = @reportId)";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@reportId", comboReportId.SelectedValue);

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

                    cmd.Parameters.AddWithValue("@reportId", comboReportId.SelectedValue);

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

                    cmd.Parameters.AddWithValue("@reportId", comboReportId.SelectedValue);

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

                    cmd.Parameters.AddWithValue("@reportId", comboReportId.SelectedValue);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
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
                                fieldDLTComments.Enabled = false;
                                bSubmitDLTComment.Enabled = false;
                                bSubmitDLTComment.Text = "Feedback Given";
                            }
                        }
                    }
                    cmd.Parameters.Clear();

                    literalWarning.Text = "Deadline for feedback: " + report.reportDate.AddDays(14).ToShortDateString();

                    if (DateTime.Now.CompareTo(report.reportDate.AddDays(14)) > 0)
                    {
                        fieldDLTComments.Enabled = false;
                        bSubmitDLTComment.Enabled = false;
                        bSubmitDLTComment.Text = "Deadline Passed";
                    }

                    panelSelectReportId.Visible = false;
                    panelCMRBody.Visible = true;
                    panelDLTComments.Visible = true;
                }
            }
        }

        protected void bSubmitDLTComment_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    Report report = (Report)Session["report"];
                    int staffId = 0;

                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM staff WHERE username = @uid";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@uid", HttpContext.Current.User.Identity.Name);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            staffId = reader.GetInt32(0);
                        }
                    }

                    cmd.Parameters.Clear();

                    cmd.CommandText = "UPDATE reports SET dlt_comment = @dltComment, dlt_comment_date = @dltCommentDate, dlt = @dlt WHERE report_id = @reportId";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@dltComment", fieldDLTComments.Text);
                    cmd.Parameters.AddWithValue("@dltCommentDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@dlt", staffId);
                    cmd.Parameters.AddWithValue("@reportId", report.reportId);

                    cmd.ExecuteNonQuery();

                    cmd.Parameters.Clear();

                    int pvcId = 0;
                    int dltId = 0;
                    int clId = 0;
                    int cmId = 0;

                    cmd.CommandText = "SELECT pro_vice_chancellor, director_learning_quality FROM faculty WHERE faculty_code = (" +
                        "SELECT registered_faculty FROM course WHERE course_code IN (" +
                            "SELECT DISTINCT parent_course FROM coursework WHERE coursework_code IN (" +
                                "SELECT coursework_code FROM statistic WHERE stat_id IN (" +
                                    "SELECT stat_id FROM reports WHERE report_id = @reportId))))";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@reportId", report.reportId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                pvcId = reader.GetInt32(0);
                            }

                            if (!reader.IsDBNull(1))
                            {
                                dltId = reader.GetInt32(1);
                            }
                        }
                    }

                    cmd.Parameters.Clear();

                    cmd.CommandText = "SELECT course_leader, course_moderator FROM course WHERE course_code = (" +
                        "SELECT DISTINCT parent_course FROM coursework WHERE coursework_code IN(" +
                            "SELECT coursework_code FROM statistic WHERE stat_id IN(" +
                                "SELECT stat_id FROM reports WHERE report_id = @reportId)))";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@reportId", report.reportId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                clId = reader.GetInt32(0);
                            }

                            if (!reader.IsDBNull(1))
                            {
                                cmId = reader.GetInt32(1);
                            }
                        }
                    }
                    cmd.Parameters.Clear();

                    string pvcEmail = "";
                    string dltEmail = "";
                    string clEmail = "";
                    string cmEmail = "";

                    if(pvcId != 0)
                    {
                        cmd.CommandText = "SELECT email FROM staff WHERE staff_id = @pvcId";
                        cmd.Prepare();

                        cmd.Parameters.AddWithValue("@pvcId", pvcId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                pvcEmail = reader.GetString(0);
                            }
                        }
                    }

                    cmd.Parameters.Clear();

                    if (dltId != 0)
                    {
                        cmd.CommandText = "SELECT email FROM staff WHERE staff_id = @dltId";
                        cmd.Prepare();

                        cmd.Parameters.AddWithValue("@dltId", dltId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                dltEmail = reader.GetString(0);
                            }
                        }
                    }

                    cmd.Parameters.Clear();

                    if (clId != 0)
                    {
                        cmd.CommandText = "SELECT email FROM staff WHERE staff_id = @clId";
                        cmd.Prepare();

                        cmd.Parameters.AddWithValue("@clId", clId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clEmail = reader.GetString(0);
                            }
                        }
                    }

                    cmd.Parameters.Clear();

                    if (cmId != 0)
                    {
                        cmd.CommandText = "SELECT email FROM staff WHERE staff_id = @cmId";
                        cmd.Prepare();

                        cmd.Parameters.AddWithValue("@cmId", cmId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                cmEmail = reader.GetString(0);
                            }
                        }
                    }

                    cmd.Parameters.Clear();

                    try
                    {
                        if (pvcEmail != "")
                        {
                            using (MailMessage mail = new MailMessage())
                            {
                                mail.From = new MailAddress("comp1640.noreply@gmail.com");
                                mail.To.Add(pvcEmail);
                                mail.Subject = "Course Monitoring Report for " + literalCourseworkTitle.Text;
                                mail.Body = "The Course Monitoring Report (CMR) for Academic Session " + literalAcademicYear.Text + " has been given feedback.<br/><br/>Visit the link below to view the CMR right away, or you may login manually via the website.<br/><br/>Link:<br/><a href='http://comp1640.ddns.net/Management/FeedbackCMR.aspx?reportId=" + report.reportId + "'>View Report</a><br/><br/><br/>Disclaimer: This is an auto-generated email, hence no signature is required. It will also not reply to any queries.";
                                mail.IsBodyHtml = true;

                                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                                {
                                    smtp.Credentials = new NetworkCredential("comp1640.noreply@gmail.com", "Ilikeapple");
                                    smtp.EnableSsl = true;
                                    smtp.Send(mail);
                                }
                            }
                        }

                        if(dltEmail != "")
                        {
                            using (MailMessage mail = new MailMessage())
                            {
                                mail.From = new MailAddress("comp1640.noreply@gmail.com");
                                mail.To.Add(dltEmail);
                                mail.Subject = "Course Monitoring Report for " + literalCourseworkTitle.Text;
                                mail.Body = "The Course Monitoring Report (CMR) for Academic Session " + literalAcademicYear.Text + " has been given feedback.<br/><br/>Visit the link below to view the CMR right away, or you may login manually via the website.<br/><br/>Link:<br/><a href='http://comp1640.ddns.net/Management/FeedbackCMR.aspx?reportId=" + report.reportId + "'>View Report</a><br/><br/><br/>Disclaimer: This is an auto-generated email, hence no signature is required. It will also not reply to any queries.";
                                mail.IsBodyHtml = true;

                                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                                {
                                    smtp.Credentials = new NetworkCredential("comp1640.noreply@gmail.com", "Ilikeapple");
                                    smtp.EnableSsl = true;
                                    smtp.Send(mail);
                                }
                            }
                        }

                        if(clEmail != "")
                        {
                            using (MailMessage mail = new MailMessage())
                            {
                                mail.From = new MailAddress("comp1640.noreply@gmail.com");
                                mail.To.Add(clEmail);
                                mail.Subject = "Course Monitoring Report for " + literalCourseworkTitle.Text;
                                mail.Body = "The Course Monitoring Report (CMR) for Academic Session " + literalAcademicYear.Text + " has been given feedback.<br/><br/>Visit the link below to view the CMR right away, or you may login manually via the website.<br/><br/>Link:<br/><a href='http://comp1640.ddns.net/Course/ViewCMR.aspx?reportId=" + report.reportId + "'>View Report</a><br/><br/><br/>Disclaimer: This is an auto-generated email, hence no signature is required. It will also not reply to any queries.";
                                mail.IsBodyHtml = true;

                                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                                {
                                    smtp.Credentials = new NetworkCredential("comp1640.noreply@gmail.com", "Ilikeapple");
                                    smtp.EnableSsl = true;
                                    smtp.Send(mail);
                                }
                            }
                        }

                        if(cmEmail != "")
                        {
                            using (MailMessage mail = new MailMessage())
                            {
                                mail.From = new MailAddress("comp1640.noreply@gmail.com");
                                mail.To.Add(cmEmail);
                                mail.Subject = "Course Monitoring Report for " + literalCourseworkTitle.Text;
                                mail.Body = "The Course Monitoring Report (CMR) for Academic Session " + literalAcademicYear.Text + " has been given feedback.<br/><br/>Visit the link below to view the CMR right away, or you may login manually via the website.<br/><br/>Link:<br/><a href='http://comp1640.ddns.net/Course/ViewCMR.aspx?reportId=" + report.reportId + "'>View Report</a><br/><br/><br/>Disclaimer: This is an auto-generated email, hence no signature is required. It will also not reply to any queries.";
                                mail.IsBodyHtml = true;

                                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                                {
                                    smtp.Credentials = new NetworkCredential("comp1640.noreply@gmail.com", "Ilikeapple");
                                    smtp.EnableSsl = true;
                                    smtp.Send(mail);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        literalWarning.Text = ex.Message;
                    }

                    conn.Close();
                    literalActionSuccess.Text = "Feedback successfully submitted. Page will refresh in 3 seconds.";
                    Response.AddHeader("REFRESH", "3;");
                }
            }
        }
    }
}