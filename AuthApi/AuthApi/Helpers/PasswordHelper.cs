namespace AuthApi.Helpers
{
    public static class PasswordHelper
    {
        public static bool VerifyPassword(string inputPassword, string storedHashedPassword)
        {
            var inputHash = ComputeHash(inputPassword.Trim());
            return inputHash == storedHashedPassword;
        }

        public static string ComputeHash(string input)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(input);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
