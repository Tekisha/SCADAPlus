using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Activation;

namespace SCADA_Core.Utilities
{
    public class DIServiceHostFactory : ServiceHostFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public DIServiceHostFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            return new DIServiceHost(_serviceProvider, serviceType, baseAddresses);
        }
    }

    public class DIServiceHost : ServiceHost
    {
        public DIServiceHost(IServiceProvider serviceProvider, Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            foreach (var cd in ImplementedContracts.Values)
            {
                cd.Behaviors.Add(new DependencyInjectionServiceBehavior(serviceProvider));
            }
        }
    }

    public class DependencyInjectionServiceBehavior : IServiceBehavior
    {
        private readonly IServiceProvider _serviceProvider;

        public DependencyInjectionServiceBehavior(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters) { }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcherBase cdb in serviceHostBase.ChannelDispatchers)
            {
                var cd = cdb as ChannelDispatcher;
                if (cd != null)
                {
                    foreach (var ed in cd.Endpoints)
                    {
                        ed.DispatchRuntime.InstanceProvider = new DIInstanceProvider(_serviceProvider, serviceDescription.ServiceType);
                    }
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase) { }
    }

    public class DIInstanceProvider : IInstanceProvider
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Type _serviceType;

        public DIInstanceProvider(IServiceProvider serviceProvider, Type serviceType)
        {
            _serviceProvider = serviceProvider;
            _serviceType = serviceType;
        }

        public object GetInstance(InstanceContext instanceContext)
        {
            return _serviceProvider.GetRequiredService(_serviceType);
        }

        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return GetInstance(instanceContext);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance) { }
    }
}
