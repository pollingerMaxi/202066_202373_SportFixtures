using System;
using SportFixtures.Data.Entities;

namespace SportFixtures.BusinessLogic.Interfaces
{
    public interface ISportBusinessLogic
    {
        bool UniqueName(string sportName);

        void AddSport(string name);
    }
}