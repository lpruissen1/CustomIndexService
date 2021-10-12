using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeData.Model
{
	public class Communcation
	{
		public string T { get; set; }
		public string msg { get; set; }
	}
	public class MinuteBar
	{
		public string T { get; set; }
		public string S { get; set; }
		public decimal o { get; set; }
		public decimal h { get; set; }
		public decimal l { get; set; }
		public decimal c { get; set; }
		public decimal v { get; set; }
		public DateTime t { get; set; }
	}
}
