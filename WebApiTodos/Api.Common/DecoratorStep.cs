using Interfaces.BusinessLogic;
using System;

namespace Api.Common
{
    public delegate void EventHandler<T>(T Data, IStep<T> Step);
    public delegate void EventResultHandler<T>(T Data, IStep<T> Step, IResult res);
    public delegate void EventExceptionHandler<T>(T Data, IStep<T> Step, Exception ex);

    public class DecoratorStep<T> : IStep<T>, IDecoratorStep<T>
    {
        public EventHandler<T> StartEvent;
        public EventResultHandler<T> FinishHandler;
        public EventExceptionHandler<T> ExceptionEvent;

        public IStep<T> DecoratedStep { get; private set; }
        public DecoratorStep(IStep<T> pNext)
        {
            DecoratedStep = pNext;
        }


        public IStep<T> Next {
            get
            {
                return DecoratedStep?.Next;
            }

            set
            {
                if (DecoratedStep != null)
                    DecoratedStep.Next = value;
            }
        }

        public string Description()
        {
            return DecoratedStep?.Description();
        }

        public IResult Execute(T concept)
        {
            IResult Result = null;


            try
            {
                StartEvent?.Invoke(concept, this);
                Result = DecoratedStep?.Execute(concept);
                FinishHandler?.Invoke(concept, this, Result);
            }
            catch (Exception ex)
            {
                ExceptionEvent?.Invoke(concept, this, ex);
                throw ex;
            }

            return Result;

        }
    }
}
