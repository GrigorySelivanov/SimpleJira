using AutoMapper;
using Data.DTOs.TaskDTOs;
using Data.DTOs.UserDTOs;
using Data.Models;

namespace GazpromTestProject.Mapping
{
    class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TaskModel, GetTaskDTO>();
            CreateMap<TaskModel, GetTaskShortDTO>();

            CreateMap<User, UserShortDTO>();
            CreateMap<User, LoginResponseDTO>();
        }
    }
}
