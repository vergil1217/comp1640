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
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void bAuthSecQA_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Session["usernameForgot"] = fieldUsername.Text;

                literalActionSuccessful.Text = "";
                literalActionFailure.Text = "";

                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;

                        cmd.CommandText = "SELECT security_question, security_answer FROM staff WHERE username = @username";
                        cmd.Prepare();

                        cmd.Parameters.AddWithValue("@username", fieldUsername.Text);

                        conn.Open();

                        string secQ = "";
                        string secA = "";
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                secQ = reader.GetString(0);
                                secA = reader.GetString(1);
                            }
                        }

                        if (secQ.Equals("") && secA.Equals(""))
                        {
                            literalActionFailure.Text = "Password reset fail. Reason: Username doesn't exist.";
                            fieldUsername.Text = "";
                            fieldSecurityAnswer.Text = "";
                            comboSecurityQuestion.SelectedIndex = 0;
                            conn.Close();
                            return;
                        }

                        string secAnsHash = "";
                        using (SHA1 pw = new SHA1CryptoServiceProvider())
                        {
                            secAnsHash = BitConverter.ToString(pw.ComputeHash(Encoding.UTF8.GetBytes(fieldSecurityAnswer.Text))).Replace("-", "").ToLower();
                        }

                        if (secQ.Equals(comboSecurityQuestion.SelectedItem.Text) && secA.Equals(secAnsHash))
                        {
                            panelResetPassword.Visible = true;
                            panelSecQA.Visible = false;
                        }
                        else
                        {
                            literalActionFailure.Text = "Password reset fail. Reason: Security Question and Answer mismatch.";
                            conn.Close();
                            fieldUsername.Text = "";
                            fieldSecurityAnswer.Text = "";
                            comboSecurityQuestion.SelectedIndex = 0;
                        }
                    }
                }
            }
        }

        protected void bResetPassword_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                literalActionSuccessful.Text = "";
                literalActionFailure.Text = "";

                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "UPDATE staff SET pw = @newPassword WHERE username = @username";
                        cmd.Prepare();

                        string newPwHash = "";
                        using (SHA1 pw = new SHA1CryptoServiceProvider())
                        {
                            newPwHash = BitConverter.ToString(pw.ComputeHash(Encoding.UTF8.GetBytes(fieldNewPassword.Text))).Replace("-", "").ToLower();
                        }

                        cmd.Parameters.AddWithValue("@newPassword", newPwHash);
                        cmd.Parameters.AddWithValue("@username", Session["usernameForgot"].ToString());

                        Session.Remove("usernameForgot");

                        fieldNewPassword.Enabled = false;
                        fieldConfirmNewPassword.Enabled = false;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        literalActionSuccessful.Text = "Password reset successful. You will be redirected to the login page in 3 seconds.";
                        Response.AddHeader("REFRESH", "3;URL=Login.aspx");
                        conn.Close();
                    }
                }
            }
        }
    }
}