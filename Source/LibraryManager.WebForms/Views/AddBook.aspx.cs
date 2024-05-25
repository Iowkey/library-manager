using LibraryManager.Api.DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;

namespace LibraryManager.WebForms.Views
{
    public partial class AddBook : Page
    {
        private readonly ApiClient _apiClient = new ApiClient();

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await LoadCategoriesAsync();
            }
        }

        private async Task LoadCategoriesAsync()
        {
            var categories = await _apiClient.GetCategoriesAsync();
            CategoryListBox.DataSource = categories;
            CategoryListBox.DataTextField = "Name";
            CategoryListBox.DataValueField = "CategoryId";
            CategoryListBox.DataBind();
        }

        protected async void AddBookButton_Click(object sender, EventArgs e)
        {
            var categoryText = CategoryTextBox.Text.Trim();
            var categories = await _apiClient.GetCategoriesAsync();
            var existingCategory = categories.FirstOrDefault(c => c.Name.Equals(categoryText, StringComparison.OrdinalIgnoreCase));

            if (existingCategory == null)
            {
                categoryText = char.ToUpper(categoryText[0]) + categoryText.Substring(1).ToLower();
                var newCategory = new CategoryDto { Name = categoryText };
                var createdCategory = await _apiClient.CreateCategoryAsync(newCategory);
                existingCategory = createdCategory;
            }

            var book = new BookDto
            {
                Title = TitleTextBox.Text.Trim(),
                Author = AuthorTextBox.Text.Trim(),
                ISBN = ISBNTextBox.Text.Trim(),
                PublicationYear = int.Parse(PublicationYearTextBox.Text.Trim()),
                Quantity = int.Parse(QuantityTextBox.Text.Trim()),
                CategoryId = existingCategory.CategoryId
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
                CategoryListBox.Text = string.Empty;
            }
            else
            {
                MessageLabel.Text = "Error adding book.";
            }
        }

        protected void CategoryListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CategoryTextBox.Text = CategoryListBox.SelectedItem.Text;
        }

        protected void BackToHomeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }
    }
}