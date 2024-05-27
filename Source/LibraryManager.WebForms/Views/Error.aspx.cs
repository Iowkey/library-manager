using System;

namespace LibraryManager.WebForms.Views
{
    public partial class Error : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MessageLabel.Text = "An unexpected error occurred. Please ensure that input data is valid and try again.";
            }
        }

        protected void BackToHomeButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}