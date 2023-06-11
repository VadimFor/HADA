using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library.CAD;

namespace library.EN
{
    public class EN_Mensaje
    {
        /// <summary>
        /// Identificador del mensaje.
        /// </summary>
        private int _id;
        /// <summary>
        /// Texto del mensaje.
        /// </summary>
        private string _texto;
        /// <summary>
        /// Usuario emisor del mensaje.
        /// </summary>
        private string _usuario_emi;
        /// <summary>
        /// Usuario receptor del mensaje.
        /// </summary>
        private string _usuario_recep;
        /// <summary>
        /// Objeto de capa de acceso a datos para Mensaje.
        /// </summary>
        CAD_Mensaje cad = new CAD_Mensaje();
        /// <summary>
        /// Propiedad para el identificador del mensaje.
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// Propiedad para el texto de un mensaje.
        /// </summary>
        public string Texto
        {
            get { return _texto; }
            set { _texto = value; }
        }
        /// <summary>
        /// Propiedad para el usuario emisor de un mensaje.
        /// </summary>
        public string Usuario_emi
        {
            get { return _usuario_emi; }
            set { _usuario_emi = value; }
        }
        /// <summary>
        /// Propiedad para el usuario receptor de un mensaje.
        /// </summary>
        public string Usuario_recep
        {
            get { return _usuario_recep; }
            set { _usuario_recep = value; }
        }
        /// <summary>
        /// Constructor por defecto de un mensaje.
        /// </summary>
        public EN_Mensaje()
        {
            Id = -1;
            Texto = "";
            Usuario_emi = "";
            Usuario_recep = "";
        }
        /// <summary>
        /// Constructor de un mensaje.
        /// </summary>
        /// <param name="id">Identificador del mensaje.</param>
        /// <param name="texto">Texto del mensaje.</param>
        /// <param name="usuario_emi">Usuario emisor del mensaje.</param>
        /// <param name="usuario_recep">Usuario receptor del mensaje.</param>
        public EN_Mensaje(int id, string texto, string usuario_emi, string usuario_recep)
        {
            this.Id = id;
            this.Texto = texto;
            this.Usuario_emi = usuario_emi;
            this.Usuario_recep = usuario_recep;
        }
        /// <summary>
        /// Método para crear un mensaje.
        /// </summary>
        /// <returns>True si se ha completado la operación con éxito, false si no.</returns>
        public bool createMensaje()
        {
            return cad.createMensaje(this);
        }
        /// <summary>
        /// Método para leer los mensajes entre un usuario emisor (el usuario que tiene sesión inicidada) y otro usuario receptor (el que se ha seleccionado mediante el DropDownList en la página html_mensajes).
        /// </summary>
        /// <returns></returns>
        public DataTable readMensajes()
        {
            return cad.readMensajes(this);
        }
        /// <summary>
        /// Método para eliminar un mensaje.
        /// </summary>
        /// <returns>True si se ha completado la operación con éxito, false si no.</returns>
        public bool deleteMensaje()
        {
            return cad.deleteMensaje(this);
        }
    }
}
