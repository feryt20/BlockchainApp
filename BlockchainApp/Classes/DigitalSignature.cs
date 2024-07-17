using System.Security.Cryptography;
using System.Text;

namespace BlockchainApp.Classes
{
    public class DigitalSignature
    {
        //private RSACryptoServiceProvider _rsa;

        public DigitalSignature()
        {
            //_rsa = new RSACryptoServiceProvider();
        }

        //public string SignData(string data, string privateKey)
        //{
        //    _rsa.FromXmlString(privateKey);
        //    var dataBytes = Encoding.UTF8.GetBytes(data);
        //    var signatureBytes = _rsa.SignData(dataBytes, new SHA256CryptoServiceProvider());
        //    return Convert.ToBase64String(signatureBytes);
        //}

        //public bool VerifyData(string data, string publicKey, string signature)
        //{
        //    _rsa.FromXmlString(publicKey);
        //    var dataBytes = Encoding.UTF8.GetBytes(data);
        //    var signatureBytes = Convert.FromBase64String(signature);
        //    return _rsa.VerifyData(dataBytes, new SHA256CryptoServiceProvider(), signatureBytes);
        //}


        public string SignData(string data, string privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKey);
                var dataBytes = Encoding.UTF8.GetBytes(data);
                var signatureBytes = rsa.SignData(dataBytes, CryptoConfig.MapNameToOID("SHA256"));
                return Convert.ToBase64String(signatureBytes);
            }
        }

        public bool VerifyData(string data, string publicKey, string signature)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);
                var dataBytes = Encoding.UTF8.GetBytes(data);
                var signatureBytes = Convert.FromBase64String(signature);
                return rsa.VerifyData(dataBytes, CryptoConfig.MapNameToOID("SHA256"), signatureBytes);
            }
        }
    }
}
