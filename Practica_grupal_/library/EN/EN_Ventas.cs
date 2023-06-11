using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library.CAD;

namespace library.EN
{
    public class EN_Venta : EN_Anuncio
    {
        /// <summary>
        /// Hipoteca de una propiedad en venta.
        /// </summary>
        private float _precioPorM;
        /// <summary>
        /// Propiedad de _hipoteca.
        /// </summary>
        public float PrecioPorM
        {
            get { return _precioPorM; }
            set { _precioPorM = value; }
        }
        /// <summary>
        /// Objeto CAD_Venta para llamar a los métodos de CAD_Venta.
        /// </summary>
        protected CAD_Venta cadVenta = new CAD_Venta();

        public EN_Venta()
        {
            _precioPorM = 0;
        }

        /// <summary>
        /// Constructor sobrecargado.
        /// </summary>
        /// <param name="id">Id del anuncio.</param>
        /// <param name="precio">Precio del anuncio.</param>
        /// <param name="desc">Descripción del anuncio.</param>
        /// <param name="hipoteca">Hipoteca del anuncio.</param>
        public EN_Venta(int id, float precio, string desc, int propiedad, string contacto, float precioPorM) : base(id, precio, desc, propiedad, contacto)
        {
            PrecioPorM = precioPorM;
        }
        /// <summary>
        /// Guarda el anuncio en la BD.
        /// </summary>
        /// <returns>True si se ha completado la operación con éxito, false si no.</returns>
        public override bool createAnuncio()
        {
            if (!cadVenta.readAnuncio(this)) // Si el anuncio no existe ya, se crea.
            {
                return cadVenta.createVenta(this);
            }
            return false;
        }
        /// <summary>
        /// Recupera el anuncio indicado de la BD.
        /// </summary>
        /// <returns>True si se ha completado la operación con éxito, false si no.</returns>
        public override bool readAnuncio()
        {
            return cadVenta.readVenta(this);
        }
        /// <summary>
        /// Actualiza este anuncio en la BD.
        /// </summary>
        /// <returns>True si se ha completado la operación con éxito, false si no.</returns>
        public override bool updateAnuncio()
        {
            if (cadVenta.readAnuncio(this)) // Si el anuncio está en la base de datos, se puede actualizarlo.
            {
                return cadVenta.updateVenta(this);
            }
            return false;
        }
    }
}
