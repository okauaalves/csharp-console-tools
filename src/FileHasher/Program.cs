using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FileHasher
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: FileHasher <file|text> [--text] [--algo md5|sha1|sha256|sha512]");
                return;
            }

            string input = args[0];
            string algo = "sha256";
            bool isText = false;

            for (int i = 1; i < args.Length; i++)
            {
                if (args[i] == "--algo" && i + 1 < args.Length) algo = args[++i];
                if (args[i] == "--text") isText = true;
            }

            byte[] bytes = isText
                ? Encoding.UTF8.GetBytes(input)
                : File.ReadAllBytes(input);

            string hash = ComputeHash(bytes, algo);
            Console.WriteLine($"Algorithm : {algo.ToUpper()}");
            Console.WriteLine($"Input     : {(isText ? "text" : input)}");
            Console.WriteLine($"Hash      : {hash}");
        }

        static string ComputeHash(byte[] data, string algorithm)
        {
            HashAlgorithm hasher = algorithm.ToLower() switch
            {
                "md5"    => MD5.Create(),
                "sha1"   => SHA1.Create(),
                "sha256" => SHA256.Create(),
                "sha512" => SHA512.Create(),
                _        => throw new ArgumentException($"Unknown algorithm: {algorithm}")
            };

            using (hasher)
            {
                byte[] hash = hasher.ComputeHash(data);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
    }
}
