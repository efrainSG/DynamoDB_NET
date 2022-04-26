using System;

namespace MovieRank.Libs.Models
{
    public abstract class Constants
    {
        public const string RegionName = "us-east-2";

        public enum tableNames
        {
            MovieRanking001, // Movie name, Description, Ranking, Rank DateTime, Actors, User Id
            PhoneContacts, // name, phone, address
            Tasks, // date, title, description
            BudgetItems // name, amount
        };

        public enum tableIndexes
        {
            MovieName_index
        }

        public enum fieldNames
        {
            MovieName,
            Description,
            Ranking,
            RankDateTime,
            Actors,
            UserId,
            ContactName,
            PhoneNumber,
            Address,
            Birthday,
            Date,
            TaskTitle,
            ItemName,
            Amount
        };

        public static string ToEnumString(tableNames item) => Enum.GetName(typeof(tableNames), item);
        public static string ToEnumString(fieldNames item) => Enum.GetName(typeof(fieldNames), item);
        public static string ToEnumString(tableIndexes item) => (Enum.GetName(typeof(tableIndexes), item)).Replace("_", "_");
    }
}
