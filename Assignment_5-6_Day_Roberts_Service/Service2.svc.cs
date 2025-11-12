using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;


namespace Assignment_5_6_Day_Roberts_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service2" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service2.svc or Service2.svc.cs at the Solution Explorer and start debugging.
    public class Service2 : IService2
    {

        /*
         *Service to get weather based on address and returns forecast of days requested
         *the street adress is required, but other parts of an address is optional
         *the service will be split into three parts
         *1. To find the usable geolocation the address will be turned to lat lon 
         *  using Us Census geocode api
         *2. Using the lat lon to find grid points used to get weather forecasts
         *3. Getting the weather forecasts based on grid points, both 2 and 3 using the NWS api
         */
        public string[] WeatherForecast(double days, string street, string city ="", string state = "", string zip = "")
        {
            // check if number of days are in range
            if (days <= 0 || days >7)
            {
                throw new ArgumentException("Number of days must be greater than 0.");
            }

            // geocoding to get the lat, lon of the address to get forecasts
            // could be seperated but nothing else needs this yet
            Uri geocode = new Uri("https://geocoding.geo.census.gov/geocoder/locations/");
            UriTemplate geocodeTemplate = new UriTemplate("adress?street={street}&city={city}&state={state}&zip={zip}" +
                "&benchmark=4&format=json");
            Uri geocodeUri = geocodeTemplate.BindByPosition(geocode, street, city, state, zip);
            WebClient geoclient = new WebClient();
            string geostring;
            try
            {
                geostring = geoclient.DownloadString(geocodeUri);
            }
            catch
            {
                throw new Exception("Unable to connect to location finder.");
            }

            var geonode = JsonNode.Parse(geostring);
            var response = geonode["result"]["addressMatches"];
            if (response == null)
            {
                throw new Exception("Address not found!");
            }

            string lat, lon;
            lat = response[1]["coordinates"]["x"].ToString();
            lon = response[1]["coordinates"]["y"].ToString();

            Uri weather = new Uri("https://api.weather.gov/");
            UriTemplate getgridTemplate = new UriTemplate("points/{lat},{lon}");
            Uri getgridUri = getgridTemplate.BindByPosition(weather, lat, lon);
            WebClient gridclient = new WebClient();
            string gridstring;
            try
            {
                gridstring = gridclient.DownloadString(getgridUri);
            }
            catch
            {
                throw new Exception("issue getting weather location");
            }
            var gridnode = JsonNode.Parse(gridstring);
            response = gridnode["properties"];
            if (response == null)
            {
                throw new Exception("Issue getting weather location");
            }

            // there is a element in the response that has the Uri to get the forecast based on given location
            Uri forecast = new Uri(response["forecast"].ToString());
            WebClient forecastclient = new WebClient();
            string forecaststring;
            try
            {
                forecaststring = forecastclient.DownloadString(forecast);
            }
            catch
            {
                throw new Exception("Issue getting forecast");
            }
            var forecastnode = JsonNode.Parse(forecaststring);
            response = forecastnode["properties"]["periods"];
            if (response == null)
            {
                throw new Exception("Issue getting forecast");
            }

            string[] forecastlist = new string[0];
            for (int i = 0;i < days *2;i++)
            {
                string halfdayforcast = response[i]["name"].ToString() + response[i]["detailedForecast"].ToString();
                forecastlist.Append(halfdayforcast);
            }

            return forecastlist;
            
        }
    }
}
