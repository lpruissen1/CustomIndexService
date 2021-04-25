using System;

namespace Core
{
    public static class DateTimeExtensions
    {
        public static double ToUnix(this DateTime time)
        {
            return ((DateTimeOffset)time).ToUnixTimeSeconds();
        }
    }
}
