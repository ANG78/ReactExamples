using System.Collections.Generic;

namespace Interfaces.BusinessLogic.Services.Request
{
    
    public enum EnumOperation
    {
        READ,
        NEW,
        EDITION, 
        DELETE
    }
    

    public interface IManagementPermission
    {
        string User { get; set; }
    }

    public interface IManagementModelRequest : IRequestMustBeCompleted, IManagementPermission
    {
        EnumOperation Type { get; set; }
    }

    public interface IManagementModelRequest<T> : IManagementModelRequest, IManagementPermission
    {
        T Item { get; set; }
    }

    public interface IManagementModelRetrieverRequest<T> : IManagementModelRequest
    {
        string Code { get; set; }
        IEnumerable<T>  Items { get; set; }
    }

}
