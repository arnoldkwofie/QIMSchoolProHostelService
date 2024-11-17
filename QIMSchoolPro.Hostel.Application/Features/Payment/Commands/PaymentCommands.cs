using MediatR;
using QIMSchoolPro.Hostel.Processors.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Application.Features.Payment.Commands
{
    public static class CreatePayment
    {

        public class CreatePaymentCommand : IRequest
        {
            public PaymentCommand Payload { get; set; }

            public CreatePaymentCommand(PaymentCommand payload)
            {
                Payload = payload;
            }
        }

        public class Handler : IRequestHandler<CreatePaymentCommand>
        {
            private readonly PaymentProcessor _paymentProcessor;

            public Handler(PaymentProcessor paymentProcessor)
            {
                _paymentProcessor = paymentProcessor;

            }
            public async Task Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
            {
                await _paymentProcessor.Create(request.Payload);
            }
        }


    }
}
