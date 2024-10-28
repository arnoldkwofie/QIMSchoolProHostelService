using Microsoft.AspNetCore.SignalR;
using QIMSchoolPro.Hostel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Processors.Helpers
{
    public class BookingHub : Hub
    {
        public async Task AcknowledgeBedBooked(string connectionId, int roomId, int bedId)
        {
            // Log or process acknowledgment
            Console.WriteLine($"Acknowledgment received for roomId: {roomId}, bedId: {bedId} from {connectionId}");

        }

    }
}
