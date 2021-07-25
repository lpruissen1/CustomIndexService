using StockScreener.Database.Repos.Interfaces;
using System;
using System.Collections.Generic;

namespace StockInformation
{
	public class StockInformationService : IStockInformationService
	{
		private readonly ICompanyInfoRepository companyInfoRepository;

		public StockInformationService(ICompanyInfoRepository companyInfoRepository)
		{
			this.companyInfoRepository = companyInfoRepository;
		}

		public IEnumerable<string> GetAllTickers()
		{
			return companyInfoRepository.GetAllTickers();
		}
	}
}
