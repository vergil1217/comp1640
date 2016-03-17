using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EWSD.Account
{
    public partial class Manage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            literalActionSuccessful.Text = "";
        }

        protected void bChangePassword_Click(object sender, EventArgs e)
        {
            literalPasswordChangeFailure.Text = "";
            if (Page.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT pw FROM staff WHERE username = @username";
                        cmd.Prepare();

                        conn.Open();

                        string pwFromDb = "";
                        string pwHash = "";
                        string newPwHash = "";

                        using (SHA1 pw = new SHA1CryptoServiceProvider())
                        {
                            pwHash = BitConverter.ToString(pw.ComputeHash(Encoding.UTF8.GetBytes(fieldCurrentPassword.Text))).Replace("-", "").ToLower();
                            newPwHash = BitConverter.ToString(pw.ComputeHash(Encoding.UTF8.GetBytes(fieldNewPassword.Text))).Replace("-", "").ToLower();
                        }

                        cmd.Parameters.AddWithValue("@username", HttpContext.Current.User.Identity.Name);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            pwFromDb = reader.GetString(0);
                        }

                        if (!pwHash.Equals(pwFromDb))
                        {
                            conn.Close();
                            literalPasswordChangeFailure.Text = "Password change failure. Reason: Current password mismatch.";
                            return;
                        }

                        cmd.Parameters.Clear();
                        cmd.CommandText = "UPDATE staff SET pw = @newPassword WHERE username = @username";
                        cmd.Prepare();

                        cmd.Parameters.AddWithValue("@newPassword", newPwHash);
                        cmd.Parameters.AddWithValue("@username", HttpContext.Current.User.Identity.Name);

                        cmd.ExecuteNonQuery();
                        literalActionSuccessful.Text = "Password change successful.";
                        conn.Close();
                    }
                }
            }
        }

        protected void bUpdateSecQA_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        string securityAnswerHash = "";

                        using (SHA1 pw = new SHA1CryptoServiceProvider())
                        {
                            securityAnswerHash = BitConverter.ToString(pw.ComputeHash(Encoding.UTF8.GetBytes(fieldSecurityAnswer.Text))).Replace("-", "").ToLower();
                        }

                        cmd.Connection = conn;
                        cmd.CommandText = "UPDATE staff SET security_question = @secQ, security_answer = @secA WHERE username = @username";
                        cmd.Prepare();

                        cmd.Parameters.AddWithValue("@secQ", comboSecurityQuestion.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@secA", securityAnswerHash);
                        cmd.Parameters.AddWithValue("@username", HttpContext.Current.User.Identity.Name);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        literalActionSuccessful.Text = "Security Question and Answer update successful.";
                        conn.Close();

                        comboSecurityQuestion.SelectedIndex = 0;
                        fieldSecurityAnswer.Text = "";
                    }
                }
            }
        }

        protected void bUpdateEmail_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "UPDATE staff SET email = @newEmail WHERE username = @username";
                        cmd.Prepare();

                        cmd.Parameters.AddWithValue("@newEmail", fieldNewEmail.Text);
                        cmd.Parameters.AddWithValue("@username", HttpContext.Current.User.Identity.Name);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        literalActionSuccessful.Text = "Email update successful.";
                        conn.Close();
                        fieldNewEmail.Text = "";
                    }
                }
            }
        }
    }
}