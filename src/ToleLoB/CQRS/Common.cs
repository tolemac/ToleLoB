using System.Security.Cryptography;
using System.Text;

namespace ToleLoB.CQRS
{
    public static class Common
    {
        public static string GetStringHash(string str)
        {
            using (var algorithm = SHA256.Create())
            {
                var hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(str));
                return Encoding.UTF8.GetString(hash);
            }
        }
    }
}