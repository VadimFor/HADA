using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library.EN;
using System.IO;

namespace usuWeb
{
    public partial class html_registrarse1 : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
            {
                Response.Redirect("default.aspx");
            }
        }

        protected void EventoRegistro(object sender, EventArgs e)
        {
            String email = EmailTextBox.Text;
            String pass = PasswordTextBox.Text;
            String dni = DNITextBox.Text;
            String nombre = NombreTextBox.Text;
            String apellido = ApellidoTextBox.Text;
            String telefono = TelefonoTextBox.Text;
            String message = "";

            var todoOk = checkAll(email, pass, dni, nombre, apellido, telefono, out message);
            if (todoOk)
            {
                EN_Usuario u = new EN_Usuario(email, pass, nombre, dni, telefono, apellido);
                if (u.crearUsuario())
                {
                    //Session["user"] = u;
                    Response.Redirect("html_verification.aspx");
                }
                else
                {
                    string script = @"Func('Error: Usuario ya existe')";
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

        protected Boolean checkAll(String email, String password, String nif, String nombre, String apellidos, String telefono, out String message)
        {
            if (!EmailValid(email))
            {
                message = "Correo no valido";
                return false;
            }
            else if (!ValidatePassword(password, out message))
            {
                return false;
            }
            else if (!checkNIF(nif))
            {
                message = "Nif no valido";
                return false;

            }
            else if (!checkCadena(nombre))
            {
                message = "Nombre no valido";
                return false;
            }
            else if (!checkCadena(apellidos))
            {
                message = "Apellido no valido";
                return false;
            }
            else if (!checkTelefono(telefono))
            {
                message = "Telefono no valido";
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

        public static Boolean ValidatePassword(string password, out string ErrorMessage)
        {
            var input = password;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                ErrorMessage = "La contraseña no puede ser vacía";
                return false;
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@"^.{8,15}$");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                ErrorMessage = "La contraseña tiene que contener almenos una minúscula";
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ErrorMessage = "La contraseña tiene que contener almenos una mayúscula";
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                ErrorMessage = "La contraseña tiene que contener almenos un valor numérico";
                return false;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                ErrorMessage = "La contraseña tiene que contener almenos un caracter especial";
                return false;
            }
            else
            {
                return true;
            }
        }

        private Boolean checkNIF(string nif)
        {
            if (Regex.IsMatch(nif, "^(([A-Z]\\d{8})|[A-Z]\\d{7}[A-Z]|(\\d{8}[A-Z]))$"))
            {
                return true;
            }
            return false;
        }

        private Boolean checkTelefono(string telefono)
        {
            if (Regex.IsMatch(telefono, "^(\\+34|0034|34)?[ -]*(6|7|9)[ -]*([0-9][ -]*){8}$"))
            {
                return true;
            }
            return false;
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