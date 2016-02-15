using LycanTally.Core.Contexts;
using LycanTally.Core.Entities;
using LycanTally.Logic.Extensions.Strings;
using System.Collections.Generic;
using System.Linq;
using Z.Core.Extensions;

namespace LycanTally.Logic.Services.Roles
{
    public class RoleSavingService
    {
        private readonly ILycanTallyContext Db;

        public RoleSavingService(ILycanTallyContext db)
        {
            Db = db;
        }

        public void SaveUsersRoles(IQueryable<Article> articles)
        {
            List<Article> deaths = articles.Where(a => a.Body.Contains("[killed ") &&
                                                  a.Subject != "bobtally help")
                                           .ToList();

            foreach(Article deathArticle in deaths)
            {
                string articleBody = deathArticle.Body.ToLower();
                List<int> killIndexes = articleBody.AllIndexesOf("[killed ");

                foreach(int startOfKillIndex in killIndexes)
                {
                    int userID = FindUserIdInString(articleBody, startOfKillIndex);
                    int roleID = FindUserRoleIdInString(articleBody, startOfKillIndex, killIndexes);

                    if (userID > 0 && roleID > 0)
                        SaveUserRole(deathArticle.ThreadID, userID, roleID);
                }

                //when we figure out who died, we will have to figure out the role they were in
            }
        }

        private int FindUserIdInString(string articleBody, int startOfKillIndex)
        {
            int endOfKillIndex = articleBody.IndexOf("]", (startOfKillIndex + 8));
            string userName = articleBody.Substring((startOfKillIndex + 8), endOfKillIndex - (startOfKillIndex + 8));

            User user = Db.Users.FirstOrDefault(u => u.Name.ToLower() == userName);

            if (user.IsNull())
                return 0;

            return user.ID;
        }

        private int FindUserRoleIdInString(string articleBody, int startOfRoleSearchIndex, List<int> killIndexes)
        {
            string textToSearch;
            int nextIndex = killIndexes.IndexOf(startOfRoleSearchIndex) + 1;

            if (killIndexes.Count() == 1)
                textToSearch = articleBody;
            else if (nextIndex == killIndexes.Count())
                textToSearch = articleBody.Substring(startOfRoleSearchIndex);
            else
                textToSearch = articleBody.Substring(startOfRoleSearchIndex, killIndexes[nextIndex] - startOfRoleSearchIndex);

            foreach (Role role in Db.Roles.ToList())
            {
                if (textToSearch.Contains(role.Name.ToLower()))
                    return role.ID;

                if (role.Aliases.IsNotNullOrWhiteSpace())
                {
                    List<string> aliases = role.Aliases.Split(',').ToList();

                    foreach (string alias in aliases)
                    {
                        if (textToSearch.Contains(alias.ToLower()))
                            return role.ID;
                    }
                }
            }

            return 0;
        }

        private void SaveUserRole(int threadID, int userID, int roleID)
        {
            User_Thread_Roles oldRole = Db.User_Thread_Roles.FirstOrDefault(utr => utr.ThreadID == threadID &&
                                                                            utr.UserID == userID);

            if (oldRole.IsNull())
            {
                User_Thread_Roles newRole = new User_Thread_Roles();
                newRole.ThreadID = threadID;
                newRole.UserID = userID;
                newRole.RoleID = roleID;

                Db.User_Thread_Roles.Add(newRole);
            }
            else
                oldRole.RoleID = roleID;


            Db.SaveChanges();
        }
    }
}
