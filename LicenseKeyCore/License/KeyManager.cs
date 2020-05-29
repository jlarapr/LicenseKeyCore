using LicenseKeyCore.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace LicenseKeyCore.License
{
    public class KeyManager
    {
        private string EncryptionKey = string.Empty;

        public KeyManager(string encryptionKey)
        {
            this.EncryptionKey = encryptionKey;
        }

        public bool GenerateKey(KeyValuesClass KeyValues, ref string ProductKey)
        {

            //RSACryptoServiceProvider cryptoServiceProvider = null;

            //try
            //{//windows

            //    cryptoServiceProvider = new RSACryptoServiceProvider(1024, new CspParameters()
            //    {
            //        Flags = CspProviderFlags.UseMachineKeyStore
            //    });

            //}
            //catch (Exception)
            //{//docker linux
            //    cryptoServiceProvider = new RSACryptoServiceProvider();
            //}

            try
            {
                string empty = string.Empty;
                byte[] bytes1 = BitConverter.GetBytes((short)KeyValues.Header);
                byte[] bytes2 = BitConverter.GetBytes((short)KeyValues.ProductCode);
                byte[] bytes3 = BitConverter.GetBytes((short)KeyValues.Version);
                byte[] bytes4 = BitConverter.GetBytes((short)(byte)KeyValues.Edition);
                byte[] bytes5 = BitConverter.GetBytes((short)(byte)KeyValues.Type);
                DateTime expiration = KeyValues.Expiration;
                string str1 = expiration.Day.ToString().PadLeft(2, '0');
                string str2 = empty + str1;
                expiration = KeyValues.Expiration;
                string str3 = expiration.Month.ToString().PadLeft(2, '0');
                string str4 = str2 + str3;
                expiration = KeyValues.Expiration;
                string str5 = expiration.Year.ToString();
                byte[] bytes6 = BitConverter.GetBytes(Convert.ToUInt32(str4 + str5));
                byte[] bytes7 = BitConverter.GetBytes(new Random().Next(0, (int)byte.MaxValue));
                byte[] bytes8 = BitConverter.GetBytes((short)KeyValues.Footer);
                byte[] array1;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    memoryStream.Write(bytes1, 0, 1);
                    memoryStream.Write(bytes2, 0, 1);
                    memoryStream.Write(bytes3, 0, 1);
                    memoryStream.Write(bytes4, 0, 1);
                    memoryStream.Write(bytes5, 0, 1);
                    memoryStream.Write(bytes6, 0, 4);
                    memoryStream.Write(bytes7, 0, 1);
                    memoryStream.Write(bytes8, 0, 1);
                    array1 = memoryStream.ToArray();
                }
                byte[] buffer = new RijndaelManaged().CreateEncryptor(new byte[32]
                {
          (byte) 9,
          (byte) 192,
          (byte) 133,
          (byte) 135,
          (byte) 96,
          (byte) 254,
          (byte) 70,
          (byte) 21,
          (byte) 34,
          (byte) 88,
          (byte) 251,
          (byte) 164,
          (byte) 153,
          (byte) 21,
          (byte) 202,
          (byte) 129,
          (byte) 146,
          (byte) 199,
          (byte) 146,
          (byte) 21,
          (byte) 169,
          (byte) 72,
          (byte) 3,
          (byte) 36,
          (byte) 231,
          (byte) 22,
          (byte) 209,
          (byte) 188,
          (byte) 118,
          (byte) 36,
          (byte) 48,
          (byte) 194
                }, new byte[16]
                {
          (byte) 148,
          (byte) 5,
          (byte) 58,
          (byte) 123,
          (byte) 59,
          (byte) 115,
          (byte) 65,
          (byte) 151,
          (byte) 197,
          (byte) 88,
          (byte) 86,
          (byte) 179,
          (byte) 206,
          (byte) 85,
          (byte) 34,
          (byte) 76
                }).TransformFinalBlock(array1, 0, array1.Length);
                byte[] array2;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    memoryStream.Write(buffer, 0, 8);
                    memoryStream.Write(bytes1, 0, 1);
                    memoryStream.Write(bytes7, 0, 1);
                    memoryStream.Write(buffer, 8, buffer.Length - 8);
                    array2 = memoryStream.ToArray();
                }
                string base32String = Base32Converter.ToBase32String(array2);
                ProductKey = string.Format("{0}-{1}-{2}-{3}-{4}-{5}", (object)base32String.Substring(0, 5), (object)base32String.Substring(5, 5), (object)base32String.Substring(10, 5), (object)base32String.Substring(15, 5), (object)base32String.Substring(20, 5), (object)base32String.Substring(25, 5));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DisassembleKey(string ProductKey, ref KeyValuesClass KeyValues)
        {
            //RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider(1024, new CspParameters()
            //{
            //    Flags = CspProviderFlags.UseMachineKeyStore
            //});
            try
            {
                if (string.IsNullOrEmpty(ProductKey))
                    throw new ArgumentNullException("Product Key is null or empty.");
                if (ProductKey.Length != 35)
                    throw new ArgumentException("Product key is invalid.");
                byte[] buffer1 = Base32Converter.FromBase32String(ProductKey.Replace("-", ""));
                byte[] buffer2 = new byte[2];
                byte[] buffer3 = new byte[1];
                byte[] numArray = new byte[16];
                using (MemoryStream memoryStream = new MemoryStream(buffer1))
                {
                    memoryStream.Read(numArray, 0, 8);
                    memoryStream.Read(buffer2, 0, 1);
                    memoryStream.Read(buffer3, 0, 1);
                    memoryStream.Read(numArray, 8, numArray.Length - 8);
                    memoryStream.ToArray();
                }
                byte[] buffer4 = new RijndaelManaged().CreateDecryptor(new byte[32]
                {
          (byte) 9,
          (byte) 192,
          (byte) 133,
          (byte) 135,
          (byte) 96,
          (byte) 254,
          (byte) 70,
          (byte) 21,
          (byte) 34,
          (byte) 88,
          (byte) 251,
          (byte) 164,
          (byte) 153,
          (byte) 21,
          (byte) 202,
          (byte) 129,
          (byte) 146,
          (byte) 199,
          (byte) 146,
          (byte) 21,
          (byte) 169,
          (byte) 72,
          (byte) 3,
          (byte) 36,
          (byte) 231,
          (byte) 22,
          (byte) 209,
          (byte) 188,
          (byte) 118,
          (byte) 36,
          (byte) 48,
          (byte) 194
                }, new byte[16]
                {
          (byte) 148,
          (byte) 5,
          (byte) 58,
          (byte) 123,
          (byte) 59,
          (byte) 115,
          (byte) 65,
          (byte) 151,
          (byte) 197,
          (byte) 88,
          (byte) 86,
          (byte) 179,
          (byte) 206,
          (byte) 85,
          (byte) 34,
          (byte) 76
                }).TransformFinalBlock(numArray, 0, numArray.Length);
                byte[] buffer5 = new byte[2];
                byte[] buffer6 = new byte[2];
                byte[] buffer7 = new byte[2];
                byte[] buffer8 = new byte[2];
                byte[] buffer9 = new byte[2];
                byte[] buffer10 = new byte[4];
                byte[] buffer11 = new byte[2];
                byte[] buffer12 = new byte[2];
                using (MemoryStream memoryStream = new MemoryStream(buffer4))
                {
                    memoryStream.Read(buffer5, 0, 1);
                    memoryStream.Read(buffer6, 0, 1);
                    memoryStream.Read(buffer7, 0, 1);
                    memoryStream.Read(buffer8, 0, 1);
                    memoryStream.Read(buffer9, 0, 1);
                    memoryStream.Read(buffer10, 0, 4);
                    memoryStream.Read(buffer11, 0, 1);
                    memoryStream.Read(buffer12, 0, 1);
                }
                KeyValuesClass keyValuesClass = new KeyValuesClass();
                keyValuesClass.Header = (byte)BitConverter.ToInt16(buffer5, 0);
                keyValuesClass.ProductCode = (byte)BitConverter.ToInt16(buffer6, 0);
                keyValuesClass.Version = (byte)BitConverter.ToInt16(buffer7, 0);
                keyValuesClass.Edition = (Edition)BitConverter.ToInt16(buffer8, 0);
                keyValuesClass.Type = (LicenseType)BitConverter.ToInt16(buffer9, 0);
                if (keyValuesClass.Type == LicenseType.TRIAL)
                {
                    string str = BitConverter.ToUInt32(buffer10, 0).ToString().PadLeft(8, '0');
                    keyValuesClass.Expiration = new DateTime((int)Convert.ToInt16(str.Substring(4, 4)), (int)Convert.ToInt16(str.Substring(2, 2)), (int)Convert.ToInt16(str.Substring(0, 2)));
                }
                keyValuesClass.Footer = (byte)BitConverter.ToInt16(buffer12, 0);
                KeyValues = keyValuesClass;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ValidKey(ref string ProductKey)
        {
            using (new RSACryptoServiceProvider())
            {
                try
                {
                    if (string.IsNullOrEmpty(ProductKey))
                        throw new ArgumentNullException("Product Key is null or empty.");
                    if (ProductKey.Length != 35)
                        throw new ArgumentException("Product key is invalid.");
                    byte[] buffer1 = Base32Converter.FromBase32String(ProductKey.Replace("-", ""));
                    byte[] buffer2 = new byte[1];
                    byte[] buffer3 = new byte[1];
                    byte[] numArray = new byte[16];
                    using (MemoryStream memoryStream = new MemoryStream(buffer1))
                    {
                        memoryStream.Read(numArray, 0, 8);
                        memoryStream.Read(buffer2, 0, 1);
                        memoryStream.Read(buffer3, 0, 1);
                        memoryStream.Read(numArray, 8, numArray.Length - 8);
                        memoryStream.ToArray();
                    }
                    new RijndaelManaged().CreateDecryptor(new byte[32]
                    {
            (byte) 9,
            (byte) 192,
            (byte) 133,
            (byte) 135,
            (byte) 96,
            (byte) 254,
            (byte) 70,
            (byte) 21,
            (byte) 34,
            (byte) 88,
            (byte) 251,
            (byte) 164,
            (byte) 153,
            (byte) 21,
            (byte) 202,
            (byte) 129,
            (byte) 146,
            (byte) 199,
            (byte) 146,
            (byte) 21,
            (byte) 169,
            (byte) 72,
            (byte) 3,
            (byte) 36,
            (byte) 231,
            (byte) 22,
            (byte) 209,
            (byte) 188,
            (byte) 118,
            (byte) 36,
            (byte) 48,
            (byte) 194
                    }, new byte[16]
                    {
            (byte) 148,
            (byte) 5,
            (byte) 58,
            (byte) 123,
            (byte) 59,
            (byte) 115,
            (byte) 65,
            (byte) 151,
            (byte) 197,
            (byte) 88,
            (byte) 86,
            (byte) 179,
            (byte) 206,
            (byte) 85,
            (byte) 34,
            (byte) 76
                    }).TransformFinalBlock(numArray, 0, numArray.Length);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public int LoadSuretyFile(string filename, ref LicenseInfo LicInfo)
        {
            if (!File.Exists(filename))
                return -1;
            ObjectPacketLicense objectPacketLicense = new ObjectPacketLicense(filename);
            try
            {
                LicenseInfo licenseInfo = objectPacketLicense.ReadLicense(this.EncryptionKey, (byte)1);
                if (licenseInfo == null)
                    return -3;
                LicInfo = licenseInfo;
                return 1;
            }
            catch
            {
                return -2;
            }
        }

        public bool SaveSuretyFile(string filename, LicenseInfo licInfo)
        {
            try
            {
                new ObjectPacketLicense(filename).SaveLicenseToFile(this.EncryptionKey, licInfo, (byte)1, LicenseKeyCore.Algorithm.AlgorithmType.Rijndael, AlgorithmKeyType.MD5);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
