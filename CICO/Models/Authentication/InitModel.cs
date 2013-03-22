using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models.Authentication
{
    public class InitModel
    {
        [DisplayName("Arrival Date")]
        [Required]
        public DateTime ArrivalDate { get; set; }
        [StringLength(65)]
        [Required]
        [DisplayName("Given Name(s)")]
        public string GivenName { get; set; }
        [StringLength(65)]
        [Required]
        [DisplayName("Surname")]
        public string Surname { get; set; }
        [DisplayName("Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$", ErrorMessage = "Invalid e-mail.")]
        public string EmailAddress { get; set; }
        [DisplayName("Employee Id")]
        [Required]
        public int EmployeeId { get; set; }
    }
}