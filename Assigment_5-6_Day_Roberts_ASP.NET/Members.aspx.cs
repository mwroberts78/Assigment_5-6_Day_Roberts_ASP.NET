using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assigment_5_6_Day_Roberts_ASP.NET
{
    public partial class Members : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["member_user"] == null)
            {
                Response.Redirect("~/MemberLogin.aspx");
                return;
            }
            else
            {
                var user = Session["member_user"] as System.Xml.Linq.XElement;
                if (user != null)
                {
                    string username = (string)user.Element("email");
                    lblLoggedIn.Text = "Logged in as: <strong>" + username + "</strong>";
                }
            }
        }
    }
}