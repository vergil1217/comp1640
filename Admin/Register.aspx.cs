using EWSD.Domain;
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

namespace EWSD.Admin
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            literalSuccessMessage.Text = "";
            literalErrorMessage.Text = "";

            if (Page.IsValid)
            {   
                try
                {
                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                    {
                        conn.Open();

                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "SELECT * FROM staff WHERE username = @username";
                            cmd.Prepare();

                            cmd.Parameters.AddWithValue("@username", fieldUsername.Text);

                            Staff s = null;

                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    s = new Staff(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetDateTime(8), reader.GetDateTime(9), reader.GetByte(10));
                                }
                            }

                            if (s != null)
                            {
                                literalErrorMessage.Text = "Username already exists. Please try another username";
                                fieldUsername.Text = "";
                                fieldFirstName.Text = "";
                                fieldLastName.Text = "";
                                fieldEmail.Text = "";
                                fieldSecurityAnswer.Text = "";
                                fieldUsername.Focus();
                            }
                            else
                            {
                                if (fieldFirstName.Text.Any(char.IsDigit) || fieldLastName.Text.Any(char.IsDigit))
                                {
                                    literalErrorMessage.Text = "Staff registration failure. Reason: Names cannot contain numbers.";
                                    return;
                                }
                                string passwordHash = "";
                                string secAnswerHash = "";

                                using (SHA1 pw = new SHA1CryptoServiceProvider())
                                {
                                    passwordHash = BitConverter.ToString(pw.ComputeHash(Encoding.UTF8.GetBytes(fieldPassword.Text))).Replace("-", "").ToLower();
                                    secAnswerHash = BitConverter.ToString(pw.ComputeHash(Encoding.UTF8.GetBytes(fieldSecurityAnswer.Text))).Replace("-", "").ToLower();
                                }
                                cmd.Parameters.Clear();
                                cmd.CommandText = "INSERT INTO staff (username, pw, f_name, l_name, email, security_question, security_answer, created_date, last_login_date) VALUES (@username, @pw, @f_name, @l_name, @email, @secQuestion, @secAnswer, @creationDate, @lastLoginDate);";
                                cmd.Prepare();

                                cmd.Parameters.AddWithValue("@username", fieldUsername.Text);
                                cmd.Parameters.AddWithValue("@pw", passwordHash);
                                cmd.Parameters.AddWithValue("@f_name", fieldFirstName.Text);
                                cmd.Parameters.AddWithValue("@l_name", fieldLastName.Text);
                                cmd.Parameters.AddWithValue("@email", fieldEmail.Text);
                                cmd.Parameters.AddWithValue("@secQuestion", comboSecurityQuestion.SelectedItem.Text);
                                cmd.Parameters.AddWithValue("@secAnswer", secAnswerHash);
                                cmd.Parameters.AddWithValue("@creationDate", DateTime.Now);
                                cmd.Parameters.AddWithValue("@lastLoginDate", DateTime.Now);

                                cmd.ExecuteNonQuery();

                                literalSuccessMessage.Text = "Staff successfully registered.";

                                fieldUsername.Text = "";
                                fieldFirstName.Text = "";
                                fieldLastName.Text = "";
                                fieldEmail.Text = "";
                                fieldSecurityAnswer.Text = "";
                                fieldUsername.Focus();
                            }
                        }


                        conn.Close();
                    }
                }
                catch (SqlException ex)
                {
                    literalErrorMessage.Text = "Staff registration failed. Reason: " + ex.Message;
                }
            }
        }
    }
}