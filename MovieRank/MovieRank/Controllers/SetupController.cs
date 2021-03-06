using Microsoft.AspNetCore.Mvc;
using MovieRank.Services;
using System.Threading.Tasks;

namespace MovieRank.Controllers
{
    [Route("setup")]
    public class SetupController : Controller
    {
        private readonly ISetupService setupService;

        public SetupController(ISetupService setupService)
        {
            this.setupService = setupService;
        }

        [HttpPost, Route("createtable/{dynamoDbtableName}")]
        public async Task<IActionResult> CreateDynamoDbTable(string dynamoDbtableName)
        {
            await setupService.CreateDynamoDbTable(dynamoDbtableName);

            return Ok();
        }

        [HttpDelete, Route("deletetable/{dynamoDbTableName}")]
        public async Task<IActionResult> DeleteTable(string dynamoDbTableName)
        {
            await setupService.DeleteDynamoDbTable(dynamoDbTableName);

            return Ok();
        }
    }
}
