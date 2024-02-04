using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Shared.Domain
{
	public class Application: BaseDomainModel
    {

        [Required]
        
        public string? Status { get; set; }

        [Required]
        public string? Resume { get; set; }


        [Required]
        public int? JobId { get; set; }
        public virtual Job? Job { get; set; }

        [Required]
        public int? JobSeekerId { get; set; }
        public virtual JobSeeker? JobSeeker { get; set; }



       

    }
}
