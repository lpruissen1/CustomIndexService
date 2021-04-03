namespace Database.Model.User.CustomIndices
{
    public class MarketCaps
    {
        public MarketCapitalzation[] MarketCapGroups;

        public bool IsNull()
        {
            return MarketCapGroups is null;
        }
    }
}