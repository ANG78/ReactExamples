using Interfaces.BusinessLogic.Entities;
using Interfaces.BusinessLogic.Services.Request;

namespace BusinessLogic.Steps.Common
{
    public class PermisionValidate
    {
        public EnumOperation Operation { get; private set; }
        public EnumPermission Permission { get; private set; }

        public PermisionValidate(EnumOperation operation, EnumPermission permission) {
            Operation = operation;
            Permission = permission;
        }
    }
}
