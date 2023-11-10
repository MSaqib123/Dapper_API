using Dapper.Database.Model;
using Dapper.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Database.Repository
{
    public interface IBookRepository
    {
        Task<bool> Add(Book book);
        Task<bool> Update(Book book);
        Task<bool> Delete(Guid id);
        Task<Book> GetById(Guid id);
        Task<IEnumerable<Book>> Get();
        //____ bulk Data ___
        Task AddList(IEnumerable<Book> books);
    }
}
