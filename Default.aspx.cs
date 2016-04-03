using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EWSD
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
                if (HttpContext.Current.User.IsInRole("Administrator"))
                {
                    Response.Redirect("/Admin/AdminHome.aspx");
                }
                else if(HttpContext.Current.User.IsInRole("CM") || HttpContext.Current.User.IsInRole("CL"))
                {
                    Response.Redirect("/Course/Default.aspx");
                }
                else if(HttpContext.Current.User.IsInRole("PVC") || HttpContext.Current.User.IsInRole("DLT"))
                {
                    Response.Redirect("/Management/Default.aspx");
                }
            }
        }
    }
}