using LibraryManager.Api.DTOs;
using System;
using System.Web.UI;

namespace LibraryManager.WebForms.Views
{
    public partial class EditBook : Page
    {
        private readonly ApiClient _apiClient = new ApiClient();
        private int BookId => int.TryParse(Request.QueryString["id"], out var id) ? id : 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategories();
                BindBookDetails();
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

        private async void BindBookDetails()
        {
            var book = await _apiClient.GetBookAsync(BookId);

            if (book != null)
            {
                TitleTextBox.Text = book.Title;
                AuthorTextBox.Text = book.Author;
                ISBNTextBox.Text = book.ISBN;
                PublicationYearTextBox.Text = book.PublicationYear.ToString();
                QuantityTextBox.Text = book.Quantity.ToString();
                CategoryDropDownList.SelectedValue = book.CategoryId.ToString();
            }
            else
            {
                MessageLabel.Text = "Book not found.";
            }
        }

        protected async void UpdateBookButton_Click(object sender, EventArgs e)
        {
            var book = new BookDto
            {
                BookId = BookId,
                Title = TitleTextBox.Text,
                Author = AuthorTextBox.Text,
                ISBN = ISBNTextBox.Text,
                PublicationYear = int.Parse(PublicationYearTextBox.Text),
                Quantity = int.Parse(QuantityTextBox.Text),
                CategoryId = int.Parse(CategoryDropDownList.SelectedValue)
            };

            var updatedBook = await _apiClient.UpdateBookAsync(book);

            if (updatedBook != null)
            {
                SuccessLabel.Text = "Book updated successfully!";
            }
            else
            {
                MessageLabel.Text = "Error updating book.";
            }
        }
    }
}