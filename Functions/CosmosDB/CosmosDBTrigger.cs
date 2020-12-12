using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.SignalRService;

namespace NgHive.Functions
{
    public static class CosmosDBTrigger
    {
        [FunctionName("CosmosDBTrigger")]
        public static Task Run([CosmosDBTrigger(
            databaseName: "Votes",
            collectionName: "Votes",
            ConnectionStringSetting = "CosmosDBConnection",
            LeaseCollectionName = "VotesLeases",
            CreateLeaseCollectionIfNotExists = true)] IReadOnlyList<Document> votes,
            [SignalR(HubName = "votes")] IAsyncCollector<SignalRMessage> signalRMessages)
        {
            return signalRMessages.AddAsync(
                new SignalRMessage
                {
                    Target = "votesUpdated",
                    Arguments = new[] { votes }
                });
        }
    }
}