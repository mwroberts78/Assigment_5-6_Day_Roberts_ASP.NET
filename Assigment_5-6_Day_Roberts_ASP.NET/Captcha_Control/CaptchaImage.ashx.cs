using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Assigment_5_6_Day_Roberts_ASP.NET.Captcha_Control
{
    /// <summary>
    /// Generates a CAPTCHA image with random numbers. - Code by Matthew Roberts
    /// </summary>
    public class CaptchaImage : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Session == null || context.Session["CaptchaCode"] == null)
            {
                context.Response.StatusCode = 500;
                context.Response.Write("Session or CaptchaCode is not available.");
                return;
            }

            string captchaCode = context.Session["CaptchaCode"] as string ?? "00000";

            using (Bitmap bmp = new Bitmap(120, 40))
            using (Graphics g = Graphics.FromImage(bmp))
            using (Font font = new Font("Arial", 20, FontStyle.Bold))
            {
                g.Clear(Color.White);
                g.DrawString(captchaCode, font, Brushes.Black, 10, 5);

                context.Response.ContentType = "image/png";
                bmp.Save(context.Response.OutputStream, ImageFormat.Png);
            }
        }

        public bool IsReusable => false;
    }
}