using System.Collections.Generic;
using Interfaces.BusinessLogic.Services.Request;

namespace BusinessLogic.Steps.Common
{
    public class ManagementModelRetrieverRequest<T> : IManagementModelRetrieverRequest<T>, IManagementPermission
    {
        public string Code { get; set; }
        public IEnumerable<T> Items { get; set; }
        public string User { get; set; }
        public EnumOperation Type { get; set; }
    }
}