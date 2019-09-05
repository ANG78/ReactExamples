using AutoMapper;
using Api.Controllers.ViewModels;
using Interfaces.BusinessLogic.Entities;
using Entities.BusinessLogic.Entities;

namespace BusinessLogic.Mappers
{
    public class WorkerProfile : Profile
    {
        public WorkerProfile()
        {
            CreateMap<ITask, TaskViewModel>()
                .ForMember(x => x.Status, op => op.MapFrom(sr => sr.Status.ToString()));
              

            CreateMap<TaskViewModel, ITask>().As<Task>();
            CreateMap<TaskViewModel, Task>()
                .ForMember(x => x.Status, op => op.MapFrom(sr =>  EnumStatusTask.Unexpected.Parser(sr.Status) )); 
        }
    }
 
}
