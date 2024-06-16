using IDriver;
using ServiceReference1;
using System.Security.Cryptography;
using System.Security.Policy;
using System.ServiceModel;
using System.Text;

namespace RealTimeDriver;

public class RealTimeDriver : IDriver.IDriver, ServiceReference1.IRealTimeUnitServiceCallback
{
    private Dictionary<string, double> currentValues = new Dictionary<string, double>();
    private List<RealTimeUnitServiceClientBase> clients = new();

    private CspParameters csp;
    private RSACryptoServiceProvider rsa;
    const string KEY_EXPORT_FOLDER = @"C:\rtu_keys";
    const string KEY_FILE = @"rsaPublicKey.txt";

    public RealTimeDriver()
    {
        currentValues = new Dictionary<string, double>();
        clients = new();
        ImportAsmKeys();
    }

    public double GetValue(string address)
    {
        if (!currentValues.ContainsKey(address))
        {
            return double.NaN;
        }

        return currentValues[address];
    }

    public void Connect(string address)
    {
        Console.WriteLine("[INFO] RTD connecting to " + address);
        InstanceContext ic = new InstanceContext(this);
        var client = new RealTimeUnitServiceClientBase(ic, new WSDualHttpBinding(), new EndpointAddress(address));
        clients.Add(client);
        client.Subscribe();
    }

    private void ImportAsmKeys()
    {
        string path = Path.Combine(KEY_EXPORT_FOLDER, KEY_FILE);
        using (StreamReader reader = new StreamReader(path))
        {
            csp = new CspParameters();
            rsa = new RSACryptoServiceProvider(csp);
            string publicKeyText = reader.ReadToEnd();
            rsa.FromXmlString(publicKeyText);
        }

    }


    private bool CheckSignature(RTUMessage message)
    {
        using (SHA256 sha = SHA256Managed.Create())
        {
            var hashValue = sha.ComputeHash(Encoding.UTF8.GetBytes(message.Message));
            var deformatter = new RSAPKCS1SignatureDeformatter(rsa);
            deformatter.SetHashAlgorithm("SHA256");
            return deformatter.VerifySignature(hashValue, message.Signature);
        }
    }

    public void OnMessagePublished(RTUMessage message)
    {
        if (!CheckSignature(message))
        {
            Console.WriteLine($"Invalid signature from {message.Address}");
            return;
        }

        currentValues[message.Address] = message.Value;
    }
}
