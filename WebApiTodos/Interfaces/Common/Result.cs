using Interfaces.BusinessLogic;
using System.Collections.Generic;
using System.Linq;

namespace Interfaces.Common
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractResult<T> : IResultWithCodeEnum<T>
    {
        private T code;
        private object[] details;
        private string message;

        protected AbstractResult(T x, string messag, params object[] detail)
        {
            code = x;
            details = detail;
            message = messag;
        }

        public T Code
        {
            get
            {
                return code;
            }
        }

        public string Message(string separatorCode = "#")
        {
            if (details != null && details.Length >= 1)
            {
                return this.Code + separatorCode + " " + string.Format(message, details);
            }
            else
            {
                return this.Code + separatorCode + " " + (message);
            }

        }

        public string CodeAsString
        {
            get
            {
                object aux = (object)Code;
                return aux.ToString();
            }
        }

        public virtual EResult ComputeResult()
        {
            if ( string.IsNullOrWhiteSpace(CodeAsString) )
                return EResult.ERROR;

            if (CodeAsString.StartsWith("OK"))
                return EResult.OK;

            if (CodeAsString.StartsWith("ERROR"))
                return EResult.ERROR;

            if (CodeAsString.StartsWith("WARNING"))
                return EResult.WARNING;

            return EResult.ERROR;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public class ResultList : IResultContainer
    {
        public ResultList(IList<IResult> oResultDetails)
        {
            ResultDetails = oResultDetails;
        }

        public ResultList(IResult oResultDetails)
        {
            ResultDetails = new List<IResult>();
            ResultDetails.Add(oResultDetails);
        }

        public IList<IResult> ResultDetails { get; set; }


        public EResult ComputeResult()
        {
            if (ResultDetails == null || !ResultDetails.Any(x => x.ComputeResult().IsError()))
            {
                return EResult.OK;
            }

            return EResult.ERROR;
        }

        public string Message(string separatorCode = "#")
        {
            string message = "";
            if (ResultDetails == null || ResultDetails.Count == 0)
                return "Empty";

            foreach (var aux in ResultDetails)
            {
                if (message.Length > 0)
                    message += "\n";

                message += aux.Message(separatorCode);
            }

            return message;
        }

        public string CodeAsString
        {
            get
            {
                if (ResultDetails != null && ResultDetails.Count() == 1)
                {
                    var res1 = ResultDetails[0];
                    return res1.CodeAsString;
                }

                return ComputeResult().ToString();

            }

        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="X"></typeparam>
    public abstract class AbstractResultObject<T, X> : AbstractResult<T>, IResultObject<X>
    {
        private X item;

        public X GetItem()
        {
            return item;
        }

        public void SetItem(X value)
        {
            item = value;
        }

        public AbstractResultObject(T x, X oItem, string messag, params object[] detail) : base(x, messag, detail)
        {
            SetItem(oItem);
        }

        public AbstractResultObject(T x, string messag, params object[] detail) : base(x, messag, detail)
        {
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="X"></typeparam>
    public abstract class AbstractResultObjects<T, X> : 
            AbstractResult<T>, 
            IResultObjects<X> 
    {
        private IList<X> items;

        public IEnumerable<X> GetItems()
        {
            return items;
        }

        public void SetItems(IList<X> value)
        {
            items = value;
        }

        public AbstractResultObjects(T x, IList<X> oItems, string messag, params object[] detail) : base(x, messag, detail)
        {
            SetItems(oItems);
        }

        public AbstractResultObjects(T x, string messag, params object[] detail) : base(x, messag, detail)
        {
        }

    }
}
