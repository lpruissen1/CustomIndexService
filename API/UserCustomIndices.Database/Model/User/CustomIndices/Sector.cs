using Database.Core;
using System.Collections.Generic;

namespace UserCustomIndices.Database.Model.User.CustomIndices
{
    public class Sector : DbEntity
    {
        public List<string> Sectors;
        public List<string> Industries;
    }
}