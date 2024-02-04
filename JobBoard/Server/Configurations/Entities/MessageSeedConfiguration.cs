using JobBoard.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Drawing;
using static Duende.IdentityServer.Models.IdentityResources;


namespace JobBoard.Server.Configurations.Entities
{
    public class MessageSeedConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
          //  builder.HasData(
          //  new Message
          //  {
          //      Id = 1,
          //      Content ="Hellow",
                
          //      DateCreated = DateTime.Now,
          //      DateUpdated = DateTime.Now,
          //      CreatedBy = "System",
          //      UpdatedBy = "System",
          //      EmployerId = 1
          //  }
          //);
        }
    }
}
