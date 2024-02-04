using JobBoard.Server.Data;
using JobBoard.Server.IRepository;
using JobBoard.Server.Models;
using JobBoard.Shared.Domain;
using JobBoard.Server.Data;
using JobBoard.Server.IRepository;
using JobBoard.Server.Models;
using JobBoard.Server.Repository;
using JobBoard.Shared.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JobBoard.Server.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Employer> _employers;
        
        private IGenericRepository<Company> _companies;
        private IGenericRepository<Job> _jobs;
        private IGenericRepository<Application> _applications;
        private IGenericRepository<Message> _messages;
        private IGenericRepository<JobSeeker> _jobseekers;

        private UserManager<ApplicationUser> _userManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IGenericRepository<Employer> Employers
            => _employers ??= new GenericRepository<Employer>(_context);
        
        public IGenericRepository<Company> Companies
            => _companies ??= new GenericRepository<Company>(_context);
        public IGenericRepository<Message> Messages
            => _messages ??= new GenericRepository<Message>(_context);
        public IGenericRepository<Job> Jobs
            => _jobs ??= new GenericRepository<Job>(_context);
        public IGenericRepository<Application> Applications
            => _applications ??= new GenericRepository<Application>(_context);
        public IGenericRepository<JobSeeker> JobSeekers
            => _jobseekers ??= new GenericRepository<JobSeeker>(_context);
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save(HttpContext httpContext)
        {
            //To be implemented
            string user = "System";

            var entries = _context.ChangeTracker.Entries()
                .Where(q => q.State == EntityState.Modified ||
                    q.State == EntityState.Added);

            foreach (var entry in entries)
            {
                ((BaseDomainModel)entry.Entity).DateUpdated = DateTime.Now;
                ((BaseDomainModel)entry.Entity).UpdatedBy = user;
                if (entry.State == EntityState.Added)
                {
                    ((BaseDomainModel)entry.Entity).DateCreated = DateTime.Now;
                    ((BaseDomainModel)entry.Entity).CreatedBy = user;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}