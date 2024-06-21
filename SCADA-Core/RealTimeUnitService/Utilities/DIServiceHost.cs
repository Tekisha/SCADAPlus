using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Microsoft.Extensions.DependencyInjection;

namespace RealTimeUnitService.Utilities;

public class DIServiceHostFactory(IServiceProvider serviceProvider) : ServiceHostFactory
{
    public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses)
    {
        var serviceType = Type.GetType(constructorString);
        return new DIServiceHost(serviceProvider, serviceType, baseAddresses);
    }
}

public class DIServiceHost : ServiceHost
{
    public DIServiceHost(IServiceProvider serviceProvider, Type serviceType, params Uri[] baseAddresses)
        : base(serviceType, baseAddresses)
    {
        Description.Behaviors.Add(new DependencyInjectionServiceBehavior(serviceProvider));
    }
}

public class DependencyInjectionServiceBehavior(IServiceProvider serviceProvider) : IServiceBehavior
{
    public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
        Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
    {
    }

    public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
    {
        foreach (var cdb in serviceHostBase.ChannelDispatchers)
        {
            if (cdb is not ChannelDispatcher cd) continue;
            foreach (var ed in cd.Endpoints)
                ed.DispatchRuntime.InstanceProvider =
                    new DIInstanceProvider(serviceProvider, serviceDescription.ServiceType);
        }
    }

    public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
    {
    }
}

public class DIInstanceProvider(IServiceProvider serviceProvider, Type serviceType) : IInstanceProvider
{
    public object GetInstance(InstanceContext instanceContext)
    {
        return serviceProvider.GetRequiredService(serviceType);
    }

    public object GetInstance(InstanceContext instanceContext, Message message)
    {
        return GetInstance(instanceContext);
    }

    public void ReleaseInstance(InstanceContext instanceContext, object instance)
    {
    }
}