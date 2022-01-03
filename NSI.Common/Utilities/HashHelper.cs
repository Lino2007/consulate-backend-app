using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NSI.Common.Utilities
{
    public static class HashHelper
    {
        public static string ComputeFileHash(Stream stream)
        {
            using var hasher = SHA512.Create();
            var hash = hasher.ComputeHash(stream);
            stream.Close();
            return ByteArrayToString(hash);
        }

        private static string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("x2"));
            }

            return sOutput.ToString().ToLower();
        }
    }
}
