using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models.Authentication
{
    public class InitModel
    {
        [DisplayName("Arrival Date" + "*Enter best estimate. Can be changed later")]
        [Required]
        public DateTime ArrivalDate { get; set; }
        [RegularExpression("^([0-9]{1,2})[./-]+([0-9]{1,2})[./-]+([0-9]{2}|[0-9]{4})$", ErrorMessage = "Invalid format for Arrival Date.")]
        [StringLength(65)]
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [StringLength(65)]
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$", ErrorMessage = "Invalid e-mail.")]
        public string EmailAddress { get; set; }
        
        
        public int? EmpId { get; set; }
    }
}
