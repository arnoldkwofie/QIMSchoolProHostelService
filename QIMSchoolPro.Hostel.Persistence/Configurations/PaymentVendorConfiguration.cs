using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
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
    public class PaymentVendorConfiguration : IEntityTypeConfiguration<PaymentVendor>
    {
        public void Configure(EntityTypeBuilder<PaymentVendor> builder)
        {
            builder.ToTable(nameof(PaymentVendor));

            builder.OwnsOne(a => a.Audit);
            builder.OwnsOne(e => e.Audit, b =>
            {
                b.Property(e => e.EntityStatus).HasConversion(new EnumToStringConverter<EntityStatus>());
            }); builder.HasQueryFilter(a => a.Audit.EntityStatus == EntityStatus.Normal);

        }
    }
}
