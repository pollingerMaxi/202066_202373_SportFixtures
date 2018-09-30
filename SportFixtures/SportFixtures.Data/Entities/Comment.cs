using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EncounterId { get; set; }
        public String Text { get; set; }
    }
}
