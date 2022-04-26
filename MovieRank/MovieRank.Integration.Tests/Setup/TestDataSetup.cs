using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MovieRank.Integration.Tests.Setup {
    public class TestDataSetup {
        private static readonly IAmazonDynamoDB dynamoDBClient = new AmazonDynamoDBClient(new AmazonDynamoDBConfig {
            ServiceURL = "http://localhost:8000"
        });

        public async Task CreateTable() {
            var request = new CreateTableRequest {
                AttributeDefinitions = new List<AttributeDefinition>()
                {
                    new AttributeDefinition
                    {
                        AttributeName = "UserId",
                        AttributeType = "N"
                    },
                    new AttributeDefinition
                    {
                        AttributeName = "MovieName",
                        AttributeType = "S"
                    }
                },
                KeySchema = new List<KeySchemaElement>
                {
                    new KeySchemaElement
                    {
                        AttributeName = "UserId",
                        KeyType = "HASH"
                    },
                    new KeySchemaElement
                    {
                        AttributeName = "MovieName",
                        KeyType = "RANGE"
                    }
                },
                ProvisionedThroughput = new ProvisionedThroughput {
                    ReadCapacityUnits = 1,
                    WriteCapacityUnits = 1
                },
                TableName = "MovieRank",

                GlobalSecondaryIndexes = new List<GlobalSecondaryIndex>
                {
                    new GlobalSecondaryIndex
                    {
                        IndexName = "MovieName-index",
                        KeySchema = new List<KeySchemaElement>
                        {
                            new KeySchemaElement
                            {
                                AttributeName = "MovieName",
                                KeyType = "HASH"
                            }
                        },
                        ProvisionedThroughput = new ProvisionedThroughput
                        {
                            ReadCapacityUnits = 1,
                            WriteCapacityUnits = 1
                        },
                        Projection = new Projection
                        {
                            ProjectionType = "ALL"
                        }
                    }
                }

            };

            await dynamoDBClient.CreateTableAsync(request);
            await WaitUntilTableActive(request.TableName);
        }

        private static async Task WaitUntilTableActive(string tableName) {
            string status = null;
            do {
                Thread.Sleep(5000);
                try {
                    status = await GetTableStatus(tableName);
                } catch (ResourceNotFoundException) {

                    throw;
                }
            } while (status != "ACTIVE");
        }

        private static async Task<string> GetTableStatus(string tableName) {
            var response = await dynamoDBClient.DescribeTableAsync(new DescribeTableRequest {
                TableName = tableName
            });

            return response.Table.TableStatus;
        }
    }
}
