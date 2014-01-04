using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Cico.Models.Helpers;

namespace Cico.Models    
{
    public partial class Dependent:EntityBaseWithKey
    {
        public Dependent()
        {
            //SameECData = true;
        }
        [DisplayName("First Name")]
        [StringLength(65)]
        public string FirstName { get; set; }
     
        [DisplayName("Last Name")]
        [StringLength(66)]
        public string LastName { get; set; }
     
        [DisplayName("Relationship")]
        [StringLength(20)]
        public string Relationship { get; set; }
        
        [DisplayName("Date of Birth")]
        public DateTime? DateOfBirth { get; set; }
        
        [DisplayName("Title/Salutation")]
        [StringLength(65)]
        public string Title { get; set; }
        
        [DisplayName("Preferred Name")]
        [StringLength(65)]
        public string PreferredName { get; set; }
        
        [DisplayName("Nationality")]
        [StringLength(255)]
        public string Nationality { get; set; }
        
        [DisplayName("Email - Personal")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$", ErrorMessage = "Invalid e-mail.")]
        public  string PersonalEmail { get; set; }
        [DisplayName("Passport Number")]
        public string PassportNumber { get; set; }
        [DisplayName("Passport Type")]
        public string PassportType { get; set; }
        [DisplayName("Passport Expiration")]
        public DateTime? PassportExpiration { get; set; }
        [DisplayName("Visa Number")]
        public string VisaNumber { get; set; }
        [DisplayName("Visa Expiration")]
        public DateTime? VisaExpiration { get; set; }
        
        
        [DisplayName("Emergency Info Same as Primary")]
        public bool SameECData { get; set; }

        [DisplayName("Home Same as Primary")]
        public bool SameAddressData { get; set; }

        [DisplayName("School Name")]
        [StringLength(65)]
        public string SchoolName { get; set; }
        [DisplayName("Emergency Contact Email Address")]
        [DataType(DataType.EmailAddress)]

        public string EmergencyContactEmail { get; set; }
        [DisplayName("Emergency Contact Name")]
        [StringLength(65)]
        public string EmergencyContactName { get; set; }
        [DisplayName("Emergency Contact LastName")]
        [StringLength(65)]
        public string EmergencyContactLastName { get; set; }
        [DisplayName("Emergency Contact Relationship")]
        [StringLength(30)]
        public string EmergencyContactRelationship { get; set; }
        [DisplayName("Emergency Contact Phone")]
        public string EmergencyContactPhone { get; set; }
        [DisplayName("Emergency Contact Phone 2")]
        [DefaultValue(52)]
        public string EmergencyContactPhone2 { get; set; }
        public string EmergencyContactOfficePhone { get; set; }
        [DisplayName("Personal Mobile Phone")]
        [StringLength(30)]
        public string PersonalMobilePhone { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual IList<DependentFile> DependentFiles { get; set; }

        [EmbasssyNameDisplayName("{0} Resident Address")]
        [StringLength(30)]
        public string ResidentPhoneNumber { get; set; }

        [EmbasssyNameDisplayName("{0} Resident Phone Number")]
        [StringLength(30)]
        public string ResidentAddress { get; set; }
    }


    
}