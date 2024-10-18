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
    public class BuildingConfiguration : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.ToTable(nameof(Building));
            builder.OwnsOne<Audit>(typeof(Audit).Name);
            builder.OwnsOne(e => e.Audit, b =>
            {
                b.Property(e => e.EntityStatus).HasConversion(new EnumToStringConverter<EntityStatus>());
            });
            builder.OwnsOne(b => b.Geolocation, geo =>
                {
                    geo.Property(g => g.Latitude).HasColumnName("Latitude");
                    geo.Property(g => g.Longitude).HasColumnName("Longitude");
                });
            builder.HasQueryFilter(a => a.Audit.EntityStatus == EntityStatus.Normal);
        }
    }
}
