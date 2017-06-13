using System.Collections.Generic;

namespace Tools.Helper.Enum
{
    public static class EnumHelper
    {
        public static IEnumerable<System.Enum> GetFlags(System.Enum input)
        {
            foreach (System.Enum value in System.Enum.GetValues(input.GetType()))
                if (input.HasFlag(value))
                    yield return value;
        }
    }
}
