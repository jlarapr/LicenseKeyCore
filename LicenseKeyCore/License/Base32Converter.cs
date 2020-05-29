using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseKeyCore.License
{
    public class Base32Converter
    {
        private static readonly char[] BASE32_TABLE = new char[32]
  {
      '0',
      '1',
      '2',
      '3',
      '4',
      '5',
      '6',
      '7',
      '8',
      '9',
      'A',
      'B',
      'C',
      'D',
      'E',
      'F',
      'G',
      'H',
      'J',
      'K',
      'L',
      'M',
      'N',
      'P',
      'R',
      'S',
      'T',
      'V',
      'W',
      'X',
      'Y',
      'Z'
  };

        public static string ToBase32String(byte[] buffer)
        {
            char[] chArray = new char[buffer.Length * 2];
            int num1 = buffer.Length % 3;
            if (num1 != 0)
                throw new InvalidOperationException("Input data incorrect. Required multiple of 3 bytes length.");
            int num2 = buffer.Length - num1;
            int length = 0;
            for (int index = 0; index < num2; index += 3)
            {
                chArray[length + 0] = Base32Converter.BASE32_TABLE[((int)buffer[index] & 248) >> 3];
                chArray[length + 1] = Base32Converter.BASE32_TABLE[((int)buffer[index] & 7) << 2 | ((int)buffer[index + 1] & 192) >> 6];
                chArray[length + 2] = Base32Converter.BASE32_TABLE[((int)buffer[index + 1] & 62) >> 1];
                chArray[length + 3] = Base32Converter.BASE32_TABLE[((int)buffer[index + 1] & 1) << 4 | ((int)buffer[index + 2] & 240) >> 4];
                chArray[length + 4] = Base32Converter.BASE32_TABLE[(int)buffer[index + 2] & 15];
                length += 5;
            }
            return new string(chArray, 0, length);
        }

        public static byte[] FromBase32String(string base32)
        {
            byte[] numArray1 = new byte[base32.Length];
            int num1 = base32.Length % 5;
            if (num1 != 0)
                throw new InvalidOperationException("Base32 input string incorrect. Required multiple of 5 character length.");
            int num2 = base32.Length - num1;
            int length = 0;
            for (int index = 0; index < num2; index += 5)
            {
                long num3 = (long)(Base32Converter.GetBase32Number(base32[index]) << 19) | (long)Base32Converter.GetBase32Number(base32[index + 1]) << 14 | (long)Base32Converter.GetBase32Number(base32[index + 2]) << 9 | (long)Base32Converter.GetBase32Number(base32[index + 3]) << 4 | (long)(byte)Base32Converter.GetBase32Number(base32[index + 4]);
                numArray1[length + 0] = (byte)((num3 & 16711680L) >> 16);
                numArray1[length + 1] = (byte)((num3 & 65280L) >> 8);
                numArray1[length + 2] = (byte)((ulong)num3 & (ulong)byte.MaxValue);
                length += 3;
            }
            byte[] numArray2 = new byte[length];
            Array.Copy((Array)numArray1, 0, (Array)numArray2, 0, length);
            return numArray2;
        }

        private static int GetBase32Number(char c)
        {
            if (c == 'I' || c == 'O' || (c == 'Q' || c == 'U'))
                throw new ArgumentOutOfRangeException();
            int num1 = (int)c - 48;
            if (num1 > 9)
            {
                int num2 = num1 - 16;
                if (num2 > 9)
                {
                    --num2;
                    if (num2 > 13)
                    {
                        --num2;
                        if (num2 > 14)
                        {
                            --num2;
                            if (num2 > 17)
                                --num2;
                        }
                    }
                }
                num1 = num2 + 9;
            }
            if (num1 < 0 || num1 > 31)
                throw new ArgumentOutOfRangeException();
            return num1;
        }
    }
}
