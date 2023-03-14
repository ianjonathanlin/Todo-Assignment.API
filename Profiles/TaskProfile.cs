using AutoMapper;

namespace Todo_Assignment.API.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<Data.Entities.TaskEntity, Models.TaskModel>();
        }
    }
}
