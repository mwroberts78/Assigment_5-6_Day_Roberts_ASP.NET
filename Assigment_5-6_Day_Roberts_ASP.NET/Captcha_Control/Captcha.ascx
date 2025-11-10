<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Captcha.ascx.cs" Inherits="Assigment_5_6_Day_Roberts_ASP.NET.Captcha" %>
<img src="/Captcha_Control/CaptchaImage.ashx" alt="CAPTCHA" />
<asp:TextBox ID="txtCaptchaInput" runat="server" />
<asp:Button ID="btnValidateCaptcha" runat="server" Text="Validate" OnClick="btnValidateCaptcha_Click" />
<asp:Button ID="btnRefreshCaptcha" runat="server" Text="Refresh" OnClick="btnRefreshCaptcha_Click" />
<asp:Label ID="lblCaptchaResult" runat="server" ForeColor="Red" />
