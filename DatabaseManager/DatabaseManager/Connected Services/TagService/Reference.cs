﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseManager.TagService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TagDto", Namespace="http://schemas.datacontract.org/2004/07/SCADA_Core.DTOs")]
    [System.SerializableAttribute()]
    public partial class TagDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool AlarmsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DriverField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double HighLimitField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IoAddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double LowLimitField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool OnOffScanField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ScanTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UnitsField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Alarms {
            get {
                return this.AlarmsField;
            }
            set {
                if ((this.AlarmsField.Equals(value) != true)) {
                    this.AlarmsField = value;
                    this.RaisePropertyChanged("Alarms");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Driver {
            get {
                return this.DriverField;
            }
            set {
                if ((object.ReferenceEquals(this.DriverField, value) != true)) {
                    this.DriverField = value;
                    this.RaisePropertyChanged("Driver");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double HighLimit {
            get {
                return this.HighLimitField;
            }
            set {
                if ((this.HighLimitField.Equals(value) != true)) {
                    this.HighLimitField = value;
                    this.RaisePropertyChanged("HighLimit");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string IoAddress {
            get {
                return this.IoAddressField;
            }
            set {
                if ((object.ReferenceEquals(this.IoAddressField, value) != true)) {
                    this.IoAddressField = value;
                    this.RaisePropertyChanged("IoAddress");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double LowLimit {
            get {
                return this.LowLimitField;
            }
            set {
                if ((this.LowLimitField.Equals(value) != true)) {
                    this.LowLimitField = value;
                    this.RaisePropertyChanged("LowLimit");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool OnOffScan {
            get {
                return this.OnOffScanField;
            }
            set {
                if ((this.OnOffScanField.Equals(value) != true)) {
                    this.OnOffScanField = value;
                    this.RaisePropertyChanged("OnOffScan");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ScanTime {
            get {
                return this.ScanTimeField;
            }
            set {
                if ((this.ScanTimeField.Equals(value) != true)) {
                    this.ScanTimeField = value;
                    this.RaisePropertyChanged("ScanTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Units {
            get {
                return this.UnitsField;
            }
            set {
                if ((object.ReferenceEquals(this.UnitsField, value) != true)) {
                    this.UnitsField = value;
                    this.RaisePropertyChanged("Units");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BaseTagInfoDto", Namespace="http://schemas.datacontract.org/2004/07/SCADA_Core.DTOs")]
    [System.SerializableAttribute()]
    public partial class BaseTagInfoDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IdField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Id {
            get {
                return this.IdField;
            }
            set {
                if ((object.ReferenceEquals(this.IdField, value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AlarmDto", Namespace="http://schemas.datacontract.org/2004/07/SCADA_Core.DTOs")]
    [System.SerializableAttribute()]
    public partial class AlarmDto : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool AcknowledgedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AlarmNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double LimitField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private DatabaseManager.TagService.AlarmPriority PriorityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TagIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TypeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Acknowledged {
            get {
                return this.AcknowledgedField;
            }
            set {
                if ((this.AcknowledgedField.Equals(value) != true)) {
                    this.AcknowledgedField = value;
                    this.RaisePropertyChanged("Acknowledged");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AlarmName {
            get {
                return this.AlarmNameField;
            }
            set {
                if ((object.ReferenceEquals(this.AlarmNameField, value) != true)) {
                    this.AlarmNameField = value;
                    this.RaisePropertyChanged("AlarmName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double Limit {
            get {
                return this.LimitField;
            }
            set {
                if ((this.LimitField.Equals(value) != true)) {
                    this.LimitField = value;
                    this.RaisePropertyChanged("Limit");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public DatabaseManager.TagService.AlarmPriority Priority {
            get {
                return this.PriorityField;
            }
            set {
                if ((this.PriorityField.Equals(value) != true)) {
                    this.PriorityField = value;
                    this.RaisePropertyChanged("Priority");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TagId {
            get {
                return this.TagIdField;
            }
            set {
                if ((object.ReferenceEquals(this.TagIdField, value) != true)) {
                    this.TagIdField = value;
                    this.RaisePropertyChanged("TagId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Type {
            get {
                return this.TypeField;
            }
            set {
                if ((object.ReferenceEquals(this.TypeField, value) != true)) {
                    this.TypeField = value;
                    this.RaisePropertyChanged("Type");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AlarmPriority", Namespace="http://schemas.datacontract.org/2004/07/SCADA_Core.Enums")]
    public enum AlarmPriority : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        High = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Medium = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Low = 3,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TagService.ITagController")]
    public interface ITagController {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/GetTagValue", ReplyAction="http://tempuri.org/ITagController/GetTagValueResponse")]
        double GetTagValue(string address, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/GetTagValue", ReplyAction="http://tempuri.org/ITagController/GetTagValueResponse")]
        System.Threading.Tasks.Task<double> GetTagValueAsync(string address, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/AddTag", ReplyAction="http://tempuri.org/ITagController/AddTagResponse")]
        void AddTag(DatabaseManager.TagService.TagDto tagDto, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/AddTag", ReplyAction="http://tempuri.org/ITagController/AddTagResponse")]
        System.Threading.Tasks.Task AddTagAsync(DatabaseManager.TagService.TagDto tagDto, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/RemoveTag", ReplyAction="http://tempuri.org/ITagController/RemoveTagResponse")]
        void RemoveTag(string id, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/RemoveTag", ReplyAction="http://tempuri.org/ITagController/RemoveTagResponse")]
        System.Threading.Tasks.Task RemoveTagAsync(string id, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/ChangeOutputValue", ReplyAction="http://tempuri.org/ITagController/ChangeOutputValueResponse")]
        void ChangeOutputValue(string tagId, double newValue, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/ChangeOutputValue", ReplyAction="http://tempuri.org/ITagController/ChangeOutputValueResponse")]
        System.Threading.Tasks.Task ChangeOutputValueAsync(string tagId, double newValue, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/GetOutputValue", ReplyAction="http://tempuri.org/ITagController/GetOutputValueResponse")]
        double GetOutputValue(string tagId, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/GetOutputValue", ReplyAction="http://tempuri.org/ITagController/GetOutputValueResponse")]
        System.Threading.Tasks.Task<double> GetOutputValueAsync(string tagId, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/TurnScanOnOff", ReplyAction="http://tempuri.org/ITagController/TurnScanOnOffResponse")]
        void TurnScanOnOff(string tagId, bool onOff, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/TurnScanOnOff", ReplyAction="http://tempuri.org/ITagController/TurnScanOnOffResponse")]
        System.Threading.Tasks.Task TurnScanOnOffAsync(string tagId, bool onOff, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/GetAllTags", ReplyAction="http://tempuri.org/ITagController/GetAllTagsResponse")]
        DatabaseManager.TagService.BaseTagInfoDto[] GetAllTags(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/GetAllTags", ReplyAction="http://tempuri.org/ITagController/GetAllTagsResponse")]
        System.Threading.Tasks.Task<DatabaseManager.TagService.BaseTagInfoDto[]> GetAllTagsAsync(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/GetAlarm", ReplyAction="http://tempuri.org/ITagController/GetAlarmResponse")]
        DatabaseManager.TagService.AlarmDto GetAlarm(string alarmName, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/GetAlarm", ReplyAction="http://tempuri.org/ITagController/GetAlarmResponse")]
        System.Threading.Tasks.Task<DatabaseManager.TagService.AlarmDto> GetAlarmAsync(string alarmName, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/GetInvokedAlarms", ReplyAction="http://tempuri.org/ITagController/GetInvokedAlarmsResponse")]
        DatabaseManager.TagService.AlarmDto[] GetInvokedAlarms(string tagId, double value, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/GetInvokedAlarms", ReplyAction="http://tempuri.org/ITagController/GetInvokedAlarmsResponse")]
        System.Threading.Tasks.Task<DatabaseManager.TagService.AlarmDto[]> GetInvokedAlarmsAsync(string tagId, double value, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/CreateAlarm", ReplyAction="http://tempuri.org/ITagController/CreateAlarmResponse")]
        DatabaseManager.TagService.AlarmDto CreateAlarm(DatabaseManager.TagService.AlarmDto newAlarm, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/CreateAlarm", ReplyAction="http://tempuri.org/ITagController/CreateAlarmResponse")]
        System.Threading.Tasks.Task<DatabaseManager.TagService.AlarmDto> CreateAlarmAsync(DatabaseManager.TagService.AlarmDto newAlarm, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/DeleteAlarm", ReplyAction="http://tempuri.org/ITagController/DeleteAlarmResponse")]
        DatabaseManager.TagService.AlarmDto DeleteAlarm(string alarmName, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/DeleteAlarm", ReplyAction="http://tempuri.org/ITagController/DeleteAlarmResponse")]
        System.Threading.Tasks.Task<DatabaseManager.TagService.AlarmDto> DeleteAlarmAsync(string alarmName, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/UpdateAlarm", ReplyAction="http://tempuri.org/ITagController/UpdateAlarmResponse")]
        DatabaseManager.TagService.AlarmDto UpdateAlarm(DatabaseManager.TagService.AlarmDto updatedAlarm, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/UpdateAlarm", ReplyAction="http://tempuri.org/ITagController/UpdateAlarmResponse")]
        System.Threading.Tasks.Task<DatabaseManager.TagService.AlarmDto> UpdateAlarmAsync(DatabaseManager.TagService.AlarmDto updatedAlarm, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/GetAlarmsByTag", ReplyAction="http://tempuri.org/ITagController/GetAlarmsByTagResponse")]
        DatabaseManager.TagService.AlarmDto[] GetAlarmsByTag(string tagId, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/GetAlarmsByTag", ReplyAction="http://tempuri.org/ITagController/GetAlarmsByTagResponse")]
        System.Threading.Tasks.Task<DatabaseManager.TagService.AlarmDto[]> GetAlarmsByTagAsync(string tagId, string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/GetAllAlarms", ReplyAction="http://tempuri.org/ITagController/GetAllAlarmsResponse")]
        DatabaseManager.TagService.AlarmDto[] GetAllAlarms(string token);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITagController/GetAllAlarms", ReplyAction="http://tempuri.org/ITagController/GetAllAlarmsResponse")]
        System.Threading.Tasks.Task<DatabaseManager.TagService.AlarmDto[]> GetAllAlarmsAsync(string token);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITagControllerChannel : DatabaseManager.TagService.ITagController, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TagControllerClient : System.ServiceModel.ClientBase<DatabaseManager.TagService.ITagController>, DatabaseManager.TagService.ITagController {
        
        public TagControllerClient() {
        }
        
        public TagControllerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TagControllerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TagControllerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TagControllerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public double GetTagValue(string address, string token) {
            return base.Channel.GetTagValue(address, token);
        }
        
        public System.Threading.Tasks.Task<double> GetTagValueAsync(string address, string token) {
            return base.Channel.GetTagValueAsync(address, token);
        }
        
        public void AddTag(DatabaseManager.TagService.TagDto tagDto, string token) {
            base.Channel.AddTag(tagDto, token);
        }
        
        public System.Threading.Tasks.Task AddTagAsync(DatabaseManager.TagService.TagDto tagDto, string token) {
            return base.Channel.AddTagAsync(tagDto, token);
        }
        
        public void RemoveTag(string id, string token) {
            base.Channel.RemoveTag(id, token);
        }
        
        public System.Threading.Tasks.Task RemoveTagAsync(string id, string token) {
            return base.Channel.RemoveTagAsync(id, token);
        }
        
        public void ChangeOutputValue(string tagId, double newValue, string token) {
            base.Channel.ChangeOutputValue(tagId, newValue, token);
        }
        
        public System.Threading.Tasks.Task ChangeOutputValueAsync(string tagId, double newValue, string token) {
            return base.Channel.ChangeOutputValueAsync(tagId, newValue, token);
        }
        
        public double GetOutputValue(string tagId, string token) {
            return base.Channel.GetOutputValue(tagId, token);
        }
        
        public System.Threading.Tasks.Task<double> GetOutputValueAsync(string tagId, string token) {
            return base.Channel.GetOutputValueAsync(tagId, token);
        }
        
        public void TurnScanOnOff(string tagId, bool onOff, string token) {
            base.Channel.TurnScanOnOff(tagId, onOff, token);
        }
        
        public System.Threading.Tasks.Task TurnScanOnOffAsync(string tagId, bool onOff, string token) {
            return base.Channel.TurnScanOnOffAsync(tagId, onOff, token);
        }
        
        public DatabaseManager.TagService.BaseTagInfoDto[] GetAllTags(string token) {
            return base.Channel.GetAllTags(token);
        }
        
        public System.Threading.Tasks.Task<DatabaseManager.TagService.BaseTagInfoDto[]> GetAllTagsAsync(string token) {
            return base.Channel.GetAllTagsAsync(token);
        }
        
        public DatabaseManager.TagService.AlarmDto GetAlarm(string alarmName, string token) {
            return base.Channel.GetAlarm(alarmName, token);
        }
        
        public System.Threading.Tasks.Task<DatabaseManager.TagService.AlarmDto> GetAlarmAsync(string alarmName, string token) {
            return base.Channel.GetAlarmAsync(alarmName, token);
        }
        
        public DatabaseManager.TagService.AlarmDto[] GetInvokedAlarms(string tagId, double value, string token) {
            return base.Channel.GetInvokedAlarms(tagId, value, token);
        }
        
        public System.Threading.Tasks.Task<DatabaseManager.TagService.AlarmDto[]> GetInvokedAlarmsAsync(string tagId, double value, string token) {
            return base.Channel.GetInvokedAlarmsAsync(tagId, value, token);
        }
        
        public DatabaseManager.TagService.AlarmDto CreateAlarm(DatabaseManager.TagService.AlarmDto newAlarm, string token) {
            return base.Channel.CreateAlarm(newAlarm, token);
        }
        
        public System.Threading.Tasks.Task<DatabaseManager.TagService.AlarmDto> CreateAlarmAsync(DatabaseManager.TagService.AlarmDto newAlarm, string token) {
            return base.Channel.CreateAlarmAsync(newAlarm, token);
        }
        
        public DatabaseManager.TagService.AlarmDto DeleteAlarm(string alarmName, string token) {
            return base.Channel.DeleteAlarm(alarmName, token);
        }
        
        public System.Threading.Tasks.Task<DatabaseManager.TagService.AlarmDto> DeleteAlarmAsync(string alarmName, string token) {
            return base.Channel.DeleteAlarmAsync(alarmName, token);
        }
        
        public DatabaseManager.TagService.AlarmDto UpdateAlarm(DatabaseManager.TagService.AlarmDto updatedAlarm, string token) {
            return base.Channel.UpdateAlarm(updatedAlarm, token);
        }
        
        public System.Threading.Tasks.Task<DatabaseManager.TagService.AlarmDto> UpdateAlarmAsync(DatabaseManager.TagService.AlarmDto updatedAlarm, string token) {
            return base.Channel.UpdateAlarmAsync(updatedAlarm, token);
        }
        
        public DatabaseManager.TagService.AlarmDto[] GetAlarmsByTag(string tagId, string token) {
            return base.Channel.GetAlarmsByTag(tagId, token);
        }
        
        public System.Threading.Tasks.Task<DatabaseManager.TagService.AlarmDto[]> GetAlarmsByTagAsync(string tagId, string token) {
            return base.Channel.GetAlarmsByTagAsync(tagId, token);
        }
        
        public DatabaseManager.TagService.AlarmDto[] GetAllAlarms(string token) {
            return base.Channel.GetAllAlarms(token);
        }
        
        public System.Threading.Tasks.Task<DatabaseManager.TagService.AlarmDto[]> GetAllAlarmsAsync(string token) {
            return base.Channel.GetAllAlarmsAsync(token);
        }
    }
}
