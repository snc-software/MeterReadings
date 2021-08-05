using MeterReadings.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeterReadings.Infrastructure.Configuration
{
    public class MeterReadingConfiguration : IEntityTypeConfiguration<MeterReadingModel>
    {
        public void Configure(EntityTypeBuilder<MeterReadingModel> builder)
        {
            builder.ToTable("MeterReadings")
                .HasKey(x => x.Id);

            builder.Property(x => x.AccountId)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.MeterReadingDateTime)
                .HasColumnType("DateTime")
                .IsRequired();

            builder.Property(x => x.MeterReadValue)
                .HasColumnType("varchar(5)")
                .IsRequired();
        }
    }
}