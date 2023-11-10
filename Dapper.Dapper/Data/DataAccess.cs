using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Database.Data
{
    public class DataAccess:IDataAccess
    {
        private readonly IConfiguration _config;
        public DataAccess(IConfiguration config)
        {
            _config = config;
        }

        //___ return List of  T ______
        public async Task<IEnumerable<T>> Get<T, P>(string Query,P parameter,string connectionId = "default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            return await connection.QueryAsync<T>(Query,parameter);
        }

        //___ return no data ______ Save Data into database
        public async Task SaveData<P>(string Query,P parameter,string connectionId = "default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            await connection.ExecuteAsync(Query,parameter);
        }
    }
}
