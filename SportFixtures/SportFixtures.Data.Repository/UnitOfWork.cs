using SportFixtures.Data.Access;
using SportFixtures.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private Context context;
        private GenericRepository<Team> teamRepository;
        private GenericRepository<Sport> sportRepository;

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

        public GenericRepository<Sport> SportRepository
        {
            get
            {
                if (this.sportRepository == null)
                {
                    this.sportRepository = new GenericRepository<Sport>(context);
                }
                return sportRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

    }
}
