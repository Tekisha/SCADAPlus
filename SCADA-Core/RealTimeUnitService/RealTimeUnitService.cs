using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace RealTimeUnitService;

public class RealTimeUnitService : IRealTimeUnitService
{
    public delegate void MessagePublishedDelegate(RTUMessage message);

    private const string KeyExportFolder = @"C:\rtu_keys";
    private const string KeyFile = "rsaPublicKey.txt";
    private readonly string _address;
    private readonly double _maxValue;
    private readonly double _minValue;
    private CspParameters _csp;
    private RSACryptoServiceProvider _rsa;


    public RealTimeUnitService(string address, double minValue, double maxValue)
    {
        _address = address;
        _minValue = minValue;
        _maxValue = maxValue;
        var keyPath = Path.Combine(KeyExportFolder, KeyFile);
        if (!File.Exists(keyPath))
        {
            CreateAsmKeys();
            ExportPublicKey();
        }
        else
        {
            ImportAsmKeys();
        }

        var dataPublishingThread = new Thread(PublishDataPeriodically);
        dataPublishingThread.Start();
    }

    public void Subscribe()
    {
        Console.WriteLine("[INFO] Something subscribed");
        OnMessagePublished += OperationContext.Current.GetCallbackChannel<IRealTimeUnitServiceCallback>()
            .OnMessagePublished;
    }

    public event MessagePublishedDelegate OnMessagePublished;

    public void ExportPublicKey()
    {
        if (!Directory.Exists(KeyExportFolder)) Directory.CreateDirectory(KeyExportFolder);

        var path = Path.Combine(KeyExportFolder, KeyFile);

        if (File.Exists(path)) return;

        using var writer = new StreamWriter(path);
        // Need to export private key so all RTU instances can use the same key
        writer.Write(_rsa.ToXmlString(true));
    }

    private void ImportAsmKeys()
    {
        var path = Path.Combine(KeyExportFolder, KeyFile);
        using var reader = new StreamReader(path);
        _csp = new CspParameters();
        _rsa = new RSACryptoServiceProvider(_csp);
        var publicKeyText = reader.ReadToEnd();
        _rsa.FromXmlString(publicKeyText);
    }

    private void PublishDataPeriodically()
    {
        while (true)
        {
            var newValue = new Random().NextDouble() * (_maxValue - _minValue) + _minValue;
            PublishValue(newValue, _address);
            Thread.Sleep(150);
        }
        // ReSharper disable once FunctionNeverReturns
    }


    public void CreateAsmKeys()
    {
        _csp = new CspParameters();
        _rsa = new RSACryptoServiceProvider(_csp);
    }

    public void PublishValue(double value, string address)
    {
        var message = new RTUMessage(value, address);
        var signedMessage = Sign(message);
        if (OnMessagePublished == null) return;

        foreach (var handler in OnMessagePublished.GetInvocationList().Cast<MessagePublishedDelegate>())
            try
            {
                handler(signedMessage);
            }
            catch (TimeoutException)
            {
                Console.WriteLine("Handler timed out. Removing it.");
                OnMessagePublished -= handler;
            }
    }

    public RTUMessage Sign(RTUMessage message)
    {
        using var sha = SHA256.Create();
        var hashValue = sha.ComputeHash(Encoding.UTF8.GetBytes(message.Message));

        var formatter = new RSAPKCS1SignatureFormatter(_rsa);
        formatter.SetHashAlgorithm("SHA256");

        message.Signature = formatter.CreateSignature(hashValue);

        return message;
    }
}