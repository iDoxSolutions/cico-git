﻿using System;
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
        public string FirstName { get; set; }
        [StringLength(65)]
        [Required]
        [DisplayName("LastName")]
        public string LastName { get; set; }
        [DisplayName("Email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$", ErrorMessage = "Invalid e-mail.")]
        public string EmailAddress { get; set; }
        [DisplayName("Employee Id")]
        [Required]
        public int EmployeeId { get; set; }
        public int? EmpId { get; set; }
    }
}