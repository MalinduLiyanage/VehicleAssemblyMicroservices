using System.Collections;
using System.Text;
using System.Security.Cryptography;
using System.Text;

namespace AdminApi.Utilities.PasswordHashUtility
{
    public class PasswordHashUtilityService : IPasswordHashUtilityService
    {
        public byte[]? PasswordHash;
        public string? generatedPassword;

        public PasswordHashUtilityService() 
        {
            SetRandomPassword();
        }
        private string SetRandomPassword()
        {
            generatedPassword = GenerateRandomPassword(8);
            using (var sha256 = SHA256.Create())
            {
                PasswordHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(generatedPassword));
            }
            return generatedPassword;
        }
        public bool VerifyPassword(string userPassword, byte[] dbPassword)
        {
            using (var sha256 = SHA256.Create())
            {
                var userHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(userPassword));
                return StructuralComparisons.StructuralEqualityComparer.Equals(dbPassword, userHash);
            }
        }
        private static string GenerateRandomPassword(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
