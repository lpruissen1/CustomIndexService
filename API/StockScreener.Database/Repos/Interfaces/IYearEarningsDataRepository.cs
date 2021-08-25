﻿using Database.Core;
using StockScreener.Database.Model;
using System.Collections.Generic;

namespace StockScreener.Database.Repos.Interfaces
{
	public interface IYearEarningsDataRepository : IBaseRepository<YearEarningsData>
	{
		void Load(IEnumerable<YearEarningsData> info);
	}

	public interface IYearDividendDataRepository : IBaseRepository<YearDividendData>
	{
		void Load(IEnumerable<YearDividendData> info);
	}
}