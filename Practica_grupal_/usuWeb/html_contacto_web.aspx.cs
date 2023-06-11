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
    public partial class html_contacto_web : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if ((EN_Usuario)Session["user"] != null)
            {
                this.MasterPageFile = "/Usuario.Master";
            }
            else
            {
                this.MasterPageFile = "/Site1.Master";

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void enviarMensaje(object sender, EventArgs e)
        {
            if (name.Text != "" && email.Text != "" && asunto.Text != "" && cuerpo.Text != "")
            {
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                MailMessage message = new MailMessage();

                try
                {
                    MailAddress fromAddress = new MailAddress(email.Text, name.Text + " (" + email.Text + ")");
                    MailAddress toAddress = new MailAddress("sanvihouses@gmail.com", "SanviHouses");

                    message.From = fromAddress;
                    message.To.Add(toAddress);
                    message.Subject = asunto.Text + " (" + email.Text + ")";
                    message.Body = cuerpo.Text;
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new System.Net.NetworkCredential("sanvihouses@gmail.com", "hola123_");
                    smtpClient.Send(message);

                    Label1.Text = "Mensaje Enviado";
                }
                catch (Exception ex)
                {
                    Console.WriteLine("User operation has failed.Error: {0}", ex.Message);
                }
            }
            else
            {
                Label1.Text = "Faltan campos por rellenar";
            }

            name.Text = "";
            email.Text = "";
            asunto.Text = "";
            cuerpo.Text = "";
        }

    }
}