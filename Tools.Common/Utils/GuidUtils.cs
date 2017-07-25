using System;

namespace Tools.Common.Utils
{
    public static class GuidUtils
    {
        public static Guid Copy(this Guid guidToCopy)
        {
            return new Guid(guidToCopy.ToString());
        }
    }
}
