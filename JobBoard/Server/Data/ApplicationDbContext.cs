using Duende.IdentityServer.EntityFramework.Options;
using JobBoard.Server.Configurations.Entities;
using JobBoard.Server.Models;
using JobBoard.Shared.Domain;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection.Emit;

namespace JobBoard.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

		public DbSet<Employer> Employers { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<Job> Jobs { get; set; }
		public DbSet<Application> Applications { get; set; }
        public DbSet<JobSeeker> JobSeekers { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {


            base.OnModelCreating(builder);
           
            builder.ApplyConfiguration(new CompanySeedConfiguration());
            builder.ApplyConfiguration(new JobSeekerSeedConfiguration());
            builder.ApplyConfiguration(new RoleSeedConfiguration());
            builder.ApplyConfiguration(new UserRoleSeedConfiguration());
            builder.ApplyConfiguration(new UserSeedConfiguration());
        }


        public DbSet<JobBoard.Shared.Domain.JobSeeker> JobSeeker { get; set; } = default!;
        
	}
}
