<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TryIt.aspx.cs" Inherits="Assigment_5_6_Day_Roberts_ASP.NET._Default" %>
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

    <h2>Captcha</h2>
    <uc:Captcha ID="CaptchaControl" runat="server" />
  

</asp:Content>
