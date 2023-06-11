using library.CAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.EN
{
    class EN_Historial
    {
        private string _usuario;
        private string _contrato;
        private string _fecha;
        private int _id;

        public string usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public string contrato
        {
            get { return _contrato; }
            set { _contrato = value; }
        }

        public string fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public EN_Historial()
        {
            usuario = "";
            contrato = "";
            fecha = "";
            id = -1;
        }

        public EN_Historial(string usuario, string contrato, string fecha, int id)
        {
            this.usuario = usuario;
            this.contrato = contrato;
            this.fecha = fecha;
            this.id = id;
        }

        public bool leer_Historial()
        {
            CAD_Historial InmoData = new CAD_Historial();
            return InmoData.leer_Historial(this);
        }

        public bool borrarHistorial() 
        {
            CAD_Historial InmoData = new CAD_Historial();
            return InmoData.borrar_Historial(this);
        }
    }
}
