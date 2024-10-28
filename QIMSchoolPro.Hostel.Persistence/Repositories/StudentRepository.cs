using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QIMSchoolPro.Hostel.Domain.Entities;
using QIMSchoolPro.Hostel.Persistence.Interfaces;
using QIMSchoolPro.Hostel.Persistence.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Persistence.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public ILogger<Student> Logger { get; }

        public StudentRepository(HostelDbContext context, ILogger<Student> logger) : base(context)
        {
            Logger = logger;
        }


        public async Task<Student> GetAsync(string id)
        {
            try
            {

                var studentFromDB = await GetBaseQuery().FirstOrDefaultAsync(a => a.StudentNumber == id || a.IndexNumber == id);
                return studentFromDB;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public override IQueryable<Student> GetBaseQuery()
        {
            return base.GetBaseQuery()
                .Include(a=>a.Programme).ThenInclude(a=>a.Department).ThenInclude(a=>a.Faculty).ThenInclude(a=>a.SchoolCentre).ThenInclude(a=>a.Campus)
                .Include(a=>a.Party);


        }

    }
}
