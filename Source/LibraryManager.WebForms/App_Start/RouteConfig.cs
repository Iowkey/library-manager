using System.Web.Routing;

namespace LibraryManager.WebForms
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.Ignore("{resource}.axd/{*pathInfo}");

            routes.MapPageRoute("HomeRoute", "Home", "~/Views/Home.aspx");
            routes.MapPageRoute("AddBookRoute", "AddBook", "~/Views/AddBook.aspx");
            routes.MapPageRoute("EditBookRoute", "EditBook/{id}", "~/Views/EditBook.aspx");
            routes.MapPageRoute("DeleteBookRoute", "DeleteBook/{id}", "~/Views/DeleteBook.aspx");
            routes.MapPageRoute("ErrorRoute", "Error", "~/Views/Error.aspx");
        }
    }
}
