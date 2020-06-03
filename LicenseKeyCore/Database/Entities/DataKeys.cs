using System;
using LicenseKeyCore.License;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicenseKeyCore.Database;

namespace LicenseKeyCore.Database.Entities
{
    public class DataKeys
    {
        public int DataKeysId { get; set; }
        public string Name { get; set; }
        public String ProductID { get; set; }
        public LicenseType LicenseType { get; set; }
        public DateTime ExpereienceDays { get; set; }
        public String ProducKey { get; set; }
        public Edition Edition { get; set; }
    }
}
