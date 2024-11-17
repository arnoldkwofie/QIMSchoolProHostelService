using MediatR;
using QIMSchoolPro.Hostel.Processors.Dtos;
using QIMSchoolPro.Hostel.Processors.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Application.Features.Payment.Queries
{
    public static class GetPaymentByPartyId
    {
        public class Query : IRequest<IEnumerable<PaymentDto>>
        {
            public Query()
            {

            }

        }

        public class Handler : IRequestHandler<Query, IEnumerable<PaymentDto>>
        {
            private readonly PaymentProcessor _paymentProcessor;

            public Handler(PaymentProcessor paymentProcessor)
            {
                _paymentProcessor = paymentProcessor;
            }

            public async Task<IEnumerable<PaymentDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = await _paymentProcessor.GetPaymentByPartyId();
                return result;
            }
        }
    }
}
