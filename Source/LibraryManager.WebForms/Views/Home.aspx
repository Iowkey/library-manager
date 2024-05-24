<%@ Page Async="True" Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="LibraryManager.WebForms.Views.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Book List</h2>
    <asp:GridView ID="BooksGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="BookId" OnRowEditing="BooksGridView_RowEditing" OnRowDeleting="BooksGridView_RowDeleting">
        <Columns>
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="Author" HeaderText="Author" />
            <asp:BoundField DataField="ISBN" HeaderText="ISBN" />
            <asp:BoundField DataField="PublicationYear" HeaderText="Year" />
            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
            <asp:BoundField DataField="CategoryId" HeaderText="Category ID" />
            <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>
    <asp:LinkButton ID="AddNewBookButton" runat="server" PostBackUrl="~/Views/AddBook.aspx" Text="Add New Book" />
</asp:Content>