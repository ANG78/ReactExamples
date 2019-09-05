using Interfaces.BusinessLogic;
using System;

namespace Api.Common
{
    public interface IObserverEvent
    {
        void Start<T>(T pData, IStep<T> pStep);
        void Finish<T>(T pData, IStep<T> pStep, IResult res);
        void Exception<T>(T pData, IStep<T> pStep, Exception ex);
    }

}
