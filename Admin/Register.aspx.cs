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
                                s = new Staff(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetByte(6));
                            }
                        }

                        if (s != null)
                        {
                            literalErrorMessage.Text = "Username already exists. Please try another username";
                            fieldUsername.Text = "";
                            fieldFirstName.Text = "";
                            fieldLastName.Text = "";
                            fieldEmail.Text = "";
                            fieldUsername.Focus();
                        }
                        else
                        {
                            string passwordHash = "";

                            using (SHA1 pw = new SHA1CryptoServiceProvider())
                            {
                                passwordHash = BitConverter.ToString(pw.ComputeHash(Encoding.UTF8.GetBytes(fieldPassword.Text))).Replace("-", "").ToLower();
                            }
                            cmd.Parameters.Clear();
                            cmd.CommandText = "INSERT INTO staff (username, pw, f_name, l_name, email, user_role) VALUES (@username, @pw, @f_name, @l_name, @email, @user_role);";
                            cmd.Prepare();

                            cmd.Parameters.AddWithValue("@username", fieldUsername.Text);
                            cmd.Parameters.AddWithValue("@pw", passwordHash);
                            cmd.Parameters.AddWithValue("@f_name", fieldFirstName.Text);
                            cmd.Parameters.AddWithValue("@l_name", fieldLastName.Text);
                            cmd.Parameters.AddWithValue("@email", fieldEmail.Text);
                            cmd.Parameters.AddWithValue("@user_role", 1);

                            cmd.ExecuteNonQuery();

                            literalSuccessMessage.Text = "Staff successfully registered.";
                        }
                    }


                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                Response.Redirect("http://www.google.com");
            }
        }
    }
}