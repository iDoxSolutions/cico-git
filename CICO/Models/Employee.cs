using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Cico.Models.Helpers;

namespace Cico.Models    //  [DisplayName("")]   [StringLength()]
{
    public class Employee:EntityBaseWithKey
    {
        [DisplayName("First Name")]
        [StringLength(65)]
        public string FirstName { get; set; }
        [DisplayName("Middle Initial")]
        [StringLength(1)]
        public string MiddleInitial { get; set; }
        [DisplayName("Agency")]
        [StringLength(65)]
        public string Agency {get; set; }
        [DisplayName("Preferred Name")]
        [StringLength(65)]
        public string PreferredName { get; set; }
        [DisplayName("Last Name")]
        [StringLength(65)]
        public string LastName { get; set; }
        [DisplayName("Date of Birth")]
        public DateTime? DateOfBirth { get; set; }
        [DisplayName("Diplomatic Title")]
        [StringLength(65)]
        public string DiplomaticTitle { get; set; }
        [DisplayName("Title/Salutation")]
        [StringLength(65)]
        public string Title { get; set; }
        [DisplayName("EFT Information - Bank Account")]
        [StringLength(30)]
        public string BankAccount { get; set; }
        [DisplayName("EFT Information - Routing Number")]
        [StringLength(30)]
        public string RoutingNumber { get; set; }
        [DisplayName("Nationality")]
        [StringLength(60)]
        public string Nationality { get; set; }

        [EmbasssyNameDisplayName("{0} - Personal Cell Phone")]
        public string CellPhone { get; set; }

        [EmbasssyNameDisplayName("{0} - Office Cell Phone")]
        public string OfficeCellPhone { get; set; }
        [DisplayName("Estimated Arrival Date")]
        public DateTime? ArrivalDate { get; set; }
        [DisplayName("Estimated Departure Date")]
        public DateTime? TourEndDate { get; set; }
        [DisplayName("Agency Or Section")]
        public string AgencyOrSection { get; set; }
        [DisplayName("Position Title")]
        [StringLength(65)]
        public string PositionTitle { get; set; }
        [DisplayName("Home Leave Address")]
        [StringLength(100)]
        public string HomeAddress { get; set; }
        
        [DisplayName("Prior Post")]
        [StringLength(65)]
        public string PriorPostState { get; set; }
        [DisplayName("Email - Personal")]
        [DataType(DataType.EmailAddress)]
        public  string PersonalEmail { get; set; }
        [DisplayName("Email - Work (Agency)")]
        [DataType(DataType.EmailAddress)]
        public string WorkEmail { get; set; }
        [DisplayName("Passport Number")]
        public string PassportNumber { get; set; }
        [DisplayName("Passport Type")]
        public string PassportType { get; set; }
        [DisplayName("Passport Expiration")]
        public DateTime? PassportExpiration { get; set; }
        [DisplayName("Visa Number")]
        
        public string VisaNumber { get; set; }
        [DisplayName("Visa expiration")]
        public DateTime? VisaExpiration { get; set; }
        [DisplayName("Office")]
        [StringLength(65)]
        public string Office { get; set; }
        [DisplayName("Office Phone")]
        
        public string OfficePhone { get; set; }

        [DisplayName("In-Transit Phone")]
        
        public string PreArrivalPhone { get; set; }
        [DisplayName("Emergency Contact Email Address")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$", ErrorMessage = "Invalid e-mail.")]
        public string EmergencyContactEmail { get; set; }
        [DisplayName("Emergency Contact Given Name(s)")]
        [StringLength(65)]
        public string EmergencyContactLastName { get; set; }
        [DisplayName("Emergency Contact Relationship")]
        [StringLength(30)]
        public string EmergencyContactRelationship { get; set; }
        [DisplayName("Emergency Contact Phone")]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string EmergencyContactPhone { get; set; }
        [DisplayName("Emergency Contact Phone 2")]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string EmergencyContactPhone2 { get; set; }
        public string EmergencyContactOfficePhone { get; set; }
        [DisplayName("State of U.S. Legal Residence")]
        [StringLength(2)]
        public string LegalResidenceState { get; set; }
        [DisplayName("Proxy Email")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$", ErrorMessage = "Invalid e-mail.")]
        public string ProxyEmail { get; set; }
        
        public virtual ICollection<CheckListSession> CheckListSessions { get; set; }

        [DisplayName("Gender")]
        [StringLength(65)]
        public string Gender { get; set; }

        [DisplayName("School Name")]
        [StringLength(65)]
        public string SchoolName { get; set; }

        [DisplayName("Radio Call Sign")]
        [StringLength(65)]
        public string RadioCallSign { get; set; }

        [DisplayName("Clearance Level")]
        [StringLength(65)]
        public string ClearanceLevel { get; set; }

        [DisplayName("License Plate Info")]
        [StringLength(65)]
        public string LicensePlate { get; set; }

        [DisplayName("SSN")]
        [StringLength(11)]
        public string SSN { get; set; }

       


        [Required(ErrorMessage = "Domain user id is required")]
        public string UserId { get; set; }

        public virtual ICollection<Dependent> Dependents { get; set; }

        public virtual Staff Proxy { get; set; }

        public virtual IList<SentBoxItem> SentBoxItems { get; set; } 



    }
}