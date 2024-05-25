<%@ Page Async="True" Title="Add Book" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddBook.aspx.cs" Inherits="LibraryManager.WebForms.Views.AddBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Add New Book</h2>
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
                <asp:TextBox ID="CategoryTextBox" runat="server" AutoPostBack="false" onkeyup="filterCategories()" />
                <asp:ListBox ID="CategoryListBox" runat="server" AutoPostBack="false" Height="100px" SelectionMode="Single" OnSelectedIndexChanged="CategoryListBox_SelectedIndexChanged"></asp:ListBox>
            </td>
        </tr>
        <tr>
            <td colspan="2"><asp:Button ID="AddBookButton" runat="server" Text="Add Book" OnClick="AddBookButton_Click" /></td>
        </tr>
        <tr>
            <td colspan="2"><asp:Button ID="BackToHomeButton" runat="server" Text="Back to Home" OnClick="BackToHomeButton_Click" /></td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script type="text/javascript">
        function filterCategories() {
            var input = $("#<%= CategoryTextBox.ClientID %>").val().toLowerCase();
            $("#<%= CategoryListBox.ClientID %> option").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(input) > -1)
            });
        }
    </script>
</asp:Content>