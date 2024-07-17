using System.Security.Cryptography;

namespace BlockchainApp.Classes
{
    public class RSAKeyGenerator
    {
        public static (string publicKey, string privateKey) GenerateKeys()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    var publicKey = rsa.ToXmlString(false); // public key
                    var privateKey = rsa.ToXmlString(true); // private key
                    return (publicKey, privateKey);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
    }
}
