using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportFixtures.Portal.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EncounterId { get; set; }
        public String Text { get; set; }
    }
}
