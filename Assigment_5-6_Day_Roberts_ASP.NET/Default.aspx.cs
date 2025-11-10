using Assigment_5_6_Day_Roberts_ASP.NET.Day_Roberts_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assigment_5_6_Day_Roberts_ASP.NET
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnEstimateShipping_Click(object sender, EventArgs e)
        {
            lblShippingResult.Text = "";
            try
            {
                var client = new Day_Roberts_Service.Service1Client();
                string zip = txtZipShipping.Text.Trim();
                double weight = double.Parse(txtWeight.Text.Trim());
                double result = client.EstimateShipping(zip, weight);
                lblShippingResult.Text = $"Estimated Shipping Cost: {result:C}";
                client.Close();
            }
            catch (Exception ex)
            {
                lblShippingResult.Text = $"Error: {ex.Message}";
            }
        }

        protected void btnGetSalesTax_Click(object sender, EventArgs e)
        {
            lblTaxResult.Text = "";
            try
            {
                var client = new Service1Client();
                string zip = txtZipTax.Text.Trim();
                double amount = double.Parse(txtAmount.Text.Trim());
                double result = client.GetSalesTax(zip, amount);
                lblTaxResult.Text = $"Estimated Sales Tax: {result:C}";
                client.Close();
            }
            catch (Exception ex)
            {
                lblTaxResult.Text = $"Error: {ex.Message}";
            }
        }
    }
}