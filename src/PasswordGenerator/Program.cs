using System;
using System.Security.Cryptography;
using System.Text;

namespace PasswordGenerator
{
    class Program
    {
        const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
        const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string Digits    = "0123456789";
        const string Symbols   = "!@#$%^&*()-_=+[]{}|;:,.<>?";

        static void Main(string[] args)
        {
            int length = 16;
            bool useSymbols = true;
            int count = 5;

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "--length": length = int.Parse(args[++i]); break;
                    case "--no-symbols": useSymbols = false; break;
                    case "--count": count = int.Parse(args[++i]); break;
                    case "--help":
                        Console.WriteLine("Usage: PasswordGenerator [--length N] [--count N] [--no-symbols]");
                        return;
                }
            }

            Console.WriteLine($"Generating {count} passwords ({length} chars):\n");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(GeneratePassword(length, useSymbols));
            }
        }

        static string GeneratePassword(int length, bool includeSymbols)
        {
            var pool = Lowercase + Uppercase + Digits;
            if (includeSymbols) pool += Symbols;

            var password = new StringBuilder();
            using var rng = RandomNumberGenerator.Create();
            var buffer = new byte[4];

            // Guarantee at least one of each required type
            password.Append(PickRandom(Lowercase, rng, buffer));
            password.Append(PickRandom(Uppercase, rng, buffer));
            password.Append(PickRandom(Digits, rng, buffer));
            if (includeSymbols) password.Append(PickRandom(Symbols, rng, buffer));

            while (password.Length < length)
                password.Append(PickRandom(pool, rng, buffer));

            // Shuffle
            var chars = password.ToString().ToCharArray();
            for (int i = chars.Length - 1; i > 0; i--)
            {
                rng.GetBytes(buffer);
                int j = (int)(BitConverter.ToUInt32(buffer, 0) % (uint)(i + 1));
                (chars[i], chars[j]) = (chars[j], chars[i]);
            }

            return new string(chars);
        }

        static char PickRandom(string pool, RandomNumberGenerator rng, byte[] buffer)
        {
            rng.GetBytes(buffer);
            return pool[(int)(BitConverter.ToUInt32(buffer, 0) % (uint)pool.Length)];
        }
    }
}
