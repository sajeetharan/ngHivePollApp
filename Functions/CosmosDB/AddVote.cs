using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

namespace NgHive.Functions
{
    public static class AddVote
    {
        [FunctionName("AddVote")]
        public static IActionResult AddVoteFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "vote/{eventName}/{voteId}/{side}")] HttpRequest req,
            [CosmosDB(databaseName: "Votes", collectionName: "Votes", Id = "{voteId}", PartitionKey = "{eventName}", ConnectionStringSetting = "CosmosDBConnection")] Vote voteIn,
            [CosmosDB(databaseName: "Votes", collectionName: "Votes", ConnectionStringSetting = "CosmosDBConnection")] out Vote voteOut,
            string side)
        {
            voteOut = voteIn;
            if (side == "left")
            {
                voteOut.leftVotes++;
            }
            else
            {
                voteOut.rightVotes++;
            }
            return new OkResult();
        }
    }
}
