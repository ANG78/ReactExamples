
using Interfaces.BusinessLogic.Entities;

namespace Entities.BusinessLogic.Entities
{
    public partial class Task : ITask, IExternalId
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public EnumStatusTask Status { get; set; }
        public string Code { get { return Title; } }

    }

}
