using AutoMapper;
using Qface.Application.Shared.Common.Interfaces;
using QIMSchoolPro.Hostel.Domain.Entities;
using QIMSchoolPro.Hostel.Domain.Enums;
using QIMSchoolPro.Hostel.Persistence;
using QIMSchoolPro.Hostel.Persistence.Interfaces;
using QIMSchoolPro.Hostel.Processors.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QIMSchoolPro.Hostel.Processors.Processors
{
    [ProcessorBase]
    public class PaymentProcessor
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private readonly StudentProcessor _studentProcessor;
        private readonly IAcademicConfigurationRepository _acdemicConfigurationRepository;

        public PaymentProcessor(StudentProcessor studentProcessor, IMapper mapper, 
            IAcademicConfigurationRepository academicConfigurationRepository, IPaymentRepository paymentRepository)
        {
            _studentProcessor = studentProcessor;
            _mapper=mapper;
            _acdemicConfigurationRepository = academicConfigurationRepository;
            _paymentRepository = paymentRepository;
        }
        
        public async Task Create(PaymentCommand command)
        {
            try
            {
                var academicPeriod = await _acdemicConfigurationRepository.GetAcademicPeriodAsync();
               // var username = _identityService.GetUserName();
                var username = "9013859223";
                var student = await _studentProcessor.GetStudentByStudentNumber(username);
                var entity = Payment.Create()
                    .WithPartyId(student.PartyId)
                    .WithAcademicPeriod(academicPeriod.AcademicPeriod)
                    .WithBankInfo(command.TransactionId, command.BankBranch, command.TellerName, command.TellerId)
                    .WithComments(command.Comment)
                    .WithPaymentVendorId(command.PaymentVendorId)
                    .WithReceipt(command.ReceiptNumber)
                    .AddItems(command.PaymentBreakDown)
                    .By(command.PaymentBy, command.PaymentByContactNumber)
                    .TookPlaceOnBankPremise()
                    .On(command.PaymentDate)
                    .WithBatch(command.ReceiptNumber);


                await _paymentRepository.InsertAsync(entity);

            }
            catch (ValidationException validationEx)
            {
                throw new ValidationException($"Validation failed: {validationEx.Message}");
            }
            catch (Exception ex)
            {
                throw new ValidationException($"An error occurred while creating: {ex.Message}");
            }
        }

        public async Task<List<PaymentDto>> GetPaymentByPartyId()
        {
            var username = "9013859223";
            var student = await _studentProcessor.GetStudentByStudentNumber(username);
            var data = await _paymentRepository.GetPaymentByPartyId(student.PartyId);
            var result = _mapper.Map<List<PaymentDto>>(data);
            return result;
        }

      
    }


    public class PaymentCommand
    {

        public DateTime PaymentDate { get; set; }
        public int PaymentVendorId { get; set; }
        public string TransactionId { get; set; }
        public string BankBranch { get; set; }
        public string TellerName { get; set; }
        public string TellerId { get; set; }
        public string Comment { get; set; }
        public string ReceiptNumber { get; set; }
        public string PaymentBy { get; set; }
        public string PaymentByContactNumber { get; set; }
        public List<PaymentBreakDown> PaymentBreakDown { get; set; }
    }
}
