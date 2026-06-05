using System;
using System.Security.Cryptography;

namespace e_rehistro
{
    /// <summary>
    /// Provides PBKDF2-based password hashing and verification.
    /// Uses Rfc2898DeriveBytes with SHA1, 100 000 iterations, and a 16-byte salt.
    /// </summary>
    public static class PasswordHelper
    {
        private const int SaltSize = 16;   // 128-bit salt
        private const int HashSize = 32;   // 256-bit hash
        private const int Iterations = 100_000;

        /// <summary>
        /// Hashes a plaintext password and returns a Base64 string (salt + hash).
        /// </summary>
        public static string HashPassword(string password)
        {
            byte[] salt = new byte[SaltSize];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);
                byte[] result = new byte[SaltSize + HashSize];
                Array.Copy(salt, 0, result, 0, SaltSize);
                Array.Copy(hash, 0, result, SaltSize, HashSize);
                return Convert.ToBase64String(result);
            }
        }

        /// <summary>
        /// Verifies a plaintext password against a stored hash.
        /// Also handles legacy plaintext passwords for migration purposes.
        /// </summary>
        public static bool VerifyPassword(string password, string storedPassword)
        {
            try
            {
                byte[] hashBytes = Convert.FromBase64String(storedPassword);

                // If the decoded length doesn't match our expected format,
                // fall back to legacy plaintext comparison.
                if (hashBytes.Length != SaltSize + HashSize)
                    return password == storedPassword;

                byte[] salt = new byte[SaltSize];
                Array.Copy(hashBytes, 0, salt, 0, SaltSize);

                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
                {
                    byte[] hash = pbkdf2.GetBytes(HashSize);

                    // Constant-time comparison to prevent timing attacks
                    bool match = true;
                    for (int i = 0; i < HashSize; i++)
                    {
                        match &= (hashBytes[i + SaltSize] == hash[i]);
                    }
                    return match;
                }
            }
            catch (FormatException)
            {
                // storedPassword is not valid Base64 → legacy plaintext password.
                // TODO: Remove this fallback after all existing passwords are migrated.
                return password == storedPassword;
            }
        }
    }
}
