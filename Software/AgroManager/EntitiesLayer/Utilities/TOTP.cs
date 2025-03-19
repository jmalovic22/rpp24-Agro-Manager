using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Net;

namespace EntitiesLayer.Utilities
{
    public static class TOTP
    {
        public static string GetTOTP(byte[] privateKey, int numOfDigits, int interval)
        {
            var counter = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds / interval;
            return GetOTP(privateKey, counter, numOfDigits);
        }

        public static string GetOTP(byte[] privateKey, long iteration, int numOfDigits)
        {
            var counter = BitConverter.GetBytes(iteration);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(counter);

            var hmacSha1 = new HMACSHA256(privateKey);
            var hmacResult = hmacSha1.ComputeHash(counter);

            var offset = hmacResult[hmacResult.Length - 1] & 0xf;
            var binCode = ((hmacResult[offset] & 0x7f) << 24)
            | ((hmacResult[offset + 1] & 0xff) << 16)
            | ((hmacResult[offset + 2] & 0xff) << 8)
            | (hmacResult[offset + 3] & 0xff);

            var password = binCode % (int)Math.Pow(10, numOfDigits);

            return password.ToString(new string('0', numOfDigits));
        }

        public static string EncodeBase32(byte[] data)
        {
            const string base32Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
            StringBuilder result = new StringBuilder((data.Length * 8 + 4) / 5);

            int buffer = data[0];
            int next = 1;
            int bitsLeft = 8;
            while (bitsLeft > 0 || next < data.Length)
            {
                if (bitsLeft < 5)
                {
                    if (next < data.Length)
                    {
                        buffer <<= 8;
                        buffer |= data[next++] & 0xFF;
                        bitsLeft += 8;
                    }
                    else
                    {
                        int pad = 5 - bitsLeft;
                        buffer <<= pad;
                        bitsLeft += pad;
                    }
                }
                int index = 0x1F & (buffer >> (bitsLeft - 5));
                bitsLeft -= 5;
                result.Append(base32Chars[index]);
            }
            return result.ToString();
        }

        public static IEnumerable<byte> DecodeBase32(string encodedString)
        {
            var numOfBit = 0;
            byte decoded = 0;
            foreach (var base32Char in encodedString.ToUpper())
            {
                var base32Val = 0;
                if (base32Char >= 'A' && base32Char <= 'Z')
                {
                    base32Val = base32Char - 65;
                }
                else if (base32Char >= '2' && base32Char <= '7')
                {
                    base32Val = base32Char - 24;
                }
                var bitMask = 0x10;
                for (var i = 0; i < 5; ++i)
                {
                    decoded |= (byte)((base32Val & bitMask) != 0 ? 1 : 0);
                    if (++numOfBit == 8)
                    {
                        yield return decoded;
                        numOfBit = 0;
                        decoded = 0;
                    }
                    decoded <<= 1;
                    bitMask >>= 1;
                }
            }
        }

        public static byte[] GenerateKey(int length)
        {
            using (RandomNumberGenerator RNG = RandomNumberGenerator.Create())
            {
                byte[] Output = new byte[length];
                RNG.GetBytes(Output);
                return Output;
            }
        }

        public static string GetTOTPUri(string izdavac, string korIme, string base32SecretKey, int znamenki, int interval)
        {
            string totpAuthUri = string.Format("otpauth://totp/{0}%3A{1}?secret={2}&algorithm=SHA256&digits=6&period=30", WebUtility.UrlEncode(izdavac), WebUtility.UrlEncode(korIme), base32SecretKey, znamenki, interval);
            return totpAuthUri;
        }
    }
}
