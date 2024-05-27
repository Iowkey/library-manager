<%@ Page Async="True" Title="Error" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="LibraryManager.WebForms.Views.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Oops! Something went wrong</h2>
    <asp:Label ID="MessageLabel" runat="server" ForeColor="Black" />
    <div>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Back to Home" OnClick="BackToHomeButton_Click" />
    </div>
</asp:Content>