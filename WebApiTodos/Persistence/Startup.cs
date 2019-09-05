using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Interfaces.Repository;
using Persistence.Repository;
using Interfaces.BusinessLogic;

namespace Persistence
{

    public partial class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureRepositoryServices(IServiceCollection services)
        {
            services.AddSingleton<TaskContext>();
            services.AddScoped<ITaskRepository, TaskRepository>();
        }

    }
}
