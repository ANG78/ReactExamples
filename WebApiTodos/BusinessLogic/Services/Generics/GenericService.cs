using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.Common;
using Interfaces.BusinessLogic;
using Interfaces.BusinessLogic.Entities;
using Interfaces.BusinessLogic.Services;
using Interfaces.Repository;

namespace BusinessLogic.Services.Generics
{
    public abstract class GenericService<T> : IGenericService<T> where T : IId, IExternalId
    {
        protected readonly IGenericRepository<T> _repository;
        public GenericService(IGenericRepository<T> workerRepository)
        {
            _repository = workerRepository;
        }
        
        public IResult Edit(T item)
        {
            try
            {
                if (item.Id == 0 )
                {
                    return new Result(EnumResultBL.ERROR_ID_MUST_BE_GREATER_THAN_ZERO_IN_EDITIONS, item.Id);
                }
                _repository.Edit(item);
            }
            catch (Exception ex)
            {
                return new Result(EnumResultBL.ERROR_EXCEPTION_PERSISTENCE, ex.Message);
            }
            return Result.Ok;
        }

        public IResult Add(T item)
        {

            try
            {
                var resultCodeInserted = _repository.GetAll(item.Code);
                if (resultCodeInserted.ToList().Count > 0) {
                    return new Result(EnumResultBL.ERROR_CODE_ALREADY_EXIST, item.Code);
                }


                _repository.Add(item);
            }
            catch (Exception ex)
            {
                return new Result(EnumResultBL.ERROR_EXCEPTION_PERSISTENCE, ex.Message);
            }
            return Result.Ok;
        }
  
        public IEnumerable<T> GetAll(string code)
        {
            return _repository.GetAll(code);
        }

    }


}
