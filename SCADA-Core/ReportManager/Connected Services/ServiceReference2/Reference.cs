﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ServiceReference2
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference2.IUserController")]
    public interface IUserController
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserController/RegisterUser", ReplyAction="http://tempuri.org/IUserController/RegisterUserResponse")]
        bool RegisterUser(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserController/RegisterUser", ReplyAction="http://tempuri.org/IUserController/RegisterUserResponse")]
        System.Threading.Tasks.Task<bool> RegisterUserAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserController/LogIn", ReplyAction="http://tempuri.org/IUserController/LogInResponse")]
        string LogIn(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserController/LogIn", ReplyAction="http://tempuri.org/IUserController/LogInResponse")]
        System.Threading.Tasks.Task<string> LogInAsync(string username, string password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    public interface IUserControllerChannel : ServiceReference2.IUserController, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
    public partial class UserControllerClient : System.ServiceModel.ClientBase<ServiceReference2.IUserController>, ServiceReference2.IUserController
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public UserControllerClient() : 
                base(UserControllerClient.GetDefaultBinding(), UserControllerClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_IUserController.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public UserControllerClient(EndpointConfiguration endpointConfiguration) : 
                base(UserControllerClient.GetBindingForEndpoint(endpointConfiguration), UserControllerClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public UserControllerClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(UserControllerClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public UserControllerClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(UserControllerClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public UserControllerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public bool RegisterUser(string username, string password)
        {
            return base.Channel.RegisterUser(username, password);
        }
        
        public System.Threading.Tasks.Task<bool> RegisterUserAsync(string username, string password)
        {
            return base.Channel.RegisterUserAsync(username, password);
        }
        
        public string LogIn(string username, string password)
        {
            return base.Channel.LogIn(username, password);
        }
        
        public System.Threading.Tasks.Task<string> LogInAsync(string username, string password)
        {
            return base.Channel.LogInAsync(username, password);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IUserController))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_IUserController))
            {
                return new System.ServiceModel.EndpointAddress("http://localhost:8733/SCADA/UserController/");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return UserControllerClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_IUserController);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return UserControllerClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_IUserController);
        }
        
        public enum EndpointConfiguration
        {
            
            BasicHttpBinding_IUserController,
        }
    }
}
