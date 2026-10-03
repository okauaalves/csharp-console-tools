# csharp-console-tools

> Useful command-line utilities built with C# and .NET

A collection of practical CLI tools demonstrating C# fundamentals: cryptography, file I/O, and console apps.

## Tools

### 🔐 Password Generator
Generates cryptographically secure passwords using `RandomNumberGenerator`.

```bash
cd src/PasswordGenerator
dotnet run -- --length 20 --count 10
dotnet run -- --length 12 --no-symbols
```

### #️⃣ File Hasher
Compute MD5, SHA1, SHA256, or SHA512 hashes for files or text strings.

```bash
cd src/FileHasher
dotnet run -- myfile.zip --algo sha256
dotnet run -- "hello world" --text --algo md5
```

## Requirements

- .NET 7+ SDK

## Build

```bash
# Build all
dotnet build

# Run directly
cd src/PasswordGenerator && dotnet run
```
