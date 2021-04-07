﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public static class DateTimeExtensions
    {
        public static long ToUnix(this DateTime time)
        {
            return ((DateTimeOffset)time).ToUnixTimeSeconds();
        }
    }
    public static class MathExtensions
    {
        public static double StandardDeviation(this IEnumerable<double> values)
        {
            double avg = values.Average();
            return Math.Sqrt(values.Average(v => Math.Pow(v - avg, 2)));
        }
    }
}
