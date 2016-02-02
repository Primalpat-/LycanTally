using Ether.Outcomes;
using LycanTally.Core.Entities;
using System;
using System.Xml.Linq;

namespace LycanTally.Logic.Services.Users
{
    public class UserReader
    {
        private readonly UserParser Parser;

        public UserReader(UserParser parser)
        {
            Parser = parser;
        }

        public IOutcome<User> Read(string userUrl)
        {
            var xmlOutcome = GetXml(userUrl);

            if (xmlOutcome.Failure)
                return Outcomes.Failure<User>()
                               .WithMessagesFrom(xmlOutcome);

            return GetUser(xmlOutcome.Value);
        }

        private IOutcome<XDocument> GetXml(string userUrl)
        {
            XDocument doc;

            try
            {
                doc = XDocument.Load(userUrl);
            }
            catch(Exception ex)
            {
                return Outcomes.Failure<XDocument>()
                               .WithMessage("Error reading user")
                               .FromException(ex);
            }

            return Outcomes.Success<XDocument>()
                           .WithValue(doc);
        }

        private IOutcome<User> GetUser(XDocument doc)
        {
            User result = new User();

            try
            {
                result = Parser.Parse(doc);
            }
            catch(Exception ex)
            {
                return Outcomes.Failure<User>()
                               .WithMessage("Error parsing XDoc")
                               .FromException(ex);
            }

            return Outcomes.Success<User>()
                           .WithValue(result);
        }
    }
}
