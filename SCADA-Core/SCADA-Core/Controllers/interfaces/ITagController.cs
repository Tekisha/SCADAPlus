using ScadaPlus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Controllers.interfaces
{
    [ServiceContract]
    public interface ITagController
    {
        [OperationContract]
        double GetTagValue(string address);

        [OperationContract]
        void AddTag(Tag tag);

        [OperationContract]
        void RemoveTag(string id);

        [OperationContract]
        void ChangeOutputValue(string tagId, double newValue);

        [OperationContract]
        double GetOutputValue(string tagId);

        [OperationContract]
        void TurnScanOnOff(string tagId, bool onOff);
    }
}
