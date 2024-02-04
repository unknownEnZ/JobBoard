using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JobBoard.Shared.Domain
{
	public class Employer: BaseDomainModel
    {


        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "First Name does not meet length requurement")]

        public string? FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Last Name does not meet length requurement")]

        public string? LastName { get; set; }


        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email Address is not a valid Email Address")]
        [EmailAddress]
        public string? EmailAddress { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"(6|8|9|)\d{7}", ErrorMessage = "Contact Number is not a valid phone number")]

        public string? PhoneNumber { get; set; }


        [Required]


        public int? CompanyId { get; set; }
        public virtual Company? Company { get; set; }



       


    }
}
