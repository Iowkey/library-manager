﻿using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace LibraryManager.WebForms
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        void Application_Error(object sender, EventArgs e)
        {
            Server.ClearError();
            Response.Redirect("Error.aspx");
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}