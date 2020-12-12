using System.Collections.Generic;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;

namespace NgHive.Functions
{
    public static class GetVotes
    {
        [FunctionName("GetVotes")]
        public static IEnumerable<Vote> GetVotesFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "vote/{eventName}")] HttpRequest req,
            [CosmosDB(
                databaseName: "Votes",
                collectionName: "Votes",
                ConnectionStringSetting = "CosmosDBConnection",
                SqlQuery = "SELECT * FROM Votes where Votes.eventName = {eventName} order by Votes.id desc")]
                IEnumerable<Vote> votes)
        {
            return votes;
        }
    }
}
