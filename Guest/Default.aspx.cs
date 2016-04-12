using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EWSD.Guest
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT DISTINCT report_id FROM reports WHERE approved_by IS NOT NULL";
                        cmd.Prepare();

                        conn.Open();
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ListItem item = new ListItem(reader.GetInt32(0).ToString() , reader.GetInt32(0).ToString());
                                comboApprovedCMR.Items.Add(item);
                            }
                            if(comboApprovedCMR.Items.Count < 1)
                            {
                                comboApprovedCMR.Items.Add("No approved reports");
                                bViewReport.Enabled = false;
                            }
                        }
                    }
                }
            }
        }

        protected void bViewReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("GuestViewCMR.aspx?reportId=" + comboApprovedCMR.SelectedValue);
        }
    }
}