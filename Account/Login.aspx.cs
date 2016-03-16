using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EWSD.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LogIn(object sender, EventArgs e)
        {
            literalLoginFail.Text = "";

            if (IsValid)
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    conn.Open();

                    int userId = 0;
                    string roles = string.Empty;

                    using (SqlCommand cmd = new SqlCommand("Validate_User"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = conn;

                        cmd.Parameters.AddWithValue("@Username", fieldUsername.Text);
                        using (SHA1 pw = new SHA1CryptoServiceProvider())
                        {
                            cmd.Parameters.AddWithValue("@Password", BitConverter.ToString(pw.ComputeHash(Encoding.UTF8.GetBytes(fieldPassword.Text))).Replace("-", "").ToLower());
                        }

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            userId = reader.GetInt32(0);
                            roles = reader["Roles"].ToString();
                        }

                        conn.Close();

                        switch (userId)
                        {
                            case -1:
                                literalLoginFail.Text = "Username and/or password is incorrect.";
                                break;
                            case -2:
                                literalLoginFail.Text = "Account has not been activated.";
                                break;
                            default:
                                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, fieldUsername.Text, DateTime.Now, DateTime.Now.AddMinutes(50), checkRememberMe.Checked, roles, FormsAuthentication.FormsCookiePath);
                                string hashCookies = FormsAuthentication.Encrypt(ticket);
                                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hashCookies);
                                Response.Cookies.Add(cookie);
                                string returnUrl = Request.QueryString["ReturnUrl"];
                                if (returnUrl == null) returnUrl = "~/Default.aspx";
                                Response.Redirect(returnUrl);
                                break;
                        }

                    }
                }
            }
        }
    }
}