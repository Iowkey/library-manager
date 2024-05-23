using System;
using System.Web.UI;

namespace LibraryManager.WebForms.Views
{
    public partial class DeleteBook : Page
    {
        private readonly ApiClient _apiClient = new ApiClient();
        private int BookId => int.TryParse(Request.QueryString["id"], out var id) ? id : 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBookDetails();
            }
        }

        private async void BindBookDetails()
        {
            var book = await _apiClient.GetBookAsync(BookId);

            if (book != null)
            {
                ConfirmationLabel.Text = $"Are you sure you want to delete '{book.Title}' by {book.Author}?";
            }
            else
            {
                MessageLabel.Text = "Book not found.";
                ConfirmDeleteButton.Enabled = false;
            }
        }

        protected async void ConfirmDeleteButton_Click(object sender, EventArgs e)
        {
            var success = await _apiClient.DeleteBookAsync(BookId);

            if (success)
            {
                SuccessLabel.Text = "Book deleted successfully!";
                ConfirmDeleteButton.Enabled = false;
            }
            else
            {
                MessageLabel.Text = "Error deleting book.";
            }
        }
    }
}