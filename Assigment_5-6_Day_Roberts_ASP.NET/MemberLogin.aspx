<%@ Page Title="Member Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MemberLogin.aspx.cs" Inherits="Assigment_5_6_Day_Roberts_ASP.NET.MemberLogin" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Member Login</h2>
    <p>To login as a test user -- Username: testuser@example.com / Password: testuser</p>
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
    <br />
    <table class="noborder">
        <tr>
            <td>
                <asp:Label runat="server" Text="Email:" AssociatedControlID="txtEmail" />
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" Text="Password:" AssociatedControlID="txtPassword" />
            </td>
            <td>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                <asp:Button ID="btnCreateUser" runat="server" Text="Create New User" OnClick="btnCreateUser_Click" Visible="false" />
            </td>
        </tr>
    </table>
</asp:Content>