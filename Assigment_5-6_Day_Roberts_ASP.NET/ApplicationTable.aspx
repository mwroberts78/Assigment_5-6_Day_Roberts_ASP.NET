<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplicationTable.aspx.cs" Inherits="Assigment_5_6_Day_Roberts_ASP.NET.ApplicationTable" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <title>Application and Components Summary Table</title>
    <style>
        table {
            border-collapse: collapse;
            width: 100%;
        }
        th, td {
            border: 1px solid #000;
            padding: 6px;
            vertical-align: top;
        }
        th {
            text-align: left;           
            background-color: lightblue;
        }
        .center {
            text-align: center;
            font-weight: bold;
        }
    </style>
</head>
<body>

<table>
    <tr>
        <th colspan="4" class="center">
            Application and Components Summary Table
        </th>
    </tr>
    <tr>
        <th colspan="4">
            Service deployment located at: 
            <asp:HyperLink 
                NavigateUrl="http://webstrar38.fulton.asu.edu/page0/service1.svc" 
                Text="http://webstrar38.fulton.asu.edu/page0/service1.svc" 
                runat="server" /><br />
            Application deployment located at: 
            <asp:HyperLink 
                NavigateUrl="http://webstrar38.fulton.asu.edu/page1/Default.aspx" 
                Text="http://webstrar38.fulton.asu.edu/page1/Default.aspx" 
                runat="server" AccessKey="1" /><br />
            <br />
            Percentage of overall contribution:
            <em>Justin Day: 50%, Matthew Roberts: 50%</em>
        </th>
    </tr>
    <tr>
        <th>Provider name</th>
        <th>Page and component type, e.g., aspx, DLL, SVC, etc.</th>
        <th>Component description: What does the component do? What are inputs/parameters and output/return value?</th>
        <th>Actual resources and methods used to implement the component and where this component is used.</th>
    </tr>
    <tr>
        <td>Matthew Roberts</td>
        <td>aspx page and server controls</td>
        <td>The public pages including the TryIt that calls all services</td>
        <td>GUI design and C# code behind GUI</td>
    </tr>
    <tr>
        <td>Matthew Roberts</td>
        <td>Captcha User Control</td>
        <td>Generates random number image and verifies user input</td>
        <td>
            Captcha.ascx and CaptchaImage.ashx - generates image from random numbers. 
            <asp:HyperLink 
                NavigateUrl="~/TryIt.aspx#captchaSection" 
                Text="Link To TryIt" 
                runat="server" />
        </td>
    </tr>
    <tr>
        <td>Matthew Roberts</td>
        <td>SVC service</td>
        <td>Takes ZIP and Item cost and returns a fake sales tax amount:<br/>
            Inputs: ZIP code (string) and amount (double)<br />
            Outputs: sales tax amount (double)
        </td>
        <td>
            C# Code in Service - GetSalesTax. 
            <asp:HyperLink 
                NavigateUrl="~/TryIt.aspx#mattServiceSection" 
                Text="Link To TryIt" 
                runat="server" />
        </td>
    </tr>
    <tr>
        <td>Justin Day</td>
        <td>SVC service</td>
        <td>Takes ZIP and Item weight and returns a fake item shipping cost<br/>
            Inputs: ZIP code (string) and weight (double)<br />
            Outputs: shipping amount (double)
        </td>
        <td>
            C# Code in Service - EstimateShipping. 
            <asp:HyperLink 
                NavigateUrl="~/TryIt.aspx#justinServiceSection" 
                Text="Link To TryIt" 
                runat="server" />
        </td>
    </tr>
    <tr>
        <td>Justin Day</td>
        <td>DLL</td>
        <td>
            Hash text (will be used for password hashing)
            Inputs: text (string)<br>
            Output: hashed text (string)
        </td>
        <td>
            C# Code in DLL. 
            <asp:HyperLink 
                NavigateUrl="~/TryIt.aspx#hashSection" 
                Text="Link To TryIt" 
                runat="server" />
        </td>
    </tr>
    <tr>
        <td>Justin Day</td>
        <td>Global.asax</td>
        <td>
            Preloads reward items into application cache at application start, and into user session at first visit.
        </td>
        <td>
            C# Code in Global.asax. 
            <asp:HyperLink 
                NavigateUrl="~/TryIt.aspx#globalasaxSection" 
                Text="Link To TryIt" 
                runat="server" />
        </td>
    </tr>
</table>

</body>
</html>

