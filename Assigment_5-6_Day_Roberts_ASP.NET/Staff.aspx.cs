using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assigment_5_6_Day_Roberts_ASP.NET
{
    public partial class Staff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["staff_user"] == null)
            {
                Response.Redirect("~/StaffLogin.aspx");
                return;
            } 
            else
            {
                var user = Session["staff_user"] as System.Xml.Linq.XElement;
                if (user != null)
                {
                    string username = (string)user.Element("username");
                    lblLoggedIn.Text = "Logged in as: <strong>" + username + "</strong>";
                }
            }
        }
    }
}