using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library.CAD;

namespace library.EN
{
    /// <summary>
    /// NOTA:  Un usuario solo puede ponerse como administrador tocando la base de datos, es decir
    /// no es posible registrarse desde la página web como administrador.
    /// </summary>
    public class EN_Usuario
    {
        private string _nombre;
        private string _apellido;
        private string _nif;
        private string _email;
        private string _telefono;
        private string _clave;
        private bool _esAdmin;
        private string _hash;
        private bool _verificado;

        public string hash
        {
            get { return _hash; }
            set { _hash = value; }
        }

        public bool verificado
        {
            get { return _verificado; }
            set { _verificado = value; }
        }

        public string nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }

        public string apellido
        {
            get { return _apellido; }
            set { _apellido = value; }
        }

        public string nif
        {
            get { return _nif; }
            set { _nif = value; }
        }

        public string email
        {
            get { return _email; }
            set { _email = value; }
        }

        public string telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }
        public string clave
        {
            get { return _clave; }
            set { _clave = value; }
        }

        public bool esAdmin
        {
            get { return _esAdmin; }
            set { _esAdmin = value; }
        }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public EN_Usuario()
        {
            nombre = "";
            nif = "";
            email = "";
            telefono = "";
            esAdmin = false;
        }

        /// <summary>
        ///  Constructor usado para cuando el usuario intente iniciar sesión (existeUsuario())
        /// </summary>
        /// <param name="email_"> el email puesto en el textbox de iniciar sesión</param>
        /// <param name="contraseña_">la contraseña puesta en el textbox de iniciar sesión</param>
        public EN_Usuario(string email_, string contraseña_)
        {
            clave = contraseña_;
            email = email_;
        }

        /// <summary>
        ///  Constructor usado para cuando se registra alguien
        /// </summary>
        /// <param name="email_"> el email puesto en el textbox de iniciar sesión</param>
        /// <param name="contraseña_">la contraseña puesta en el textbox de iniciar sesión</param>
        /// <param name="nombre_">el nombre puesto en el textbox de iniciar sesión</param>
        /// <param name="nif_">el nombre puesto en el textbox de iniciar sesión</param>
        /// <param name="telefono_">el nombre puesto en el textbox de iniciar sesión</param>

        public EN_Usuario(string email_, string contraseña_, string nombre_, string nif_, string telefono_, string apellido_ )
        {
            nombre = nombre_;
            apellido = apellido_;
            nif = nif_;
            email = email_;
            telefono = telefono_;
            clave = contraseña_;
            esAdmin = false;
        }

        /// <summary>
        /// Método que comprueba si el usuario existe: busca en la base de datos si hay algún usuario con ese email & contraseña.
        /// </summary>
        /// <returns>devuelve true si el email & contraseña coinciden con alguna fila de la base de datos.</returns>
        public bool existeUsuario()
        {
            CAD_Usuario u = new CAD_Usuario();
            return u.existeUsuario(this);
        }

        /// <summary>
        /// Método que crea el usuario.
        /// </summary>
        /// <returns>devuelve true si se ha creado el usuario correctamente.</returns>
        public bool crearUsuario()
        {
            CAD_Usuario u = new CAD_Usuario();
            return u.crearUsuario(this);
        }

        /// <summary>
        /// Método para actualizar información del usuario
        /// </summary>
        /// <returns>devuelve true si se ha actualizado el usuario correctamente.</returns>
        public bool actualizarUsuario()
        {
            CAD_Usuario u = new CAD_Usuario();
            return u.actualizarUsuario(this);
        }

        /// <summary>
        /// Método que se usa en el panel de usuario.
        /// Borrar al usuario de la base de datos.
        /// </summary>
        /// <returns>devuelve true si se ha podido borrar correctamente.</returns>
        public bool borrarUsuario()
        {
            CAD_Usuario u = new CAD_Usuario();
            return u.borrarUsuario(this);
        }

        /// <summary>
        /// Devuelve un dataset con todos los usuarios de la base de datos
        /// </summary>
        /// <returns></returns>
        public DataSet admin_getUsuarios()
        {
            CAD_Usuario u = new CAD_Usuario();
            return u.admin_getUsuarios();
        }

        public bool admin_modUsuarios(DataSet dataset, SqlCommand updateCommand)
        {
            CAD_Usuario u = new CAD_Usuario();
            return u.admin_modUsuarios(dataset,updateCommand);
        }

        public bool admin_deleteUsuario(DataSet dataset, SqlCommand deleteCommand)
        {
            CAD_Usuario u = new CAD_Usuario();
            return u.admin_deleteUsuario(dataset, deleteCommand);
        }

        public bool perfilUsuario_getUsuario(EN_Usuario en)
        {
            CAD_Usuario u = new CAD_Usuario();
            return u.perfilUsuario_getUsuario(en);
        }

        public bool verificarUsuario(EN_Usuario en)
        {
            CAD_Usuario u = new CAD_Usuario();
            return u.verificarUsuario(this);
        }

    }
}
