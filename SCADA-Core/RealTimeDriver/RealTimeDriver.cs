using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;

namespace RealTimeDriver
{
    public class RealTimeDriver
    {
        private const string RTUPublicKeyLocation = "";
        public static double GetValue(string address)
        {
            return -1000;
        }
        public static void SaveValue(string address, string tagId, double value)
        {

        }
        private bool checkSignature(RTUMessage message)
        {
            RSACryptoServiceProvider rsa = ImportPublicKey();
            using (SHA256 sha = SHA256Managed.Create())
            {
                var hashValue = sha.ComputeHash(Encoding.UTF8.GetBytes(message.Message));
                var deformatter = new RSAPKCS1SignatureDeformatter(rsa);
                deformatter.SetHashAlgorithm("SHA256");
                return deformatter.VerifySignature(hashValue, message.Signature);
            }
        }

        private RSACryptoServiceProvider ImportPublicKey()
        {
            //Provera da li fajl sa javnim ključem postoji na prosleđenoj lokaciji
            FileInfo fi = new FileInfo(RTUPublicKeyLocation);
            if (fi.Exists)
            {
                using (StreamReader reader = new StreamReader(RTUPublicKeyLocation))
                {
                    var csp = new CspParameters();
                    var rsa = new RSACryptoServiceProvider(csp);
                    string publicKeyText = reader.ReadToEnd();
                    rsa.FromXmlString(publicKeyText);
                    return rsa;
                }
            }
            return null;
        }

    }
}
