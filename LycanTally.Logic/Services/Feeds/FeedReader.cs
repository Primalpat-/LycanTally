using Ether.Outcomes;
using LycanTally.Core.Entities;
using LycanTally.Logic.SignalR;
using System;
using System.Xml.Linq;

namespace LycanTally.Logic.Services.Feeds
{
    public class FeedReader
    {
        private readonly FeedParser Parser;

        public FeedReader(FeedParser parser)
        {
            Parser = parser;
        }

        /// <summary>
        /// Reads the BGG API, which will contain either a new thread or an updated one.
        /// </summary>
        /// <param name="feedUrl">URL to access the BGG API</param>
        /// <returns>Returns an Outcome object with a Thread value.</returns>
        public IOutcome<Thread> ReadNewOrUpdatedFeed(string connectionID, string feedUrl)
        {
            var xmlOutcome = GetXml(feedUrl);

            if (xmlOutcome.Failure)
                return Outcomes.Failure<Thread>()
                               .WithMessagesFrom(xmlOutcome);

            ProgressHub.SendMessage(connectionID, "Sharpening Pitchforks...", 10);
            return GetThread(connectionID, xmlOutcome.Value);
        }

        private IOutcome<XDocument> GetXml(string feedUrl)
        {
            XDocument doc;
            try
            {
                doc = XDocument.Load(feedUrl);
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<XDocument>()
                               .WithMessage("Error reading thread")
                               .FromException(ex);
            }

            return Outcomes.Success<XDocument>()
                           .WithValue(doc);
        }

        private IOutcome<Thread> GetThread(string connectionID, XDocument doc)
        {
            Thread result = new Thread();

            try
            {
                result = Parser.Parse(connectionID, doc);
            }
            catch (Exception ex)
            {
                return Outcomes.Failure<Thread>()
                               .WithMessage("Error parsing XDoc")
                               .FromException(ex);
            }

            return Outcomes.Success<Thread>()
                           .WithValue(result);
        }
    }
}