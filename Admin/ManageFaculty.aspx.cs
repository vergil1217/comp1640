using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EWSD.Admin
{
    public partial class ManageFaculty : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                ArrayList arrUnavailableFaculties = new ArrayList();

                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT faculty_code FROM faculty INNER JOIN course ON faculty_code = course.registered_faculty";
                        cmd.Prepare();

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                arrUnavailableFaculties.Add(reader.GetString(0));
                            }
                        }

                        cmd.CommandText = "SELECT * FROM faculty";
                        cmd.Prepare();

                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            bool isUnavailable;

                            while (reader.Read())
                            {
                                isUnavailable = false;

                                foreach(string str in arrUnavailableFaculties)
                                {
                                    if (reader.GetString(0).Equals(str))
                                    {
                                        isUnavailable = true;
                                        break;
                                    }
                                }

                                if (!isUnavailable)
                                {
                                    comboRemovableFaculties.Items.Add(reader.GetString(0) + " - " + reader.GetString(1));
                                }
                            }
                        }

                        if(comboRemovableFaculties.Items.Count < 1)
                        {
                            comboRemovableFaculties.Items.Add("No Removable Faculties");
                            bRemoveFaculty.Enabled = false;
                        }
                    }
                }
            }
            
        }

        protected void bAddFaculty_Click(object sender, EventArgs e)
        {
            literalActionFailure.Text = "";
            literalActionSuccess.Text = "";

            if (Page.IsValid)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = conn;
                            cmd.CommandText = "INSERT INTO faculty VALUES (@facultyCode, @facultyName, NULL, NULL)";
                            cmd.Prepare();

                            cmd.Parameters.AddWithValue("@facultyCode", fieldFacultyCode.Text);
                            cmd.Parameters.AddWithValue("@facultyName", fieldFacultyName.Text);

                            conn.Open();

                            cmd.ExecuteNonQuery();

                            conn.Close();

                            literalActionSuccess.Text = "Faculty successfully added. Page will refresh in 3 seconds.";
                            fieldFacultyCode.Text = "";
                            fieldFacultyName.Text = "";
                            Response.AddHeader("REFRESH", "3;");
                        }
                    }
                }
                catch(SqlException ex)
                {
                    literalActionFailure.Text = "Faculty addition failed. Reason: " + ex.Message;
                    fieldFacultyCode.Text = "";
                    fieldFacultyName.Text = "";
                }
            }
        }

        protected void bRemoveFaculty_Click(object sender, EventArgs e)
        {
            literalActionFailure.Text = "";
            literalActionSuccess.Text = "";

            using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM faculty WHERE faculty_code = @facultyCode";
                    cmd.Prepare();

                    cmd.Parameters.AddWithValue("@facultyCode", comboRemovableFaculties.SelectedItem.Text.Split(" - ".ToCharArray())[0]);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    literalActionSuccess.Text = "Faculty successfully removed. Page will refresh in 3 seconds.";
                    Response.AddHeader("REFRESH", "3;");
                }
            }
        }
    }
}