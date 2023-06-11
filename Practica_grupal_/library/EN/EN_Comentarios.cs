using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library.CAD;

namespace library.EN
{
    /// <summary>
    /// Clase EN_Comentarios.
    /// </summary>
    public class EN_Comentarios
    {
        private int _id;//identificador del comentario
        private string _texto;//texto del comentario
        private string _usuario;//usuario que publica el anuncio
        private int _anuncio;//identificador del anuncio asociado

        private List<EN_Comentarios> _lista = new List<EN_Comentarios>();

        //getters y setters
        public List<EN_Comentarios> lista
        {
            get { return _lista; }
            set { _lista = value; }
        }

        //getters y setters
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string texto
        {
            get { return _texto; }
            set { _texto = value; }
        }

        public string usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public int anuncio
        {
            get { return _anuncio; }
            set { _anuncio = value; }
        }        

        /// <summary>
        /// Constructor por defecto de la clase.
        /// </summary>
        public EN_Comentarios()
        {
            id = -1;
            texto = "";
            usuario = "";
            anuncio = -1;            
        }

        /// <summary>
        /// Constructor con parametros de la clase.
        /// </summary>
        /// <param name="id">id del comentario</param>
        /// <param name="texto">texto del comentario</param>
        /// <param name="usuario">usuario que hace el comentario</param>
        /// <param name="anuncio">anuncio al que pertenece el comentario</param>
        /// <param name="fecha">fecha del comentario</param>
        public EN_Comentarios(int id, string texto, string usuario, int anuncio, DateTime fecha)
        {
            this.id = id;
            this.texto = texto;
            this.usuario = usuario;
            this.anuncio = anuncio;            
        }

        /// <summary>
        /// Metodo createComentario que crea un comentario
        /// </summary>
        /// <returns>devuelve true si se crea el comentario.</returns>
        public bool createComentario()
        {
            CAD_Comentarios cadComent = new CAD_Comentarios();
            return cadComent.createComentario(this);
        }

        /// <summary>
        /// Metodo deleteComentario
        /// </summary>
        /// <returns>devuelve true si se ha borrado el comentario.</returns>
        public bool deleteComentario()
        {
            CAD_Comentarios cadComent = new CAD_Comentarios();
            if (cadComent.readComentario(this))
            {
                return cadComent.deleteComentario(this);
            }
            return false;
        }

        /// <summary>
        /// Metodo readComentario que lee un comentario.
        /// </summary>
        /// <returns>devuelve true si se ha leido el comentario.</returns>
        public bool readComentario()
        {
            CAD_Comentarios cadComent = new CAD_Comentarios();
            return cadComent.readComentario(this);
        }

        /// <summary>
        /// Metodo readFirstComentario que lee el primer comentario
        /// </summary>
        /// <returns>Devuelve true si se ha leido el primer comentario.</returns>
        public bool readFirstComentario()
        {
            CAD_Comentarios cadComent = new CAD_Comentarios();
            return cadComent.readFirstComentario(this);
        }

        /// <summary>
        /// Metodo readNextComentario que lee el siguiente comentario.
        /// </summary>
        /// <returns>Devuelve true si se ha leido el siguiente comentario.</returns>
        public bool readNextComentario()
        {
            CAD_Comentarios cadComent = new CAD_Comentarios();
            if (cadComent.readComentario(this))
            {
                return cadComent.readNextComentario(this);
            }
            return false;
        }

        /// <summary>
        /// Metodo readPrevComentario que lee el comentario anterior.
        /// </summary>
        /// <returns>Devuelve true si se ha leido el comentario anterior.</returns>
        public bool readPrevComentario()
        {
            CAD_Comentarios cadComent = new CAD_Comentarios();
            if (cadComent.readComentario(this))
            {
                return cadComent.readPrevComentario(this);
            }
            return false;
        }        

        /// <summary>
        /// Metodo updateComentario que actualiza el comentario.
        /// </summary>
        /// <param name="nuevoTexto">Texto del nuevo comentario a actualizar.</param>
        /// <returns>Devuelve true si se ha actualizado el comentario de forma correcta.</returns>
        public bool updateComentario(string nuevoTexto)
        {
            CAD_Comentarios cadComent = new CAD_Comentarios();
            return cadComent.updateComentario(this, nuevoTexto);
        }
    }
}
