using MovieRank.Libs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRank.Libs.Repositories
{
    public interface IAddrBookRepository
    {
        Task<IEnumerable<AddressBookDb>> GetAllItems();
        Task<AddressBookDb> GetPersonById(int personId);
        Task AddPerson(AddressBookDb addressBook);
        Task UpdatePerson(AddressBookDb addressBook);
        Task DeletePerson(int personId);
    }
}
