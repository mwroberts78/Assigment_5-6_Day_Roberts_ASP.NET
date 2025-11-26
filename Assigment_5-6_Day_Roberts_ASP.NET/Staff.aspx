<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Staff.aspx.cs" Inherits="Assigment_5_6_Day_Roberts_ASP.NET.Staff" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Staff Dashboard</h2>
    <p>
        <asp:Label ID="lblLoggedIn" runat="server"></asp:Label>
        <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" />
    </p>
    <p>
        Use the text boxes and update buttons to update the points each registered member has remaining.
    </p>
    <h4>All Members</h4>
    <asp:GridView ID="gvMembers" runat="server" AutoGenerateColumns="false"  OnRowCommand="gvMembers_RowCommand">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="ID" />
            <asp:BoundField DataField="Username" HeaderText="Username" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:TemplateField HeaderText="Update Points">
                 <ItemTemplate>
                     <asp:TextBox ID="txtNewPoints" runat="server" Width="60px" Text='<%# Eval("Points") %>' />
                     <asp:Button ID="btnUpdatePoints" runat="server" Text="Update" CommandName="UpdatePoints" CommandArgument='<%# Eval("Email") %>' />
                 </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="CreatedUtc" HeaderText="Created (UTC)" />
        </Columns>
    </asp:GridView>
</asp:Content>