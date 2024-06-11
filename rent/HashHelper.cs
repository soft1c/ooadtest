using System.Security.Cryptography;
using System.Text;

public static class HashHelper
{
    public static long GetLongHash(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentException("Input string cannot be null or empty", nameof(input));
        }

        using (var sha256 = SHA256.Create())
        {
            // Generišemo SHA256 hash
            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Pretvaramo hash u long
            long longHash = BitConverter.ToInt64(hash, 0);

            // Pretvaramo u pozitivan broj (ako je negativan)
            longHash = Math.Abs(longHash);

            // Smanjujemo na 7 cifara
            int sevenDigitHash = (int)(longHash % 10000000);

            return sevenDigitHash;
        }
    }
}
