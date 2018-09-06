using SportFixtures.Data.Access;
using SportFixtures.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Data.Repository
{
    public class UnitOfWork
    {
        private Context context;
        private GenericRepository<Team> teamRepository;

        public UnitOfWork(Context context)
        {
            this.context = context;
        }

        public GenericRepository<Team> TeamRepository
        {
            get
            {
                if (this.teamRepository == null)
                {
                    this.teamRepository = new GenericRepository<Team>(context);
                }
                return teamRepository;
            }
        }

    }
}
