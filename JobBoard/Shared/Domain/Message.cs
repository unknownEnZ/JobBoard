using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Shared.Domain
{
    public class Message:BaseDomainModel
    {
        

        [Required]
       
        public string? Content { get; set; }

        [Required]
        
        public int? JobSeekerId { get; set; }
        public virtual JobSeeker? JobSeeker { get; set; }


        [Required]
        
        public int? EmployerId { get; set; }
        public virtual Employer? Employer { get; set; }
    }
}
