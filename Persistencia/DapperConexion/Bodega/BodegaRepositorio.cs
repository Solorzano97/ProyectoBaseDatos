using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace Persistencia.DapperConexion.Bodega
{
    public class BodegaRepositorio : ibodegas
    {
        private readonly IFactoryConnection _factoryConnection;

        public BodegaRepositorio(IFactoryConnection factoryConnection){
            _factoryConnection = factoryConnection;
        }


        public Task<int> Actualiza(BodegaModelo parametros)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> Nuevo(BodegaModelo parametros)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<BodegaModelo>> ObtenerLista()
        {
            IEnumerable<BodegaModelo> bodegaList = null;
            var storeProcedure = "usp_Obtener_Bodegas";
            try{
                var connection = _factoryConnection.GetConnection();
                bodegaList = await connection.QueryAsync<BodegaModelo>(storeProcedure,null,commandType : CommandType.StoredProcedure);

            }catch(Exception e){

                throw new Exception ("ERROR EN LA CONSULTA DE DATOS" , e);

            }finally{
                _factoryConnection.CloseConnection();

            }
            return bodegaList;
        }

        public Task<BodegaModelo> ObtenerPorId(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}