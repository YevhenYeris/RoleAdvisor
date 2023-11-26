using AutoMapper;
using RoleAdvisor.API.Models;
using RoleAdvisor.API.Models.Recommendations;
using RoleAdvisor.Domain.Entities;

namespace RoleAdvisor.API.Automapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Employee, EmployeeModel>().ReverseMap();
        CreateMap<Role, RoleModel>().ReverseMap();
        CreateMap<Skill, SkillModel>().ReverseMap();
        CreateMap<Project, ProjectModel>().ReverseMap();
        CreateMap<Position, PositionModel>().ReverseMap();
        CreateMap<Project, ProjectRecommendationsModel>().ReverseMap();
        CreateMap<Position, PositionRecommendationsModel>().ReverseMap();
    }
}
