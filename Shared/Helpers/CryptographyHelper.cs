using System.Security.Cryptography;
using System.Text;

namespace DataAccess.Helpers
{
    /// <summary>
    /// Помощник для шифрования.
    /// </summary>
    public static class CryptographyHelper
    {
        /// <summary>
        /// Метод для хеширования пароля (искусственная реализация, чтобы не хранить пароль в открытом виде). Для теста пойдет.
        /// </summary>
        /// <param name="password">Пароль пользователя.</param>
        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
