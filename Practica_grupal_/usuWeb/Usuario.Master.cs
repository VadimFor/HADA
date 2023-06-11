using library.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace usuWeb
{
    public partial class Usuario : System.Web.UI.MasterPage
    {
        EN_Usuario user = new EN_Usuario();
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (EN_Usuario)Session["user"];
            MenuItem adminMenuItem = new MenuItem
            {
                Text = "Admin",
                Value = "8",
                NavigateUrl = "/html_admin.aspx"
            };

            if (!IsPostBack && user != null && user.esAdmin && !NavMenu.Items.Contains(adminMenuItem))
            {
                NavMenu.Items.Add(adminMenuItem);
            }
        }
        protected void cerrarSesion(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("/");
        }

        protected void NavMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            if (e.Item.Text == "")
            {
                cerrarSesion(sender, e);
            }
        }
    }
}