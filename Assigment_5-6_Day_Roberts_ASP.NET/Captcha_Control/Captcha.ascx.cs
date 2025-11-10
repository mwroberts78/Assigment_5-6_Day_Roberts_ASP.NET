using System;
using System.Web.UI;

namespace Assigment_5_6_Day_Roberts_ASP.NET
{
    /// <summary>
    /// Interaction logic for Captcha.ascx - Code by Matthew Roberts
    /// </summary>
    public partial class Captcha : UserControl
    {
        private const string CaptchaSessionKey = "CaptchaCode";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblCaptchaResult.Text = "";
                GenerateCaptcha();
            }
        }

        private void GenerateCaptcha()
        {
            var rand = new Random();
            int code = rand.Next(10000, 99999); // 5-digit code
            Session[CaptchaSessionKey] = code.ToString();
            txtCaptchaInput.Text = "";
        }
        protected void btnValidateCaptcha_Click(object sender, EventArgs e)
        {
            string expected = Session[CaptchaSessionKey] as string;
            string entered = txtCaptchaInput.Text.Trim();

            if (expected == entered)
            {
                lblCaptchaResult.ForeColor = System.Drawing.Color.Green;
                lblCaptchaResult.Text = "CAPTCHA correct!";
            }
            else
            {
                lblCaptchaResult.ForeColor = System.Drawing.Color.Red;
                lblCaptchaResult.Text = "Incorrect CAPTCHA. Try again.";
                GenerateCaptcha();
            }
        }

        protected void btnRefreshCaptcha_Click(object sender, EventArgs e)
        {
            lblCaptchaResult.Text = "";
            GenerateCaptcha();
        }

        // Optional: Expose a property to check if CAPTCHA is valid
        public bool IsCaptchaValid => lblCaptchaResult.Text == "CAPTCHA correct!";

    }
}

