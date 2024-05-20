using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SCADA_Core.Controllers.interfaces
{
    [ServiceContract]
    public interface IUserController
    {
        [OperationContract]
        bool RegisterUser(string username, string password);

        [OperationContract]
        bool LogIn(string username, string password);
    }
}
