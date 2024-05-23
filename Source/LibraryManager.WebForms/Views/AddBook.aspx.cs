using LibraryManager.Api.DTOs;
using System;
using System.Web.UI;

namespace LibraryManager.WebForms.Views
{
    public partial class AddBook : Page
    {
        private readonly ApiClient _apiClient = new ApiClient();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategories();
            }
        }

        private async void BindCategories()
        {
            var categories = await _apiClient.GetCategoriesAsync();
            CategoryDropDownList.DataSource = categories;
            CategoryDropDownList.DataTextField = "Name";
            CategoryDropDownList.DataValueField = "CategoryId";
            CategoryDropDownList.DataBind();
        }

        protected async void AddBookButton_Click(object sender, EventArgs e)
        {
            var book = new BookDto
            {
                Title = TitleTextBox.Text,
                Author = AuthorTextBox.Text,
                ISBN = ISBNTextBox.Text,
                PublicationYear = int.Parse(PublicationYearTextBox.Text),
                Quantity = int.Parse(QuantityTextBox.Text),
                CategoryId = int.Parse(CategoryDropDownList.SelectedValue)
            };

            var addedBook = await _apiClient.CreateBookAsync(book);

            if (addedBook != null)
            {
                SuccessLabel.Text = "Book added successfully!";
                TitleTextBox.Text = string.Empty;
                AuthorTextBox.Text = string.Empty;
                ISBNTextBox.Text = string.Empty;
                PublicationYearTextBox.Text = string.Empty;
                QuantityTextBox.Text = string.Empty;
                CategoryDropDownList.ClearSelection();
            }
            else
            {
                MessageLabel.Text = "Error adding book.";
            }
        }
    }
}