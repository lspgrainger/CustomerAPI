namespace Api.Customer.Domain
{
    public class Hashing
    {
        public static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(10);
        }

        public static string HashPassword(string password, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }

        public static bool ValidatePassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }
    }
}