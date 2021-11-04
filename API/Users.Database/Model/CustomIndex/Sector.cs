using Database.Core;
using System.Collections.Generic;

namespace Users.Database.Model.CustomIndex
{
	public class Sector : DbEntity
    {
        public List<string> Sectors;
        public List<string> Industries;
    }
}
