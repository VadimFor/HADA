using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using library.EN;

namespace usuWeb
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            EN_Administrador ad = new EN_Administrador();
            Application.Add("contador", ad.getVisitasAlmacenadas());

        }

        protected void Session_Start(object sender, EventArgs e)
        {
            EN_Administrador a = new EN_Administrador();
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            a.datetime = DateTime.Now;
            a.ip = ip;
            a.nuevaSesion();

            Application.Lock();
            int nuevo = (int)Application["contador"];
            nuevo++;
            Application["contador"] = nuevo;
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

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}