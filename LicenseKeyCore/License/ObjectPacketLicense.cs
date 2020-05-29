using LicenseKeyCore.Algorithm;
using LicenseKeyCore.Encrypt;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace LicenseKeyCore.License
{
    public class ObjectPacketLicense : ObjectEncryptor
    {
        private string _fileName;
        private byte[] _data;

        public ObjectPacketLicense()
        {
        }

        public ObjectPacketLicense(byte[] data)
        {
            this._data = data;
        }

        public ObjectPacketLicense(string fileName)
        {
            this._fileName = fileName;
        }

        public byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return (byte[])null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                new BinaryFormatter().Serialize((Stream)memoryStream, obj);
                return memoryStream.ToArray();
            }
        }

        public object ByteArrayToObject(byte[] arrBytes)
        {
            using (MemoryStream memoryStream1 = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                memoryStream1.Write(arrBytes, 0, arrBytes.Length);
                memoryStream1.Seek(0L, SeekOrigin.Begin);
                MemoryStream memoryStream2 = memoryStream1;
                return binaryFormatter.Deserialize((Stream)memoryStream2);
            }
        }

        protected virtual void WriteFile(
          LicenseInfo licInfo,
          byte version,
          AlgorithmType algType,
          AlgorithmKeyType algKeyType)
        {
            using (FileStream fileStream = new FileStream(this._fileName, FileMode.Create))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter((Stream)fileStream))
                {
                    try
                    {
                        binaryWriter.Write((short)5);
                        binaryWriter.Write(version);
                        binaryWriter.Write(Convert.ToByte((int)algType));
                        binaryWriter.Write(Convert.ToByte((int)algKeyType));
                        binaryWriter.Write(licInfo.Data);
                        binaryWriter.Flush();
                    }
                    catch (IOException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        binaryWriter.Close();
                        fileStream.Close();
                    }
                }
            }
        }

        protected virtual void WriteFile(
          LicenseInfo licInfo,
          byte version,
          Encryptor encryptor,
          AlgorithmType algType,
          AlgorithmKeyType algKeyType)
        {
            using (FileStream fileStream = new FileStream(this._fileName, FileMode.Create))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter((Stream)fileStream))
                {
                    try
                    {
                        binaryWriter.Write((short)5);
                        binaryWriter.Write(version);
                        binaryWriter.Write(Convert.ToByte((int)algType));
                        binaryWriter.Write(Convert.ToByte((int)algKeyType));
                        binaryWriter.Write(encryptor.ObjectCryptography(licInfo.Data, TransformType.ENCRYPT));
                        binaryWriter.Flush();
                    }
                    catch (IOException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        binaryWriter.Close();
                        fileStream.Close();
                    }
                }
            }
        }

        protected virtual byte[] WriteStream(
          LicenseInfo licInfo,
          byte version,
          Encryptor encryptor,
          AlgorithmType algType,
          AlgorithmKeyType algKeyType)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter binaryWriter = new BinaryWriter((Stream)memoryStream))
                {
                    try
                    {
                        binaryWriter.Write((short)5);
                        binaryWriter.Write(version);
                        binaryWriter.Write(Convert.ToByte((int)algType));
                        binaryWriter.Write(Convert.ToByte((int)algKeyType));
                        binaryWriter.Write(encryptor.ObjectCryptography(licInfo.Data, TransformType.ENCRYPT));
                        binaryWriter.Flush();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        binaryWriter.Close();
                        memoryStream.Close();
                    }
                }
                return memoryStream.ToArray();
            }
        }

        protected virtual byte[] WriteStream(
          LicenseInfo licInfo,
          byte version,
          AlgorithmType algType,
          AlgorithmKeyType algKeyType)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter binaryWriter = new BinaryWriter((Stream)memoryStream))
                {
                    try
                    {
                        binaryWriter.Write((short)5);
                        binaryWriter.Write(version);
                        binaryWriter.Write(Convert.ToByte((int)algType));
                        binaryWriter.Write(Convert.ToByte((int)algKeyType));
                        binaryWriter.Write(licInfo.ProductKey);
                        binaryWriter.Flush();
                    }
                    catch (IOException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        binaryWriter.Close();
                        memoryStream.Close();
                    }
                }
                return memoryStream.ToArray();
            }
        }

        protected virtual string ReadFile(
          short lHeader,
          byte version,
          out AlgorithmType algType,
          out AlgorithmKeyType algKeyType)
        {
            using (FileStream fileStream = new FileStream(this._fileName, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader((Stream)fileStream))
                {
                    string str = (string)null;
                    algType = AlgorithmType.None;
                    algKeyType = AlgorithmKeyType.None;
                    try
                    {
                        if ((int)binaryReader.ReadInt16() == (int)lHeader)
                        {
                            if ((int)binaryReader.ReadByte() == (int)version)
                            {
                                algType = (AlgorithmType)binaryReader.ReadByte();
                                algKeyType = (AlgorithmKeyType)binaryReader.ReadByte();
                                str = binaryReader.ReadString();
                            }
                        }
                    }
                    catch (IOException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        binaryReader.Close();
                        fileStream.Close();
                    }
                    return str;
                }
            }
        }

        protected virtual string ReadStream(
          short lHeader,
          byte version,
          out AlgorithmType algType,
          out AlgorithmKeyType algKeyType)
        {
            using (MemoryStream memoryStream = new MemoryStream(this._data))
            {
                using (BinaryReader binaryReader = new BinaryReader((Stream)memoryStream))
                {
                    string str = (string)null;
                    algType = AlgorithmType.None;
                    algKeyType = AlgorithmKeyType.None;
                    try
                    {
                        if ((int)binaryReader.ReadInt16() == (int)lHeader)
                        {
                            if ((int)binaryReader.ReadByte() == (int)version)
                            {
                                algType = (AlgorithmType)binaryReader.ReadByte();
                                algKeyType = (AlgorithmKeyType)binaryReader.ReadByte();
                                str = binaryReader.ReadString();
                            }
                        }
                    }
                    catch (IOException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        binaryReader.Close();
                        memoryStream.Close();
                    }
                    return str;
                }
            }
        }

        public virtual bool IsValidFileFormat(short lHeader, byte version)
        {
            using (FileStream fileStream = new FileStream(this._fileName, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader((Stream)fileStream))
                {
                    try
                    {
                        if ((int)binaryReader.ReadInt16() == (int)lHeader)
                        {
                            if ((int)binaryReader.ReadByte() == (int)version)
                                return true;
                        }
                    }
                    catch (IOException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        binaryReader.Close();
                        fileStream.Close();
                    }
                    return false;
                }
            }
        }

        public virtual bool IsValidStreamFormat(short lHeader, byte version)
        {
            using (MemoryStream memoryStream = new MemoryStream(this._data))
            {
                using (BinaryReader binaryReader = new BinaryReader((Stream)memoryStream))
                {
                    try
                    {
                        if ((int)binaryReader.ReadInt16() == (int)lHeader)
                        {
                            if ((int)binaryReader.ReadByte() == (int)version)
                                return true;
                        }
                    }
                    catch (IOException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        binaryReader.Close();
                        memoryStream.Close();
                    }
                    return false;
                }
            }
        }

        public virtual bool IsValidFileFormat(
          short lHeader,
          byte version,
          out AlgorithmType algType,
          out AlgorithmKeyType algKeyType)
        {
            using (FileStream fileStream = new FileStream(this._fileName, FileMode.Open))
            {
                using (BinaryReader binaryReader = new BinaryReader((Stream)fileStream))
                {
                    algType = AlgorithmType.None;
                    algKeyType = AlgorithmKeyType.None;
                    try
                    {
                        if ((int)binaryReader.ReadInt16() == (int)lHeader)
                        {
                            if ((int)binaryReader.ReadByte() == (int)version)
                            {
                                algType = (AlgorithmType)binaryReader.ReadByte();
                                algKeyType = (AlgorithmKeyType)binaryReader.ReadByte();
                                return true;
                            }
                        }
                    }
                    catch (IOException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        binaryReader.Close();
                        fileStream.Close();
                    }
                    return false;
                }
            }
        }

        public virtual bool IsValidStreamFormat(
          short lHeader,
          byte version,
          out AlgorithmType algType,
          out AlgorithmKeyType algKeyType)
        {
            using (MemoryStream memoryStream = new MemoryStream(this._data))
            {
                using (BinaryReader binaryReader = new BinaryReader((Stream)memoryStream))
                {
                    algType = AlgorithmType.None;
                    algKeyType = AlgorithmKeyType.None;
                    try
                    {
                        if ((int)binaryReader.ReadInt16() == (int)lHeader)
                        {
                            if ((int)binaryReader.ReadByte() == (int)version)
                            {
                                algType = (AlgorithmType)binaryReader.ReadByte();
                                algKeyType = (AlgorithmKeyType)binaryReader.ReadByte();
                                return true;
                            }
                        }
                    }
                    catch (IOException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        binaryReader.Close();
                        memoryStream.Close();
                    }
                    return false;
                }
            }
        }

        public virtual LicenseInfo ReadLicense(string secretKey, byte version)
        {
            string str = (string)null;
            AlgorithmType algType;
            AlgorithmKeyType algKeyType;
            string data = this._data == null ? this.ReadFile((short)5, version, out algType, out algKeyType) : this.ReadStream((short)5, version, out algType, out algKeyType);
            if (data != null)
            {
                switch (algType)
                {
                    case AlgorithmType.None:
                        str = data;
                        break;
                    case AlgorithmType.Rijndael:
                        try
                        {
                            this.Encryptor = (Encryptor)new AlgorithmRijndael(secretKey, algKeyType);
                            str = this.Encryptor.ObjectCryptography(data, TransformType.DECRYPT);
                            break;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    case AlgorithmType.TripleDES:
                        try
                        {
                            this.Encryptor = (Encryptor)new AlgorithmTripleDES(secretKey, algKeyType);
                            str = this.Encryptor.ObjectCryptography(data, TransformType.DECRYPT);
                            break;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    case AlgorithmType.DES:
                        try
                        {
                            this.Encryptor = (Encryptor)new AlgorithmDES(secretKey, algKeyType);
                            str = this.Encryptor.ObjectCryptography(data, TransformType.DECRYPT);
                            break;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                }
            }
            string[] strArray = str.Split('#');
            if (strArray.Length != 5)
                return (LicenseInfo)null;
            return new LicenseInfo()
            {
                FullName = strArray[0],
                ProductKey = strArray[1],
                Day = Convert.ToInt32(strArray[2]),
                Month = Convert.ToInt32(strArray[3]),
                Year = Convert.ToInt32(strArray[4])
            };
        }

        public virtual void SaveLicenseToFile(
          string secretKey,
          LicenseInfo licInfo,
          byte version,
          AlgorithmType algType,
          AlgorithmKeyType algKeyType)
        {
            switch (algType)
            {
                case AlgorithmType.None:
                    try
                    {
                        this.WriteFile(licInfo, version, AlgorithmType.None, AlgorithmKeyType.None);
                        break;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                case AlgorithmType.Rijndael:
                    try
                    {
                        this.Encryptor = (Encryptor)new AlgorithmRijndael(secretKey, algKeyType);
                        this.WriteFile(licInfo, version, this.Encryptor, AlgorithmType.Rijndael, algKeyType);
                        break;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                case AlgorithmType.TripleDES:
                    try
                    {
                        this.Encryptor = (Encryptor)new AlgorithmTripleDES(secretKey, algKeyType);
                        this.WriteFile(licInfo, version, this.Encryptor, AlgorithmType.TripleDES, algKeyType);
                        break;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                case AlgorithmType.DES:
                    try
                    {
                        this.Encryptor = (Encryptor)new AlgorithmDES(secretKey, algKeyType);
                        this.WriteFile(licInfo, version, this.Encryptor, AlgorithmType.DES, algKeyType);
                        break;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
            }
        }

        public virtual byte[] SaveLicenseToStream(
          string secretKey,
          LicenseInfo licInfo,
          byte version,
          AlgorithmType algType,
          AlgorithmKeyType algKeyType)
        {
            byte[] numArray = (byte[])null;
            switch (algType)
            {
                case AlgorithmType.None:
                    try
                    {
                        numArray = this.WriteStream(licInfo, version, AlgorithmType.None, AlgorithmKeyType.None);
                        break;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                case AlgorithmType.Rijndael:
                    try
                    {
                        this.Encryptor = (Encryptor)new AlgorithmRijndael(secretKey, algKeyType);
                        numArray = this.WriteStream(licInfo, version, this.Encryptor, AlgorithmType.Rijndael, algKeyType);
                        break;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                case AlgorithmType.TripleDES:
                    try
                    {
                        this.Encryptor = (Encryptor)new AlgorithmTripleDES(secretKey, algKeyType);
                        numArray = this.WriteStream(licInfo, version, this.Encryptor, AlgorithmType.TripleDES, algKeyType);
                        break;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                case AlgorithmType.DES:
                    try
                    {
                        this.Encryptor = (Encryptor)new AlgorithmDES(secretKey, algKeyType);
                        numArray = this.WriteStream(licInfo, version, this.Encryptor, AlgorithmType.DES, algKeyType);
                        break;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
            }
            return numArray;
        }


    }
}
