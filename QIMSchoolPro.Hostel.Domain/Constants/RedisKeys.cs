using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Domain.Constants
{
    public static class RedisKeys
    {
        public static string GetBuildings()
        {
            return $"umat:Hostel-api:building";
        }

    }

}
