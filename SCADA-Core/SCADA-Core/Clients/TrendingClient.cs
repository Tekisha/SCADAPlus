using System;
using System.ServiceModel;
using System.Threading.Tasks;
using SCADA_Core.TrendingService;

namespace SCADA_Core.Clients;

public class TrendingClient(string endpointConfigurationName)
    : ClientBase<ITrendingService>(endpointConfigurationName), ITrendingService
{
    public void SendTagValue(TagValue value)
    {
        Channel.SendTagValue(value);
    }

    public Task SendTagValueAsync(TagValue value)
    {
        throw new NotImplementedException();
    }
}