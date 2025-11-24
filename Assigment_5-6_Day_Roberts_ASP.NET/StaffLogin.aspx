<%@ Page Title="Staff Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StaffLogin.aspx.cs" Inherits="Assigment_5_6_Day_Roberts_ASP.NET.StaffLogin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Staff Login</h2>
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
    <br />
    <p>
        <asp:Label runat="server" Text="Username:" AssociatedControlID="txtUsername" />
        <asp:TextBox ID="txtUsername" runat="server" />
    </p>
    <p>
        <asp:Label runat="server" Text="Password:&nbsp;" AssociatedControlID="txtPassword" />
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
    </p>
    <br />
    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
</asp:Content>