using RealTimeDriver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RealTimeUnit
{
    public class RealTimeUnitService : IRealTimeUnitService
    {
        private CspParameters csp;
        private RSACryptoServiceProvider rsa;
        private Thread dataPublishingThread;
        private string address;
        private double minValue;
        private double maxValue;

        public delegate void MessagePublishedDelegate(RTUMessage message);
        public event MessagePublishedDelegate OnMessagePublished;

        const string KEY_EXPORT_FOLDER = @"C:\rtu_keys";
        const string KEY_FILE = @"rsaPublicKey.txt";

        public void ExportPublicKey()
        {
            if (!Directory.Exists(KEY_EXPORT_FOLDER))
            {
                Directory.CreateDirectory(KEY_EXPORT_FOLDER);
            }

            string path = Path.Combine(KEY_EXPORT_FOLDER, KEY_FILE);

            if (File.Exists(path))
            {
                return;
            }

            using (StreamWriter writer = new StreamWriter(path))
            {
                // Need to export private key so all RTU instances can use the same key
                writer.Write(rsa.ToXmlString(true));
            }

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

        private void PublishDataPeriodically()
        {
            while(true)
            {
                double newValue = new Random().NextDouble() * (maxValue - minValue) + minValue;
                PublishValue(newValue, address);
                Thread.Sleep(150);
            }
        }


        public RealTimeUnitService(string address, double minValue, double maxValue)
        {
            this.address = address;
            this.minValue = minValue;
            this.maxValue = maxValue;
            string keyPath = Path.Combine(KEY_EXPORT_FOLDER, KEY_FILE);
            if (!File.Exists(keyPath)) {
                CreateAsmKeys();
                ExportPublicKey();
            } else
            {
                ImportAsmKeys();
            }

            dataPublishingThread = new Thread(PublishDataPeriodically);
            dataPublishingThread.Start();
        }



        public void CreateAsmKeys()
        {
            csp = new CspParameters();
            rsa = new RSACryptoServiceProvider(csp);
        }

        public void Subscribe()
        {
            Console.WriteLine("[INFO] Something subscribed");
            OnMessagePublished += OperationContext.Current.GetCallbackChannel<IRealTimeUnitServiceCallback>().OnMessagePublished;
        }

        public void PublishValue(double value, string address)
        {
            RTUMessage message = new RTUMessage(value, address);
            RTUMessage signedMessage = Sign(message);
            if (OnMessagePublished == null)
            {
                return;
            }

            foreach(MessagePublishedDelegate handler in OnMessagePublished.GetInvocationList().Cast<MessagePublishedDelegate>())
            {
                try
                {
                    handler(signedMessage);
                } catch (TimeoutException)
                {
                    Console.WriteLine("Handler timed out. Removing it.");
                    OnMessagePublished -= handler;
                }
            }
        }

        public RTUMessage Sign(RTUMessage message)
        {
            using (SHA256 sha = SHA256Managed.Create())
            {
                var hashValue = sha.ComputeHash(Encoding.UTF8.GetBytes(message.Message));

                var formatter = new RSAPKCS1SignatureFormatter(rsa);
                formatter.SetHashAlgorithm("SHA256");

                message.Signature = formatter.CreateSignature(hashValue);
            }
            return message;
        }
    }
}
