using System.Security.Cryptography;
using System.Text;
using Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Application.Services.Implementations;

public class HashingService : IHashingService
{
    private readonly byte[] _key;

    public HashingService(IConfiguration config)
    {
        var key = config["Hashing:SecretKey"];
        _key = Encoding.UTF8.GetBytes(key);
    }
    
    public string HashPassword(string password)
    {
        if (password == null)
        {
            throw new ArgumentNullException(nameof(password), "Password cannot be null!");
        }

        using var hmac = new HMACSHA256(_key);
        var salt = GenerateSalt(16);
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var saltedPassword = new byte[salt.Length + passwordBytes.Length];

        Buffer.BlockCopy(salt, 0, saltedPassword, 0, salt.Length);
        Buffer.BlockCopy(passwordBytes, 0, saltedPassword, salt.Length, passwordBytes.Length);

        var hash = hmac.ComputeHash(saltedPassword);

        var result = new byte[salt.Length + hash.Length];
        Buffer.BlockCopy(salt, 0, result, 0, salt.Length);
        Buffer.BlockCopy(hash, 0, result, salt.Length, hash.Length);

        return Convert.ToBase64String(result);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        if (hashedPassword == null)
        {
            throw new ArgumentNullException(nameof(hashedPassword), "Hashed password cannot be null!");
        }
        if (password == null)
        {
            throw new ArgumentNullException(nameof(password), "Password cannot be null!");
        }

        var hashBytes = Convert.FromBase64String(hashedPassword);

        var salt = new byte[16];
        Buffer.BlockCopy(hashBytes, 0, salt, 0, salt.Length);

        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var saltedPassword = new byte[salt.Length + passwordBytes.Length];

        Buffer.BlockCopy(salt, 0, saltedPassword, 0, salt.Length);
        Buffer.BlockCopy(passwordBytes, 0, saltedPassword, salt.Length, passwordBytes.Length);

        using var hmac = new HMACSHA256(_key);
        var computedHash = hmac.ComputeHash(saltedPassword);

        var storedHash = new byte[computedHash.Length];
        Buffer.BlockCopy(hashBytes, salt.Length, storedHash, 0, storedHash.Length);

        return ByteArraysEqual(computedHash, storedHash);
    }
    
    private static byte[] GenerateSalt(int length)
    {
        byte[] salt = new byte[length];
        using var rng = new RNGCryptoServiceProvider();
        rng.GetBytes(salt);
        return salt;
    }

    private static bool ByteArraysEqual(byte[] b1, byte[] b2)
    {
        if (b1 == b2)
        {
            return true;
        }

        if (b1 is null || b2 is null || b1.Length != b2.Length)
        {
            return false;
        }

        return CryptographicOperations.FixedTimeEquals(b1, b2);
    }
}