using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Utils
{
    public class StringUtils
    {
       public static string GetPrivateGroupName(string from, string to)
        {
            var stringToCompare = string.CompareOrdinal(from, to)<0;
            return stringToCompare ? $"{from}-{to}" : $"{to}-{from}";

        }
    }
}
