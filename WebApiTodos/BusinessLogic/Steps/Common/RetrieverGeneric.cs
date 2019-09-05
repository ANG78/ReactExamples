using BusinessLogic.Common;
using Interfaces.BusinessLogic;
using Interfaces.BusinessLogic.Entities;
using Interfaces.BusinessLogic.Services;
using Interfaces.BusinessLogic.Services.Request;

namespace BusinessLogic.Steps.Common
{
    
    public class RetrieverGeneric<TModel, TService> : 
        StepTemplateGeneric<IManagementModelRetrieverRequest<TModel>>,
        IStep<IManagementModelRetrieverRequest <TModel>>
                where TService : IGenericReadService<TModel>
                where TModel : IExternalId
    {
        TService _Service;

        public RetrieverGeneric(TService pService)
        {
            _Service = pService;
        }

        public override string Description()
        {
            return "Retrieving data..";
        }

        protected override IResult ExecuteTemplate(IManagementModelRetrieverRequest<TModel> obj)
        {
            obj.Items = _Service.GetAll(obj?.Code);
            return Result.Ok;
        }
    }


   
}
