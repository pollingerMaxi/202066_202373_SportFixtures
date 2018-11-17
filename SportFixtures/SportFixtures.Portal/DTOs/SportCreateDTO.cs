using SportFixtures.Data;
using SportFixtures.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportFixtures.Portal.DTOs
{
    public class SportCreateDTO
    {
        public string Name { get; set; }
        public EncounterMode EncounterMode { get; set; }
    }
}
