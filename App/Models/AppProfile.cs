using ApplicationCore.Entities.ProjectAggregate;
using ApplicationCore.Entities.UserAggregate;
using ApplicationCore.Entities.WorkAggregate;
using AutoMapper;
using System.Collections.Generic;

namespace App.Models
{
    public class AppProfile : Profile
    {
        public AppProfile()
        {
            CreateMap<CreateProjectRequestModel, Project>();
            CreateMap<Project, ProjectResponseModel>();
            CreateMap<UpdateProjectRequestModel, Project>();

            CreateMap<Work, WorkResponseModel>().ForMember( dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.ProjectName));
            //CreateMap<WorkResponseModel, Work>();
            CreateMap<UpdateWorkRequestModel, Work>();

            CreateMap<CreateUserRequestModel, User>();
            CreateMap<User, UserResponseModel>();
            CreateMap<UpdateUserRequestModel, User>();
            //CreateMap<UserResponseModel, User>();
        }
    }
}
