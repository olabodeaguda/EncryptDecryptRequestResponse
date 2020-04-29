using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EncryptDecryptTest.Models
{
    public static class Extensions
    {
        public static byte[] ToByte(this Stream stream)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
            catch (Exception)
            {
                return Array.Empty<byte>();
            }
        }
    }
}
