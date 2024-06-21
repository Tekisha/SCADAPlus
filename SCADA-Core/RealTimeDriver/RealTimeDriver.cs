using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using ServiceReference1;

namespace RealTimeDriver;

public class RealTimeDriver : IDriver.IDriver, IRealTimeUnitServiceCallback
{
    private const string KeyExportFolder = @"C:\rtu_keys";
    private const string KeyFile = "rsaPublicKey.txt";

    // ReSharper disable once CollectionNeverQueried.Local
    private readonly List<RealTimeUnitServiceClientBase> _clients;

    private readonly Dictionary<string, double> _currentValues;
    private readonly RSACryptoServiceProvider _rsa;

    public RealTimeDriver()
    {
        _currentValues = new Dictionary<string, double>();
        _clients = new List<RealTimeUnitServiceClientBase>();
        var csp = new CspParameters();
        _rsa = new RSACryptoServiceProvider(csp);
        ImportAsmKeys();
    }

    public double GetValue(string address)
    {
        return !_currentValues.ContainsKey(address) ? double.NaN : _currentValues[address];
    }

    public void Connect(string address)
    {
        var ic = new InstanceContext(this);
        var client = new RealTimeUnitServiceClientBase(ic, new WSDualHttpBinding(), new EndpointAddress(address));
        _clients.Add(client);
        client.Subscribe();
    }

    public void OnMessagePublished(RTUMessage message)
    {
        if (!CheckSignature(message))
        {
            Console.WriteLine($"Invalid signature from {message.Address}");
            return;
        }

        _currentValues[message.Address] = message.Value;
    }

    private void ImportAsmKeys()
    {
        var path = Path.Combine(KeyExportFolder, KeyFile);
        using var reader = new StreamReader(path);
        var publicKeyText = reader.ReadToEnd();
        _rsa.FromXmlString(publicKeyText);
    }


    private bool CheckSignature(RTUMessage message)
    {
        using var sha = SHA256.Create();
        var hashValue = sha.ComputeHash(Encoding.UTF8.GetBytes(message.Message));
        var deformatter = new RSAPKCS1SignatureDeformatter(_rsa);
        deformatter.SetHashAlgorithm("SHA256");
        return deformatter.VerifySignature(hashValue, message.Signature);
    }
}