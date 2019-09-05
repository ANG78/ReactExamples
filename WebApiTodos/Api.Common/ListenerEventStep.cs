using Interfaces.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Common
{

    public class ListenerEventStep
    {
        private List<IObserverEvent> observers = new List<IObserverEvent>();

        public void Register(IObserverEvent observer)
        {
            observers.Add(observer);
        }

        public void Unregister(IObserverEvent observer)
        {
            observers.Remove(observer);
        }

        public  void StartHandler<T>(T pData, IStep<T> pStep)
        {

            observers.ForEach(x => x.Start(pData, pStep));


        }

        public  void FinishHandler<T>(T pData, IStep<T> pStep, IResult res)
        {

            observers.ForEach(x => x.Finish(pData, pStep, res));

        }


        public  void ExceptionEvent<T>(T pData, IStep<T> pStep, Exception ex)
        {
            observers.ForEach(x => x.Exception(pData, pStep, ex));

        }

    }
}
