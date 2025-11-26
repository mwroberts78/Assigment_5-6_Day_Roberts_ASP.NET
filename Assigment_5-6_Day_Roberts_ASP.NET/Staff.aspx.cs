using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

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

            if (!IsPostBack)
            {
                BindMembers();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["staff_user"] = null;
            Response.Redirect(Request.RawUrl); // Refreshes the page
        }

        protected void gvMembers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "UpdatePoints")
            {
                // Find the row index
                int index = Convert.ToInt32(((GridViewRow)((Button)e.CommandSource).NamingContainer).RowIndex);
                GridViewRow row = gvMembers.Rows[index];
                string email = e.CommandArgument.ToString();
                TextBox txtNewPoints = (TextBox)row.FindControl("txtNewPoints");
                string newPoints = txtNewPoints.Text.Trim();

                string result = UpdatePoints(email, newPoints);
                if (string.IsNullOrEmpty(result))
                {
                    // Success, rebind grid
                    BindMembers();
                }
                else
                {
                    // Optionally show error
                    lblLoggedIn.Text += "<br /><span style='color:red'>" + result + "</span>";
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

        private void BindMembers()
        {
            string xmlPath = Server.MapPath("~/App_Data/members.xml");
            var members = new List<MemberInfo>();

            if (System.IO.File.Exists(xmlPath))
            {
                XDocument doc = XDocument.Load(xmlPath);
                members = doc.Descendants("member")
                    .Select(x => new MemberInfo
                    {
                        Id = (string)x.Attribute("id"),
                        Username = (string)x.Element("username"),
                        Email = (string)x.Element("email"),
                        Points = (string)x.Element("points"),
                        CreatedUtc = FormatDate((string)x.Element("createdUtc"))
                    })
                    .ToList();
            }

            gvMembers.DataSource = members;
            gvMembers.DataBind();
        }

        private string FormatDate(string utcString)
        {
            if (DateTime.TryParse(utcString, out DateTime dt))
                return dt.ToLocalTime().ToString("MMM dd, yyyy HH:mm");
            return utcString;
        }

        public class MemberInfo
        {
            public string Id { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Points { get; set; }
            public string CreatedUtc { get; set; }
        }
    }
}