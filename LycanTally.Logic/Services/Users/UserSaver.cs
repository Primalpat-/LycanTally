using Ether.Outcomes;
using LycanTally.Core.Contexts;
using LycanTally.Core.Entities;
using System;
using System.Linq;
using Z.Core.Extensions;

namespace LycanTally.Logic.Services.Users
{
    public class UserSaver
    {
        private readonly ILycanTallyContext Db;
        private readonly UserReader Reader;

        public UserSaver(ILycanTallyContext db, UserReader reader)
        {
            Db = db;
            Reader = reader;
        }

        /// <summary>
        /// Interacts with the BGG API, and retrieves updated User data.  It then
        /// will update an existing user or save a new one.
        /// </summary>
        /// <param name="userName">Name of user to pull data for.</param>
        /// <param name="postDate">Date of the article looking for user.</param>
        /// <returns>An Outcome object containing a saved User which has been updated or created.</returns>
        public IOutcome<User> GetNewOrUpdatedUser(string userName, DateTime postDate)
        {
            User existingUser = Db.Users.Where(u => u.Name == userName &&
                                               u.LastLogin > postDate)
                                        .FirstOrDefault();

            if (existingUser.IsNotNull())
                return Outcomes.Success<User>()
                               .WithValue(existingUser);

            var userOutcome = Reader.Read(UserUrlBuilder.GetUserUrl(userName));

            if (userOutcome.Failure)
                return Outcomes.Failure<User>()
                               .WithMessagesFrom(userOutcome);

            return Outcomes.Success<User>()
                           .WithValue(CreateOrUpdateUser(userOutcome.Value));
        }
        
        private User CreateOrUpdateUser(User user)
        {
            var existingUser = Db.Users.Where(u => u.Name == user.Name)
                                       .FirstOrDefault();

            if (existingUser.IsNull())
            {
                Db.Users.Add(user);
                Db.SaveChanges();
                return user;
            }

            return UpdateUser(existingUser, user);
        }

        private User UpdateUser(User existingUser, User updatedUser)
        {
            existingUser.Name = updatedUser.Name;
            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.AvatarLink = updatedUser.AvatarLink;
            existingUser.LastLogin = updatedUser.LastLogin;
            existingUser.StateOrProvince = updatedUser.StateOrProvince;
            existingUser.Country = updatedUser.Country;
            existingUser.WebAddress = updatedUser.WebAddress;
            existingUser.XboxAccount = updatedUser.XboxAccount;
            existingUser.WiiAccount = updatedUser.WiiAccount;
            existingUser.PsnAccount = updatedUser.PsnAccount;
            existingUser.BattleNetAccount = updatedUser.BattleNetAccount;
            existingUser.SteamAccount = updatedUser.SteamAccount;
            existingUser.TradeRating = updatedUser.TradeRating;

            Db.SaveChanges();

            return existingUser;
        }
    }
}
