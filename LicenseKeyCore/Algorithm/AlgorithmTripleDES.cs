using LicenseKeyCore.Encrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LicenseKeyCore.Algorithm
{
    public class AlgorithmTripleDES : Encryptor
    {
        public AlgorithmTripleDES(string secretkey, AlgorithmKeyType AlgType)
          : base(secretkey, AlgType)
        {
        }

        private byte[] Key { get; set; }

        private byte[] IV { get; set; }

        public override void GenerateKey(string secretKey, AlgorithmKeyType type)
        {
            this.Key = new byte[24];
            this.IV = new byte[8];
            byte[] bytes = Encoding.UTF8.GetBytes(secretKey);
            switch (type)
            {
                case AlgorithmKeyType.SHA1:
                    using (SHA1Managed shA1Managed = new SHA1Managed())
                    {
                        shA1Managed.ComputeHash(bytes);
                        byte[] hash = shA1Managed.Hash;
                        for (int index = 0; index < 20; ++index)
                            this.Key[index] = hash[index];
                        for (int index = 16; index > 8; --index)
                            this.IV[16 - index] = hash[index];
                        break;
                    }
                case AlgorithmKeyType.SHA256:
                    using (SHA256Managed shA256Managed = new SHA256Managed())
                    {
                        shA256Managed.ComputeHash(bytes);
                        byte[] hash = shA256Managed.Hash;
                        for (int index = 0; index < 24; ++index)
                            this.Key[index] = hash[index];
                        for (int index = 31; index >= 24; --index)
                            this.IV[31 - index] = hash[index];
                        break;
                    }
                case AlgorithmKeyType.SHA384:
                    using (SHA384Managed shA384Managed = new SHA384Managed())
                    {
                        shA384Managed.ComputeHash(bytes);
                        byte[] hash = shA384Managed.Hash;
                        for (int index = 0; index < 24; ++index)
                            this.Key[index] = hash[index];
                        for (int index = 47; index > 39; --index)
                            this.IV[47 - index] = hash[index];
                        break;
                    }
                case AlgorithmKeyType.SHA512:
                    using (SHA512Managed shA512Managed = new SHA512Managed())
                    {
                        shA512Managed.ComputeHash(bytes);
                        byte[] hash = shA512Managed.Hash;
                        for (int index = 0; index < 24; ++index)
                            this.Key[index] = hash[index];
                        for (int index = 63; index > 55; --index)
                            this.IV[63 - index] = hash[index];
                        break;
                    }
                case AlgorithmKeyType.MD5:
                    using (MD5CryptoServiceProvider cryptoServiceProvider = new MD5CryptoServiceProvider())
                    {
                        cryptoServiceProvider.ComputeHash(bytes);
                        byte[] hash = cryptoServiceProvider.Hash;
                        for (int index = 0; index < 16; ++index)
                            this.Key[index] = hash[index];
                        for (int index = 15; index >= 8; --index)
                            this.IV[15 - index] = hash[index];
                        break;
                    }
            }
        }

        public override byte[] Transform(byte[] data, TransformType type)
        {
            MemoryStream memoryStream = (MemoryStream)null;
            ICryptoTransform transform = (ICryptoTransform)null;
            TripleDES tripleDes = TripleDES.Create();
            try
            {
                memoryStream = new MemoryStream();
                tripleDes.Key = this.Key;
                tripleDes.IV = this.IV;
                transform = type != TransformType.ENCRYPT ? tripleDes.CreateDecryptor() : tripleDes.CreateEncryptor();
                if (data == null || data.Length == 0)
                    return (byte[])null;
                CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, transform, CryptoStreamMode.Write);
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();
                return memoryStream.ToArray();
            }
            catch (CryptographicException ex)
            {
                throw new CryptographicException(ex.Message);
            }
            finally
            {
                tripleDes?.Clear();
                transform?.Dispose();
                memoryStream.Close();
            }
        }
    }
}
