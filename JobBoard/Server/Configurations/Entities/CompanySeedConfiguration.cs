using JobBoard.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Drawing;
using static Duende.IdentityServer.Models.IdentityResources;


namespace JobBoard.Server.Configurations.Entities
{
    public class CompanySeedConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData(
            new Company
            {
                Id = 1,
                CompanyName = "MicronTech",
                Industry = "IT",

                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                CreatedBy = "System",
                UpdatedBy = "System"
            }

          );

            builder.HasData(
            new Company
            {
                Id = 2,
                CompanyName = "Advan",
                Industry = "Corn",

                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                CreatedBy = "System",
                UpdatedBy = "System"
            }

          );

            builder.HasData(
            new Company
            {
                Id = 3,
                CompanyName = "Sheng SIong",
                Industry = "Teeth",

                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now,
                CreatedBy = "System",
                UpdatedBy = "System"
            }

          );
        }
    }
}
