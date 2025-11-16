<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Assigment_5_6_Day_Roberts_ASP.NET._Default" %>
<%@ Register Src="~/Captcha_Control/Captcha.ascx" TagPrefix="uc" TagName="Captcha" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        This application will allow members to log in and see how many points they have accrued and then spend 
        those points on a limited number of items. Then it will tell you the shipping and sales tax cost of the item. 
    </p>
    <p>
        The staff section will allow staff to adjust the points for members registered in the application.
    </p>
    <p>
        You can use the menu buttons above to go to the individual pages, however only the Try It page works at this time. 
    </p>  

</asp:Content>
