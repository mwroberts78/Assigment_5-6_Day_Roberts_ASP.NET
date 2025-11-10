using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Assignment_5_6_Day_Roberts_Service
{
    
    public class Service1 : IService1
    {
        /// <summary>
        /// Estimates the shipping cost based on the destination ZIP code and the package weight.
        /// </summary>
        /// <remarks>The shipping cost is calculated based on the destination and the weight of the
        /// package. Ensure that the ZIP code is valid and the package weight is positive to avoid exceptions. Code by Justin Day</remarks>
        /// <param name="zipCode">The destination ZIP code for the shipment. Must be a valid ZIP code.</param>
        /// <param name="packageWeight">The weight of the package in pounds. Must be greater than 0.</param>
        /// <returns>The estimated shipping cost as a double value.</returns>
        /// <exception cref="ArgumentException">Exception thrown if weight <= 0 </exception>
        public double EstimateShipping(string zipCode, double packageWeight)
        {
            // Simple shipping cost calculation based on weight
            if (packageWeight <= 0)
            {
                throw new ArgumentException("Package weight must be greater than 0.");
            }

            double baseCost = 5.00; // Base cost for shipping
            double weightFactor = 1.50; // Cost per pound
            double shippingCost = baseCost + (weightFactor * packageWeight);
            return shippingCost;
        }

        /// <summary>
        /// Estimates the sales tax for a given amount based on the provided ZIP code.
        /// </summary>
        /// <remarks>The sales tax is calculated based on the ZIP code and the amount provided. Code by Matthew Roberts</remarks>
        /// <param name="zipCode">The destination ZIP code for the shipment. Must be a valid ZIP code.</param>
        /// <param name="amount">The amount of the item in dollars. Must be greater than 0.</param>
        /// <returns>The estimated sales tax amount of the item as a double value. This amount is not legitimate and is not intended to be used as such.</returns>
        /// <exception cref="ArgumentException">Exception thrown if amount <= 0</exception>
        public double GetSalesTax(string zipCode, double amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than 0.");
            }
            double taxRate = 0.07; // Default tax rate
         
            if (zipCode.StartsWith("9")) // This should be west coast - make it higher. 
            {
                taxRate = 0.09;
            }
            else if (zipCode.StartsWith("1")) // This should be northeast - make it a bit lower.
            {
                taxRate = 0.06;
            }
            double salesTax = amount * taxRate;
            return salesTax;
        }
    }
}
