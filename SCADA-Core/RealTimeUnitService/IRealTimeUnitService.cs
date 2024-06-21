using System.ServiceModel;

namespace RealTimeUnitService;

[ServiceContract(CallbackContract = typeof(IRealTimeUnitServiceCallback))]
public interface IRealTimeUnitService
{
    [OperationContract]
    void Subscribe();
}

[ServiceContract]
public interface IRealTimeUnitServiceCallback
{
    [OperationContract(IsOneWay = true)]
    void OnMessagePublished(RTUMessage message);
}