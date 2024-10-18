using QIMSchoolPro.Hostel.Domain.Entities;
using QIMSchoolPro.Hostel.Persistence.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Persistence.Interfaces
{
    public interface IRoomRepository: IRepository<Room>
    {
        Task<List<Room>> GetRoomsAsync();
    }
}
