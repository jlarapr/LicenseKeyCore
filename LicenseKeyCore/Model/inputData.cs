using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicenseKeyCore.License;

namespace LicenseKeyCore.Model
{
    public class inputData
    {
        public inputData()
        {
            ProductID = string.Empty;
            LicenseType = LicenseType.TRIAL;
            Name = "unknown";
            ExperienceDays = 30;
            Edition = Edition.STANDARD;
        }
        public String ProductID { get; set; }
        public LicenseType LicenseType { get; set; }
        public String  Name { get; set; }
        public int ExperienceDays { get; set; }
        public Edition Edition { get; set; }

    }
}
