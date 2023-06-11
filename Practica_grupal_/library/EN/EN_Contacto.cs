using library.CAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library.EN
{
    public class EN_Contacto
    {
        private string _correo;
        private string _nombre;
        private string _apellido;
        private string _telefono;
        private string _foto;

        public string Foto
        {
            get { return _foto; }
            set { _foto = value; }
        }

        public string Correo
        {
            get { return _correo; }
            set { _correo = value; }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string Apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

        public EN_Contacto()
        {
            CAD_Contacto aux = new CAD_Contacto();
            this.Correo = "";
            this.Nombre = "";
            this.Apellido = "";
            this.Telefono = "";
        }

        public EN_Contacto(EN_Contacto en)
        {
            this.Correo = en.Correo;
            this.Nombre = en.Nombre;
            this.Apellido = en.Apellido;
            this.Telefono = en.Telefono;
        }
        public EN_Contacto(string correo, string nombre, string apellidos, string telef)
        {
            this.Correo = correo;
            this.Nombre = nombre;
            this.Apellido = apellidos;
            this.Telefono = telef;
        }

        public bool crearContacto()
        {

            CAD_Contacto aux = new CAD_Contacto();
            return aux.crearContacto(this);

        }

        public bool leerContacto()
        {
            bool leido = false;

            CAD_Contacto aux = new CAD_Contacto();
            leido = aux.leerContacto(this);

            return leido;
        }

        public bool actualizarContacto()
        {
            bool actualizado = false;

            if(this.leerContacto())
            {
                CAD_Contacto aux = new CAD_Contacto();
                actualizado = aux.actualizarContacto(this);
            }

            return actualizado;
        }

        public bool eliminarContacto()
        {
            bool eliminado = false;

            if(this.leerContacto())
            {
                CAD_Contacto aux = new CAD_Contacto();
                eliminado = aux.eliminarContacto(this);
            }

            return eliminado;
        }
    }
}
