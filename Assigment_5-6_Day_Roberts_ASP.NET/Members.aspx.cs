using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

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
                    string points = (string)user.Element("points");
                    lblLoggedIn.Text = "Logged in as: <strong>" + username + "</strong>";
                    lblRemainingPoints.Text = "Remaining points: <strong>" + points + "</strong>";
                }
            }

            if (!IsPostBack)
            {
                BindRewards();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session["member_user"] = null;
            Response.Redirect(Request.RawUrl); // Refreshes the page
        }

        private void BindRewards()
        {
            string xmlPath = Server.MapPath("~/App_Data/rewards.xml");
            var rewards = new List<RewardInfo>();

            if (System.IO.File.Exists(xmlPath))
            {
                XDocument doc = XDocument.Load(xmlPath);
                rewards = doc.Descendants("reward")
                    .Select(x => new RewardInfo
                    {
                        Id = (string)x.Attribute("id"),
                        Name = (string)x.Element("name"),
                        Cost = (string)x.Element("cost"),
                        Weight = (string)x.Element("weight"),
                        ImageUrl = ResolveUrl("~/" + (string)x.Element("imageUrl"))
                    })
                    .ToList();
            }

            gvRewards.DataSource = rewards;
            gvRewards.DataBind();
        }

        protected void gvRewards_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            string rewardId = e.CommandArgument.ToString();

            // Load rewards.xml and find the reward by id
            string xmlPath = Server.MapPath("~/App_Data/rewards.xml");
            XDocument doc = XDocument.Load(xmlPath);
            var reward = doc.Descendants("reward")
                .FirstOrDefault(x => (string)x.Attribute("id") == rewardId);

            if (reward == null)
            {
                ShowErrorModal("Reward not found.");
            }

            // Get cost and weight from XML
            int cost = int.Parse((string)reward.Element("cost"));
            double weight = double.Parse((string)reward.Element("weight"));
            decimal dollarValue = decimal.Parse((string)reward.Element("dollarValue"));

            // Get user points from session
            var user = Session["member_user"] as System.Xml.Linq.XElement;
            int userPoints = int.Parse((string)user.Element("points"));

            if (userPoints < cost)
            {
                ShowErrorModal($"You do not have enough points to purchase this reward. You need {cost - userPoints} more points.");
                return;
            }

            confirmRewardId = rewardId;
            confirmDollarValue = dollarValue;
            confirmWeight = weight;

            lblConfirmMessage.Text = $"<strong>Reward:</strong> {(string)reward.Element("name")}<br />" +
                $"<strong>Item Value:</strong> {dollarValue:C}<br />" +
                $"<strong>Cost in Points:</strong> {cost} points<br /><br />" +
                $"Enter your ZIP code to calculate shipping and tax.";

            lblShipping.Text = "";
            lblTax.Text = "";
            lblTotal.Text = "";

            btnRedeem.Visible = false;

            pnlConfirm.Style["display"] = "block";
            ScriptManager.RegisterStartupScript(this, GetType(), "showConfirm", $"document.getElementById('{pnlConfirm.ClientID}').style.display='block';", true);

        }

        protected void btnUpdateZip_Click(object sender, EventArgs e)
        {
            string zip = txtZip.Text.Trim();
            if (string.IsNullOrEmpty(zip))
            {
                lblShipping.Text = "";
                lblTax.Text = "";
                lblTotal.Text = "";
                lblConfirmMessage.Text += "<br /><span style='color:red'>Please enter a ZIP code.</span>";
                return;
            }

            // Calculate shipping and tax
            string shippingStr = GetShippingCost(zip, confirmWeight.ToString());
            string taxStr = GetSalesTax(zip, confirmDollarValue.ToString());

            decimal shipping = 0m, tax = 0m;
            decimal.TryParse(shippingStr.Replace("$", "").Replace(",", ""), out shipping);
            decimal.TryParse(taxStr.Replace("$", "").Replace(",", ""), out tax);

            decimal total = shipping + tax;

            lblShipping.Text = $"<strong>Shipping:</strong> {shipping:C}";
            lblTax.Text = $"<strong>Sales Tax:</strong> {tax:C}";
            lblTotal.Text = $"<strong>Total Cost:</strong> {total:C}";

            // Show Redeem button only after successful calculation
            btnRedeem.Visible = true;
        }

        protected void btnRedeem_Click(object sender, EventArgs e)
        {
            string xmlPath = Server.MapPath("~/App_Data/rewards.xml");
            string membersPath = Server.MapPath("~/App_Data/members.xml");

            XDocument doc = XDocument.Load(xmlPath);
            var reward = doc.Descendants("reward")
                .FirstOrDefault(x => (string)x.Attribute("id") == confirmRewardId);

            Redeem((string)reward.Element("cost"));

            pnlConfirm.Style["display"] = "none";
            ScriptManager.RegisterStartupScript(this, GetType(), "hideConfirm", $"document.getElementById('{pnlConfirm.ClientID}').style.display='none';", true);

            Response.Redirect(Request.RawUrl);
        }

        protected void btnCloseConfirm_Click(object sender, EventArgs e)
        {
            pnlConfirm.Style["display"] = "none";
            ScriptManager.RegisterStartupScript(this, GetType(), "hideConfirm", $"document.getElementById('{pnlConfirm.ClientID}').style.display='none';", true);
        }

        protected void btnCloseModal_Click(object sender, EventArgs e)
        {
            pnlModal.Style["display"] = "none";
            ScriptManager.RegisterStartupScript(this, GetType(), "hideModal", "document.getElementById('" + pnlModal.ClientID + "').style.display='none';", true);
        }

        private void ShowErrorModal(string message)
        {
            lblModalMessage.Text = message;
            pnlModal.Style["display"] = "block";
            ScriptManager.RegisterStartupScript(
                this, GetType(), "showModal",
                $"document.getElementById('{pnlModal.ClientID}').style.display='block';", true);
        }

        public class RewardInfo
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Cost { get; set; }
            public string Weight { get; set; }
            public string ImageUrl { get; set; }
        }

        protected string Redeem(string cost) 
        {
            /* 
             * Assumes that the user is logged in and their info is in session
             * to reach have access to reach this method.
             * needs to cost parameter of reward to input
             */
            // Redemption logic to for points goes here
            // Will either be successful or show difference in points needed
            string result = "";

            var user = Session["member_user"] as System.Xml.Linq.XElement;
            string username = "";
            string points = "";
            if (user != null)
            {
                username = (string)user.Element("email");
                points = (string)user.Element("points");
            }
            else 
            {
                result = "User not found.";
                return result;
            }

            if (int.Parse(points) < int.Parse(cost))
            {
                // if insufficient points
                string difference = (int.Parse(cost) - int.Parse(points)).ToString();
                result = "Unsuccessful. Insufficient by " + difference + " points.";
            }
            else
            {
                // successful redemption
                // update on session and db before returning success message
                string newPoints = (int.Parse(points) - int.Parse(cost)).ToString();
                result = "Redemption successful! You have " + newPoints + " points remaining.";

                user.SetElementValue("points", newPoints);
                Session["member_user"] = user;

                // update points in XML database
                string xmlPath = Server.MapPath("~/App_Data/members.xml");
                System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Load(xmlPath);
                var userInDb = doc.Descendants("member")
                    .FirstOrDefault(x => ((string)x.Element("email")).ToLower() == username.ToLower());
                if (userInDb != null)
                {
                    userInDb.SetElementValue("points", newPoints);
                    doc.Save(xmlPath);
                }
                else
                {
                    result = "Error updating points in database.";
                    return result;

                }
            }

            return result;
        }

        protected string GetShippingCost(string zip, string weight)
        {
            // items need shipping cost calculation based on zip code and weight
            // zip code is user provided and cost and weight are from the item being shipped

            string result = "";
            try
            {
                var client = new Day_Roberts_Service.Service1Client();
                result = client.EstimateShipping(zip, double.Parse(weight)).ToString("C");
                client.Close();
            }
            catch (Exception ex)
            {
                result = $"Error: {ex.Message}";
            }

            return result;
        }

        protected string GetSalesTax(string zip, string cost)
        {
            // items need sales tax calculation based on zip code and cost of items
            // zip code is user provided and cost is from the item being purchased
            string result = "";
            try
            {
                var client = new Day_Roberts_Service.Service1Client();
                result = client.GetSalesTax(zip, double.Parse(cost)).ToString("C");
                client.Close();
            }
            catch (Exception ex)
            {
                result = $"Error: {ex.Message}";
            }
            return result;
        }

        private string confirmRewardId
        {
            get { return ViewState["ConfirmRewardId"] as string; }
            set { ViewState["ConfirmRewardId"] = value; }
        }
        private decimal confirmDollarValue
        {
            get { return ViewState["ConfirmDollarValue"] != null ? (decimal)ViewState["ConfirmDollarValue"] : 0m; }
            set { ViewState["ConfirmDollarValue"] = value; }
        }
        private double confirmWeight
        {
            get { return ViewState["ConfirmWeight"] != null ? (double)ViewState["ConfirmWeight"] : 0.0; }
            set { ViewState["ConfirmWeight"] = value; }
        }
    }
}