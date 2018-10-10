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

            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<User, LoginDTO>()
                .ForMember(e => e.Email, src => src.MapFrom(u => u.Email))
                .ForMember(e => e.Password, src => src.MapFrom(u => u.Password));
            CreateMap<LoginDTO, User>()
                .ForMember(e => e.Email, src => src.MapFrom(u => u.Email))
                .ForMember(e => e.Password, src => src.MapFrom(u => u.Password));

            CreateMap<Encounter, EncounterDTO>()
                .ForMember(e => e.Home, src => src.MapFrom(u => u.Team1))
                .ForMember(e => e.Visitor, src => src.MapFrom(u => u.Team2));
            CreateMap<EncounterDTO, Encounter>()
                .ForMember(e => e.Team1, src => src.MapFrom(u => u.Home))
                .ForMember(e => e.Team2, src => src.MapFrom(u => u.Visitor));
        }
    }
}
