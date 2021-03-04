namespace Database.Model.User.CustomIndices
{
    public class Sectors
    {
        public Sector[] SectorGroups;

        public bool IsNotNull()
        {
            return SectorGroups is not null;
        }
    }
}