using System;
using System.Security.Cryptography;

namespace SCADA_Core.Utilities;

public static class TokenGenerator
{
    public static string GenerateToken(string username)
    {
        using var crypto = new RNGCryptoServiceProvider();
        var randVal = new byte[32];
        crypto.GetBytes(randVal);
        var randStr = Convert.ToBase64String(randVal);
        return username + randStr;
    }
}