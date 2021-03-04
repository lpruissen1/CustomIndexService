namespace Database.Model.StockData
{
    public class StockIndex : DbEntity
    {
        public string Name { get; set; }
        public string[] Tickers{ get; set; }

        public override string GetPrimaryKey()
        {
            return Id;
        }
    }
}
