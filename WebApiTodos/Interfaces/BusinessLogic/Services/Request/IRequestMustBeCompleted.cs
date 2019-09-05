using System;

namespace Interfaces.BusinessLogic.Services.Request
{
    public interface IRequestMustBeCompleted
    {
    }


    public static class IRequestMustBeCompletedExtension
    {
        public static T HelperCast<T>(this IRequestMustBeCompleted ob, object value) where T :class
        {

            if (!(value is IManagementModelRequest))
            {
                return null;
            }

            Type t = value.GetType();
            if (t.IsGenericType)
            {
                var ItemValue = t.GetProperty("Item")?.GetValue(value);
                if (ItemValue != null && ItemValue is T)
                {
                    return ItemValue as T;
                }
            }

            return null;
        }
    }


}
