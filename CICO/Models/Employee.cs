using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Cico.Models.Helpers;

namespace Cico.Models    //  [DisplayName("")]   [StringLength()]
{
    public partial class Employee
    {
        public override void OnSave()
        {
            if (Dependents != null)
            {
                foreach (var dependent in Dependents)
                {
                    if (dependent.SameECData)
                    {
                        dependent.EmergencyContactName = EmergencyContactName;
                        dependent.EmergencyContactOfficePhone = EmergencyContactOfficePhone;
                        dependent.EmergencyContactPhone = EmergencyContactPhone;
                        dependent.EmergencyContactPhone2 = EmergencyContactPhone2;
                        dependent.EmergencyContactEmail = EmergencyContactEmail;
                    }

                    if (dependent.SameAddressData)
                    {
                        dependent.ResidentPhoneNumber = HomePhone;
                        dependent.ResidentAddress = ResidentAddress;

                    }

                }
            }
        }
    }

    public partial class Employee:EntityBaseWithKey
    {
        #region Column1 
        //1
        [DisplayName("First Name")]
        [StringLength(65)]
        public string FirstName { get; set; }
        //2
        [DisplayName("Middle Initial")]
        [StringLength(1)]
        public string MiddleInitial { get; set; }
        //3
        [DisplayName("Last Name")]
        [StringLength(65)]
        public string LastName { get; set; }
        //4
        [DisplayName("Email - Personal")]
        [DataType(DataType.EmailAddress)]
        public string PersonalEmail { get; set; }
        //5
        [DisplayName("Title/Salutation")]
        [StringLength(65)]
        public string Title { get; set; }
        //6
        [DisplayName("Date of Birth")]
        public DateTime? DateOfBirth { get; set; }
        //7
        [DisplayName("Nationality")]
        [StringLength(60)]
        public string Nationality { get; set; }
        //8
        [DisplayName("Diplomatic Title")]
        [StringLength(65)]
        public string DiplomaticTitle { get; set; }
        //9
        [DisplayName("Home Leave Address")]
        [StringLength(100)]
        public string HomeAddress { get; set; }
        //10
        [DisplayName("State of U.S. Legal Residence")]
        [StringLength(2)]
        public string LegalResidenceState { get; set; }
        //11
        [EmbasssyNameDisplayName("{0} Resident Phone Number")]
        public string HomePhone { get; set; }
        //12
        [DisplayName("Passport Number")]
        public string PassportNumber { get; set; }
        //13
        [DisplayName("Passport Expiration")]
        public DateTime? PassportExpiration { get; set; }
        //14
        [DisplayName("Passport Type")]
        public string PassportType { get; set; }
        //15
        [DisplayName("Office Mobile Phone")]
        public string OfficeCellPhone { get; set; }
        //16
        [DisplayName("Office Phone")]
        public string OfficePhone { get; set; }
        //17
        [DisplayName("Position Title")]
        [StringLength(65)]
        public string PositionTitle { get; set; }
        //18
        [EmbasssyNameDisplayName("{0} Resident Address")]
        public string ResidentAddress { get; set; }
        //19
        [DisplayName("In-Transit Phone")]
        public string PreArrivalPhone { get; set; }
        //20
        [DisplayName("Preferred Name")]
        [StringLength(65)]
        public string PreferredName { get; set; }
        //21 
        [DisplayName("Prior Post")]
        [StringLength(65)]
        public string PriorPost { get; set; }
        //22
        [DisplayName("EFT Information - Routing Number")]
        [StringLength(30)]
        public string RoutingNumber { get; set; }
        #endregion

        #region column2
        //1
        [DisplayName("Estimated Arrival Date")]
        public DateTime? ArrivalDate { get; set; }
        //2
        [DisplayName("Estimated Departure Date")]
        public DateTime? TourEndDate { get; set; }
        //3
        [DisplayName("Section")]
        public string Section { get; set; }
        //4
        [DisplayName("Office")]
        [StringLength(65)]
        public string Office { get; set; }
        //5
        [DisplayName("Visa expiration")]
        public DateTime? VisaExpiration { get; set; }
        //6
        [DisplayName("Visa Number")]
        public string VisaNumber { get; set; }
        //7
        [DisplayName("Email, Office")]
        [DataType(DataType.EmailAddress)]
        public string WorkEmail { get; set; }
        //8
        [DisplayName("Agency")]
        [StringLength(65)]
        public string Agency { get; set; }
        //9
        [DisplayName("EFT Information - Bank Account")]
        [StringLength(30)]
        public string BankAccount { get; set; }
        //10
        [DisplayName("Personal Mobile Phone")]
        public string CellPhone { get; set; }
        //11
        [DisplayName("Emergency Contact Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmergencyContactEmail { get; set; }
        //12
        [DisplayName("Emergency Contact Name")]
        [StringLength(65)]
        public string EmergencyContactName { get; set; }
        //13
        [DisplayName("Emergency Contact Office Phone")]
        public string EmergencyContactOfficePhone { get; set; }
        //14
        [DisplayName("Emergency Contact Home Phone")]
        public string EmergencyContactPhone { get; set; }
        //15
        [DisplayName("Emergency Contact Mobile Phone")]
        public string EmergencyContactPhone2 { get; set; }
        //16
        [DisplayName("Emergency Contact Relationship")]
        [StringLength(30)]
        public string EmergencyContactRelationship { get; set; }
        //17
        [DisplayName("Radio Call Sign")]
        [StringLength(65)]
        public string RadioCallSign { get; set; }
        //18
        [DisplayName("Clearance Level")]
        [StringLength(65)]
        public string ClearanceLevel { get; set; }
        //19
        [DisplayName("License Plate Info")]
        [StringLength(65)]
        public string LicensePlate { get; set; }
        //20
        [DisplayName("SSN")]
        [StringLength(11)]
        public string SSN { get; set; }
        //21
        [DisplayName("Gender")]
        [StringLength(65)]
        public string Gender { get; set; }
        #endregion

        [DisplayName("Safe Word")]
        [StringLength(50)]
        public string SafeWord
        {
            get;
            set;
        }

        [DisplayName("Proxy Email")]
        [DataType(DataType.EmailAddress)]
        public string ProxyEmail { get; set; }
     
        [Required(ErrorMessage = "Domain user id is required")]
        public string UserId { get; set; }



        public virtual ICollection<CheckListSession> CheckListSessions { get; set; }
        public virtual ICollection<Dependent> Dependents { get; set; }
        public virtual Staff Proxy { get; set; }
        public virtual IList<SentBoxItem> SentBoxItems { get; set; }

       
    }
}