using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.BusinessLogic.Implementations
{
    public class PositionTableLogic
    {
        private ISportBusinessLogic sportBL;
        private IEncounterBusinessLogic encounterBL;

        public PositionTableLogic(ISportBusinessLogic sportBL, IEncounterBusinessLogic encounterBL)
        {
            this.sportBL = sportBL;
            this.encounterBL = encounterBL;
        }

        public ICollection<Position> GeneratePositionTable(int sportId)
        {
            List<Position> positions = new List<>();

        }
    }
}
