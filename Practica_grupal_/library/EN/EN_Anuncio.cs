using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library.CAD;

namespace library.EN
{
    public class EN_Anuncio
    {
        /// <summary>
        /// Identificador de un anuncio.
        /// </summary>
        private int _id;
        /// <summary>
        /// Precio asociado a un anuncio.
        /// </summary>
        private float _precio;
        /// <summary>
        /// Descripción de un anuncio.
        /// </summary>
        private string _desc;
        /// <summary>
        /// La propiedad anunciada por el anuncio.
        /// </summary>
        private int _propiedad;
        /// <summary>
        /// Contacto de un anuncio.
        /// </summary>
        private string _contacto;
        /// <summary>
        /// Objeto CADAnuncio para llamar a los métodos de CADAnuncio.
        /// </summary>
        protected CAD_Anuncio cad = new CAD_Anuncio();
        /// <summary>
        /// Objeto ENPropiedad.
        /// </summary>
        public EN_Propiedad _enPropiedad;
        /// <summary>
        /// Propiedad de _enPropiedad.
        /// </summary>
        public EN_Propiedad ENPropiedad
        {
            get { return _enPropiedad; }
            set { _enPropiedad = value; }
        }
        public int Propiedad
        {
            get { return _propiedad; }
            set { _propiedad = value; }
        }
        /// <summary>
        /// Propiedad de _id.
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// Propiedad de _precio.
        /// </summary>
        public float Precio
        {
            get { return _precio; }
            set { _precio = value; }
        }
        /// <summary>
        /// Propiedad de _desc.
        /// </summary>
        public string Desc
        {
            get { return _desc; }
            set { _desc = value; }
        }
        /// <summary>
        /// Propiedad de _contacto.
        /// </summary>
        public string Contacto
        {
            get { return _contacto; }
            set { _contacto = value; }
        }
        /// <summary>
        /// Constructor por defecto.
        /// </summary>
        public EN_Anuncio()
        {
            this.Id = -1;
            this.Precio = -1;
            this.Desc = "";
            this.Propiedad = -1;
            this.Contacto = "";
        }
        /// <summary>
        /// Constructor sobrecargado.
        /// </summary>
        /// <param name="id">Id del anuncio.</param>
        /// <param name="precio">Precio del anuncio.</param>
        /// <param name="desc">Descripción del anuncio.</param>
        //  <param name="propiedad">Identificador del propietario del anuncio.</param>
        //  <param name="contacto">Contacto del anuncio.</param>
        public EN_Anuncio(int id, float precio, string desc, int propiedad, string contacto)
        {
            this.Id = id;
            this.Precio = precio;
            this.Desc = desc;
            this.Propiedad = propiedad;
            this.ENPropiedad.Id = propiedad;
            this.ENPropiedad.leerPropiedad();
            this.Contacto = contacto;
        }
        /// <summary>
        /// Guarda el anuncio en la BD.
        /// </summary>
        /// <returns>True si se ha completado la operación con éxito, false si no.</returns>
        public virtual bool createAnuncio()
        {
            if (!cad.readAnuncio(this)) // Si el anuncio no existe ya, se crea.
            {
                return cad.createAnuncio(this);
            }
            return false;
        }
        /// <summary>
        /// Recupera el anuncio indicado de la BD.
        /// </summary>
        /// <returns>True si se ha completado la operación con éxito, false si no.</returns>
        public virtual bool readAnuncio()
        {
            return cad.readAnuncio(this);
        }
        /// <summary>
        /// Actualiza este anuncio en la BD.
        /// </summary>
        /// <returns>True si se ha completado la operación con éxito, false si no.</returns>
        public virtual bool updateAnuncio()
        {
            if (cad.readAnuncio(this)) // Si el anuncio está en la base de datos, se puede actualizarlo.
            {
                return cad.updateAnuncio(this);
            }
            return false;
        }
        /// <summary>
        /// Borra este anuncio en la BD.
        /// </summary>
        /// <returns>True si se ha completado la operación con éxito, false si no.</returns>
        public bool deleteAnuncio()
        {
            if (cad.readAnuncio(this)) // Si el anuncio existe, se elimina.
            {
                return cad.deleteAnuncio(this);
            }
            return false;
        }
        /// <summary>
        /// Método para devolver los resultados de leer los comentarios asociados a un anuncio.
        /// </summary>
        /// <returns>Devuelve objeto DataTable con los comentarios asociados al anuncio.</returns>
        public DataTable readComentariosDeAnuncio()
        {
            DataTable comentarios = new DataTable();
            if (cad.readAnuncio(this))
            {
                comentarios = cad.readComentariosPorAnuncio(this);
            }
            return comentarios;
        }
        /// <summary>
        /// Método para devolver los datos para un anuncio.
        /// </summary>
        /// <returns>DataTable con datos del anuncio.</returns>
        public virtual DataTable getDataFromAnuncio()
        {
            DataTable datos = new DataTable();
            datos = cad.getDataFromAnuncio(this);
            return datos;
        }

        public bool leerPorPropiedad()
        {
            CAD_Anuncio cad = new CAD_Anuncio();
            return cad.leerPorPropiedad(this);
        }
    }
}