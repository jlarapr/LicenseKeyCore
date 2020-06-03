using LicenseKeyCore.Algorithm;
using LicenseKeyCore.Algorithm.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;

namespace LicenseKeyCore.Encrypt
{
    public abstract class Encryptor
    {
        public Encryptor(string secretKey, AlgorithmKeyType AlgType)
        {
            this.GenerateKey(secretKey, AlgType);
        }

        public Encryptor()
        {
          
        }

        public string ObjectCryptography(string data, TransformType type)
        {
            string str = (string)null;
            try
            {
                if (data.Length > 0)
                {
                    switch (type)
                    {
                        case TransformType.ENCRYPT:
                            str = Convert.ToBase64String(this.Transform(Encoding.UTF8.GetBytes(data), TransformType.ENCRYPT));
                            break;
                        case TransformType.DECRYPT:
                            str = Encoding.UTF8.GetString(this.Transform(Convert.FromBase64String(data), TransformType.DECRYPT));
                            break;
                    }
                }
            }
            catch (CryptographicException ex)
            {
                throw ex;
            }
            return str;
        }

        public byte[] ObjectCryptography(byte[] data, TransformType type)
        {
            byte[] numArray = (byte[])null;
            try
            {
                if (data != null)
                {
                    if (data.Length != 0)
                    {
                        switch (type)
                        {
                            case TransformType.ENCRYPT:
                                numArray = this.Transform(data, TransformType.ENCRYPT);
                                break;
                            case TransformType.DECRYPT:
                                numArray = this.Transform(data, TransformType.DECRYPT);
                                break;
                        }
                    }
                }
            }
            catch (CryptographicException ex)
            {
                throw ex;
            }
            return numArray;
        }

        public string Encrypt(string data)
        {
            try
            {
                return data.Length > 0 ? Convert.ToBase64String(this.Transform(Encoding.UTF8.GetBytes(data), TransformType.ENCRYPT)) : (string)null;
            }
            catch (CryptographicException ex)
            {
                throw ex;
            }
        }

        public string Decrypt(string data)
        {
            try
            {
                return data.Length > 0 ? Encoding.UTF8.GetString(this.Transform(Convert.FromBase64String(data), TransformType.DECRYPT)) : (string)null;
            }
            catch (CryptographicException ex)
            {
                throw ex;
            }
        }

        public abstract void GenerateKey(string secretKey, AlgorithmKeyType type);

        public abstract byte[] Transform(byte[] data, TransformType type);

    }
}
