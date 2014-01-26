<<<<<<< HEAD
﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models.Authentication
{
    public class InitModel
    {
        [DisplayName("Arrival Date" + "*Enter best estimate. Can be changed later")]
        [Required]
        public DateTime ArrivalDate { get; set; }
        [RegularExpression("^(3[01]|[12][0-9]|0[1-9])/(1[0-2]|0[1-9])/[0-9]{4}$", ErrorMessage = "Invalid format for Arrival Date.")]
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
=======
﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cico.Models.Authentication
{
    public class InitModel
    {
         [DisplayName("Arrival Date" + "*Enter best estimate. Can be changed later")]
       //  [RegularExpression(@"^(3[01]|[12][0-9]|0[1-9])/(1[0-2]|0[1-9])/[0-9]{4}$", ErrorMessage = "Invalid format for Arrival Date.")]
      //  [Required]
        public DateTime ArrivalDate { get; set; }
        
       
        [DisplayName("First Name")]
         [StringLength(65)]
   //      [Required]
        public string FirstName { get; set; }
       
        [DisplayName("Last Name")]
        [StringLength(65)]
      //  [Required]
        public string LastName { get; set; }
        [DisplayName("Email")]
  //      [Required]
        [DataType(DataType.EmailAddress)]
        //[RegularExpression(@"^[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?$", ErrorMessage = "Invalid e-mail.")]
        public string EmailAddress { get; set; }
        
        
        public int? EmpId { get; set; }
    }
}
>>>>>>> 66858e2356cd8f1f9b5604d77e3591930568bbf8
