using IDriver;
using ServiceReference1;
using System.ServiceModel;

namespace RealTimeDriver;

public class RealTimeDriver : IDriver.IDriver, ServiceReference1.IRealTimeUnitServiceCallback
{
    private Dictionary<string, double> currentValues = new Dictionary<string, double>();
    private List<RealTimeUnitServiceClientBase> clients = new();
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

    private bool CheckSignature(RTUMessage message)
    {
        return true;
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
