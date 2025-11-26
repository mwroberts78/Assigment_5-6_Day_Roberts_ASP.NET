<%@ Page Title="Member Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MemberLogin.aspx.cs" Inherits="Assigment_5_6_Day_Roberts_ASP.NET.MemberLogin" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Member Login</h2>
    <p>To login as a test user -- Username: testuser@example.com / Password: testuser</p>
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
        <tr id="rowPassword" runat="server" visible="false">
            <td>
                <asp:Label runat="server" Text="Password:" AssociatedControlID="txtPassword" />
            </td>
            <td>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnNext" runat="server" Text="Next" OnClick="btnNext_Click" />
                <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" Visible="false" />
            </td>
        </tr>
    </table>

    <!-- Modal for messages -->
    <asp:Panel ID="pnlModal" runat="server" CssClass="error-modal" Style="display:none;">
        <asp:Label ID="lblModalMessage" runat="server" ForeColor="Red" />
        <br /><br />
        <asp:Button ID="btnModalCreateUser" runat="server" Text="Create New User" OnClick="btnCreateUser_Click" Visible="false" />
        <asp:Button ID="btnCloseModal" runat="server" Text="Close" OnClick="btnCloseModal_Click" />
    </asp:Panel>
</asp:Content>