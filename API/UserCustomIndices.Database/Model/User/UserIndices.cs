using Database.Core;
using System;
using System.Collections.Generic;

namespace Database.Model.User
{
    public class UserIndices : DbEntity
    {
        public Guid UserId { get; set; }

        public List<string> IndexId{ get; set; }
    }
}
