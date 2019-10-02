using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Api.Controllers.ViewModels;
using Interfaces.BusinessLogic;
using Interfaces.BusinessLogic.Entities;
using Interfaces.BusinessLogic.Services.Request;
//using System.Web.Http.Cors;

namespace Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TaskController : ControllerGenericBaseFullREST<ITask, TaskViewModel, TaskViewModel>
    {

        public TaskController(IMapper mapper,
                                IStep<IManagementModelRequest<ITask>> businessLogic,
                                IStep<IManagementModelRetrieverRequest<ITask>> businessRetrieverLogic
                               ) : base(mapper, businessRetrieverLogic, businessLogic)
        {

        }

    }

    
}
