using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using JobBoard.Shared.Domain;
using System.Net.Mail;

namespace JobBoard.Server.Configurations.Entities
{
    public class JobSeekerSeedConfiguration : IEntityTypeConfiguration<JobSeeker>
    {
        public void Configure(EntityTypeBuilder<JobSeeker> builder)
        {
            builder.HasData(
            new JobSeeker
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "john.doe@example.com",
                PhoneNumber = "912345678",
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                CreatedBy = "System",
                UpdatedBy = "System"

            },
            new JobSeeker
            {
                Id = 2,
                FirstName = "Bob",
                LastName = "Johnson",
                EmailAddress = "bob.johnson@yahoo.com",
                PhoneNumber = "612345678",
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                CreatedBy = "System",
                UpdatedBy = "System"
            },
            new JobSeeker
            {
                Id = 3,
                FirstName = "Alice",
                LastName = "Smith",
                EmailAddress = "alice.smith@gmail.com",
                PhoneNumber = "812345678",
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                CreatedBy = "System",
                UpdatedBy = "System"
            }
         );
        }
    }
}
