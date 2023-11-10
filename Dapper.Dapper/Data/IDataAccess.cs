using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Database.Data
{
    public interface IDataAccess
    {
        Task<IEnumerable<T>> Get<T, P>(string Query, P parameter, string connectionId = "default");
        Task SaveData<P>(string Query, P parameter, string connectionId = "default");

        Task AddBulkData<T>(IEnumerable<T>data, string tableName, string parameterType, string connectionId = "default" );
    }
}
