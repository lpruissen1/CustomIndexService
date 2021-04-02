using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AggregationService.Core
{
	public static class DateTimeExtensions
	{
		public static long ToUnix(this DateTime time)
		{
			return ((DateTimeOffset)time).ToUnixTimeSeconds();
		}
	}
}
