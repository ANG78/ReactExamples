using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Steps.Common;
using Interfaces.BusinessLogic;
using Interfaces.BusinessLogic.Entities;
using Interfaces.BusinessLogic.Services.Request;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
//using System.Web.Http.Cors;

namespace Api.Controllers
{
    
   // [EnableCors(origins: "*", headers: "*", methods: "*")]
    public abstract class ControllerGenericBaseRead<TModel, TModelView, TModelViewGet> :
        ControllerBase
        where TModel : IId
        where TModelViewGet : new()
    {
        protected readonly IStep<IManagementModelRetrieverRequest<TModel>> _retrieverBusinessLogic;
        protected readonly IMapper _mapper;

        public ControllerGenericBaseRead(IMapper mapper,
                                         IStep<IManagementModelRetrieverRequest<TModel>> retrieverbusinessLogic)
        {
            _retrieverBusinessLogic = retrieverbusinessLogic;
            _mapper = mapper;
        }

        public string CurrentUser
        {
            get
            {
                return "Anonymous";
            }

        }


        [HttpGet]
        public ActionResult< IEnumerable<TModelViewGet>> Get()
        {
            var request = new ManagementModelRetrieverRequest<TModel> {
                User = CurrentUser
            };

            var result = _retrieverBusinessLogic.Execute(request);
            if (result.ComputeResult().IsOk())
            {
                return Ok(_mapper.Map<IEnumerable<TModelViewGet>>(request.Items));
            }

            return BadRequest(result);
            
        }

        [HttpGet("{code}", Name = "Get[controller]")]
        public ActionResult <TModelViewGet> Get(string code)
        {
            var request = new ManagementModelRetrieverRequest<TModel>
            {
                User = CurrentUser,
                Code = code
            };


            var result = _retrieverBusinessLogic.Execute(request);
            if (!result.ComputeResult().IsOk()
                ||
                request.Items?.Count() != 1)
            {

                return NotFound(result);
            }
            
            return _mapper.Map<TModelViewGet>(request.Items.FirstOrDefault());

        }
        
    }
    public abstract class ControllerGenericBaseFullREST<TModel, TModelView, TModelViewGet> :
            ControllerGenericBaseRead<TModel, TModelView, TModelViewGet>
        where TModel : IId
        where TModelViewGet : new()
    {
        protected readonly IStep<IManagementModelRequest<TModel>> _businessLogic;

        public ControllerGenericBaseFullREST(IMapper mapper,
                                     IStep<IManagementModelRetrieverRequest<TModel>> retrieverbusinessLogic,
                                     IStep<IManagementModelRequest<TModel>> businessLogic
                                   ) : base(mapper, retrieverbusinessLogic)
        {
            _businessLogic = businessLogic;

        }
       
        [HttpPost]
        public ActionResult<TModelViewGet> Post([FromBody] TModelView value)
        {

            var request = new ManagementModelRequest<TModel>
            {
                User = CurrentUser,
                Item = _mapper.Map<TModel>(value),
                Type = EnumOperation.NEW
            };

            var result = _businessLogic.Execute(request);

            if (result.ComputeResult().IsOk())
            {
                return Ok(_mapper.Map<TModelViewGet>(request.Item));
            }

            return BadRequest(result.Message());

        }

        [HttpPut("{id}")]
        public ActionResult<TModelViewGet> Put(int id, [FromBody] TModelView value)
        {
            var request = new ManagementModelRequest<TModel>
            {
                User = CurrentUser,
                Item = _mapper.Map<TModel>(value),
                Type = EnumOperation.EDITION
            };

            request.Item.Id = id;

            var result = _businessLogic.Execute(request);
            if (result.ComputeResult().IsOk())
            {
                return Ok(_mapper.Map<TModelViewGet>(request.Item));
            }

            return BadRequest(result.Message());

        }

    }

}

