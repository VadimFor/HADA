using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library.CAD;

namespace library.EN
{
    class EN_Contrato
    {

        private string _usu_vendedor;
        private string _usu_comprador;
        private int _propiedad;
        private int _id;

        public string usu_vendedor
        {
            get { return _usu_vendedor; }
            set { _usu_vendedor = value; }
        }

        public string usu_comprador
        {
            get { return _usu_comprador; }
            set { _usu_comprador = value; }
        }

        public string propiedad
        {
            get { return _propiedad; }
            set { _fecha = _propiedad; }
        }

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public EN_Contrato()
        {
            usu_vendedor = "";
            usu_comprador = "";
            propiedad = -1;
            id = -1;
        }

        public EN_Contrato(string usu_vendedor, string usu_comprador, int propiedad, int id)
        {
            this.usu_vendedor = usu_vendedor;
            this.usu_comprador = usu_comprador;
            this.propiedad = propiedad;
            this.id = id;
        }

        public bool leer_Contrato()
        {
            CAD_Contrato InmoData = new CAD_Contrato();
            return InmoData.leer_Contrato(this);
        }

        public bool borrarContrato()
        {
            CAD_Contrato InmoData = new CAD_Contrato();
            return InmoData.borrar_Contrato(this);
        }
    }
}
