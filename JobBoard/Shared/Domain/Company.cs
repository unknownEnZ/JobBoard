using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoard.Shared.Domain
{
	public class Company: BaseDomainModel
	{
        [Required]
       
        public string? CompanyName { get; set; }

        [Required]
        
        public string? Industry { get; set; }
	

		//public int DepartmentId { get; set; }
		//public virtual Department? Department { get; set; }

	}
}
