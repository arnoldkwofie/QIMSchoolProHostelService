using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using Qface.Domain.Shared.ValueObjects;
using QIMSchoolPro.Hostel.Domain.Entities;
using QIMSchoolPro.Hostel.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qface.Domain.Shared.Enums;

namespace QIMSchoolPro.Hostel.Persistence.Configurations
{
    public partial class PartyConfiguration : IEntityTypeConfiguration<Party>
    {
        public void Configure(EntityTypeBuilder<Party> builder)
        {
            builder.ToTable(nameof(Party));
            builder.OwnsOne(a => a.Name);
            builder.Navigation(a => a.Name).IsRequired();
            builder.HasQueryFilter(a => a.Audit.EntityStatus == EntityStatus.Normal);

            builder.OwnsOne(a => a.AddressLine);

            builder.OwnsOne(e => e.PrimaryEmailAddress, b =>
            {
                b.OwnsOne<Email>("Email", b =>
                {
                    b.Property(p => p.Value).HasColumnName("Email");
                    b.Property(x => x.EmailType)
                    .HasConversion(new EnumToStringConverter<EmailType>());

                });
            });
            builder.OwnsOne(e => e.OtherEmailAddress, b =>
            {
                b.OwnsOne<Email>("Email", b =>
                {
                    b.Property(p => p.Value).HasColumnName("Email");
                    b.Property(x => x.EmailType)
                    .HasConversion(new EnumToStringConverter<EmailType>());

                });
            });
            builder.OwnsOne(e => e.PrimaryPhoneNumber, b =>
            {
                b.OwnsOne(x => x.Phone);
            });
            builder.OwnsOne(e => e.OtherPhoneNumber, b =>
            {
                b.OwnsOne(x => x.Phone);
            });
            builder.OwnsOne<Audit>(typeof(Audit).Name);


            builder.Property(x => x.MaritalStatus)
                .HasConversion(new EnumToStringConverter<MaritalStatus>());

            builder.OwnsOne(e => e.Audit, b =>
            {
                b.Property(e => e.EntityStatus).HasConversion(new EnumToStringConverter<EntityStatus>());
            });
            builder.OwnsOne(e => e.Name, b =>
            {
                b.Property(e => e.Sex).HasConversion(new EnumToStringConverter<Sex>());
            });


        }
    }


}
