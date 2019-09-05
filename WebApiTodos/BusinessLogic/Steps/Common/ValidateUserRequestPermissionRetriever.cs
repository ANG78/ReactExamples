using BusinessLogic.Common;
using Interfaces.BusinessLogic;
using Interfaces.BusinessLogic.Entities;
using Interfaces.BusinessLogic.Services;
using Interfaces.BusinessLogic.Services.Request;
using System.Linq;

namespace BusinessLogic.Steps.Common
{

    public class ValidateUserRequestPermissionRetriever<T> :
            StepTemplateGeneric<IManagementModelRetrieverRequest<T>>
    {
        readonly PermisionValidate[] _permissions;
        IUserService _userService;
        public ValidateUserRequestPermissionRetriever(IUserService userService, params PermisionValidate[] permisionValidates) 
        {
            _userService = userService;
            _permissions = permisionValidates;
        }

        public override string Description()
        {
            return "Permission Validation";
        }

        protected override IResult ExecuteTemplate(IManagementModelRetrieverRequest<T> req)
        {

            if (_permissions == null || _permissions.Length == 0)
            {
                return Result.Ok;
            }

            PermisionValidate covered = _permissions.FirstOrDefault(x => req.Type == x.Operation);
            if (covered == null)
            {
                return Result.Ok;
            }

            _userService.Validate(req.User, covered.Permission);

            return Result.Ok;
        }
    }
}
