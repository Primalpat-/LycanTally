using Ether.Outcomes;
using LycanTally.Core.Entities;
using System;
using System.Xml.Linq;

namespace LycanTally.Core.Threads
{
    public class ThreadReader
    {
        private readonly ThreadParser Parser;

        public ThreadReader(ThreadParser parser)
        {
            Parser = parser;
        }

        public IOutcome<Thread> Read(string threadUrl)
        {
            var xmlOutcome = GetXml(threadUrl);

            if (xmlOutcome.Failure)
                return Outcomes.Failure<Thread>()
                               .WithMessagesFrom(xmlOutcome);

            return GetThread(xmlOutcome.Value);
        }

        private IOutcome<XDocument> GetXml(string threadUrl)
        {
            XDocument doc;

            try
            {
                doc = XDocument.Load(threadUrl);
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

        private IOutcome<Thread> GetThread(XDocument doc)
        {
            Thread result = new Thread();

            try
            {
                result = Parser.Parse(doc);
            }
            catch (Exception ex)
            {
                Outcomes.Failure<Thread>()
                        .WithMessage("Error parsing XDoc")
                        .FromException(ex);
            }

            return Outcomes.Success<Thread>()
                           .WithValue(result);
        }
    }
}