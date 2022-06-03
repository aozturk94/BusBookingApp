using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bus_Ticket_Booking.WebUI.Models
{
    public class UserDetailsModel
    {
        public string UserId { get; set; }
        [Required(ErrorMessage = "First Name is required!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "User Name is required!")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,15}$",
         ErrorMessage = "Characters are not allowed!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is required!")]
        [RegularExpression("^[A-Za-z0-9._%+-]*@[A-Za-z0-9.-]*\\.[A-Za-z0-9-]{2,}$",
        ErrorMessage = "Email is must be properly formatted.")]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public IEnumerable<string> SelectedRoles { get; set; }
    }
}
