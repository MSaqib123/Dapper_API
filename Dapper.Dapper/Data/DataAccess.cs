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

        //___ Bulk Insert ____
        public async Task AddBulkData<T>(IEnumerable<T> data, string tableName, string parameterType , string connectionId = "default" )
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            var parameters = new DynamicParameters();
            parameters.Add($"@{parameterType}", ToDataTable(data), DbType.Object, ParameterDirection.Input);
            await connection.ExecuteAsync($"sp_Add{tableName}s", parameters, commandType: CommandType.StoredProcedure);
        }

        private DataTable ToDataTable<T>(IEnumerable<T> obj)
        {
            var type = typeof(T);
            var properties = type.GetProperties();

            var table = new DataTable();

            foreach (var property in properties)
            {
                table.Columns.Add(property.Name, property.PropertyType);
            }

            foreach (var item in obj)
            {
                var values = properties.Select(p => p.GetValue(item)).ToArray();
                table.Rows.Add(values);
            }

            return table;
        }

    }
}
