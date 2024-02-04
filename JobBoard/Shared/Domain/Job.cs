using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Shared.Domain
{
	public class Job: BaseDomainModel
	{
        [Required]
       
        public string? JobTitle { get; set; }

        [Required]


        public string? Description { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double? Salary { get; set; }

        [Required]

        public string? Location { get; set; }

        [Required]
       
        public string? JobType { get; set; }

        [Required]
       
        public string? rate { get; set; }

        [Required]
        public int? EmployerId { get; set; }

        public virtual Employer? Employer { get; set; }

        //public virtual List<Application>? Applications { get; set; }

        public virtual List<Application>? Applications { get; set; }

    }
}
