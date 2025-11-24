<%@ Page Title="Register Member" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RegisterMember.aspx.cs" Inherits="Assigment_5_6_Day_Roberts_ASP.NET.RegisterMember" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Register New Member</h2>
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
            <td>
                <asp:Label runat="server" Text="Confirm Password:" AssociatedControlID="txtConfirmPassword" />
            </td>
            <td>
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnRegister" runat="server" Text="Register" OnClick="btnRegister_Click" />
            </td>
        </tr>
    </table>
</asp:Content>