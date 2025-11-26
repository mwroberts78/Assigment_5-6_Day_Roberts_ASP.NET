using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Assigment_5_6_Day_Roberts_ASP.NET
{
    public partial class RegisterMember : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Pre-fill email if passed from login
                string email = Request.QueryString["email"];
                if (!string.IsNullOrEmpty(email))
                    txtEmail.Text = email;
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                ShowModalMessage("All fields are required.");
                return;
            }

            if (password != confirmPassword)
            {
                ShowModalMessage("Passwords do not match.");
                return;
            }

            string xmlPath = Server.MapPath("~/App_Data/members.xml");
            XDocument doc;

            if (File.Exists(xmlPath))
            {
                doc = XDocument.Load(xmlPath);
            }
            else
            {
                doc = new XDocument(new XElement("members"));
            }

            // Check for existing email
            bool exists = doc.Descendants("member")
                .Any(x => ((string)x.Element("email")).ToLower() == email.ToLower());

            if (exists)
            {
                ShowModalMessage("A member with this email already exists.");
                return;
            }

            // Hash password
            string passwordHash = Assignment_5_6_Day_Roberts_NET_DLL.Crypto_NET.Hash(password);

            // Generate new member id
            int nextId = doc.Descendants("member")
                .Select(x => (string)x.Attribute("id"))
                .Where(id => id != null && id.StartsWith("M"))
                .Select(id => int.TryParse(id.Substring(1), out int n) ? n : 0)
                .DefaultIfEmpty(0)
                .Max() + 1;

            string newId = $"M{nextId:D3}";

            // Create new member element, username is set to email
            XElement newMember = new XElement("member",
                new XAttribute("id", newId),
                new XElement("username", email),
                new XElement("passwordHash", passwordHash),
                new XElement("email", email),
                new XElement("points", 500),
                new XElement("createdUtc", DateTime.UtcNow.ToString("u"))
            );

            doc.Root.Add(newMember);
            doc.Save(xmlPath);

            Response.Redirect("~/MemberLogin.aspx?registered=1");
        }

        protected void btnCloseModal_Click(object sender, EventArgs e)
        {
            pnlModal.Style["display"] = "none";
            ScriptManager.RegisterStartupScript(this, GetType(), "hideModal",
                $"document.getElementById('{pnlModal.ClientID}').style.display='none';", true);
        }

        private void ShowModalMessage(string message)
        {
            lblModalMessage.Text = message;
            pnlModal.Style["display"] = "block";
            ScriptManager.RegisterStartupScript(this, GetType(), "showModal",
                $"document.getElementById('{pnlModal.ClientID}').style.display='block';", true);
        }
    }
}