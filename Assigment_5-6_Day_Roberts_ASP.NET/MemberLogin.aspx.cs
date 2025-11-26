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
            if (!IsPostBack)
            {
                // Initial state: only email and Next button visible
                rowPassword.Visible = false;
                btnLogin.Visible = false;
                txtPassword.Text = "";
            }

            if (Request.QueryString["registered"] == "1")
            {
                ShowModalMessage("Registration successful! Please log in.", System.Drawing.Color.Green);
            }

            Session["member_user"] = null;
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                ShowModalMessage("Please enter your email.", System.Drawing.Color.Red);
                return;
            }

            string xmlPath = Server.MapPath("~/App_Data/members.xml");
            if (!System.IO.File.Exists(xmlPath))
            {
                ShowModalMessage("Member database not found.", System.Drawing.Color.Red);
                return;
            }

            var doc = System.Xml.Linq.XDocument.Load(xmlPath);
            var member = doc.Descendants("member")
                .FirstOrDefault(x => ((string)x.Element("email")).ToLower() == email.ToLower());

            if (member != null)
            {
                // Email exists, show password and Login button
                rowPassword.Visible = true;
                btnLogin.Visible = true;
                btnModalCreateUser.Visible = false;
                txtPassword.Focus();
            }
            else
            {
                // Email not found, show modal with Create New User button
                rowPassword.Visible = false;
                btnLogin.Visible = false;
                ShowModalMessage("Email not found. Would you like to create a new account?", System.Drawing.Color.Black, true);
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            string xmlPath = Server.MapPath("~/App_Data/members.xml");
            var doc = System.Xml.Linq.XDocument.Load(xmlPath);
            var member = doc.Descendants("member")
                .FirstOrDefault(x => ((string)x.Element("email")).ToLower() == email.ToLower());

            if (member == null)
            {
                ShowModalMessage("Email not found.", System.Drawing.Color.Red);
                return;
            }

            string passwordHash = (string)member.Element("passwordHash");
            string enteredHash = Assignment_5_6_Day_Roberts_NET_DLL.Crypto_NET.Hash(password);

            if (passwordHash == enteredHash)
            {
                Session["member_user"] = member;
                Response.Redirect("~/Members.aspx");
            }
            else
            {
                ShowModalMessage("Incorrect password.", System.Drawing.Color.Red);
            }
        }

        protected void btnCreateUser_Click(object sender, EventArgs e)
        {
            // Redirect to registration page
            Response.Redirect("~/RegisterMember.aspx?email=" + Server.UrlEncode(txtEmail.Text.Trim()));
        }

        protected void btnCloseModal_Click(object sender, EventArgs e)
        {
            pnlModal.Style["display"] = "none";
            ScriptManager.RegisterStartupScript(this, GetType(), "hideModal",
                $"document.getElementById('{pnlModal.ClientID}').style.display='none';", true);
        }

        private void ShowModalMessage(string message, System.Drawing.Color color, bool showCreateUser = false)
        {
            lblModalMessage.Text = message;
            lblModalMessage.ForeColor = color;
            btnModalCreateUser.Visible = showCreateUser;
            pnlModal.Style["display"] = "block";
            ScriptManager.RegisterStartupScript(this, GetType(), "showModal",
                $"document.getElementById('{pnlModal.ClientID}').style.display='block';", true);
        }
    }
}