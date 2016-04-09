using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EWSD.Course
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated == false)
            {
                Response.AddHeader("REFRESH", "5,URL=/Account/Login.aspx");
            }
            else
            {
                if (HttpContext.Current.User.IsInRole("CM"))
                {
                    labelRoleCP.Text = "Course Moderator Control Panel";
                    panelCMCP.Visible = true;
                }
                else if (HttpContext.Current.User.IsInRole("CL"))
                {
                    labelRoleCP.Text = "Course Leader Control Panel";
                    panelCLCP.Visible = true;
                }
            }
        }
    }
}