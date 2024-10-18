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
    public partial class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable(nameof(Student))
            .HasKey(o => o.StudentNumber);
            builder.OwnsOne<Audit>(typeof(Audit).Name);
            builder.OwnsOne<YearGroup>(typeof(YearGroup).Name);
            builder.OwnsOne<Guardian>(typeof(Guardian).Name);
            //builder.Property(a => a.StudentType).HasConversion(new EnumToStringConverter<StudentType>());
            builder.Ignore(a => a.PhotoUrl);
            builder.Ignore(a => a.Year);

            builder.Ignore(a => a.HasCompleteSchool);
            builder.OwnsOne(e => e.Audit, b =>
            {
                b.Property(e => e.EntityStatus).HasConversion(new EnumToStringConverter<EntityStatus>());
            });

            builder.HasQueryFilter(a => a.Audit.EntityStatus == EntityStatus.Normal);


        }
    }
}
