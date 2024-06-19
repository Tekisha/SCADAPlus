﻿using ServiceReference1;
using ServiceReference2;
using System.ServiceModel;

namespace ReportManager.Commands;

internal class ExitCommand(IUserController userController, IReportController reportController) : ICommand
{
    public string GetDescription()
    {
        return "Exit application.";
    }

    public void Execute(string _)
    {
        ((IClientChannel)reportController).Close();
        ((IClientChannel)userController).Close();
        Environment.Exit(0);
    }
}