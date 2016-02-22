using LycanTally.Logic.Services.Feeds;
using LycanTally.Logic.Services.Roles;
using Microsoft.AspNet.SignalR;
using System.Linq;

namespace LycanTally.Logic.SignalR
{
    public class ProgressHub : Hub
    {
        private readonly FeedUrlBuilder UrlBuilder;
        private readonly FeedReader FeedReader;
        private readonly FeedSaver FeedSaver;
        private readonly RoleSavingService RoleSaver;

        public ProgressHub(FeedUrlBuilder urlBuilder, FeedReader feedReader, FeedSaver feedSaver, RoleSavingService roleSaver)
        {
            UrlBuilder = urlBuilder;
            FeedReader = feedReader;
            FeedSaver = feedSaver;
            RoleSaver = roleSaver;
        }

        public static void SendMessage(string connectionID, string msg, int count)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ProgressHub>();
            hubContext.Clients.Client(connectionID).sendMessage(string.Format(msg + " ({0}%)", count), count);
        }

        public void UpdateThreadFromApi(string connectionID, int threadID)
        {
            Clients.Caller.sendMessage("Gathering Townsfolk... (0%)", 0);
            string feedUrl = UrlBuilder.GetFeedUrl(threadID);
            Clients.Caller.sendMessage("Crowd Gathered. (6%)", 6);

            Clients.Caller.sendMessage("Sharpening Pitchforks... (6%)", 6);
            var feedOutcome = FeedReader.ReadNewOrUpdatedFeed(connectionID, feedUrl);
            
            if (feedOutcome.Failure)
            {
                Clients.Caller.sendMessage("Unable to read feed from API.", 100);
                return;
            } 
            else
                Clients.Caller.sendMessage("Pitchforks sufficiently sharpened. (56%)", 56);

            Clients.Caller.sendMessage("Constructing Gallows... (56%)", 56);
            var saveOutcome = FeedSaver.Save(connectionID, feedOutcome.Value);

            if (saveOutcome.Failure)
            {
                Clients.Caller.sendMessage("Unable to save feed to database.", 100);
                return;
            }   
            else
                Clients.Caller.sendMessage("This'll hafta do. (95%)", 95);

            Clients.Caller.sendMessage("Hunting for werewolves... (95%)", 95);
            RoleSaver.SaveUsersRoles(saveOutcome.Value.AsQueryable());
            Clients.Caller.sendMessage("Time for lynching! (100%)", 100);
        }
    }
}