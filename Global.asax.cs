using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
namespace InventoryManagement
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Application["TotalUsers"] = 0;
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
            {
                Path = "~/Scripts/jquery-3.5.1.min.js",  // Adjust the path if necessary
                DebugPath = "~/Scripts/jquery-3.5.1.js",
                CdnPath = "https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js",
                CdnDebugPath = "https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.js"
            });

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            Application["TotalUsers"] = (int)Application["TotalUsers"] + 1;
            Application.UnLock();


        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            if (Application["ActiveUsers"] == null)
                Application["ActiveUsers"] = 0;
            Application["ActiveUsers"] = (int)Application["ActiveUsers"] + 1;
            Application.UnLock();


        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}