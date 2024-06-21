using System.ServiceModel;

namespace SCADA_Core.Controllers.interfaces;

[ServiceContract]
public interface IUserController
{
    [OperationContract]
    bool RegisterUser(string username, string password);

    [OperationContract]
    string LogIn(string username, string password);
}