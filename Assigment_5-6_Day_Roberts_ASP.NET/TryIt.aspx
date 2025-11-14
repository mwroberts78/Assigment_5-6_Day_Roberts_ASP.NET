<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TryIt.aspx.cs" Inherits="Assigment_5_6_Day_Roberts_ASP.NET.TryIt" %>
<%@ Register Src="~/Captcha_Control/Captcha.ascx" TagPrefix="uc" TagName="Captcha" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

  
    <h2>Estimate Shipping</h2>
    <asp:Label runat="server" Text="ZIP Code:" AssociatedControlID="txtZipShipping" />
    <asp:TextBox ID="txtZipShipping" runat="server" />
    <asp:Label runat="server" Text="Package Weight:" AssociatedControlID="txtWeight" />
    <asp:TextBox ID="txtWeight" runat="server" />
    <asp:Button ID="btnEstimateShipping" runat="server" Text="Estimate Shipping" OnClick="btnEstimateShipping_Click" />
    <br />
    <asp:Label ID="lblShippingResult" runat="server" ForeColor="Blue" />

    <hr />

    <h2>Get Sales Tax</h2>
    <asp:Label runat="server" Text="ZIP Code:" AssociatedControlID="txtZipTax" />
    <asp:TextBox ID="txtZipTax" runat="server" />
    <asp:Label runat="server" Text="Amount:" AssociatedControlID="txtAmount" />
    <asp:TextBox ID="txtAmount" runat="server" />
    <asp:Button ID="btnGetSalesTax" runat="server" Text="Get Sales Tax" OnClick="btnGetSalesTax_Click" />
    <br />
    <asp:Label ID="lblTaxResult" runat="server" ForeColor="Green" />

    <hr />

    <h2>Hash Password</h2>
    <asp:Label runat="server" Text="Password:" AssociatedControlID="txtPassword" />
    <asp:TextBox ID="txtPassword" runat="server" />
    <asp:Button ID="btnHashPassword" runat="server" Text="Hash Password" OnClick="btnHashPassword_Click" />
    <br />
    <asp:Label ID="lblHashedPassword" runat="server" ForeColor="Green" />

    <hr />

     <h2>Preloaded Rewards</h2>
    <p>
        This section demonstrates loading data at Application Start and storing it in each user's Session.
    </p>
    <p>
        The rewards are loaded into the application cache at app start and into each user's session when they first visit this page.
    </p>
    <p>
        You can click the button to show the rewards that are stored in your session.
    </p>
     <asp:Button ID="btnShowRewardsInfo" runat="server" Text="Show Session Rewards" OnClick="btnShowRewardsInfo_Click" />
     <br />
     <asp:Label ID="lblRewardsInfo" runat="server" ForeColor="Green" />
    <asp:Label ID="lblRewardsNames" runat="server" ForeColor="Green" />

     <hr />

    <h2>Captcha</h2>
    <uc:Captcha ID="CaptchaControl" runat="server" />
  

</asp:Content>
