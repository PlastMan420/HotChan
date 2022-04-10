using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Security.Cryptography;

namespace HotChanApi.Authentication;

public class SigningIssuerCertificate : IDisposable
{
    private readonly RSA rsa;

    public void Dispose() => rsa?.Dispose();

    public RsaSecurityKey GetIssuerSigningKey()
    {
        string publicXmlKey = File.ReadAllText("./public_key.xml");
        rsa.FromXmlString(publicXmlKey);

        return new RsaSecurityKey(rsa);
    }

    public SigningCredentials GetAudienceSigningKey()
    {
        string privateXmlKey = File.ReadAllText("./private_key.xml");
        rsa.FromXmlString(privateXmlKey);

        return new SigningCredentials(
            key: new RsaSecurityKey(rsa),
            algorithm: SecurityAlgorithms.RsaSha256);
    }
}

