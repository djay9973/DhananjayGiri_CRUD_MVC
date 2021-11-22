using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AdminCMS.Models;

namespace AdminCMS.Models
{
    public class Custom
    {
        [Required]
        [Display(Name = "Firt Name")]
        [MaxLength(20, ErrorMessage = "Max 20 characters")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(20, ErrorMessage = "Max 20 characters")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Contact No")]
        [MaxLength(12, ErrorMessage = "Max 12 characters")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        [MaxLength(50)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Password")]
        [MaxLength(8, ErrorMessage = "Max 8 characters")]
        public string Password { get; set; }
        [MetadataType(typeof(Custom))]
        public partial class Account
        {
        }
    }
}