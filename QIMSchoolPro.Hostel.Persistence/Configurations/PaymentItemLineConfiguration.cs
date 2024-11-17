using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QIMSchoolPro.Hostel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qface.Domain.Shared.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace QIMSchoolPro.Hostel.Persistence.Configurations
{
    public class PaymentItemLineConfiguration : IEntityTypeConfiguration<PaymentItemLine>
    {
        public void Configure(EntityTypeBuilder<PaymentItemLine> builder)
        {
            builder.ToTable(nameof(PaymentItemLine));
            builder.OwnsOne(a => a.Amount);
            builder.OwnsOne(a => a.Token);
            builder.OwnsOne(a => a.Amount, b =>
            {
                b.OwnsOne(x => x.Rate);
            });
            builder.OwnsOne(a => a.Audit);
            builder.OwnsOne(e => e.Audit, b =>
            {
                b.Property(e => e.EntityStatus).HasConversion(new EnumToStringConverter<EntityStatus>());
            }); builder.HasQueryFilter(a => a.Audit.EntityStatus == EntityStatus.Normal);

        }
    }
}
