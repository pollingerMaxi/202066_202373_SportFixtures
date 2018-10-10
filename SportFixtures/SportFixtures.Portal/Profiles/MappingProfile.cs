using AutoMapper;
using SportFixtures.Data.Entities;
using SportFixtures.Portal.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportFixtures.Portal.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Team, TeamDTO>();
            CreateMap<TeamDTO, Team>();

            CreateMap<Sport, SportDTO>();
            CreateMap<SportDTO, Sport>();
        }
    }
}
