using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml.Linq;

namespace Assigment_5_6_Day_Roberts_ASP.NET
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Load and cache rewards for the whole app
            string path = Server.MapPath("~/App_Data/rewards.xml");
            XDocument doc = XDocument.Load(path);

            Application["RewardsData"] = doc;        // store whole doc
            Application["RewardsList"] = doc.Root.Elements("reward").ToList();

            Application["AppStartedUtc"] = DateTime.UtcNow;
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Copy preloaded rewards into this user's session
            Session["Rewards"] = Application["RewardsList"];
        }
    }
}