using LicenseKeyCore.Algorithm;
using LicenseKeyCore.Algorithm.Enums;


namespace LicenseKeyCore.Encrypt
{
    public class ObjectEncryptor
    {
        protected Encryptor Encryptor { get; set; }

        public virtual string ObjectCryptography(
          string secretKey,
          string data,
          TransformType type,
          AlgorithmType algType,
          AlgorithmKeyType algKeyType)
        {
            string str = (string)null;
            switch (algType)
            {
                case AlgorithmType.Rijndael:
                    this.Encryptor = (Encryptor) new AlgorithmRijndael(secretKey, algKeyType);
                    str = this.Encryptor.ObjectCryptography(data, type);
                    break;
                case AlgorithmType.TripleDES:
                    this.Encryptor = (Encryptor) new AlgorithmTripleDES(secretKey, algKeyType);
                    str = this.Encryptor.ObjectCryptography(data, type);
                    break;
                case AlgorithmType.DES:
                    this.Encryptor = (Encryptor) new AlgorithmDES(secretKey, algKeyType);
                    str = this.Encryptor.ObjectCryptography(data, type);
                    break;
            }
            return str;
        }
    }
}
