using Dapper.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Database.Repository
{
    public interface IPersonRepository
    {
        Task<bool> AddPerson(Person person);
        Task<bool> UpdatePerson(Person person);
        Task<bool> DeletePerson(int id);
        Task<Person> GetPersonById(int id);
        Task<IEnumerable<Person>> GetPersons();
    }
}
