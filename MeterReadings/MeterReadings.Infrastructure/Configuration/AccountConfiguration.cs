using MeterReadings.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeterReadings.Infrastructure.Configuration
{
    public class AccountConfiguration : IEntityTypeConfiguration<AccountModel>
    {
        public void Configure(EntityTypeBuilder<AccountModel> builder)
        {
            builder.ToTable("Accounts")
                .HasKey(x => x.AccountId);

            builder.Property(x => x.FirstName)
                .HasColumnType("varchar(100)")
                .IsRequired();
            
            builder.Property(x => x.LastName)
                .HasColumnType("varchar(100)")
                .IsRequired();

//TODO This can be seeded through sql server management studio, but for demo purposes..
            builder.HasData(
                new AccountModel
                {
                    AccountId = 2344,
                    FirstName = "Tommy",
                    LastName = "Test"
                },
                new AccountModel
                {
                    AccountId = 2233,
                    FirstName = "Barry",
                    LastName = "Test"
                },
                new AccountModel
                {
                    AccountId = 8766,
                    FirstName = "Sally",
                    LastName = "Test"
                },
                new AccountModel
                {
                    AccountId = 2345,
                    FirstName = "Jerry",
                    LastName = "Test"
                },
                new AccountModel
                {
                    AccountId = 2346,
                    FirstName = "Ollie",
                    LastName = "Test"
                },
                new AccountModel
                {
                    AccountId = 2347,
                    FirstName = "Tara",
                    LastName = "Test"
                },
                new AccountModel
                {
                    AccountId = 2348,
                    FirstName = "Tammy",
                    LastName = "Test"
                },
                new AccountModel
                {
                    AccountId = 2349,
                    FirstName = "Simon",
                    LastName = "Test"
                },
                new AccountModel
                {
                    AccountId = 2350,
                    FirstName = "Colin",
                    LastName = "Test"
                },
                new AccountModel
                {
                    AccountId = 2351,
                    FirstName = "Gladys",
                    LastName = "Test"
                },
                new AccountModel
                {
                    AccountId = 2352,
                    FirstName = "Greg",
                    LastName = "Test"
                },
                new AccountModel
                {
                    AccountId = 2353,
                    FirstName = "Tony",
                    LastName = "Test"
                },
                new AccountModel
                {
                    AccountId = 2355,
                    FirstName = "Arthur",
                    LastName = "Test"
                },
                new AccountModel
                {
                    AccountId = 2356,
                    FirstName = "Craig",
                    LastName = "Test"
                },
                new AccountModel
                {
                    AccountId = 6776,
                    FirstName = "Laura",
                    LastName = "Test"
                },
                new AccountModel
                {
                    AccountId = 4534,
                    FirstName = "JOSH",
                    LastName = "TEST"
                },
                new AccountModel
                {
                    AccountId = 1234,
                    FirstName = "Freya",
                    LastName = "Test"
                },
                new AccountModel
                {
                    AccountId = 1239,
                    FirstName = "Noddy",
                    LastName = "Test"
                },
                new AccountModel
                {
                    AccountId = 1240,
                    FirstName = "Archie",
                    LastName = "Test"
                },
                new AccountModel
                {
                    AccountId = 1241,
                    FirstName = "Lara",
                    LastName = "Test"
                },
                new AccountModel
                {
                    AccountId = 1242,
                    FirstName = "Tim",
                    LastName = "Test"
                },
                new AccountModel
                {
                    AccountId = 1243,
                    FirstName = "Graham",
                    LastName = "Test"
                },
                new AccountModel
                {
                    AccountId = 1244,
                    FirstName = "Tony",
                    LastName = "Test"
                },
                new AccountModel
                {
                    AccountId = 1245,
                    FirstName = "Neville",
                    LastName = "Test"
                },
                new AccountModel
                {
                    AccountId = 1246,
                    FirstName = "Jo",
                    LastName = "Test"
                },
                new AccountModel
                {
                    AccountId = 1247,
                    FirstName = "Jim",
                    LastName = "Test"
                },
                new AccountModel
                {
                    AccountId = 1248,
                    FirstName = "Pam",
                    LastName = "Test"
                }
            );
        }
    }
}