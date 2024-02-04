using JobBoard.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Drawing;
using static Duende.IdentityServer.Models.IdentityResources;


namespace JobBoard.Server.Configurations.Entities
{
    public class EmployerSeedConfiguration : IEntityTypeConfiguration<Employer>
    {
        public void Configure(EntityTypeBuilder<Employer> builder)
        {
          //  builder.HasData(
          //  new Employer
          //  {
          //      Id = 1,
          //      FirstName = "Jon",
          //      LastName = "Lim",
          //      EmailAddress = "Jon@gmail.com",

          //      PhoneNumber = "67901233",
          //      DateCreated = DateTime.Now,
          //      DateUpdated = DateTime.Now,
          //      CreatedBy = "System",
          //      UpdatedBy = "System",
          //      CompanyId = 1
          //  }
          //);
        }
    }
}
