using System.Collections.Generic;

namespace MovieRank.Contracts
{
    public class AddrBookRequest
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Birthday { get; set; }
    }
}
