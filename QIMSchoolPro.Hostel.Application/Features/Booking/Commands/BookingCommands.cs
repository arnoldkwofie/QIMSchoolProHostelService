using MediatR;
using QIMSchoolPro.Hostel.Processors.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Application.Features.Booking.Commands
{
    public static class CreateBooking
    {

        public class CreateBookingCommand : IRequest<ResponseData>
        {
            public int Id { get; set; }

            public CreateBookingCommand(int id)
            {
                Id = id;
            }
        }

        public class Handler : IRequestHandler<CreateBookingCommand, ResponseData>
        {
            private readonly BookingProcessor _bookingProcessor;

            public Handler(BookingProcessor bookingProcessor)
            {
                _bookingProcessor = bookingProcessor;

            }
            public async Task<ResponseData> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
            {
               var response = await _bookingProcessor.Create(request.Id, 0);
                return response;
            }
        }


    }
}
