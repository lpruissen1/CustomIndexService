using System;

namespace Core
{
    public static class DateTimeExtensions
    {
        public static long ToUnix(this DateTime time)
        {
            return ((DateTimeOffset)time).ToUnixTimeSeconds();
        }
    }
}
