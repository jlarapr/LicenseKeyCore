using System.Management;
using System.Security.Cryptography;
using System.Text;

namespace LicenseKeyCore.License
{
    public class ComputerInfo
    {
        public static string GetComputerId()
        {
            return ComputerInfo.GetHash("CPU >> " + ComputerInfo.CpuId() + "\nBIOS >> " + ComputerInfo.BiosId() + "\nBASE >> " + ComputerInfo.BaseId());
        }

        private static string GetHash(string s)
        {
            return ComputerInfo.GetHexString(new MD5CryptoServiceProvider().ComputeHash(new ASCIIEncoding().GetBytes(s)));
        }

        private static string GetHexString(byte[] bt)
        {
            string str1 = string.Empty;
            for (int index = 0; index < bt.Length; ++index)
            {
                int num1 = (int)bt[index];
                int num2 = num1 & 15;
                int num3 = num1 >> 4 & 15;
                string str2 = num3 <= 9 ? str1 + num3.ToString() : str1 + ((char)(num3 - 10 + 65)).ToString();
                str1 = num2 <= 9 ? str2 + num2.ToString() : str2 + ((char)(num2 - 10 + 65)).ToString();
                if (index + 1 != bt.Length && (index + 1) % 2 == 0)
                    str1 += "-";
            }
            return str1;
        }

        private static string identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string str = "";
            foreach (ManagementObject instance in new ManagementClass(wmiClass).GetInstances())
            {
                if (instance[wmiMustBeTrue].ToString() == "True")
                {
                    if (str == "")
                    {
                        try
                        {
                            str = instance[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return str;
        }

        private static string identifier(string wmiClass, string wmiProperty)
        {
            string str = "";
            try
            {
                foreach (ManagementObject instance in new ManagementClass(wmiClass).GetInstances())
                {
                    if (str == "")
                    {
                        try
                        {
                            str = instance[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch (System.Exception)
            {
            }
            return str;
        }

        private static string CpuId()
        {
            string str1 = string.Empty;
            try
            {
                str1 = ComputerInfo.identifier("Win32_Processor", "UniqueId");
                if (str1 == "")
                {
                    str1 = ComputerInfo.identifier("Win32_Processor", "ProcessorId");
                    if (str1 == "")
                    {
                        string str2 = ComputerInfo.identifier("Win32_Processor", "Name");
                        if (str2 == "")
                            str2 = ComputerInfo.identifier("Win32_Processor", "Manufacturer");
                        str1 = str2 + ComputerInfo.identifier("Win32_Processor", "MaxClockSpeed");
                    }
                }
            }
            catch (System.Exception)
            {
            }
            return str1;
        }

        private static string BiosId()
        {
            return ComputerInfo.identifier("Win32_BIOS", "Manufacturer") + ComputerInfo.identifier("Win32_BIOS", "SMBIOSBIOSVersion") + ComputerInfo.identifier("Win32_BIOS", "IdentificationCode") + ComputerInfo.identifier("Win32_BIOS", "SerialNumber") + ComputerInfo.identifier("Win32_BIOS", "ReleaseDate") + ComputerInfo.identifier("Win32_BIOS", "Version");
        }

        private static string DiskId()
        {
            return ComputerInfo.identifier("Win32_DiskDrive", "Model") + ComputerInfo.identifier("Win32_DiskDrive", "Manufacturer") + ComputerInfo.identifier("Win32_DiskDrive", "Signature") + ComputerInfo.identifier("Win32_DiskDrive", "TotalHeads");
        }

        private static string BaseId()
        {
            return ComputerInfo.identifier("Win32_BaseBoard", "Model") + ComputerInfo.identifier("Win32_BaseBoard", "Manufacturer") + ComputerInfo.identifier("Win32_BaseBoard", "Name") + ComputerInfo.identifier("Win32_BaseBoard", "SerialNumber");
        }

        private static string VideoId()
        {
            return ComputerInfo.identifier("Win32_VideoController", "DriverVersion") + ComputerInfo.identifier("Win32_VideoController", "Name");
        }

        private static string MacId()
        {
            return ComputerInfo.identifier("Win32_NetworkAdapterConfiguration", "MACAddress", "IPEnabled");
        }
    }
}
