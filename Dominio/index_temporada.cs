namespace Dominio
{
    public class indexTemporada
    {
        public int IndexTemporadaId {get;set;}
        public int TemporadaId {get;set;}
        public int ProductoId {get;set;}

        public temporada temporada {get;set;}
        public producto producto {get;set;}
    
    }
}