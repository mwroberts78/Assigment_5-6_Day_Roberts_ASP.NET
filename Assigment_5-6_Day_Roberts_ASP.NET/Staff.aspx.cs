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

        protected string UpdatePoints(string username, string points)
        {
            // method to update member points from staff page
            // takes in username and points to update
            // will only be available if staff user is logged in

            string result = "";

            if (points != null && username != null)
            {
                string xmlPath = Server.MapPath("~/App_Data/members.xml");
                System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Load(xmlPath);
                var userInDb = doc.Descendants("member")
                    .FirstOrDefault(x => ((string)x.Element("email")).ToLower() == username.ToLower());
                if (userInDb != null)
                {
                    userInDb.SetElementValue("points", points);
                    doc.Save(xmlPath);
                }
                else
                {
                    result = "Error updating points in database.";
                    return result;

                }

            }
            else 
            {
                result = "Username or points missing.";
                return result;
            }

            return result;

        }
}