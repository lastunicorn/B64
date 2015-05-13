using System;
using System.Text;

namespace DustInTheWind.B64
{
    public class Base64Encoder
    {
        public string Encode(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public string Decode(string base64EncodedData)
        {
            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF7.GetString(base64EncodedBytes);
        }
    }
}