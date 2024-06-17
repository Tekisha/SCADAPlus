using System.ServiceModel;
using Trending.Models;

namespace Trending.Services;

[ServiceContract]
public interface ITrendingService
{
    [OperationContract(IsOneWay = true)]
    void SendTagValue(TagValue value);
}