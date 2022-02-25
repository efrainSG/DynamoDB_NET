using MovieRank.Contracts;
using MovieRank.Libs.Models;
using System.Collections.Generic;
using System.Linq;

namespace MovieRank.Libs.Mappers
{
    public class AddrBookMapper : IAddrBookMapper
    {
        public IEnumerable<AddrBookResponse> ToAddrBookContract(IEnumerable<AddressBookDb> items)
        {
            return items.Select(ToAddrBookContract);
        }

        public AddrBookResponse ToAddrBookContract(AddressBookDb addrBook)
        {
            return new AddrBookResponse
            {
                Address = addrBook.Address,
                Birthday = addrBook.Birthday,
                ContactId = addrBook.ContactId,
                ContactName = addrBook.ContactName,
                PhoneNumber = addrBook.PhoneNumber
            };
        }

        public AddressBookDb ToAddrBookModel(int contactId, AddrBookRequest addrBookRequest)
        {
            return new AddressBookDb
            {
                Address = addrBookRequest.Address,
                PhoneNumber = addrBookRequest.PhoneNumber,
                ContactName = addrBookRequest.ContactName,
                ContactId = contactId,
                Birthday = addrBookRequest.Birthday
            };
        }

        public AddressBookDb ToAddrBookModel(int contactId, AddrBookRequest addrBookRequest, AddrBookUpdRequest addrBookUpdRequest)
        {
            return new AddressBookDb
            {
                Birthday = addrBookUpdRequest.Birthday,
                ContactId = contactId,
                ContactName = addrBookRequest.ContactName,
                PhoneNumber = addrBookUpdRequest.PhoneNumber,
                Address = addrBookUpdRequest.Address
            };
        }
    }
}
