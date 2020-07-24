using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TinyDesk.Models
{
    public class Register : BaseModel
    {
        public Register()
        {
        }

        public virtual int OrderId { get; set; }
        [MinLength(5, ErrorMessage = "The name must be at least two characters long.")]
        [MaxLength(50, ErrorMessage = "The name cannot contain more than 50 characters.")]
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        [Required]
        public string Phone { get; set; } = "";
        [Required]
        public string Address { get; set; } = "";
        [Required]
        public string Address2 { get; set; } = "";
        [Required]
        public string City { get; set; } = "";
        [Required]
        public string State { get; set; } = "";
        [Required]
        public string Country { get; set; } = "";
        [Required]
        public string ZipCode { get; set; } = "";
    }
}
