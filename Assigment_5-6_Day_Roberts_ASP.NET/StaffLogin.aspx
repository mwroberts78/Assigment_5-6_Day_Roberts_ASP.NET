<%@ Page Title="Staff Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StaffLogin.aspx.cs" Inherits="Assigment_5_6_Day_Roberts_ASP.NET.StaffLogin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Staff Login</h2>
     <p>To login as a test staff member -- Username: TA / Password: Cse445!</p>
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

    <!-- Modal for messages -->
    <asp:Panel ID="pnlModal" runat="server" CssClass="error-modal" Style="display:none;">
        <asp:Label ID="lblModalMessage" runat="server" ForeColor="Red" />
        <br />
        <br />
        <asp:Button ID="btnCloseModal" runat="server" Text="Close" OnClick="btnCloseModal_Click" />
    </asp:Panel>
</asp:Content>