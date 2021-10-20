using System.Collections.Generic;


namespace Dominio
{
    public class cliente
    {
       
        public int ClienteId {get;set;}
        public string nombre {get;set;}
        public string apellido {get;set;}
        public string nombreUsuario {get;set;}
        public string nit {get;set;}
        public string correo {get;set;}
        public string contrasena {get;set;}
        public string tarjeta {get;set;}
        public string nombreTarjeta {get;set;}

         public ICollection<factura> facturausuario {get;set;} //relacion
    }
}