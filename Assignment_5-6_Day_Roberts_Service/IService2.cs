using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Assignment_5_6_Day_Roberts_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService2" in both code and config file together.
    [ServiceContract]
    public interface IService2
    {
        /*
         * Weather forecast service that takes in a address and number of days to forecast
         * and outputs that many days of weather forecast for that address
         */
        [OperationContract]
        string[] WeatherForecast(double days, string street, string city = "", string state = "", string zip = "");
    }
}
