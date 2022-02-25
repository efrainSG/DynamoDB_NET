using Amazon.DynamoDBv2.DataModel;

namespace MovieRank.Libs.Models
{
    [DynamoDBTable("PhoneContacts")]
    public class AddressBookDb
    {
        [DynamoDBHashKey]
        public int ContactId { get; set; }

        [DynamoDBGlobalSecondaryIndexHashKey]
        public string ContactName { get; set; }

        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Birthday { get; set; }
    }
}
