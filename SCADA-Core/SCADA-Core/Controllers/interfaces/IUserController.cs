﻿using System.ServiceModel;

namespace SCADA_Core.Controllers.interfaces;

[ServiceContract]
public interface IUserController
{
    [OperationContract]
    bool RegisterUser(string username, string password);

    [OperationContract]
    bool LogIn(string username, string password);
}