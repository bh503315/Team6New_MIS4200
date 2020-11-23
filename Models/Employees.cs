using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team6New_MIS4200.Models
{
    public class Employees
    {
        [Required]
        public Guid ID { get; set; }

        [Display(Name = "Email Address Required")]
        [Required]
        [EmailAddress(ErrorMessage = "Enter your most frequently used email address")]
        public string Email { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Employee First Name is required")]
        [StringLength(20)]
        public string firstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Employee Last Name is required")]
        [StringLength(20)]
        public string lastName { get; set; }

        public string fullName { get { return lastName + ", " + firstName; } }

        [Display(Name = "Primary Phone")]
        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(\(\d{3}\) |\d{3}-)\d{3}-\d{4}$",
            ErrorMessage = "Phone numbers must be in the format (xxx) xxx-xxxx or xxx-xxx-xxxx")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Office")]
        [Required(ErrorMessage = "Office Name Required")]
        [StringLength(50)]
        public string Office { get; set; }

        [Display(Name = "Current Position")]
        [Required(ErrorMessage = "Current Position Required")]
        [StringLength(75)]
        public string Position { get; set; }

        [Display(Name = "Hire Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime hireDate { get; set; }

        [Display(Name = "Employee Profile Picture")]
        [Required]
        public string photo { get; set; }

        [ForeignKey("Nominee")]
        public ICollection<Recognition> recognition { get; set; }

        [ForeignKey("Recognizor")]
        public ICollection<Recognition> nominations { get; set; }
    }
}