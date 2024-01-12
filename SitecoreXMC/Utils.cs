using System;
using System.Globalization;

namespace Sitecore.XMC
{
    public static class Utils
    {
        public static DateTimeOffset GetDateTimeOffset(string isoDateTime)
        {
            return DateTimeOffset.ParseExact(isoDateTime, "yyyyMMdd'T'HHmmss'Z'", CultureInfo.InvariantCulture);
        }
    }
}