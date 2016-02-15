using System.Collections.Generic;

namespace LycanTally.Core.Entities
{
    public partial class Alignment
    {
        public Alignment()
        {
            this.Roles = new List<Role>();
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
    }
}
