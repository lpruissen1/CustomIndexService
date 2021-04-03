namespace Database.Model.User.CustomIndices
{
    public class MarketCaps
    {
        public MarketCap[] MarketCapGroups;

        public bool IsNull()
        {
            return MarketCapGroups is null;
        }
    }
}