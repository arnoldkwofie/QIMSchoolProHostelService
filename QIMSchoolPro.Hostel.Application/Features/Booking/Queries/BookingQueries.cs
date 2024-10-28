using MediatR;
using QIMSchoolPro.Hostel.Processors.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Application.Features.Booking.Queries
{
    public static class GetBooking
    {
        public class Query : IRequest<BookingData>
        {
            public Query()
            {

            }

        }

        public class Handler : IRequestHandler<Query, BookingData>
        {
            private readonly BookingProcessor _bookingProcessor;

            public Handler(BookingProcessor bookingProcessor)
            {
                _bookingProcessor = bookingProcessor;
            }

            public async Task<BookingData> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _bookingProcessor.GetBooking();
                return result;
            }
        }
    }
}