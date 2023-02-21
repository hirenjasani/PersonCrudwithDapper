using System;
using System.ComponentModel.DataAnnotations;

namespace PersonCrudwithDapper.Models
{
    public class PersonViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [Required]
        [StringLength(50)]
        public string State { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(500)]
        public string Hobbies { get; set; }

        [Display(Name = "Accept Terms and Conditions")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must accept the terms and conditions.")]
        public bool AcceptTermsAndCondition { get; set; }
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }

}
