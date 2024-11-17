using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Qface.Application.Shared.Common.Interfaces;
using QIMSchoolPro.Hostel.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Processors.Processors
{

    [ProcessorBase]
    public class UserProcessor
    {
        private readonly IIdentityService _identityService;
        private readonly StudentProcessor _studentProcessor;
        private readonly IBookingRepository _bookingRepository;
        private readonly IBedRepository _bedRepository;

        public UserProcessor(
            IIdentityService identityService,
            StudentProcessor studentProcessor,
            IBookingRepository bookingRepository, IBedRepository bedRepository
            )
        {
            _identityService = identityService;
            _studentProcessor = studentProcessor;
            _bookingRepository = bookingRepository;
            _bedRepository = bedRepository;

        }

        public async Task<UserViewModel> GetLoginUser()
        {
            var user = new UserViewModel();

                var refNo = _identityService.GetUserName();
                var student = await _studentProcessor.GetStudentByStudentNumber(refNo);
                if (student == null) return user;
                user.Name = student.Party.Name.FullNamev3;
                user.StudentNumber = student.StudentNumber;
                user.YearGroup = student.YearGroup.AdmittedYear;
                user.PhoneNumber = student?.Party?.PrimaryPhoneNumber?.Phone?.Number;
                user.HostelId = student?.HallId;

                var booking = await _bookingRepository.GetUserBooking(refNo);
                if (booking!=null)
                {
                    user.IsBooked=true;
                    if(booking.ConfirmationDate!=null) 
                    {
                        user.IsOwned=true;  
                    }
                }


            return user;
        }

    }

    public class UserViewModel
    {
        public string StudentNumber { get; set; }
        public string Name { get; set; }
        public int YearGroup { get; set; }
        public bool IsBooked { get; set; }=false;
        public bool IsOwned { get; set; }=false ;
        public string? PhoneNumber { get; set; }
        public int? HostelId { get; set; }   


    }
}
