using Microsoft.Extensions.DependencyInjection;
using BusinessLogic.Services;
using Interfaces.BusinessLogic.Services.Request;
using AutoMapper;
using System.Reflection;
using Interfaces.BusinessLogic.Services;
using Interfaces.BusinessLogic.Entities;
using Microsoft.Extensions.Configuration;
using BusinessLogic.Steps.Common;
using BusinessLogic.Steps;

namespace Api.Common
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }


        

        public void ConfigureRepositoryServices(IServiceCollection services)
        {

            services.AddAutoMapper(Assembly.Load("Api.Common"));

            new Persistence.Startup(Configuration).ConfigureRepositoryServices(services);


            // add Services
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IUserService, UserService>();

            /*IManagementModelRetrieverRequest<ITask>*/
            services.AddScoped(sp=>
                     new ChainOfResponsibilityBuilder(sp).
                        Get(
                                  new ValidateUserRequestPermissionRetriever<ITask>(sp.GetService<IUserService>(),  
                                                                                        new PermisionValidate(EnumOperation.READ, EnumPermission.READ_TASK)),
                                  new RetrieverGeneric<ITask, ITaskService> (sp.GetService<ITaskService>())
            ));


            // IManagementModelRequest<ITask>
            services.AddScoped(sp =>
                    new ChainOfResponsibilityBuilder(sp).
                        Get (
                                new ValidateUserRequestPermission<ITask>(sp.GetService<IUserService>(),
                                                                                new PermisionValidate(EnumOperation.NEW,EnumPermission.CREATE_TASK),
                                                                                new PermisionValidate(EnumOperation.EDITION, EnumPermission.UPDATE_TASK)),
                                new ValidateStatusOnCreation(),
                                new ValidateToggleStatus(sp.GetService<ITaskService>()),
                                new ValidateAndCompleteTask( 20, 100),
                                new StepSaveModel<ITask>(sp.GetService<ITaskService>())
                            ));

        }

    }
}
