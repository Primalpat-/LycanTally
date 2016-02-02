using LycanTally.Core.Entities;
using System;
using System.Linq;
using System.Xml.Linq;

namespace LycanTally.Logic.Services.Users
{
    public class UserParser
    {
        public User Parse(XDocument doc)
        {
            User user = new User();

            user.ID = (int)doc.Element("user").Attribute("id");
            user.Name = doc.Element("user").Attribute("name").Value;
            user.TermsOfUse = doc.Element("user").Attribute("termsofuse").Value;
            user.FirstName = doc.Element("user").Element("firstname").Attribute("value").Value;
            user.LastName = doc.Element("user").Element("lastname").Attribute("value").Value;
            user.AvatarLink = doc.Element("user").Element("avatarlink").Attribute("value").Value;
            user.YearRegistered = (int)doc.Element("user").Element("yearregistered").Attribute("value");
            user.LastLogin = DateTime.Parse(doc.Element("user").Element("lastlogin").Attribute("value").Value,
                                            System.Globalization.CultureInfo.CurrentCulture,
                                            System.Globalization.DateTimeStyles.AdjustToUniversal);
            user.StateOrProvince = doc.Element("user").Element("stateorprovince").Attribute("value").Value;
            user.Country = doc.Element("user").Element("country").Attribute("value").Value;
            user.WebAddress = doc.Element("user").Element("webaddress").Attribute("value").Value;
            user.XboxAccount = doc.Element("user").Element("xboxaccount").Attribute("value").Value;
            user.WiiAccount = doc.Element("user").Element("wiiaccount").Attribute("value").Value;
            user.PsnAccount = doc.Element("user").Element("psnaccount").Attribute("value").Value;
            user.BattleNetAccount = doc.Element("user").Element("battlenetaccount").Attribute("value").Value;
            user.SteamAccount = doc.Element("user").Element("steamaccount").Attribute("value").Value;
            user.TradeRating = (int)doc.Element("user").Element("traderating").Attribute("value");

            return user;
        }
    }
}
