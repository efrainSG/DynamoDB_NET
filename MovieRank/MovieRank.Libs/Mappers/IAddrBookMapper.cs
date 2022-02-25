using Amazon.DynamoDBv2.Model;
using MovieRank.Contracts;
using MovieRank.Libs.Models;
using System.Collections.Generic;

namespace MovieRank.Libs.Mappers
{
    public interface IAddrBookMapper
    {
        IEnumerable<AddrBookResponse> ToAddrBookContract(IEnumerable<AddressBookDb> item);
        AddrBookResponse ToAddrBookContract(AddressBookDb addrBook);
        AddressBookDb ToAddrBookModel(int contactId, AddrBookRequest addrBookRequest);
        /// <summary>
        /// Updates except Contact Id and contact name.
        /// </summary>
        /// <param name="contactId"></param>
        /// <param name="addrBookRequest"></param>
        /// <param name="addrBookUpdRequest"></param>
        /// <returns></returns>
        AddressBookDb ToAddrBookModel(int contactId, AddrBookRequest addrBookRequest, AddrBookUpdRequest addrBookUpdRequest);
    }
}
