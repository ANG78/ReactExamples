using Interfaces.BusinessLogic.Services.Request;

namespace BusinessLogic.Steps.Common
{
    public class ManagementModelRequest<T> : IManagementModelRequest<T>, IManagementPermission
    {
        public T Item { get; set; }
        public EnumOperation Type { get; set; }
        public string User { get; set; }
    }
}