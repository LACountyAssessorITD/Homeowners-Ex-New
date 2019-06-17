using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeOwners_Exemption.Models
{
    public class UserStaff
    {
        [Key]
        public string EmployeeID { get; set; }
        public string Users { get; set; }
    }
}
