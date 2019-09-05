using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces.BusinessLogic.Services
{

    public interface IGenericReadService<T>
    {        
        IEnumerable<T> GetAll(string code = null);
    }

    public interface IGenericWriteService<T>
    {
        IResult Add(T item);
        IResult Edit(T item);
    }

    public interface IGenericService<T> : IGenericReadService<T>, IGenericWriteService<T>
    {
    }




}
