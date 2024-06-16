using RealTimeDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeUnit
{
    [ServiceContract(CallbackContract = typeof(IRealTimeUnitServiceCallback))]
    public interface IRealTimeUnitService
    {
        [OperationContract]
        void Subscribe();
    }
    public interface IRealTimeUnitServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnMessagePublished(RTUMessage message);
    }
}
