using System.ServiceModel;
using SCADA_Core.DTOs;

namespace SCADA_Core.Controllers.interfaces;

[ServiceContract]
public interface ITagController
{
    [OperationContract]
    double GetTagValue(string address);

    [OperationContract]
    void AddTag(TagDto tagDto);

    [OperationContract]
    void RemoveTag(string id);

    [OperationContract]
    void ChangeOutputValue(string tagId, double newValue);

    [OperationContract]
    double GetOutputValue(string tagId);

    [OperationContract]
    void TurnScanOnOff(string tagId, bool onOff);
}