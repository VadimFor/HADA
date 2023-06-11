using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library.CAD;

namespace library.EN
{
    public class EN_Administrador : EN_Usuario
    {
        private DateTime _datetime;
        private TimeSpan _time;
        private string _ip;
        private int _mañana = 0;
        private int _tarde = 0;
        private int _noche = 0;

        public int mañana
        {
            get { return _mañana; }
            set { _mañana = value; }
        }

        public int tarde
        {
            get { return _tarde; }
            set { _tarde = value; }
        }

        public int noche
        {
            get { return _noche; }
            set { _noche = value; }
        }

        public DateTime datetime
        {
            get { return _datetime; }
            set { _datetime = value; }
        }

        public TimeSpan time
        {
            get { return _time; }
            set { _time = value; }
        }

        public string ip
        {
            get { return _ip; }
            set { _ip = value; }
        }
        public EN_Administrador() : base() { }

        public bool nuevaSesion()
        {
            CAD_Administrador u = new CAD_Administrador();
            return u.nuevaSesion(this);
        }

        public bool recuperarEstadisticas()
        {
            CAD_Administrador u = new CAD_Administrador();
            return u.recuperarEstadisticas(this);
        }

        public int getVisitasAlmacenadas()
        {
            CAD_Administrador u = new CAD_Administrador();
            return u.getVisitasAlmacenadas();
        }
    }
}