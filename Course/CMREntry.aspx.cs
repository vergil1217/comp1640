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

            literalSubjectList.Text = "";
            comboCw1.Items.Clear();
            comboCw2.Items.Clear();
            comboCw3.Items.Clear();
            comboGddCw1.Items.Clear();
            comboGddCw2.Items.Clear();
            comboGddCw3.Items.Clear();

            if (!panelCMRBody.Visible)
            {
                panelCMRBody.Visible = true;
            }

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

        protected void bSendEmail_Click(object sender, EventArgs e)
        {
            /*
            *The URL to your Web Mail Interface is http://mail.yourdomain.com.
            *SMTP (port 25), POP3 (port 110) and IMAP (port 143) server are all 'mail.yourdomain.com'.
            *The alternative SMTP Port is 8889 or 587 (Some Internet Service Provider might block port 25.)
            *For SSL Ports, SMTP:465, POP3:995, IMAP:993
            */

            try
            {
                bool success = false;

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("comp1640.noreply@gmail.com");
                    mail.To.Add("zombiewalking891@hotmail.com");
                    mail.Subject = "Hello World";
                    mail.Body = "<h1>Hello</h1>";
                    mail.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("comp1640.noreply@gmail.com", "Ilikeapple");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                        success = true;
                    }
                }

                if (success)
                {
                    Response.Redirect("/Default.aspx");
                }
            }
            catch(Exception ex)
            {
                string ea = ex.Message.ToString();

            }
        }

        protected void bSubmitReport_Click(object sender, EventArgs e)
        {

        }

        protected void comboCw1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = ((DropDownList)sender).SelectedIndex;
            comboCw1.SelectedIndex = selectedIndex;
            comboGddCw1.SelectedIndex = selectedIndex;
        }

        protected void comboCw2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = ((DropDownList)sender).SelectedIndex;
            comboCw2.SelectedIndex = selectedIndex;
            comboGddCw2.SelectedIndex = selectedIndex;
        }

        protected void comboCw3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = ((DropDownList)sender).SelectedIndex;
            comboCw3.SelectedIndex = selectedIndex;
            comboGddCw3.SelectedIndex = selectedIndex;
        }
    }
}