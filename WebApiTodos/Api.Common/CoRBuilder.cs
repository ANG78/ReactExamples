using Interfaces.BusinessLogic;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Common
{
 
    public class ChainOfResponsibilityBuilder
    {
        public static ListenerEventStep Listener { get; set; } = new ListenerEventStep();

        readonly IServiceProvider _service;
        public ChainOfResponsibilityBuilder(IServiceProvider service)
        {
            _service = service;
        }

        public IStep<T> Get<T>( params IStep<T>[] parameters)

        {
            IList<IStep<T>> listToProcess = parameters.ToList();

            if (Listener != null)
            {
                var listToProcess2 = listToProcess.Select(x => new DecoratorStep<T>(x)).ToList();

                foreach (var aux in listToProcess2)
                {
                    aux.ExceptionEvent += Listener.ExceptionEvent;
                    aux.FinishHandler += Listener.FinishHandler;
                    aux.StartEvent += Listener.StartHandler;
                }
                var listToProcess3 = listToProcess2.Select(x=> (IStep<T>) x).ToList();
                listToProcess = listToProcess3;
            }

            var result = listToProcess.First();
            listToProcess = listToProcess.Reverse().ToList();
            var current = listToProcess.FirstOrDefault();
            for (int i = 1; i < listToProcess.Count; i++)
            {
                listToProcess[i].Next = current;
                current = listToProcess[i];
            }
            return result;
        }

        public T GetService<T>()
        {
            return _service.GetRequiredService<T>();
        }
    }
}
