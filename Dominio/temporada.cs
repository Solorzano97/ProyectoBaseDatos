using System.Collections.Generic;

namespace Dominio
{
    public class temporada
    {
        public int TemporadaId {get;set;}
        public string nombre {get;set;}

        public ICollection<indexTemporada> TemporadaBodehga {get;set;} //relacion
    }
}