using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Domain.Constants
{
    public static class RedisKeys
    {
        public static string GetHostels()
        {
            return $"umat:Hostel-api:Hostels";
        }

        public static string GetRooms()
        {
            return $"umat:Hostel-api:rooms";
        }

    }

}
