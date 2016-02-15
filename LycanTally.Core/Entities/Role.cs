using System.Collections.Generic;

namespace LycanTally.Core.Entities
{
    public partial class Role
    {
        public Role()
        {
            this.User_Thread_Roles = new List<User_Thread_Roles>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Aliases { get; set; }
        public int AlignmentID { get; set; }
        public virtual Alignment Alignment { get; set; }
        public virtual ICollection<User_Thread_Roles> User_Thread_Roles { get; set; }
    }
}
