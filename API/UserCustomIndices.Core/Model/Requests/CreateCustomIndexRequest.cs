﻿using Core;
using System.Collections.Generic;

namespace UserCustomIndices.Core.Model.Requests
{
	public class CreateCustomIndexRequest
	{
		public string UserId { get; set; }
		public List<string> Markets { get; set; }
		public List<string> Sectors { get; init; } = new List<string>();
		public List<string> Industries { get; init; } = new List<string>();
		public List<TimedRangeRule> TimedRangeRule { get; init; } = new List<TimedRangeRule>();
		public List<RangedRule> RangedRule { get; init; } = new List<RangedRule>();
	}
}
