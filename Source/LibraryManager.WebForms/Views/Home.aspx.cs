using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LibraryManager.WebForms.Views
{
    public partial class Home : Page
    {
        private readonly ApiClient _apiClient = new ApiClient();

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBooksGrid();
            }
        }

        private async void BindBooksGrid()
        {
            var books = await _apiClient.GetBooksAsync();
            BooksGridView.DataSource = books;
            BooksGridView.DataBind();
        }

        protected async void BooksGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int bookId = Convert.ToInt32(BooksGridView.DataKeys[e.RowIndex].Values[0]);
            Response.Redirect($"~/Views/DeleteBook.aspx?id={bookId}", false);
            Context.ApplicationInstance.CompleteRequest();
            //Response.Redirect($"~/Views/DeleteBook.aspx?id={bookId}");
            //BindBooksGrid();
        }

        protected void BooksGridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int bookId = Convert.ToInt32(BooksGridView.DataKeys[e.NewEditIndex].Values[0]);
            Response.Redirect($"~/Views/EditBook.aspx?id={bookId}");
        }
    }
}