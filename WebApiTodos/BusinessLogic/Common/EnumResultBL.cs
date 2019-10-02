using Interfaces.BusinessLogic;
using Interfaces.Common;
using Interfaces.ICommon;

namespace BusinessLogic.Common
{
    public enum EnumResultBL
    {
        [Description("OK")]
        OK = 0,
       
        [Description("Valid Status is requiered")]
        ERROR_VALID_STATUS_REQUIRED,
        [Description("Task is already set to status {0}")]
        ERROR_TARGET_STATUS_ALREADY_REACHED,

        [Description("status supplied not allowed on creation of tasks: {0}")]
        ERROR_STATUS_NOT_ALLOWED_ON_CREATION,

        [Description("Title is not found in the system {0}")]
        ERROR_CODE_NOT_EXIST,
        [Description("ERROR_ID_MUST_BE_GREATER_THAN_ZERO_IN_EDITIONS")]
        ERROR_ID_MUST_BE_GREATER_THAN_ZERO_IN_EDITIONS,
        [Description("ERROR_EXCEPTION_PERSISTENCE {0}")]
        ERROR_EXCEPTION_PERSISTENCE,
        [Description("ERROR_UNEXPECTED_EXCEPTION")]
        ERROR_UNEXPECTED_EXCEPTION,
        [Description("ERROR_UNEXPECTED_RESULT")]
        ERROR_UNEXPECTED_RESULT,

        [Description("Code Already Exists: {0}")]
        ERROR_CODE_ALREADY_EXIST,

        [Description("Title is Empty")]
        ERROR_TITLE_EMPTY,

        [Description("exceeded the number of characters for a Description: {0}")]
        ERROR_VALID_MAX_DEC,

        [Description("exceeded the number of characters for a Title {0}")]
        ERROR_VALID_MAX_TITLE,

        [Description("The user is not allowed this action")]
        ERROR_PERMISSION_VALIDATIONS
    }





}
