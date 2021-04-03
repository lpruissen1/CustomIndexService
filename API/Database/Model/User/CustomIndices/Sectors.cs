namespace Database.Model.User.CustomIndices
{
    public class Sectors
    {
        public Sector[] SectorGroups;

        public bool IsNull()
        {
            return SectorGroups is null;
        }
    }
}