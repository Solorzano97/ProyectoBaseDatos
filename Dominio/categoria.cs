using System.Collections.Generic;


namespace Dominio
{
    public class categoria
    {
        
        public int CategoriaId {get; set;}
        public string NombreCategoria {get; set;}
        

        public ICollection<producto> productoLista{get;set;} //relacion
        
    }
}