using Dapper.Database.Data;
using Dapper.Database.Model;
using Dapper.Database.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Database.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly IDataAccess _db;
        private readonly IConfiguration _config;
        public BookRepository(IDataAccess db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public async Task<bool> Add(Book Book)
        {
            try
            {
                string Query = "Insert into dbo.Book(title,author,year) values(@title,@author,@year)";
                await _db.SaveData(Query,new {title=Book.Title,author=Book.Author,year=Book.Year});
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Update(Book Book)
        {
            try
            {
                string Query = "Update dbo.Book set title = @title , author= @author, year = @year where Id = @id";
                await _db.SaveData(Query, new { title = Book.Title, author = Book.Author, year = Book.Year ,Id=Book.Id});
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                string Query = "delete dbo.Book where Id = @Id";
                await _db.SaveData(Query, new { Id = id });
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Book> GetById(Guid id)
        {
            try
            {
                string Query = "select * from dbo.Book where id = @id";
                var getFirst = await _db.Get<Book,dynamic>(Query, new { id = id });
                Book obj = getFirst.FirstOrDefault();
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Book>> Get()
        {
            try
            {
                string Query = "select * from dbo.Book ";
                var getFirst = await _db.Get<Book, dynamic>(Query, new { });
                var List = getFirst.ToList();
                return List;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //_______ Insert Bulk Data _____
        //public async Task AddList(IEnumerable<Book> Books)
        //{
        //    using IDbConnection connection = new SqlConnection(_config.GetConnectionString("default"));
        //    var parameters = new DynamicParameters();
        //    parameters.Add("@typBook", ToDataTable(Books), DbType.Object, ParameterDirection.Input);
        //    await connection.ExecuteAsync("sp_AddBooks", parameters, commandType: CommandType.StoredProcedure);
        //}

        //private DataTable ToDataTable(IEnumerable<Book> obj)
        //{
        //    var table = new DataTable();
        //    table.Columns.Add("Title", typeof(string));
        //    table.Columns.Add("Author", typeof(string));
        //    table.Columns.Add("Year", typeof(int));
        //    foreach (var item in obj)
        //    {
        //        table.Rows.Add(item.Title, item.Author, item.Year);
        //    }
        //    return table;
        //}


        public async Task AddList(IEnumerable<Book> books)
        {
            try
            {
                //Select only those column which are defined in procedure
                var selectList = books.Select(x=> new {x.Title,x.Author,x.Year});
                await _db.AddBulkData(selectList, "Book" , "typBook");
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
