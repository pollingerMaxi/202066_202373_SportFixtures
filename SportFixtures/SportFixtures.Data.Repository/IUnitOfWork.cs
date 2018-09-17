using System;
using System.Collections.Generic;
using System.Text;
using SportFixtures.Data.Entities;

namespace SportFixtures.Data.Repository
{
    public interface IUnitOfWork
    {
        GenericRepository<Sport> SportRepository { get; }
        GenericRepository<Team> TeamRepository { get; }

        void Save();
    }
}
