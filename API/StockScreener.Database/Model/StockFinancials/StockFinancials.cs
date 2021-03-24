using Database.Core;
using System.Collections.Generic;

namespace StockScreener.Database.Model.StockFinancials
{
    public class StockFinancials : StockDbEntity
    {
        public StockFinancials()
        {
            MarketCap = new List<MarketCap>();
            DividendsPerShare = new List<DividendsPerShare>();
            EarningsPerShare = new List<EarningsPerShare>();
            SalesPerShare = new List<SalesPerShare>();
            BookValuePerShare = new List<BookValuePerShare>();
            PayoutRatio = new List<PayoutRatio>();
            CurrentRatio = new List<CurrentRatio>();
            DebtToEquityRatio = new List<DebtToEquityRatio>();
            EnterpriseValue = new List<EnterpriseValue>();
            EBITDA = new List<EBITDA>();
            FreeCashFlow = new List<FreeCashFlow>();
            GrossMargin = new List<GrossMargin>();
            ProfitMargin = new List<ProfitMargin>();
            Revenues = new List<Revenues>();
            WorkingCapital = new List<WorkingCapital>();
        }
        /// <summary>
        /// Database entry representing the total value of the company based on share price and total shares.
        /// </summary>
        public List<MarketCap> MarketCap { get; init; }

        /// <summary>
        /// Database entry representing dividends paid out per common share of a company
        /// </summary>
        public List<DividendsPerShare> DividendsPerShare { get; init; }

        /// <summary>
        /// Database entry representing earnings per common share of a company
        /// </summary>
        public List<EarningsPerShare> EarningsPerShare { get; init; }

        /// <summary>
        /// Database entry representing revenue earned per common share of a company
        /// </summary>
        public List<SalesPerShare> SalesPerShare { get; init; }

        /// <summary>
        /// Database entry representing net asset value (total assets - total liabilities) per share 
        /// </summary>
        public List<BookValuePerShare> BookValuePerShare { get; init; }

        /// <summary>
        /// Database entry representing proportion of company earnings paid out in dividends
        /// </summary>
        public List<PayoutRatio> PayoutRatio { get; init; }

        /// <summary>
        /// Database entry representing the ratio of current assets to current liabilities
        /// </summary>
        public List<CurrentRatio> CurrentRatio { get; init; }

        /// <summary>
        /// Database entry representing total liabilities divided by total shareholder equity
        /// </summary>
        public List<DebtToEquityRatio> DebtToEquityRatio { get; set; }

        /// <summary>
        /// Database entry representing market capitalization plus total debt, minus cash
        /// </summary>
        public List<EnterpriseValue> EnterpriseValue { get; init; }

        /// <summary>
        /// Database entry representing a companies earnings before interest, taxes, depreciation, and amortization
        /// </summary>
        public List<EBITDA> EBITDA { get; init; }

        /// <summary>
        /// Database entry representing cash generated after accounting for cash outflows to support operations
        /// </summary>
        public List<FreeCashFlow> FreeCashFlow { get; init; }

        /// <summary>
        /// Database entry representing revenues minus cost of goods sold
        /// </summary>
        public List<GrossMargin> GrossMargin { get; init; }

        /// <summary>
        /// Database entry representing profit (sales minus all expenses) divided by revenue
        /// </summary>
        public List<ProfitMargin> ProfitMargin { get; init; }

        /// <summary>
        /// Database entry representing income generated from normal business operations
        /// </summary>
        public List<Revenues> Revenues { get; init; }

        /// <summary>
        /// Database entry representing current assets minus current liabilities
        /// </summary>
        public List<WorkingCapital> WorkingCapital { get; init; }

        public int Version = 1;
    }
}
