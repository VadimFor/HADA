using library.CAD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace library.EN
{
    public class EN_Propiedad
    {
        private int _id;
        private int _metros;
        private string _cod_postal;
        private string _img1;
        private string _img2;
        private string _img3;
        private string _direccion;

        private string _usuario;
        private string _categoria;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int Metros
        {
            get { return _metros; }
            set { _metros = value; }
        }

        public string Cod_postal
        {
            get { return _cod_postal; }
            set { _cod_postal = value; }
        }

        public string Img1
        {
            get { return _img1; }
            set { _img1 = value; }
        }
        public string Img2
        {
            get { return _img2; }
            set { _img2 = value; }
        }
        public string Img3
        {
            get { return _img3; }
            set { _img3 = value; }
        }

        public string Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        public string Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public string Categoria
        {
            get { return _categoria; }
            set { _categoria = value; }
        }

        public EN_Propiedad()
        {
            this.Metros = 0;
            this.Cod_postal = "0";
        }
        public EN_Propiedad(EN_Propiedad en)
        {
            this.Metros = en.Metros;
            this.Cod_postal = en.Cod_postal;
        }
        public EN_Propiedad(int metros, string codigoP)
        {
            this.Metros = metros;
            this.Cod_postal = codigoP;
        }

        public DataTable mostrarPropiedades()
        {
            CAD_Propiedad propiedad = new CAD_Propiedad();
            DataTable lista = propiedad.mostrarPropiedades();

            return lista;
        }

        public DataTable filtrarCategorias(string categoria)
        {
            CAD_Propiedad aux = new CAD_Propiedad();
            DataTable lista = aux.filtrarCategorias(categoria);

            return lista;
        }
        public bool nuevaPropiedad()
        {
            bool anyadida = false;

            if(!this.leerPropiedad())
            {
                CAD_Propiedad aux = new CAD_Propiedad();
                anyadida = aux.nuevaPropiedad(this);
            }

            return anyadida;
        }

        public bool leerPropiedad()
        {
            bool leida = false;

            CAD_Propiedad aux = new CAD_Propiedad();
            leida = aux.leerPropiedad(this);

            return leida;
        }

        public bool actualizarPropiedad()
        {
            bool actualizada = false;

            if (this.leerPropiedad())
            {
                CAD_Propiedad aux = new CAD_Propiedad();
                actualizada = aux.actualizarPropiedad(this);
            }
            
            return actualizada;
        }

        public bool eliminarPropiedad()
        {
            bool eliminada = false;

            if (this.leerPropiedad())
            {
                CAD_Propiedad aux = new CAD_Propiedad();
                eliminada = aux.eliminarPropiedad(this);
            }

            return eliminada;
        }

        /// <summary>
        /// Devuelve un dataset con todas las propiedades de la base de datos
        /// </summary>
        /// <returns></returns>
        public DataSet admin_getPropiedades()
        {
            CAD_Propiedad u = new CAD_Propiedad();
            return u.admin_getPropiedades();
        }

        public bool admin_modPropiedades(DataSet dataset, SqlCommand updateCommand)
        {
            CAD_Propiedad u = new CAD_Propiedad();
            return u.admin_modPropiedades(dataset, updateCommand);
        }

        public bool admin_deletePropiedades(DataSet dataset, SqlCommand deleteCommand)
        {
            CAD_Propiedad u = new CAD_Propiedad();
            return u.admin_deletePropiedades(dataset, deleteCommand);
        }

        public int obtenerId()
        {
            int idPublicar = -1;

            CAD_Propiedad aux = new CAD_Propiedad();
            idPublicar = int.Parse(aux.obtenerId(this).ToString());

            return idPublicar;
        }
        public DataTable mostrarPropiedadesDefault()
        {
            CAD_Propiedad propiedad = new CAD_Propiedad();
            DataTable lista = propiedad.mostrarPropiedadesDefault();

            return lista;
        }

    }
}
