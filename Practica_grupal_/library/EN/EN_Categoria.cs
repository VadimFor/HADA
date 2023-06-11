using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library.CAD;
using System.Data;

namespace library.EN
{
    public class EN_Categoria
    {
        private string _tipo;
        private string _descripcion;

        public string tipo
        {
            get { return _tipo; }
            set { _tipo = value; }
        }

        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public EN_Categoria()
        {
            tipo = "";
            descripcion = "";
        }

        public EN_Categoria(string tipo, string descripcion)
        {
            this.tipo = tipo;
            this.descripcion = descripcion;
        }

        public DataSet leer_Categoria()
        {
            CAD_Categoria categoria = new CAD_Categoria();
            return categoria.leer_Categoria(this);
        }
    }
}
