using System;

namespace Interfaces.BusinessLogic.Entities
{
    public enum EnumStatusTask
    {
        Unexpected = -1,
        Completed,
        Pending,
    }

    public static class EnumStatusTaskExtension
    {
        public static bool IsOk(this EnumStatusTask e )
        {
            return !EnumStatusTask.Unexpected.Equals(e);
        }

        public static EnumStatusTask Parser(this EnumStatusTask e, string stringToParser )
        {
            stringToParser = stringToParser?.Trim().ToUpper();
            if (string.IsNullOrEmpty(stringToParser)) {
                return EnumStatusTask.Unexpected;
            }

            EnumStatusTask resParser;
            if (Enum.TryParse( stringToParser, true, out resParser)) {
                return resParser;
            }

            return EnumStatusTask.Unexpected;
        }
    }

}
