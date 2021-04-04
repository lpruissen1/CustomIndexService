namespace Database.Model.User.CustomIndices
{
    public class MarketCaps
    {
        public MarketCapitalization[] MarketCapGroups;

        public bool IsNull()
        {
            return MarketCapGroups is null;
        }
    }
}