using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml.Linq;
using Microsoft.Ajax.Utilities;
using Antlr.Runtime.Tree;

namespace Assigment_5_6_Day_Roberts_ASP.NET
{
    public partial class StaffLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["staff_user"] = null;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ShowModalMessage("Please enter both username and password.");
                return;
            }

            var encryptedPassword = Assignment_5_6_Day_Roberts_NET_DLL.Crypto_NET.Hash(password);

            var user = ValidateStaffUser(username, encryptedPassword);

            if (user !=null)
            {
                Session["staff_user"] = user;

                string returnUrl = Request.QueryString["ReturnUrl"];
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    Response.Redirect(returnUrl);
                } else
                {
                    Response.Redirect("~/Staff.aspx");
                }

            }
            else 
            {
                ShowModalMessage("Invalid username or password.");
            }

        }

        public XElement ValidateStaffUser(string username, string password)
        {
            string xmlPath = Server.MapPath("~/App_Data/staff.xml");

            if (!File.Exists(xmlPath))
                return null;

            XDocument doc = XDocument.Load(xmlPath);

            var user = doc.Descendants("user")
                .FirstOrDefault(x => ((string)x.Element("username")).ToLower() == username.ToLower()
                    && (string)x.Element("passwordHash") == password);

            return user;
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