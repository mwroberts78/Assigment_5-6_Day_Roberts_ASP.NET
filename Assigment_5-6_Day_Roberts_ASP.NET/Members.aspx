<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Members.aspx.cs" Inherits="Assigment_5_6_Day_Roberts_ASP.NET.Members" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <asp:Label ID="lblLoggedIn" runat="server"></asp:Label>
    </p>
    <p>
        <asp:Label ID="lblRemainingPoints" runat="server"></asp:Label>
        
    </p>
    <p>
        <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" />
    </p>
    <h2>Available Rewards</h2>
    <asp:GridView ID="gvRewards" runat="server" AutoGenerateColumns="False" OnRowCommand="gvRewards_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="Image">
                <ItemTemplate>
                    <asp:Image ID="imgReward" runat="server" ImageUrl='<%# Eval("ImageUrl") %>' Width="100px" Height="100px" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:TemplateField>
                <ItemTemplate>
                    <%#Eval("Cost") + " points" %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnPurchase" runat="server" Text="Purchase" CommandName="Purchase" CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <!-- Modal for error messages -->
    <asp:Panel ID="pnlModal" runat="server" CssClass="modal" style="display:none;">
        <asp:Label ID="lblModalMessage" runat="server" ForeColor="Red" />
        <br />
        <br />
        <asp:Button ID="btnCloseModal" runat="server" Text="Close" OnClick="btnCloseModal_Click" />
    </asp:Panel>

    <!-- Confirmation Modal -->
    <asp:Panel ID="pnlConfirm" runat="server" CssClass="modal" Style="display:none;">
        <asp:Label ID="lblConfirmMessage" runat="server" />
        <br /><br />
        <asp:Label runat="server" Text="Enter ZIP code:" AssociatedControlID="txtZip" />
        <asp:TextBox ID="txtZip" runat="server" />
        <asp:Button ID="btnUpdateZip" runat="server" Text="Update" OnClick="btnUpdateZip_Click" />
        <br /><br />
        <asp:Label ID="lblShipping" runat="server" />
        <br />
        <asp:Label ID="lblTax" runat="server" />
        <br />
        <asp:Label ID="lblTotal" runat="server" />
        <br /><br />
        <asp:Button ID="btnRedeem" runat="server" Text="Redeem" OnClick="btnRedeem_Click" />
        <asp:Button ID="btnCloseConfirm" runat="server" Text="Close" OnClick="btnCloseConfirm_Click" />
    </asp:Panel>
    
</asp:Content>


