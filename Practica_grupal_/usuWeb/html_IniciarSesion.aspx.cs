using library.EN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace usuWeb
{
    public partial class html_InicioSesion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["user"] != null)
            {
                Response.Redirect("default.aspx");
            }
        }

        protected void EventoValidar(object sender, EventArgs e)
        {
            String email = EmailTextBox.Text;
            String pass = PasswordTextBox.Text;
            String message = "";

            var todoOk = checkAll(email, pass, out message);

            if (todoOk)
            {
                EN_Usuario u = new EN_Usuario();
                u.email = email;
                u.clave = pass;
                if (u.existeUsuario())
                {
                    Session["user"] = u;
                    Response.Redirect("html_perfilUsuario.aspx");
                }
                else
                {
                    string script = @"Func('Usuario o contraseña incorrectos')";
                    //Response.Write("<script>alert('" + message +"');</script");
                    ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", script, true);
                }
            }
            else
            {
                string script = @"Func('" + message + "')";
                //Response.Write("<script>alert('" + message +"');</script");
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", script, true);
            }


        }

        protected Boolean checkAll(String email, String password, out String message)
        {
            if (!EmailValid(email))
            {
                message = "Correo no valido";
                return false;
            }
            else if (!checkCadena(password))
            {
                message = "La contraseña no puede ser vacia";
                return false;
            }
            else
            {
                message = "ok";
                return true;
            }
        }

        protected Boolean EmailValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        private Boolean checkCadena(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return false;
            }
            return true;
        }
    }
}