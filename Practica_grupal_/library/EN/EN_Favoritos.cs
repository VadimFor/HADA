using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library.CAD;
using library;

namespace library.EN
{
    /// <summary>
    /// Clase EN_Favoritos.
    /// </summary>
    public class EN_Favoritos
    {
        private List<EN_Anuncio> _lista = new List<EN_Anuncio>();
        private string _usuario_id;
        private int _anuncio_id;
        private string _imagen;
        private string _desde;
        private string _limite;
        private bool _avanzar = false;
        private bool _volver = false;
        private string _hasta;
        private EN_Usuario _usuario = new EN_Usuario();

        public EN_Usuario usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }
        public string hasta
        {
            get { return _hasta; }
            set { _hasta = value; }
        }

        public bool avanzar
        {
            get { return _avanzar; }
            set { _avanzar = value; }
        }
        public bool volver
        {
            get { return _volver; }
            set { _volver = value; }
        }


        public string imagen
        {
            get { return _imagen; }
            set { _imagen = value; }
        }
        public string usuario_id
        {
            get { return _usuario_id; }
            set { _usuario_id = value; }
        }
        public int anuncio_id
        {
            get { return _anuncio_id; }
            set { _anuncio_id = value; }
        }

        public string desde
        {
            get { return _desde; }
            set { _desde = value; }
        }

        public string limite
        {
            get { return _limite; }
            set { _limite = value; }
        }

        public List<EN_Anuncio> lista
        {
            get { return _lista; }
            set { _lista = value; }
        }



        /// <summary>
        /// Constructor por defecto de la clase.
        /// </summary>
        public EN_Favoritos()
        {
            usuario_id = "";
            anuncio_id = -1;
        }

        /// <summary>
        /// Constructor con parametros de la clase.
        /// </summary>
        /// <param name="usu_id">Parametro del id del usuario.</param>
        /// <param name="anu_id">Parametro del id del anuncio.</param>
        public EN_Favoritos(string usu_id, int anu_id)
        {
            usuario_id = usu_id;
            anuncio_id = anu_id;
        }

        /// <summary>
        /// Metodo readFavoritos que lee un favorito.
        /// </summary>
        /// <returns>Devuelve true si se ha leido de forma correcta.</returns>
        public bool readFavoritos()
        {
            CAD_Favoritos cadFav = new CAD_Favoritos();
            return cadFav.readFavoritos(this);
        }

        /// <summary>
        /// Metodo createFavoritos que crea un favorito nuevo.
        /// </summary>
        /// <returns>Devuelve true si se ha creado de forma correcta.</returns>
        public bool createFavoritos()
        {
            CAD_Favoritos cadFav = new CAD_Favoritos();
            
            if(!cadFav.leerFavorito(this))
            {
                return cadFav.createFavoritos(this);
            }

            return false;
        }

        /// <summary>
        /// Metodo deleteFavortos que borra un favorito ya existente.
        /// </summary>
        /// <returns>Devuelve true si se ha borrado de forma correcta.</returns>
        public bool deleteFavoritos()
        {
            CAD_Favoritos cadFav = new CAD_Favoritos();
            if (cadFav.leerFavorito(this))
            {
                return cadFav.deleteFavoritos(this);
            }
            return false;
        }

        /// <summary>
        /// Metodo updateFavoritos que actualiza un favorito.
        /// </summary>
        /// <returns>Devuelve true si se ha actulizado de forma correcta.</returns>
        public bool updateFavoritos()
        {
            CAD_Favoritos cadFav = new CAD_Favoritos();
            if (cadFav.readFavoritos(this))
            {
                return cadFav.updateFavoritos(this);
            }
            return false;
        }
    }
}
