namespace Turtle;

public enum CompressedDataFormat
{
    Brotli,
    GZip
}

public enum EncodeFormat
{
    Base64,
    Hex
}

public enum HashFunction
{
    Md5,
    Sha1,
    Sha256,
    Sha384,
    Sha512
}

public static class Data
{
    public static byte[] Compress(CompressedDataFormat format, string text)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(text);

        byte[] compressed = Array.Empty<byte>();

        switch (format)
        {
            case CompressedDataFormat.Brotli:
                using (var memoryStream = new MemoryStream())
                {
                    using (var brotliStream = new BrotliStream(memoryStream, CompressionLevel.Optimal))
                    {
                        brotliStream.Write(bytes, 0, bytes.Length);
                    }

                    compressed = memoryStream.ToArray();
                }

                break;
            case CompressedDataFormat.GZip:
                using (var memoryStream = new MemoryStream())
                {
                    using (var gzipStream = new GZipStream(memoryStream, CompressionLevel.Optimal))
                    {
                        gzipStream.Write(bytes, 0, bytes.Length);
                    }

                    compressed = memoryStream.ToArray();
                }

                break;
            default:
                break;
        }

        return compressed;
    }

    public static string Decode(EncodeFormat format, string text)
    {
        string decoded = "";

        switch (format)
        {
            case EncodeFormat.Base64:
                byte[] base64Bytes = Convert.FromBase64String(text);
                decoded = Encoding.UTF8.GetString(base64Bytes);

                break;
            case EncodeFormat.Hex:
                byte[] hexBytes = Convert.FromHexString(text);
                decoded = Encoding.UTF8.GetString(hexBytes);

                break;
            default:
                break;
        }

        return decoded;
    }

    public static string Decompress(CompressedDataFormat format, byte[] bytes)
    {
        byte[] decompressed = Array.Empty<byte>();

        switch (format)
        {
            case CompressedDataFormat.Brotli:
                using (var memoryStream = new MemoryStream(bytes))
                {
                    using (var outputStream = new MemoryStream())
                    {
                        using (var decompressStream = new BrotliStream(memoryStream, CompressionMode.Decompress))
                        {
                            decompressStream.CopyTo(outputStream);
                        }

                        decompressed = outputStream.ToArray();
                    }
                }

                break;
            case CompressedDataFormat.GZip:
                using (var memoryStream = new MemoryStream(bytes))
                {
                    using (var outputStream = new MemoryStream())
                    {
                        using (var decompressStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                        {
                            decompressStream.CopyTo(outputStream);
                        }

                        decompressed = outputStream.ToArray();
                    }
                }

                break;
            default:
                break;
        }

        return Encoding.UTF8.GetString(decompressed);
    }

    public static string Encode(EncodeFormat format, string text)
    {
        string encoded = "";

        switch (format)
        {
            case EncodeFormat.Base64:
                byte[] base64Bytes = Encoding.UTF8.GetBytes(text);
                encoded = Convert.ToBase64String(base64Bytes);

                break;
            case EncodeFormat.Hex:
                byte[] hexBytes = Encoding.UTF8.GetBytes(text);
                encoded = Convert.ToHexString(hexBytes);

                break;
            default:
                break;
        }

        return encoded;
    }

    public static string Hash(HashFunction function, string text)
    {
        HashAlgorithm algorithm;

        switch (function)
        {
            case HashFunction.Md5:
                algorithm = MD5.Create();
                break;
            case HashFunction.Sha1:
                algorithm = SHA1.Create();
                break;
            case HashFunction.Sha256:
                algorithm = SHA256.Create();
                break;
            case HashFunction.Sha384:
                algorithm = SHA384.Create();
                break;
            case HashFunction.Sha512:
                algorithm = SHA512.Create();
                break;
            default:
                algorithm = MD5.Create();
                break;
        }

        byte[] hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(text));

        StringBuilder sb = new();

        foreach (byte b in hash)
        {
            sb.Append(b.ToString("X2"));
        }

        return sb.ToString();
    }

    public static string Pack(object obj)
    {
        return JsonConvert.SerializeObject(obj);
    }

    public static T Unpack<T>(string serialized)
    {
        return JsonConvert.DeserializeObject<T>(serialized);
    }
}