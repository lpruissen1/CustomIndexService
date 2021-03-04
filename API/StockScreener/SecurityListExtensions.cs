using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockScreener.Core
{
    public static class SecurityListExtensions
    {
        public static SecuritiesList ToSecurityList(this IEnumerable<Security> securityList)
        {
            var list = new SecuritiesList();
            list.AddRange(securityList);
            return list;
        }
    }
}
