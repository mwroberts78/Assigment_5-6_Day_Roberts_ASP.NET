using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Xml.Linq;

namespace Assigment_5_6_Day_Roberts_ASP.NET
{
    public partial class MemberLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["registered"] == "1")
            {
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Registration successful! Please log in.";
            }

            Session["member_user"] = null;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            string xmlPath = Server.MapPath("~/App_Data/members.xml");
            if (!File.Exists(xmlPath))
            {
                lblMessage.Text = "Member database not found.";
                return;
            }

            XDocument doc = XDocument.Load(xmlPath);

            var member = doc.Descendants("member")
                .FirstOrDefault(x => ((string)x.Element("email")).ToLower() == email.ToLower());

            if (member == null)
            {
                lblMessage.Text = "Email not found. Would you like to create a new user?";
                btnCreateUser.Visible = true;
                return;
            }

            var passwordHash = (string)member.Element("passwordHash");
            var enteredHash = Assignment_5_6_Day_Roberts_NET_DLL.Crypto_NET.Hash(password);

            if (passwordHash == enteredHash)
            {
                Session["member_user"] = member;
                Response.Redirect("~/Members.aspx");
            }
            else
            {
                lblMessage.Text = "Incorrect password.";
            }
        }

        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            // Redirect to registration page
            Response.Redirect("~/RegisterMember.aspx?email=" + Server.UrlEncode(txtEmail.Text.Trim()));
        }
    }
}