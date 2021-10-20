using System.Collections.Generic;


namespace Dominio
{
    public class rol
    {
       
        public int RolId {get;set;}
        public string Descripcion {get;set;}

        public ICollection<empleado> empleadorol {get;set;} //relacion
    }
}