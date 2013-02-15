using System;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName{get; set; }
        [Required]
        public string MiddleName { get; set; }
        public string PreferredName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Title { get; set; }
        public string Nationality { get; set; }
        public string HomePhone { get; set; }
        public string HomePhone2 { get; set; }
        public string CellPhone { get; set; }
        public DateTime? TourStartDate { get; set; }
        public DateTime? TourEndDate { get; set; }
        public string AgencyOrSection { get; set; }
        public string PositionTitle { get; set; }
        public string HomeStreetAddress { get; set; }
        public string HomeStreetAddress2 { get; set; }
        public string HomeState { get; set; }
        public string HomeCity { get; set; }
        public string HomeZip { get; set; }
        public string HomeEmail { get; set; }
        public  string PersonalEmail { get; set; }
        public string PassportNumber { get; set; }
        public string PassportType { get; set; }
        public DateTime? PassportExpiration { get; set; }
        public string VisaNumber { get; set; }
        public string VisaExpiration { get; set; }
        public string PostOfAssignment { get; set; }
        public string Agency { get; set; }
        public string Office { get; set; }
        public string RoomNumber { get; set; }
        public string OfficePhone { get; set; }
        public string Extension { get; set; }
        public string OfficeDirect { get; set; }
        public string EmergencyContactLastName { get; set; }
        public string EmergencyContactFirstName { get; set; }
        public string EmergencyContactRelationship { get; set; }
        public string EmergencyContactCity { get; set; }
        public string EmergencyContactState { get; set; }
        public string EmergencyContactZip { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string EmergencyContactOfficePhone { get; set; }
        public string LegalResidenceState { get; set; }
        public string LegalResidenceHomeAddress { get; set; }
       
    }
}