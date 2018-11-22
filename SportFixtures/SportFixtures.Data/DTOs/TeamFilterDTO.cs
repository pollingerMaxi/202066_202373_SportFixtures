using SportFixtures.Data.Entities;
using SportFixtures.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportFixtures.Data;

namespace SportFixtures.Data.DTOs
{
    public class TeamFilterDTO
    {
        public string Name { get; set; }
        public Order? Order { get; set; }
    }
}