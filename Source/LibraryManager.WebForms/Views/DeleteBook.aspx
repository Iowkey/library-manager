<%@ Page Async="True" Title="Delete Book" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeleteBook.aspx.cs" Inherits="LibraryManager.WebForms.Views.DeleteBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Delete Book</h2>
    <asp:Label ID="MessageLabel" runat="server" ForeColor="Red" />
    <asp:Label ID="SuccessLabel" runat="server" ForeColor="Green" />
    <asp:Label ID="ConfirmationLabel" runat="server" />
    <asp:Button ID="ConfirmDeleteButton" runat="server" Text="Confirm Delete" OnClick="ConfirmDeleteButton_Click" />
    <asp:Button ID="CancelButton" runat="server" Text="Cancel" OnClick="CancelDeleteButton_Click" />
</asp:Content>