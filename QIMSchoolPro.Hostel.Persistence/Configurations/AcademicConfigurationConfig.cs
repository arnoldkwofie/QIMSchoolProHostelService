using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Qface.Domain.Shared.ValueObjects;
using QIMSchoolPro.Hostel.Domain.Entities;
using QIMSchoolPro.Hostel.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Qface.Domain.Shared.Enums;

namespace QIMSchoolPro.Hostel.Persistence.Configurations
{
    public class AcademicConfigurationCofig : IEntityTypeConfiguration<AcademicConfiguration>
    {
        public void Configure(EntityTypeBuilder<AcademicConfiguration> builder)
        {
            builder.ToTable(nameof(AcademicConfiguration));
            builder.OwnsOne<Audit>(typeof(Audit).Name);
            builder.OwnsOne<AcademicPeriod>(typeof(AcademicPeriod).Name);
            builder.OwnsOne(a => a.OngoingActivity);
            builder.OwnsOne(e => e.Audit, b =>
            {
                b.Property(e => e.EntityStatus).HasConversion(new EnumToStringConverter<EntityStatus>());
            });
            builder.HasQueryFilter(a => a.Audit.EntityStatus == EntityStatus.Normal);

            //builder.HasData(new List<AcademicConfiguration>
            //{
            //	new AcademicConfiguration
            //	{
            //		Id=1,
            //		AcademicPeriod = AcademicPeriod.Create("2021/2022",Semester.FirstSemester),
            //		Active = true,
            //		OngoingActivity = new AcademicPeriodOngoinActivity
            //		{
            //			LecturerAssessment = false,
            //			SemesterRegistration =false
            //		},
            //		Group = AcademicConfigurationGroup.Default,

            //	}
            //});
        }
    }

}
