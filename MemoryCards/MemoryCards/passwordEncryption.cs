using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BCrypt.Net;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCards
{
    public static class passwordEncryption
    {
        public static string Encrypt(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool Decrypt(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
