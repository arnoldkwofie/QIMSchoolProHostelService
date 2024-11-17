using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using Qface.Domain.Shared.ValueObjects;
using QIMSchoolPro.Hostel.Domain.Entities;
using QIMSchoolPro.Hostel.Domain.Enums;
using QIMSchoolPro.Hostel.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qface.Domain.Shared.Enums;

namespace QIMSchoolPro.Hostel.Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable(nameof(Payment));
            builder.OwnsOne<Audit>(typeof(Audit).Name);
            builder.OwnsOne(a => a.Payee);
            builder.OwnsOne(a => a.PaymentVendorDetail);
            builder.OwnsOne<AcademicPeriod>(typeof(AcademicPeriod).Name);
            builder.HasQueryFilter(a => a.Audit.EntityStatus == EntityStatus.Normal);

            builder.OwnsOne(e => e.Audit, b =>
            {
                b.Property(e => e.EntityStatus).HasConversion(new EnumToStringConverter<EntityStatus>());
            });
            builder.Property(e => e.PaymentMode).HasConversion(new EnumToStringConverter<PaymentMode>());


        }
    }
}
