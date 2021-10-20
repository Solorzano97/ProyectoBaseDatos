using System.Collections.Generic;


namespace Dominio
{
    public class producto
    {
        
        public int ProductoId {get; set;}
        
        public string nombre {get; set;}
        public string descripcion {get; set;}
        public string imagen {get; set;}
        public double precio {get; set;}
        public double descuento {get; set;}
        public int CategoriaId {get; set;}


        public categoria categoria{get;set;}//ancla

        public ICollection<existencias> existenciasproducto {get;set;} //relacion

         public ICollection<detalle> Detalleproducto {get;set;} //relacion

          public ICollection<movimientos> movimientosproducto {get;set;} //relacion

         public ICollection<indexTemporada> indextemp {get;set;} //relacion

           

        
    }
}