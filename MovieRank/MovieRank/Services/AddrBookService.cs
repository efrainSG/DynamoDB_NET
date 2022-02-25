using MovieRank.Contracts;
using MovieRank.Libs.Mappers;
using MovieRank.Libs.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRank.Services
{
    public class AddrBookService : IAddrBookService
    {
        private readonly IAddrBookRepository repository;
        private readonly IAddrBookMapper map;

        public AddrBookService(IAddrBookRepository repository, IAddrBookMapper map)
        {
            this.repository = repository;
            this.map = map;
        }

        public async Task<IEnumerable<AddrBookResponse>> GetAllItems()
        {
            var response = await repository.GetAllItems();
            return map.ToAddrBookContract(response);
        }

        public async Task<AddrBookResponse> GetPersonById(int personId)
        {
            var response = await repository.GetPersonById(personId);
            return map.ToAddrBookContract(response);
        }

        public async Task AddPerson(int personId, AddrBookRequest request)
        {
            await repository.AddPerson(map.ToAddrBookModel(personId, request));
        }

        public async Task UpdatePerson(int personId, AddrBookUpdRequest request)
        {
            var req = await GetPersonById(personId);
            var _req = new AddrBookRequest
            {
                Address = req.Address,
                Birthday = req.Birthday,
                ContactId = req.ContactId,
                ContactName = req.ContactName,
                PhoneNumber = req.PhoneNumber
            };
            await repository.UpdatePerson(map.ToAddrBookModel(personId, _req, request));
        }

        public async Task DeletePerson(int personId)
        {
            await repository.DeletePerson(personId);
        }
    }
}
