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
                                    arrStats.Add(new Statistics(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), Convert.ToDouble(reader.GetDecimal(3)), Convert.ToDouble(reader.GetDecimal(4)), Convert.ToDouble(reader.GetDecimal(5)), Convert.ToDouble(reader.GetDecimal(6)), Convert.ToDouble(reader.GetDecimal(7)), Convert.ToDouble(reader.GetDecimal(8)), Convert.ToDouble(reader.GetDecimal(9)), Convert.ToDouble(reader.GetDecimal(10)), Convert.ToDouble(reader.GetDecimal(11)), Convert.ToDouble(reader.GetDecimal(12)), Convert.ToDouble(reader.GetDecimal(13)), Convert.ToDouble(reader.GetDecimal(14)), Convert.ToDouble(reader.GetDecimal(15))));
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

                            literalAcademicSession.Text = ((Statistics)report.statistics[0]).academicSession;

                            cmd.CommandText = "SELECT course_title FROM course WHERE course_code IN (" +
                                "SELECT parent_course FROM coursework WHERE coursework_code IN (" +
                                    "SELECT coursework_code FROM statistic WHERE stat_id IN (" +
                                        "SELECT stat_id FROM reports WHERE report_id = @reportId)))";
                            cmd.Prepare();

                            cmd.Parameters.AddWithValue("@reportId", comboReportId.SelectedValue);

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

                            cmd.Parameters.AddWithValue("@reportId", comboReportId.SelectedValue);

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
                    cmd.CommandText = "SELECT DISTINCT academic_session FROM reports WHERE stat_id IN (" +
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
                            ListItem item = new ListItem(reader.GetString(0), reader.GetString(0));
                            comboAcademicSession.Items.Add(item);
                        }

                        if(comboAcademicSession.Items.Count < 1)
                        {
                            comboAcademicSession.Items.Add("No academic session available");
                            bSelectAcademicSession.Enabled = false;
                        }

                        panelSelectFaculty.Visible = false;
                        panelSelectAcademicSession.Visible = true;
                    }
                }
            }
        }

        protected void bSelectAcademicSession_Click(object sender, EventArgs e)
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
                                    "AND academic_session = @acadSession and approved_by IS NOT NULL";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@facultyCode", comboFaculties.SelectedValue);
                    cmd.Parameters.AddWithValue("@acadSession", comboAcademicSession.SelectedValue);

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
                        else
                        {
                            panelSelectAcademicSession.Visible = false;
                            panelSelectReportId.Visible = true;
                        }
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
                            arrStats.Add(new Statistics(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), Convert.ToDouble(reader.GetDecimal(3)), Convert.ToDouble(reader.GetDecimal(4)), Convert.ToDouble(reader.GetDecimal(5)), Convert.ToDouble(reader.GetDecimal(6)), Convert.ToDouble(reader.GetDecimal(7)), Convert.ToDouble(reader.GetDecimal(8)), Convert.ToDouble(reader.GetDecimal(9)), Convert.ToDouble(reader.GetDecimal(10)), Convert.ToDouble(reader.GetDecimal(11)), Convert.ToDouble(reader.GetDecimal(12)), Convert.ToDouble(reader.GetDecimal(13)), Convert.ToDouble(reader.GetDecimal(14)), Convert.ToDouble(reader.GetDecimal(15))));
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

                    literalAcademicSession.Text = ((Statistics)report.statistics[0]).academicSession;

                    cmd.CommandText = "SELECT course_title FROM course WHERE course_code IN (" +
                        "SELECT parent_course FROM coursework WHERE coursework_code IN (" +
                            "SELECT coursework_code FROM statistic WHERE stat_id IN (" +
                                "SELECT stat_id FROM reports WHERE report_id = @reportId)))";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@reportId", comboReportId.SelectedValue);

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

                    cmd.Parameters.AddWithValue("@reportId", comboReportId.SelectedValue);

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
                                "SELECT stat_id FROM reports WHERE report_id = @reportid)))";
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
                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress("comp1640.noreply@gmail.com");
                            if (pvcEmail != "")
                            {
                                mail.To.Add(pvcEmail);
                            }
                            if (dltEmail != "")
                            {
                                mail.To.Add(dltEmail);
                            }
                            mail.Subject = "Course Monitoring Report for " + literalCourseTitle.Text;
                            mail.Body = "The Course Monitoring Report (CMR) for Academic Session " + literalAcademicSession.Text + " has been given feedback.<br/><br/>Visit the link below to view the CMR right away, or you may login manually via the website.<br/><br/>Link:<br/><a href='http://comp1640.ddns.net/Management/FeedbackCMR?reportId=" + report.reportId + "'>View Report</a><br/><br/><br/>Disclaimer: This is an auto-generated email, hence no signature is required. It will also not reply to any queries.";
                            mail.IsBodyHtml = true;

                            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                            {
                                smtp.Credentials = new NetworkCredential("comp1640.noreply@gmail.com", "Ilikeapple");
                                smtp.EnableSsl = true;
                                smtp.Send(mail);
                            }
                        }

                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress("comp1640.noreply@gmail.com");
                            if (clEmail != "")
                            {
                                mail.To.Add(clEmail);
                            }
                            if (cmEmail != "")
                            {
                                mail.To.Add(cmEmail);
                            }
                            mail.Subject = "Course Monitoring Report for " + literalCourseTitle.Text;
                            mail.Body = "The Course Monitoring Report (CMR) for Academic Session " + literalAcademicSession.Text + " has been given feedback.<br/><br/>Visit the link below to view the CMR right away, or you may login manually via the website.<br/><br/>Link:<br/><a href='http://comp1640.ddns.net/Course/ViewCMR?reportId=" + report.reportId + "'>View Report</a><br/><br/><br/>Disclaimer: This is an auto-generated email, hence no signature is required. It will also not reply to any queries.";
                            mail.IsBodyHtml = true;

                            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                            {
                                smtp.Credentials = new NetworkCredential("comp1640.noreply@gmail.com", "Ilikeapple");
                                smtp.EnableSsl = true;
                                smtp.Send(mail);
                            }
                        }

                        conn.Close();
                        literalAcademicSession.Text = "Feedback successfully submitted. Page will refresh in 3 seconds.";
                        Response.AddHeader("REFRESH", "3;");
                    }
                    catch (Exception ex)
                    {
                        literalWarning.Text = ex.Message;
                    }
                }
            }
        }
    }
}