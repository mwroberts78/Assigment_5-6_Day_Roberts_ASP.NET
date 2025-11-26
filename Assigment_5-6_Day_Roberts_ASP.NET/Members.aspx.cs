using Microsoft.SqlServer.Server;
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
    }
}