using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Qface.Application.Shared.Common.Interfaces;
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

        public UserProcessor(
            IIdentityService identityService,
            StudentProcessor studentProcessor

            )
        {
            _identityService = identityService;
            _studentProcessor = studentProcessor;

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
               


            return user;
        }

    }

    public class UserViewModel
    {
        public string StudentNumber { get; set; }
        public string Name { get; set; }
        public int YearGroup { get; set; }
      
       
     
    }
}
