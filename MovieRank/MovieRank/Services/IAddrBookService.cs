using MovieRank.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRank.Services
{
    public interface IAddrBookService
    {
        Task<IEnumerable<AddrBookResponse>> GetAllItems();
        Task<AddrBookResponse> GetPersonById(int personId);
        Task AddPerson(int personId, AddrBookRequest request);
        Task UpdatePerson(int personId, AddrBookUpdRequest request);
        Task DeletePerson(int personId);
    }
}
