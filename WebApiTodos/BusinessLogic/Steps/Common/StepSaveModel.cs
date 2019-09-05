using BusinessLogic.Common;
using Interfaces.BusinessLogic;
using Interfaces.BusinessLogic.Services;
using Interfaces.BusinessLogic.Services.Request;

namespace BusinessLogic.Steps.Common
{


    public class StepSaveModel<T> : StepTemplateGeneric< IManagementModelRequest<T> >
    {
        readonly IGenericService<T> _service;

        public StepSaveModel(IGenericService<T> service) : base(null)
        {
            _service = service;
        }

        public override string Description()
        {
            return "Saving Model in Repositories.";
        }

        protected override IResult ExecuteTemplate( IManagementModelRequest<T> obj)
        {
            if (obj.Type == EnumOperation.NEW)
            {
               return _service.Add(obj.Item);
            }
            else if (obj.Type == EnumOperation.EDITION)
            {
                return _service.Edit(obj.Item);
            }


            return Result.Ok;
        }
    }
}
