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
                            comboCourses.Items.Add("Select a course");
                            while (reader.Read())
                            {
                                ListItem item = new ListItem(reader.GetString(1), reader.GetString(0));
                                comboCourses.Items.Add(item);
                            }
                        }

                        if(comboCourses.Items.Count == 1)
                        {
                            panelCMRHead.Visible = false;
                            panelCMRBody.Visible = false;
                            literalWarning.Text = "You do not belong to any courses. Check with your higher ups.";
                        }
                        else
                        {
                            panelCMRBody.Visible = false;
                        }
                    }
                }
            }
        }

        protected void comboCourses_SelectedIndexChanged(object sender, EventArgs e)
        {
            string courseCode = ((DropDownList)sender).SelectedValue.ToString();
            if (((DropDownList)sender).SelectedIndex == 0)
            {
                panelCMRBody.Visible = false;
                literalCourseCode.Text = "";
                return;
            }

            literalSubjectList.Text = "";
            comboCw1.Items.Clear();
            comboCw2.Items.Clear();
            comboCw3.Items.Clear();
            comboGddCw1.Items.Clear();
            comboGddCw2.Items.Clear();
            comboGddCw3.Items.Clear();

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

            fieldCw3Mean.Text = "";
            fieldCw3Median.Text = "";
            fieldCw3StdDev.Text = "";
            fieldGddCw3Group1.Text = "";
            fieldGddCw3Group2.Text = "";
            fieldGddCw3Group3.Text = "";
            fieldGddCw3Group4.Text = "";
            fieldGddCw3Group5.Text = "";
            fieldGddCw3Group6.Text = "";
            fieldGddCw3Group7.Text = "";
            fieldGddCw3Group8.Text = "";
            fieldGddCw3Group9.Text = "";
            fieldGddCw3Group10.Text = "";

            if (!panelCMRBody.Visible)
            {
                panelCMRBody.Visible = true;
            }

            literalCourseCode.Text = courseCode;

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT coursework_code, coursework_title FROM coursework WHERE parent_course = @course";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@course", courseCode);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            literalSubjectList.Text += reader.GetString(0).ToUpper() + " - " + reader.GetString(1) + "<br/>";
                            ListItem item = new ListItem(reader.GetString(0).ToUpper(), reader.GetString(0));
                            comboCw1.Items.Add(item);
                            comboCw2.Items.Add(item);
                            comboCw3.Items.Add(item);
                            comboGddCw1.Items.Add(item);
                            comboGddCw2.Items.Add(item);
                            comboGddCw3.Items.Add(item);
                        }
                    }
                }
            }
        }

        protected void bSubmitReport_Click(object sender, EventArgs e)
        {
            literalWarning.Text = "";

            if(fieldAcademicSession.Text == "")
            {
                literalWarning.Text = "Academic session is required.";
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
                Statistics stat1 = new Statistics(0, fieldAcademicSession.Text, comboCw1.SelectedValue, double.Parse(fieldCw1Mean.Text), double.Parse(fieldCw1Median.Text), double.Parse(fieldCw1StdDev.Text), double.Parse(fieldGddCw1Group1.Text), double.Parse(fieldGddCw1Group2.Text), double.Parse(fieldGddCw1Group3.Text), double.Parse(fieldGddCw1Group4.Text), double.Parse(fieldGddCw1Group5.Text), double.Parse(fieldGddCw1Group6.Text), double.Parse(fieldGddCw1Group7.Text), double.Parse(fieldGddCw1Group8.Text), double.Parse(fieldGddCw1Group9.Text), double.Parse(fieldGddCw1Group10.Text));
                Statistics stat2 = new Statistics(0, fieldAcademicSession.Text, comboCw2.SelectedValue, double.Parse(fieldCw2Mean.Text), double.Parse(fieldCw2Median.Text), double.Parse(fieldCw2StdDev.Text), double.Parse(fieldGddCw2Group1.Text), double.Parse(fieldGddCw2Group2.Text), double.Parse(fieldGddCw2Group3.Text), double.Parse(fieldGddCw2Group4.Text), double.Parse(fieldGddCw2Group5.Text), double.Parse(fieldGddCw2Group6.Text), double.Parse(fieldGddCw2Group7.Text), double.Parse(fieldGddCw2Group8.Text), double.Parse(fieldGddCw2Group9.Text), double.Parse(fieldGddCw2Group10.Text));
                Statistics stat3 = new Statistics(0, fieldAcademicSession.Text, comboCw3.SelectedValue, double.Parse(fieldCw3Mean.Text), double.Parse(fieldCw3Median.Text), double.Parse(fieldCw3StdDev.Text), double.Parse(fieldGddCw3Group1.Text), double.Parse(fieldGddCw3Group2.Text), double.Parse(fieldGddCw3Group3.Text), double.Parse(fieldGddCw3Group4.Text), double.Parse(fieldGddCw3Group5.Text), double.Parse(fieldGddCw3Group6.Text), double.Parse(fieldGddCw3Group7.Text), double.Parse(fieldGddCw3Group8.Text), double.Parse(fieldGddCw3Group9.Text), double.Parse(fieldGddCw3Group10.Text));

                ArrayList arrStats = new ArrayList();
                arrStats.Add(stat1);
                arrStats.Add(stat2);
                arrStats.Add(stat3);

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO statistic (academic_session, coursework_code, mean, median, standard_deviation, grade_dist_group_1, grade_dist_group_2, grade_dist_group_3, grade_dist_group_4, grade_dist_group_5, grade_dist_group_6, grade_dist_group_7, grade_dist_group_8, grade_dist_group_9, grade_dist_group_10) VALUES " +
                        "(@academicSession, @cwCode, @mean, @median, @stdDev, @group1, @group2, @group3, @group4, @group5, @group6, @group7, @group8, @group9, @group10)";
                    cmd.Prepare();

                    conn.Open();

                    foreach(Statistics s in arrStats)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@academicSession", s.academicSession);
                        cmd.Parameters.AddWithValue("@cwCode", s.courseworkCode);
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

                    cmd.CommandText = "SELECT stat_id FROM statistic WHERE stat_id IN (SELECT TOP 3 stat_id FROM statistic ORDER BY stat_id DESC) ORDER BY stat_id";
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

                    int reportNo = 1;

                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        reader.Read();
                        reportNo = reader.GetInt32(0);
                    }

                    reportNo++;

                    Report report = new Report(reportNo, arrStats, int.Parse(fieldStudentCount.Text), fieldGeneralComments.Text, fieldActionTaken.Text, DateTime.Now, null, null, DateTime.MinValue, null);

                    cmd.CommandText = "INSERT INTO reports VALUES (@reportId, @statId, @acadSession, @studCount, @comments, @actionTaken, @reportDate, null, null, null, null)";

                    foreach (Statistics s in report.statistics)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@reportId", report.reportId);
                        cmd.Parameters.AddWithValue("@statId", s.statisticId);
                        cmd.Parameters.AddWithValue("@acadSession", s.academicSession);
                        cmd.Parameters.AddWithValue("@studCount", report.studentCount);
                        cmd.Parameters.AddWithValue("@comments", report.comments);
                        cmd.Parameters.AddWithValue("@actionTaken", report.actionTaken);
                        cmd.Parameters.AddWithValue("@reportDate", report.reportDate);

                        cmd.ExecuteNonQuery();
                    }

                    cmd.Parameters.Clear();

                    cmd.CommandText = "select email from staff where staff_id = (select course_moderator from course where course_code = @courseCode)";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@courseCode", comboCourses.SelectedValue);

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
                            mail.Subject = "Course Monitoring Report for Course " + comboCourses.SelectedValue.ToUpper();
                            mail.Body = "The Course Monitoring Report (CMR) for Academic Session " + fieldAcademicSession.Text + " is now ready for approval.<br/><br/>Visit the link below to review the CMR right away, or you may login manually via the website.<br/><br/>Link:<br/><a href='http://comp1640.ddns.net/Course/ViewCMR.aspx?reportId=" + report.reportId + "'>View Report</a><br/><br/><br/>Disclaimer: This is an auto-generated email, hence no signature is required. It will also not reply to any queries.";
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

        protected void comboCw1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = ((DropDownList)sender).SelectedIndex;
            comboCw1.SelectedIndex = selectedIndex;
            comboGddCw1.SelectedIndex = selectedIndex;
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
        }

        protected void comboCw2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = ((DropDownList)sender).SelectedIndex;
            comboCw2.SelectedIndex = selectedIndex;
            comboGddCw2.SelectedIndex = selectedIndex;
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
        }

        protected void comboCw3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = ((DropDownList)sender).SelectedIndex;
            comboCw3.SelectedIndex = selectedIndex;
            comboGddCw3.SelectedIndex = selectedIndex;
            fieldCw3Mean.Text = "";
            fieldCw3Median.Text = "";
            fieldCw3StdDev.Text = "";
            fieldGddCw3Group1.Text = "";
            fieldGddCw3Group2.Text = "";
            fieldGddCw3Group3.Text = "";
            fieldGddCw3Group4.Text = "";
            fieldGddCw3Group5.Text = "";
            fieldGddCw3Group6.Text = "";
            fieldGddCw3Group7.Text = "";
            fieldGddCw3Group8.Text = "";
            fieldGddCw3Group9.Text = "";
            fieldGddCw3Group10.Text = "";
        }
    }
}