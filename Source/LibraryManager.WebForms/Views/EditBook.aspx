<%@ Page Title="Edit Book" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditBook.aspx.cs" Inherits="LibraryManager.WebForms.Views.EditBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Edit Book</h2>
    <asp:Label ID="MessageLabel" runat="server" ForeColor="Red" />
    <asp:Label ID="SuccessLabel" runat="server" ForeColor="Green" />
    <table>
        <tr>
            <td>Title:</td>
            <td><asp:TextBox ID="TitleTextBox" runat="server" /></td>
        </tr>
        <tr>
            <td>Author:</td>
            <td><asp:TextBox ID="AuthorTextBox" runat="server" /></td>
        </tr>
        <tr>
            <td>ISBN:</td>
            <td><asp:TextBox ID="ISBNTextBox" runat="server" /></td>
        </tr>
        <tr>
            <td>Publication Year:</td>
            <td><asp:TextBox ID="PublicationYearTextBox" runat="server" /></td>
        </tr>
        <tr>
            <td>Quantity:</td>
            <td><asp:TextBox ID="QuantityTextBox" runat="server" /></td>
        </tr>
        <tr>
            <td>Category:</td>
            <td>
                <asp:DropDownList ID="CategoryDropDownList" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2"><asp:Button ID="UpdateBookButton" runat="server" Text="Update Book" OnClick="UpdateBookButton_Click" /></td>
        </tr>
    </table>
</asp:Content>