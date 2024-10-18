using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Qface.Domain.Shared.ValueObjects;
using QIMSchoolPro.Hostel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Qface.Domain.Shared.Enums;

namespace QIMSchoolPro.Hostel.Persistence.Configurations
{
    public partial class SchoolCentreConfiguration : IEntityTypeConfiguration<SchoolCentre>
    {
        public void Configure(EntityTypeBuilder<SchoolCentre> builder)
        {
            builder.ToTable(nameof(SchoolCentre));
            builder.Property(a => a.Name).IsRequired();
            builder.OwnsOne<Audit>(typeof(Audit).Name);
            builder.OwnsOne(e => e.Audit, b =>
            {
                b.Property(e => e.EntityStatus).HasConversion(new EnumToStringConverter<EntityStatus>());
            }); builder.HasQueryFilter(a => a.Audit.EntityStatus == EntityStatus.Normal);


        }



    }
}
