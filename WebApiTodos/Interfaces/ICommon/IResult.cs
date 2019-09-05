using System.Collections.Generic;

namespace Interfaces.BusinessLogic
{
    public enum EResult
    {
        OK,
        ERROR,
        WARNING
    }
    
    public static class ExtensionResult
    {
        public static bool IsOk(this EResult e)
        {
            return e == EResult.OK || e == EResult.WARNING;
        }

        public static bool IsWarning(this EResult e)
        {
            return e == EResult.WARNING;
        }

        public static bool IsError(this EResult e)
        {
            return e == EResult.ERROR;
        }
    }



    public interface IResult
    {
        EResult ComputeResult();
        string Message(string separatorCode = "#");
        string CodeAsString { get; }

    }

  
    /// <summary>
    /// this interface must be defined by classes whose result Code is bounded by an Enum
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResultWithCodeEnum<T> : IResult 
    {
        T Code { get; }
    }

    /// <summary>
    /// this interface must be defined by classes whose result Code is bounded by an Enum and also must return a list of objects
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IResultObjects<X> : IResult
    {
        IEnumerable<X> GetItems();
    }

    public interface IResultObject<X> : IResult
    {
        X GetItem();
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IResultContainer : IResult
    {
        IList<IResult> ResultDetails { get; }
    }
}
