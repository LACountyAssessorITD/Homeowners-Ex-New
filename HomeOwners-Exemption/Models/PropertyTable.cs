using System;
using System.Collections.Generic;

namespace HomeOwners_Exemption.Models
{
    public partial class PropertyTable
    {
        public long Id { get; set; }
        public long? Ain { get; set; }
        public string StreetName { get; set; }
        public string Apt { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int? Zip { get; set; }
        public string OwnerName { get; set; }
        public decimal? OwnerSsn { get; set; }
        public DateTime? DateAcquired { get; set; }
        public DateTime? DateOccupied { get; set; }
        public DateTime? DateMovedOut { get; set; }
    }
}
