using System;

namespace LicenseKeyCore.License
{
    public class LicenseInfo
    {
        public string FullName { get; set; }

        public string ProductKey { get; set; }

        public int Day { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public DateTime CheckDate
        {
            get
            {
                return new DateTime(this.Year, this.Month, this.Day).Date;
            }
        }

        public string Data
        {
            get
            {
                return string.Format("{0}#{1}#{2}#{3}#{4}", (object)this.FullName, (object)this.ProductKey, (object)this.Day, (object)this.Month, (object)this.Year);
            }
        }
    }
}
