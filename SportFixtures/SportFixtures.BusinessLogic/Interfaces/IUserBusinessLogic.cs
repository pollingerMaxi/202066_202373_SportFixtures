using SportFixtures.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SportFixtures.BusinessLogic.Interfaces
{
    public interface IUserBusinessLogic
    {
        void AddUser(User user);
        void FollowTeam(User user, Team team);
        void Update(User user);
    }
}
