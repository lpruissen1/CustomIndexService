using StockScreener.Database.Model.StockFinancials;

namespace StockScreener.Service.IntegrationTests.StockDataHelpers
{
    public static class StockFinancialsCreator
    {
        public static StockFinancials GetStockFinancials(string ticker)
        {
            return new StockFinancials { Ticker = ticker };
        }

        public static StockFinancials AddCurrentRatio(this StockFinancials stockFinancials, double value, double timestamp = 0)
        {
            stockFinancials.CurrentRatio.Add(new CurrentRatio { currentRatio = value, timestamp = timestamp });
            return stockFinancials;
        }

        public static StockFinancials AddDebtToEquityRatio(this StockFinancials stockFinancials, double value, double timestamp = 0)
        {
            stockFinancials.DebtToEquityRatio.Add(new DebtToEquityRatio { debtToEquityRatio = value, timestamp = timestamp });
            return stockFinancials;
        }

        public static StockFinancials AddFreeCashFlow(this StockFinancials stockFinancials, double value, double timestamp = 0)
        {
            stockFinancials.FreeCashFlow.Add(new FreeCashFlow { freeCashFlow = value, timestamp = timestamp });
            return stockFinancials;
        }

        public static StockFinancials AddGrossMargin(this StockFinancials stockFinancials, double value, double timestamp = 0)
        {
            stockFinancials.GrossMargin.Add(new GrossMargin { grossMargin = value, timestamp = timestamp });
            return stockFinancials;
        }

        public static StockFinancials AddMarketCap(this StockFinancials stockFinancials, double value, double timestamp = 0)
        {
            stockFinancials.MarketCap.Add(new MarketCap { marketCap = value, timestamp = timestamp });
            return stockFinancials;
        }

        public static StockFinancials AddPayoutRatio(this StockFinancials stockFinancials, double value, double timestamp = 0)
        {
            stockFinancials.PayoutRatio.Add(new PayoutRatio { payoutRatio = value, timestamp = timestamp });
            return stockFinancials;
        }

        public static StockFinancials AddProfitMargin(this StockFinancials stockFinancials, double value, double timestamp = 0)
        {
            stockFinancials.ProfitMargin.Add(new ProfitMargin { profitMargin = value, timestamp = timestamp });
            return stockFinancials;
        }

        public static StockFinancials AddRevenue(this StockFinancials stockFinancials, double value, double timestamp = 0)
        {
            stockFinancials.Revenues.Add(new Revenues { revenues = value, timestamp = timestamp });
            return stockFinancials;
        }

        public static StockFinancials AddWorkingCapital(this StockFinancials stockFinancials, double value, double timestamp = 0)
        {
            stockFinancials.WorkingCapital.Add(new WorkingCapital { workingCapital = value, timestamp = timestamp });
            return stockFinancials;
        }

        public static StockFinancials AddEarningsPerShare(this StockFinancials stockFinancials, double value, double timestamp = 0)
        {
            stockFinancials.EarningsPerShare.Add(new EarningsPerShare { earningsPerShare = value, timestamp = timestamp });
            return stockFinancials;
        }

        public static StockFinancials AddDividendsPerShare(this StockFinancials stockFinancials, double value, double timestamp = 0)
        {
            stockFinancials.DividendsPerShare.Add(new DividendsPerShare { dividendsPerShare = value, timestamp = timestamp });
            return stockFinancials;
        }

        public static StockFinancials AddBookValuePerShare(this StockFinancials stockFinancials, double value, double timestamp = 0)
        {
            stockFinancials.BookValuePerShare.Add(new BookValuePerShare { bookValuePerShare = value, timestamp = timestamp });
            return stockFinancials;
        }

        public static StockFinancials AddSalesPerShare(this StockFinancials stockFinancials, double value, double timestamp = 0)
        {
            stockFinancials.SalesPerShare.Add(new SalesPerShare { salesPerShare = value, timestamp = timestamp });
            return stockFinancials;
        }
    }
}
