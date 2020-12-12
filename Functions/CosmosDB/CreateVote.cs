using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace NgHive.Functions
{
    public static class CreateVote
    {
        [FunctionName("CreateVote")]
        public static async Task CreateVoteFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "vote")] Vote vote,
            [CosmosDB(
                databaseName: "Votes",
                collectionName: "Votes",
                ConnectionStringSetting = "CosmosDBConnection")]IAsyncCollector<Vote> votes)
        {
            await votes.AddAsync(vote);
        }
    }
}
