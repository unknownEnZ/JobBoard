using JobBoard.Shared.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobBoard.Server.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(HttpContext httpContext);
        IGenericRepository<Company> Companies { get; }
        IGenericRepository<Employer> Employers { get; }
        IGenericRepository<Job> Jobs { get; }
        IGenericRepository<Application> Applications { get; }
        IGenericRepository<Message> Messages { get; }
        IGenericRepository<JobSeeker> JobSeekers { get; }

    }
}