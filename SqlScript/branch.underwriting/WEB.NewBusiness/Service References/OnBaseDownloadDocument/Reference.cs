﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WEB.NewBusiness.OnBaseDownloadDocument {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DocumentBytesInput", Namespace="http://statetrust.com/BinaryDoc")]
    [System.SerializableAttribute()]
    public partial class DocumentBytesInput : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string documentHandleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string overlayField;
        
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
        public string documentHandle {
            get {
                return this.documentHandleField;
            }
            set {
                if ((object.ReferenceEquals(this.documentHandleField, value) != true)) {
                    this.documentHandleField = value;
                    this.RaisePropertyChanged("documentHandle");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string overlay {
            get {
                return this.overlayField;
            }
            set {
                if ((object.ReferenceEquals(this.overlayField, value) != true)) {
                    this.overlayField = value;
                    this.RaisePropertyChanged("overlay");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="DocumentBytesOutput", Namespace="http://statetrust.com/BinaryDoc")]
    [System.SerializableAttribute()]
    public partial class DocumentBytesOutput : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Base64FileStreamField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string extensionField;
        
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
        public string Base64FileStream {
            get {
                return this.Base64FileStreamField;
            }
            set {
                if ((object.ReferenceEquals(this.Base64FileStreamField, value) != true)) {
                    this.Base64FileStreamField = value;
                    this.RaisePropertyChanged("Base64FileStream");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string extension {
            get {
                return this.extensionField;
            }
            set {
                if ((object.ReferenceEquals(this.extensionField, value) != true)) {
                    this.extensionField = value;
                    this.RaisePropertyChanged("extension");
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://statetrust.com/BinaryDoc", ConfigurationName="OnBaseDownloadDocument.HylandOutBoundContract")]
    public interface HylandOutBoundContract {
        
        [System.ServiceModel.OperationContractAttribute(Action="WebServicePublishing/DocData/DocumentData/Get_document_data", ReplyAction="WebServicePublishing/DocData/DocumentData/Get_document_data/response")]
        WEB.NewBusiness.OnBaseDownloadDocument.DocumentBytesOutput Get_document_data(WEB.NewBusiness.OnBaseDownloadDocument.DocumentBytesInput DocumentData);
        
        [System.ServiceModel.OperationContractAttribute(Action="WebServicePublishing/DocData/DocumentData/Get_document_data", ReplyAction="WebServicePublishing/DocData/DocumentData/Get_document_data/response")]
        System.Threading.Tasks.Task<WEB.NewBusiness.OnBaseDownloadDocument.DocumentBytesOutput> Get_document_dataAsync(WEB.NewBusiness.OnBaseDownloadDocument.DocumentBytesInput DocumentData);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface HylandOutBoundContractChannel : WEB.NewBusiness.OnBaseDownloadDocument.HylandOutBoundContract, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class HylandOutBoundContractClient : System.ServiceModel.ClientBase<WEB.NewBusiness.OnBaseDownloadDocument.HylandOutBoundContract>, WEB.NewBusiness.OnBaseDownloadDocument.HylandOutBoundContract {
        
        public HylandOutBoundContractClient() {
        }
        
        public HylandOutBoundContractClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public HylandOutBoundContractClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HylandOutBoundContractClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public HylandOutBoundContractClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public WEB.NewBusiness.OnBaseDownloadDocument.DocumentBytesOutput Get_document_data(WEB.NewBusiness.OnBaseDownloadDocument.DocumentBytesInput DocumentData) {
            return base.Channel.Get_document_data(DocumentData);
        }
        
        public System.Threading.Tasks.Task<WEB.NewBusiness.OnBaseDownloadDocument.DocumentBytesOutput> Get_document_dataAsync(WEB.NewBusiness.OnBaseDownloadDocument.DocumentBytesInput DocumentData) {
            return base.Channel.Get_document_dataAsync(DocumentData);
        }
    }
}
