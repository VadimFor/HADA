using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library.CAD;

namespace library.EN
{
    public class EN_Alquiler : EN_Anuncio
    {
        /// <summary>
        /// Fianza de una propiedad en alquiler.
        /// </summary>
        private float _fianza;
        /// <summary>
        /// Propiedad de _fianza.
        /// </summary>
        public float Fianza { 
            get { return _fianza; } 
            set { _fianza = value; }
        }

        public EN_Alquiler()
        {
            _fianza = 0;
        }
        /// <summary>
        /// Objeto CAD_Alquiler para llamar a los métodos de CAD_Alquiler.
        /// </summary>
        protected CAD_Alquiler cadAlq = new CAD_Alquiler();

        /// <summary>
        /// Constructor sobrecargado.
        /// </summary>
        /// <param name="id">Id del anuncio.</param>
        /// <param name="precio">Precio del anuncio.</param>
        /// <param name="desc">Descripción del anuncio.</param>
        /// <param name="fianza">Fianza del anuncio.</param>
        public EN_Alquiler(int id, float precio, string desc, int propiedad, string contacto, float fianza) : base(id, precio, desc, propiedad, contacto)
        {
            Fianza = fianza;
        }
        /// <summary>
        /// Guarda el anuncio en la BD.
        /// </summary>
        /// <returns>True si se ha completado la operación con éxito, false si no.</returns>
        public override bool createAnuncio()
        {
            if (!cadAlq.readAlquiler(this)) // Si el anuncio no existe ya, se crea.
            {
                return cadAlq.createAlquiler(this);
            }
            return false;
        }
        /// <summary>
        /// Recupera el anuncio indicado de la BD.
        /// </summary>
        /// <returns>True si se ha completado la operación con éxito, false si no.</returns>
        public override bool readAnuncio()
        {
            return cadAlq.readAnuncio(this);
        }
        /// <summary>
        /// Actualiza este anuncio en la BD.
        /// </summary>
        /// <returns>True si se ha completado la operación con éxito, false si no.</returns>
        public override bool updateAnuncio()
        {
            if (cadAlq.readAnuncio(this)) // Si el anuncio está en la base de datos, se puede actualizarlo.
            {
                return cadAlq.updateAnuncio(this);
            }
            return false;
        }
    }
}
