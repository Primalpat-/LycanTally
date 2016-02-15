namespace LycanTally.Core.Entities
{
    public partial class User_Thread_Roles
    {
        public int ID { get; set; }
        public int ThreadID { get; set; }
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public virtual Role Role { get; set; }
        public virtual Thread Thread { get; set; }
        public virtual User User { get; set; }
    }
}
