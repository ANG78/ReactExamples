using BusinessLogic.Common;
using Interfaces.BusinessLogic;
using Interfaces.BusinessLogic.Entities;
using Interfaces.BusinessLogic.Services;
using Interfaces.BusinessLogic.Services.Request;
using System.Linq;

namespace BusinessLogic.Steps.Common
{
   

    public class ValidateUserRequestPermission<T> : 
            StepTemplateGeneric<IManagementModelRequest<T>>
    {

        readonly PermisionValidate[] _permissions;
        IUserService _userService;
        public ValidateUserRequestPermission(IUserService userService, params PermisionValidate[] permisionValidates) : base(null)
        {
            _userService = userService;
            _permissions = permisionValidates;
        }

        public override string Description()
        {
            return "Permission Validation";
        }

        protected override IResult ExecuteTemplate(IManagementModelRequest<T> req)
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

            if (!_userService.Validate(req.User, covered.Permission)){
                return new Result(EnumResultBL.ERROR_PERMISSION_VALIDATIONS);
            }

            return Result.Ok;
        }
    }
}
