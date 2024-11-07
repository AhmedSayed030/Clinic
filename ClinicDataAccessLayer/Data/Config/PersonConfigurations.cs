using ClinicDataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClinicDataAccessLayer.Data.Config
{
    public class PersonConfigurations : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("People");

            builder.Property(e => e.Address)
                   .HasMaxLength(500);

            builder.Property(e => e.Name)
                   .HasMaxLength(25);

            builder.Property(e => e.Phone)
                   .HasMaxLength(11);

            builder.Property(e => e.Email)
                   .HasMaxLength(25);

            builder.HasData(PeopleData());

        }

        private static Person[] PeopleData()
        {
            return [

                new Person {
                    Id = 1,
                    Name = "Ahmed",
                    Email = "Ahmed@Gmail.com",
                    Phone = "01065363495",
                    Address = "AlFayuom",
                    DateOfBirth = new DateOnly(2006, 7, 6)
                },
                new Person {
                    Id = 2,
                    Name = "Mohamed",
                    Email = "Mohamed@Gmail.com",
                    Phone = "01065363495",
                    Address = "AlFayuom",
                    DateOfBirth = new DateOnly(2010, 7, 6)
                },
                new Person {
                    Id = 3,
                    Name = "Sayed",
                    Email = "Sayed@Gmail.com",
                    Phone = "01065363494",
                    Address = "AlFayuom",
                    DateOfBirth = new DateOnly(1970, 7, 6)
                },

            ];
        }
    }
}
