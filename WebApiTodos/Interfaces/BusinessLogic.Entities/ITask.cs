namespace Interfaces.BusinessLogic.Entities
{

    public interface ITask :  IId, IExternalId
    {
        string Title { get; set; }
        string Description { get; set; }
        EnumStatusTask Status { get; }

    }
    
}
