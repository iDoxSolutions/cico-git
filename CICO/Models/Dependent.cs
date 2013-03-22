using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models    
{
    public class Dependent:EntityBaseWithKey
    {
        [DisplayName("Given Name(s)")]
        [StringLength(65)]
        public string GivenName { get; set; }
        [DisplayName("Surname")]
        [StringLength(66)]
        public string Surname { get; set; }
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
        [StringLength(10)]
        public string Nationality { get; set; }
        public string HomePhone { get; set; }
        [DisplayName("Mexico - Personal Cell Phone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        // TODO: prefix phone with 044
        public string CellPhone { get; set; }
        [DisplayName("Residence at Post - Home Phone 2")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        // TODO: prefix phone with 044
        public string HomePhone2 { get; set; }
        public string AgencyOrSection { get; set; }
        [DisplayName("Nationality")]
        [StringLength(100)]
        public string HomeAddress { get; set; }
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
        [RegularExpression(@"[^a-zA-Z0-9]+", ErrorMessage = "Visa Number can not contain special characters.")]
        public string VisaNumber { get; set; }
        [DisplayName("Visa expiration")]
        public DateTime? VisaExpiration { get; set; }
        public string PostOfAssignment { get; set; }
        [DisplayName("Office Phone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string OfficePhone { get; set; }
        public string Extension { get; set; }
        [DisplayName("Emergency Contact Email Address")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$", ErrorMessage = "Invalid e-mail.")]
        public string EmergencyContactEmail { get; set; }
        [DisplayName("Emergency Contact Given Name(s)")]
        [StringLength(65)]
        public string EmergencyContactGivenName { get; set; }
        [DisplayName("Emergency Contact Surname")]
        [StringLength(65)]
        public string EmergencyContactSurname { get; set; }
        [DisplayName("Emergency Contact Relationship")]
        [StringLength(30)]
        public string EmergencyContactRelationship { get; set; }
        [DisplayName("Emergency Contact Phone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string EmergencyContactPhone { get; set; }
        [DisplayName("Emergency Contact Phone 2")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string EmergencyContactPhone2 { get; set; }
        public string EmergencyContactOfficePhone { get; set; }
        public virtual Employee Employee { get; set; }
    }
}