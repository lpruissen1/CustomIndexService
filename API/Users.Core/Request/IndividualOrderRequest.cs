using Core;
using System;
using System.Collections.Generic;

namespace Users.Core.Request
{
	public class IndividualOrderRequest
	{
		public List<OrderEntry> Orders { get; set; } = new List<OrderEntry>();
		public OrderDirectionValue Direction { get; set; }
	}
}
