using AuthApi.Helpers;
using AuthApi.Interfaces;
using AuthApi.Models;
using System.Text.Json;

namespace AuthApi.Services
{
    public class UserService : IUserService
    {
        private static readonly string DataFile = Path.Combine("Data", "user-data.json");

        public User ValidateUser(string username, string password)
        {
            var users = LoadUsers();

            var user = users.FirstOrDefault(u =>
                string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase)); // Case-insensitive username match

            if (user == null)
            {
                Console.WriteLine("User not found.");
                return null;
            }

            bool isPasswordValid = PasswordHelper.VerifyPassword(password, user.Password);

            if (!isPasswordValid)
            {
                Console.WriteLine("Password incorrect.");
                return null;
            }

            return user;
        }

        public void SaveUser(User user)
        {
            var users = LoadUsers();
            if (users.Any(u => u.Username.Equals(user.Username, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("User already exists.");
                return;
            }

            users.Add(user);
            var options = new JsonSerializerOptions { WriteIndented = true };
            File.WriteAllText(DataFile, JsonSerializer.Serialize(users, options));
        }

        private List<User> LoadUsers()
        {
            if (!File.Exists(DataFile))
            {
                Console.WriteLine("User data file not found. Returning empty list.");
                return new List<User>();
            }

            var json = File.ReadAllText(DataFile);
            var users = JsonSerializer.Deserialize<List<User>>(json);
            return users ?? new List<User>();
        }
    }
}
