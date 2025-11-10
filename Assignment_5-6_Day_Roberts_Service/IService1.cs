using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Assignment_5_6_Day_Roberts_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        /// <summary>
        /// Estimates the shipping cost based on the destination ZIP code and the package weight.
        /// </summary>
        /// <remarks>The shipping cost is calculated based on the destination and the weight of the
        /// package. Ensure that the ZIP code is valid and the package weight is positive to avoid exceptions. Code by Justin Day</remarks>
        /// <param name="zipCode">The destination ZIP code for the shipment. Must be a valid ZIP code.</param>
        /// <param name="packageWeight">The weight of the package in pounds. Must be greater than 0.</param>
        /// <returns>The estimated shipping cost as a double value.</returns>
        [OperationContract]
        double EstimateShipping(string zipCode, double packageWeight);

        /// <summary>
        /// Estimates the sales tax for a given amount based on the provided ZIP code.
        /// </summary>
        /// <remarks>The sales tax is calculated based on the ZIP code and the amount provided. Code by Matthew Roberts</remarks>r
        /// <param name="zipCode">The destination ZIP code for the shipment. Must be a valid ZIP code.</param>
        /// <param name="amount">The amount of the item in dollars. Must be greater than 0.</param>
        /// <returns>The estimated sales tax amount of the item as a double value. This amount is not legitimate and is not intended to be used as such.</returns>
        [OperationContract]
        double GetSalesTax(string zipCode, double amount);
    }

}
