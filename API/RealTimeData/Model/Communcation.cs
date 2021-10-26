using System;

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
		public int n { get; set; }
		public decimal vw { get; set; }
	}
	public class Quote
	{
		public string T { get; set; }
		public string S { get; set; }
		public string bx { get; set; }
		public int bs { get; set; }
		public decimal bp { get; set; }
		public string ax { get; set; }
		public decimal ap { get; set; }
		public decimal As { get; set; }
		public int s { get; set; }
		public DateTime t { get; set; }
		public string[] c { get; set; }
		public string z { get; set; }
	}
	public class Trade
	{
		public string T { get; set; } // type
		public string S { get; set; } // symbol
		public string x { get; set; } // exchange code
		public double i { get; set; } // trade id
		public decimal p { get; set; } // price
		public int s { get; set; } // size
		public DateTime t { get; set; } // time
		public string[] c { get; set; } // trade condition
		public string z { get; set; } // tape
	}
}
