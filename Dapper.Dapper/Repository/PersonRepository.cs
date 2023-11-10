using Dapper.Database.Data;
using Dapper.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Database.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IDataAccess _db;
        public PersonRepository(IDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> AddPerson(Person person)
        {
            try
            {
                string Query = "Insert into dbo.Person(name,email,address) values(@name,@email,@address)";
                await _db.SaveData(Query,new {name=person.Name,email=person.Email,address=person.Address});
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdatePerson(Person person)
        {
            try
            {
                string Query = "Update dbo.Person set name = @name ,email = @email, address = @address where id = @id";
                await _db.SaveData(Query, new {id=person.Id, name = person.Name, email = person.Email, address = person.Address });
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeletePerson(int id)
        {
            try
            {
                string Query = "delete dbo.Person where Id = @Id";
                await _db.SaveData(Query, new { Id = id });
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Person> GetPersonById(int id)
        {
            try
            {
                string Query = "select * from dbo.Person where id = @id";
                var getFirst = await _db.Get<Person,dynamic>(Query, new { id = id });
                Person obj = getFirst.FirstOrDefault();
                return obj;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Person>> GetPersons()
        {
            try
            {
                string Query = "select * from dbo.Person ";
                var getFirst = await _db.Get<Person, dynamic>(Query, new { });
                var List = getFirst.ToList();
                return List;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
