using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using library.EN;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace usuWeb
{
    public partial class html_publicar : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["InmoData"].ToString();// connection string 
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["user"] == null)
            {
                Response.Redirect("html_IniciarSesion.aspx");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                categoria.DataBind();
            }

        }

        protected void DDL_Venta_Alquiler(object sender, EventArgs e)
        {
            if(Venta_Alquiler.SelectedItem.Text == "Alquiler")
            {
                fianza.Visible = true;
            }
            else
            {
                fianza.Visible = false;
            }
        }

        protected void publicar_Click(object sender, EventArgs e)
        {
            bool datos_introducidos = true;

            if (metros.Text == "")
            {
                //metros.Text = "No se ha introducido campo metros.";
                datos_introducidos = false;
            }
            if (codigo_postal.Text == "")
            {
                //codigo_postal.Text = "No se ha introducido campo codigo postal.";
                datos_introducidos = false;
            }
            if (direccion.Text == "")
            {
                //.Text = "No se ha introducido campo direccion.";
                datos_introducidos = false;
            }
            if (precio.Text == "")
            {
                //.Text = "No se ha introducido campo precio.";
                datos_introducidos = false;
            }
            if (descripcion.Text == "")
            {
                //.Text = "No se ha introducido campo descripcion.";
                datos_introducidos = false;
            }

            // Comprobar fotos introducidas.
            // Propiedad -> m2, codp, direccion, img1, img2, img3
            // Categoria -> tipo, ?descripcion?
            // Usuario -> email

            if (datos_introducidos)
            {
                if (metros.Text.All(c => Char.IsDigit(c)) && codigo_postal.Text.All(c => Char.IsDigit(c)) && precio.Text.All(c => Char.IsDigit(c)))
                {
                    EN_Propiedad propiedad = new EN_Propiedad();
                    
                    propiedad.Metros = int.Parse(metros.Text);
                    propiedad.Cod_postal = codigo_postal.Text;
                    propiedad.Direccion = direccion.Text;
                    EN_Usuario user = new EN_Usuario();
                    user = (EN_Usuario)Session["user"];
                    propiedad.Usuario = user.email;
                    propiedad.Categoria = categoria.SelectedItem.Text;

                    if (SubirImagenes.HasFiles)
                    {
                        int cantImagenes = 1;
                        propiedad.Img1 = null;
                        propiedad.Img2 = null;
                        propiedad.Img3 = null;

                        bool check = false;

                        foreach (HttpPostedFile uploadedFile in SubirImagenes.PostedFiles)
                        {
                            if(uploadedFile.ContentType == "image/png" | uploadedFile.ContentType == "image/jpg" | uploadedFile.ContentType == "image/jpeg"){
                                String fileName = uploadedFile.FileName;
                                String filePrefix = Guid.NewGuid().ToString("N") + "-";
                                String path = Server.MapPath("/imagenes/") + filePrefix + fileName;
                                uploadedFile.SaveAs(path);
                                listaArchivosSubidos.Text += String.Format("{0}<br />", uploadedFile.FileName);
                                check = true;

                                switch (cantImagenes)
                                {
                                    case 1:
                                        propiedad.Img1 = "/imagenes/" + filePrefix + fileName;
                                        break;
                                    case 2:
                                        propiedad.Img2 = "/imagenes/" + filePrefix + fileName;
                                        break;
                                    case 3:
                                        propiedad.Img3 = "/imagenes/" + filePrefix + fileName;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            cantImagenes++;
                        }
                        if (!check)
                        {
                            return; // Faltaría un pop-up modal para avisar de que no se ha insertado.
                        }
                    }
                    else
                    {
                        return; // Faltaría un pop-up modal para avisar de que no se ha insertado.
                    }
                    
                    if (propiedad.nuevaPropiedad())
                    {
                        if (contacto_nombre.Text.All(c => Char.IsLetter(c)) && contacto_apellido.Text.All(c => Char.IsLetter(c) || Char.IsWhiteSpace(c)) && contacto_telefono.Text.All(c => Char.IsDigit(c)))
                        {
                            mensaje.Text = "Propiedad con " + propiedad.Metros + " metros y código postal " + propiedad.Cod_postal + ", dirección " + propiedad.Direccion + " subido por usuario " + propiedad.Usuario + " con categoría " + propiedad.Categoria + " insertado en la B.D.";
                            propiedad.Id = propiedad.obtenerId();

                            EN_Contacto contacto = new EN_Contacto();
                            contacto.Correo = contacto_email.Text;
                            contacto.Nombre = contacto_nombre.Text;
                            contacto.Apellido = contacto_apellido.Text;
                            contacto.Telefono = contacto_telefono.Text;

                            if (contacto.leerContacto() == false)
                            {
                                contacto.crearContacto();
                            }

                            if (Venta_Alquiler.SelectedItem.Value == "Venta")
                            {
                                int PrecioPorMetro = (int.Parse(precio.Text) / int.Parse(metros.Text));
                                EN_Venta venta = new EN_Venta();
                                venta.PrecioPorM = PrecioPorMetro;
                                venta.Precio = int.Parse(precio.Text);
                                venta.Desc = descripcion.Text;
                                venta.Propiedad = propiedad.Id;
                                venta.Contacto = contacto.Correo;
                                venta.createAnuncio();
                            }
                            else
                            {
                                EN_Alquiler alquiler = new EN_Alquiler();
                                alquiler.Fianza = float.Parse(fianza.Text);
                                alquiler.Precio = int.Parse(precio.Text);
                                alquiler.Desc = descripcion.Text;
                                alquiler.Propiedad = propiedad.Id;
                                alquiler.Contacto = contacto.Correo;
                                alquiler.createAnuncio();
                            }
                        }
                        else
                        {
                            mensaje.Text = "Nombre contacto o apellido contacto o telefono contacto no valido.";
                        }
                    }
                    else
                    {
                        mensaje.Text = "No es posible insertar propiedad.";
                    }
                }
                else
                {
                    mensaje.Text = "metros o codigop o precio invalidos. ";
                }
            }
            else
            {
                mensaje.Text = "Alguno de los campos no estan especificados.";
            }
        }
    }
}