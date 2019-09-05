using System;
using System.Collections.Generic;

namespace Interfaces.BusinessLogic
{

    public interface IStep<T>
    {
        IStep<T> Next { get; set; }
        IResult Execute(T concept);
        string Description();
    }

    public interface ICompositorSteps<Y>
    {
        IStep<Y> Steps { get; }
    }

    public interface IDecoratorStep<Z>
    {
        IStep<Z> DecoratedStep { get; }
    }
}
 