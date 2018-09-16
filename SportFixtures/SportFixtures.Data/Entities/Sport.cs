using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Data.Entities
{
    public class Sport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        public override bool Equals(object obj)
        {
            bool equals = false;

            if (obj != null && this.GetType() == obj.GetType())
            {
                Sport sport = (Sport)obj;
                equals = sport.Name.Equals(Name);
            }

            return equals;
        }

        public Sport()
        {
            this.IsDeleted = false;
        }

    }
}
