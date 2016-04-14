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

namespace EWSD.Course
{
    public partial class CMREntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        Staff courseLeader = null;

                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT * FROM staff WHERE username = @uid";
                        cmd.Prepare();

                        cmd.Parameters.AddWithValue("@uid", HttpContext.Current.User.Identity.Name);

                        conn.Open();
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                courseLeader = new Staff(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetDateTime(8), reader.GetDateTime(9), reader.GetByte(10));
                            }
                        }

                        cmd.Parameters.Clear();

                        cmd.CommandText = "SELECT * FROM course WHERE course_leader = @cl";
                        cmd.Prepare();

                        cmd.Parameters.AddWithValue("@cl", courseLeader.staffId);

                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            comboCourse.Items.Add("Select a course");
                            while (reader.Read())
                            {
                                ListItem item = new ListItem(reader.GetString(1), reader.GetString(0));
                                comboCourse.Items.Add(item);
                            }
                        }

                        if(comboCourse.Items.Count == 1)
                        {
                            panelCMRHead.Visible = false;
                            panelCMRBody.Visible = false;
                            literalWarning.Text = "You do not belong to any courses or you are not a Course Leader. Check with your higher ups.";
                        }
                        else
                        {
                            panelCMRBody.Visible = false;
                        }
                    }
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
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListItem item = new ListItem(reader.GetString(1), reader.GetString(0));
                            comboCoursework.Items.Add(item);
                        }
                    }
                    

                    panelSelectCourse.Visible = false;
                    panelCMRHead.Visible = true;
                }
            }
        }

        protected void comboCoursework_SelectedIndexChanged(object sender, EventArgs e)
        {
            string courseworkCode = ((DropDownList)sender).SelectedValue.ToString();
            if (((DropDownList)sender).SelectedIndex == 0)
            {
                panelCMRBody.Visible = false;
                literalCourseworkCode.Text = "";
                return;
            }

            literalAssessmentList.Text = "";

            fieldCw1Mean.Text = "";
            fieldCw1Median.Text = "";
            fieldCw1StdDev.Text = "";
            fieldGddCw1Group1.Text = "";
            fieldGddCw1Group2.Text = "";
            fieldGddCw1Group3.Text = "";
            fieldGddCw1Group4.Text = "";
            fieldGddCw1Group5.Text = "";
            fieldGddCw1Group6.Text = "";
            fieldGddCw1Group7.Text = "";
            fieldGddCw1Group8.Text = "";
            fieldGddCw1Group9.Text = "";
            fieldGddCw1Group10.Text = "";

            fieldCw2Mean.Text = "";
            fieldCw2Median.Text = "";
            fieldCw2StdDev.Text = "";
            fieldGddCw2Group1.Text = "";
            fieldGddCw2Group2.Text = "";
            fieldGddCw2Group3.Text = "";
            fieldGddCw2Group4.Text = "";
            fieldGddCw2Group5.Text = "";
            fieldGddCw2Group6.Text = "";
            fieldGddCw2Group7.Text = "";
            fieldGddCw2Group8.Text = "";
            fieldGddCw2Group9.Text = "";
            fieldGddCw2Group10.Text = "";

            fieldExamMean.Text = "";
            fieldExamMedian.Text = "";
            fieldExamStdDev.Text = "";
            fieldGddExamGroup1.Text = "";
            fieldGddExamGroup2.Text = "";
            fieldGddExamGroup3.Text = "";
            fieldGddExamGroup4.Text = "";
            fieldGddExamGroup5.Text = "";
            fieldGddExamGroup6.Text = "";
            fieldGddExamGroup7.Text = "";
            fieldGddExamGroup8.Text = "";
            fieldGddExamGroup9.Text = "";
            fieldGddExamGroup10.Text = "";

            if (!panelCMRBody.Visible)
            {
                panelCMRBody.Visible = true;
            }

            literalCourseworkCode.Text = courseworkCode;

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT coursework_1, coursework_2, exam FROM coursework WHERE coursework_code = @cwCode";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@cwCode", courseworkCode);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (reader.GetBoolean(0))
                            {
                                literalAssessmentList.Text += "Coursework 1<br/>";
                                rowStatCw1.Visible = true;
                                rowGddCw1.Visible = true;
                            }

                            if (reader.GetBoolean(1))
                            {
                                literalAssessmentList.Text += "Coursework 2<br/>";
                                rowStatCw2.Visible = true;
                                rowGddCw2.Visible = true;
                            }

                            if (reader.GetBoolean(2)){
                                literalAssessmentList.Text += "Exam<br/>";
                                rowStatExam.Visible = true;
                                rowGddExam.Visible = true;
                            }
                        }
                    }
                }
            }
        }

        protected void bSubmitReport_Click(object sender, EventArgs e)
        {
            literalWarning.Text = "";

            if(fieldAcademicYear.Text == "")
            {
                literalWarning.Text = "Academic Year is required.";
                return;
            }

            if(fieldStudentCount.Text == "")
            {
                literalWarning.Text = "Student count is required.";
                return;
            }

            foreach(Control c in panelCMRBody.Controls)
            {
                if(c is TextBox && !(c.ID.Equals("fieldGeneralComments") || c.ID.Equals("fieldActionTaken")))
                {
                    try
                    {
                        double.Parse(((TextBox)c).Text);
                    }
                    catch (Exception)
                    {
                        literalWarning.Text = "Only numbers allowed for statistical data and grade distribution.";
                        return;
                    }
                }
            }

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    ArrayList arrStats = new ArrayList();

                    if (rowGddCw1.Visible && rowStatCw1.Visible)
                    {
                        Statistics stat1 = new Statistics(0, int.Parse(fieldAcademicYear.Text), comboCoursework.SelectedValue, 1, double.Parse(fieldCw1Mean.Text), double.Parse(fieldCw1Median.Text), double.Parse(fieldCw1StdDev.Text), double.Parse(fieldGddCw1Group1.Text), double.Parse(fieldGddCw1Group2.Text), double.Parse(fieldGddCw1Group3.Text), double.Parse(fieldGddCw1Group4.Text), double.Parse(fieldGddCw1Group5.Text), double.Parse(fieldGddCw1Group6.Text), double.Parse(fieldGddCw1Group7.Text), double.Parse(fieldGddCw1Group8.Text), double.Parse(fieldGddCw1Group9.Text), double.Parse(fieldGddCw1Group10.Text));
                        arrStats.Add(stat1);
                    }

                    if (rowGddCw2.Visible && rowStatCw2.Visible)
                    {
                        Statistics stat2 = new Statistics(0, int.Parse(fieldAcademicYear.Text), comboCoursework.SelectedValue, 2, double.Parse(fieldCw2Mean.Text), double.Parse(fieldCw2Median.Text), double.Parse(fieldCw2StdDev.Text), double.Parse(fieldGddCw2Group1.Text), double.Parse(fieldGddCw2Group2.Text), double.Parse(fieldGddCw2Group3.Text), double.Parse(fieldGddCw2Group4.Text), double.Parse(fieldGddCw2Group5.Text), double.Parse(fieldGddCw2Group6.Text), double.Parse(fieldGddCw2Group7.Text), double.Parse(fieldGddCw2Group8.Text), double.Parse(fieldGddCw2Group9.Text), double.Parse(fieldGddCw2Group10.Text));
                        arrStats.Add(stat2);
                    }

                    if (rowGddExam.Visible && rowStatExam.Visible)
                    {
                        Statistics stat3 = new Statistics(0, int.Parse(fieldAcademicYear.Text), comboCoursework.SelectedValue, 3, double.Parse(fieldExamMean.Text), double.Parse(fieldExamMedian.Text), double.Parse(fieldExamStdDev.Text), double.Parse(fieldGddExamGroup1.Text), double.Parse(fieldGddExamGroup2.Text), double.Parse(fieldGddExamGroup3.Text), double.Parse(fieldGddExamGroup4.Text), double.Parse(fieldGddExamGroup5.Text), double.Parse(fieldGddExamGroup6.Text), double.Parse(fieldGddExamGroup7.Text), double.Parse(fieldGddExamGroup8.Text), double.Parse(fieldGddExamGroup9.Text), double.Parse(fieldGddExamGroup10.Text));
                        arrStats.Add(stat3);
                    }

                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO statistic (academic_year, coursework_code, assessment_type, mean, median, standard_deviation, grade_dist_group_1, grade_dist_group_2, grade_dist_group_3, grade_dist_group_4, grade_dist_group_5, grade_dist_group_6, grade_dist_group_7, grade_dist_group_8, grade_dist_group_9, grade_dist_group_10) VALUES " +
                        "(@academicYear, @cwCode, @assessmentType, @mean, @median, @stdDev, @group1, @group2, @group3, @group4, @group5, @group6, @group7, @group8, @group9, @group10)";
                    cmd.Prepare();

                    conn.Open();

                    foreach(Statistics s in arrStats)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@academicYear", s.academicYear);
                        cmd.Parameters.AddWithValue("@cwCode", s.courseworkCode);
                        cmd.Parameters.AddWithValue("@assessmentType", s.assessmentType);
                        cmd.Parameters.AddWithValue("@mean", s.mean);
                        cmd.Parameters.AddWithValue("@median", s.median);
                        cmd.Parameters.AddWithValue("@stdDev", s.standardDeviation);
                        cmd.Parameters.AddWithValue("@group1", s.gdGroup1);
                        cmd.Parameters.AddWithValue("@group2", s.gdGroup2);
                        cmd.Parameters.AddWithValue("@group3", s.gdGroup3);
                        cmd.Parameters.AddWithValue("@group4", s.gdGroup4);
                        cmd.Parameters.AddWithValue("@group5", s.gdGroup5);
                        cmd.Parameters.AddWithValue("@group6", s.gdGroup6);
                        cmd.Parameters.AddWithValue("@group7", s.gdGroup7);
                        cmd.Parameters.AddWithValue("@group8", s.gdGroup8);
                        cmd.Parameters.AddWithValue("@group9", s.gdGroup9);
                        cmd.Parameters.AddWithValue("@group10", s.gdGroup10);

                        cmd.ExecuteNonQuery();
                    }

                    cmd.Parameters.Clear();

                    cmd.CommandText = "SELECT stat_id FROM statistic WHERE stat_id IN (SELECT TOP " + arrStats.Count.ToString() + " stat_id FROM statistic ORDER BY stat_id DESC) ORDER BY stat_id";
                    cmd.Prepare();
                    
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int count = 0;

                        while (reader.Read())
                        {
                            ((Statistics)arrStats[count]).statisticId = reader.GetInt32(0);
                            count++;
                        }
                    }

                    cmd.Parameters.Clear();

                    cmd.CommandText = "SELECT DISTINCT TOP 1 report_id FROM reports ORDER BY report_id DESC";
                    cmd.Prepare();

                    int reportNo = 0;

                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            reportNo = reader.GetInt32(0);
                        }
                    }

                    reportNo++;

                    Report report = new Report(reportNo, arrStats, int.Parse(fieldStudentCount.Text), fieldGeneralComments.Text, fieldActionTaken.Text, DateTime.Now, null, null, DateTime.MinValue, null);

                    cmd.CommandText = "INSERT INTO reports VALUES (@reportId, @statId, @acadYear, @studCount, @comments, @actionTaken, @reportDate, null, null, null, null)";

                    foreach (Statistics s in report.statistics)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@reportId", report.reportId);
                        cmd.Parameters.AddWithValue("@statId", s.statisticId);
                        cmd.Parameters.AddWithValue("@acadYear", s.academicYear);
                        cmd.Parameters.AddWithValue("@studCount", report.studentCount);
                        cmd.Parameters.AddWithValue("@comments", report.comments);
                        cmd.Parameters.AddWithValue("@actionTaken", report.actionTaken);
                        cmd.Parameters.AddWithValue("@reportDate", report.reportDate);

                        cmd.ExecuteNonQuery();
                    }

                    cmd.Parameters.Clear();

                    cmd.CommandText = "SELECT email FROM staff WHERE staff_id = (SELECT course_moderator FROM course WHERE course_code = @courseCode)";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@courseCode", comboCourse.SelectedValue);

                    string email = "";

                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            email = reader.GetString(0);
                        }
                    }

                    try
                    {
                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress("comp1640.noreply@gmail.com");
                            mail.To.Add(email);
                            mail.Subject = "Course Monitoring Report for Course " + comboCourse.SelectedValue.ToUpper();
                            mail.Body = "The Course Monitoring Report (CMR) for Academic Session " + fieldAcademicYear.Text + " is now ready for approval.<br/><br/>Visit the link below to review the CMR right away, or you may login manually via the website.<br/><br/>Link:<br/><a href='http://comp1640.ddns.net/Course/ViewCMR.aspx?reportId=" + report.reportId + "'>View Report</a><br/><br/><br/>Disclaimer: This is an auto-generated email, hence no signature is required. It will also not reply to any queries.";
                            mail.IsBodyHtml = true;

                            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                            {
                                smtp.Credentials = new NetworkCredential("comp1640.noreply@gmail.com", "Ilikeapple");
                                smtp.EnableSsl = true;
                                smtp.Send(mail);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        literalWarning.Text = ex.Message;
                    }

                    conn.Close();
                    literalActionSuccess.Text = "Report successfully submitted. Page will refresh in 3 seconds.";
                    Response.AddHeader("REFRESH", "3;");
                }
            }
        }
    }
}