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
    public partial class html_contacto : System.Web.UI.Page
    {
        EN_Contacto en = new EN_Contacto();

        protected void Page_PreInit(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e) 
        {
            if (Request.QueryString["contacto"] != null)
            {
                en.Correo = Request.QueryString["contacto"];
                if (en.leerContacto())
                {
                    Label3.Text = Convert.ToString(en.Telefono);
                    Label4.Text = en.Correo;
                }
                else
                {
                    Response.Redirect("html_404.aspx");
                }
            }
            else
            {
                Response.Redirect("html_404.aspx");
            }
        }

        protected void enviarMensaje(object sender, EventArgs e)
        {
            EN_Usuario enUsu = new EN_Usuario();
            enUsu = (EN_Usuario)Session["user"];

            if (enUsu != null)
            {
                if (cuerpo.Text != "")
                {
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                    MailMessage message = new MailMessage();

                    try
                    {
                        MailAddress fromAddress = new MailAddress("sanvihouses@gmail.com");
                        MailAddress toAddress = new MailAddress(en.Correo);

                        message.From = fromAddress;
                        message.To.Add(toAddress);
                        message.Subject = "Consulta de (" + enUsu.email + ")";
                        message.Body = "Hola <strong>" + cuerpo.Text + "</strong>";
                        message.IsBodyHtml = true;
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new System.Net.NetworkCredential("sanvihouses@gmail.com", "hola123_");
                        smtpClient.Send(message);

                        Label5.Text = "Mensaje Enviado";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("User operation has failed.Error: {0}", ex.Message);
                    }
                }
                else
                {
                    Label5.Text = "Debes de añadir un comentario";
                }
            }
            else
            {
                Response.Redirect("html_IniciarSesion.aspx");
            }

        }
    }
}