

namespace Citizens.Core
{
    using System.IO;
    using System.Text;
    using System.Security.Cryptography;
    using System;

    public static class MD5Extension
    {
        public static string GetMd5HashFromFile(this string fileName)
        {
            using (var md5 = MD5.Create())
            {
                var stream = File.OpenRead(fileName);
                return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
            }
        }
        public static string GetMD5FromString(this string text)
        {
            using (var md5 = MD5.Create())
            {
                var buffers = UTF8Encoding.Default.GetBytes(text);
                return BitConverter.ToString(md5.ComputeHash(buffers));
            }
        }
    }
}
