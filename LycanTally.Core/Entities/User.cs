using System;
using System.Collections.Generic;

namespace LycanTally.Core.Entities
{
    public partial class User
    {
        public User()
        {
            this.Articles = new List<Article>();
            this.User_Thread_Roles = new List<User_Thread_Roles>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string TermsOfUse { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarLink { get; set; }
        public int YearRegistered { get; set; }
        public System.DateTime LastLogin { get; set; }
        public string StateOrProvince { get; set; }
        public string Country { get; set; }
        public string WebAddress { get; set; }
        public string XboxAccount { get; set; }
        public string WiiAccount { get; set; }
        public string PsnAccount { get; set; }
        public string BattleNetAccount { get; set; }
        public string SteamAccount { get; set; }
        public int TradeRating { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<User_Thread_Roles> User_Thread_Roles { get; set; }
    }
}
