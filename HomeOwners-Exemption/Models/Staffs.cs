﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace HomeOwners_Exemption.Models
{
    public class Staffs
    {
        [Key]
        public string EmployeeID { get; set; }
        public string Users { get; set; }
        public Int32 Unworked { get; set; }
        public Int32 Worked { get; set; }
    }
}
