using Amazon.DynamoDBv2;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace MovieRank.Integration.Tests.Setup {
    public class CustomWebApplicationFactory<TStratup> : WebApplicationFactory<TStratup> where TStratup : class {
        protected override void ConfigureWebHost(IWebHostBuilder builder) {
            builder.ConfigureTestServices(Services =>
            Services.AddSingleton<IAmazonDynamoDB>(cc => {
                var clientConfig = new AmazonDynamoDBConfig {
                    ServiceURL = "http://localhost:8000"
                };
                return new AmazonDynamoDBClient(clientConfig); 
            })
            );
        }
    }
}
