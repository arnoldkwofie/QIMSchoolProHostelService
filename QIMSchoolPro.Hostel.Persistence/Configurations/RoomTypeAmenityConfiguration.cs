using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Qface.Domain.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QIMSchoolPro.Hostel.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Qface.Domain.Shared.Enums;

namespace QIMSchoolPro.Hostel.Persistence.Configurations
{
    public class RoomTypeAmenityConfiguration : IEntityTypeConfiguration<RoomTypeAmenity>
    {
        public void Configure(EntityTypeBuilder<RoomTypeAmenity> builder)
        {
            builder.ToTable(nameof(RoomTypeAmenity));
            builder.OwnsOne<Audit>(typeof(Audit).Name);
            builder.OwnsOne(e => e.Audit, b =>
            {
                b.Property(e => e.EntityStatus).HasConversion(new EnumToStringConverter<EntityStatus>());
            });
            builder.HasQueryFilter(a => a.Audit.EntityStatus == EntityStatus.Normal);
        }
    }
}
