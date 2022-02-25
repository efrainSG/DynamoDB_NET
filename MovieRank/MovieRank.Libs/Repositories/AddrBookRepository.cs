using Amazon.DynamoDBv2.DataModel;
using MovieRank.Libs.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieRank.Libs.Repositories
{
    public class AddrBookRepository : IAddrBookRepository
    {
        private readonly DynamoDBContext context;

        public AddrBookRepository(DynamoDBContext context)
        {
            this.context = context;
        }

        public async Task AddPerson(AddressBookDb addressBook)
        {
            await context.SaveAsync(addressBook);
        }

        public async  Task DeletePerson(int personId)
        {
            await context.DeleteAsync(personId);
        }

        public async Task<IEnumerable<AddressBookDb>> GetAllItems()
        {
            return await context.ScanAsync<AddressBookDb>(new List<ScanCondition>()).GetRemainingAsync();
        }

        public async Task<AddressBookDb> GetPersonById(int personId)
        {
            return await context.LoadAsync<AddressBookDb>(personId);
        }

        public async Task UpdatePerson(AddressBookDb addressBook)
        {
            await context.SaveAsync(addressBook);
        }
    }
}
