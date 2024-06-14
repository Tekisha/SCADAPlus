﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseManager.UserService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UserService.IUserController")]
    public interface IUserController {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserController/RegisterUser", ReplyAction="http://tempuri.org/IUserController/RegisterUserResponse")]
        bool RegisterUser(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserController/RegisterUser", ReplyAction="http://tempuri.org/IUserController/RegisterUserResponse")]
        System.Threading.Tasks.Task<bool> RegisterUserAsync(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserController/LogIn", ReplyAction="http://tempuri.org/IUserController/LogInResponse")]
        bool LogIn(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserController/LogIn", ReplyAction="http://tempuri.org/IUserController/LogInResponse")]
        System.Threading.Tasks.Task<bool> LogInAsync(string username, string password);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserControllerChannel : DatabaseManager.UserService.IUserController, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserControllerClient : System.ServiceModel.ClientBase<DatabaseManager.UserService.IUserController>, DatabaseManager.UserService.IUserController {
        
        public UserControllerClient() {
        }
        
        public UserControllerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UserControllerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserControllerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserControllerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool RegisterUser(string username, string password) {
            return base.Channel.RegisterUser(username, password);
        }
        
        public System.Threading.Tasks.Task<bool> RegisterUserAsync(string username, string password) {
            return base.Channel.RegisterUserAsync(username, password);
        }
        
        public bool LogIn(string username, string password) {
            return base.Channel.LogIn(username, password);
        }
        
        public System.Threading.Tasks.Task<bool> LogInAsync(string username, string password) {
            return base.Channel.LogInAsync(username, password);
        }
    }
}
