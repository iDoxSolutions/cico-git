using System;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName{get; set; }
        [Required]
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string Title { get; set; }
    }
}