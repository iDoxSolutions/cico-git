﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cico.Models.SharePoint.SpWs
{
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "CopySoap", Namespace = "http://schemas.microsoft.com/sharepoint/soap/")]
    public partial class Copy : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback CopyIntoItemsLocalOperationCompleted;

        private System.Threading.SendOrPostCallback CopyIntoItemsOperationCompleted;

        private System.Threading.SendOrPostCallback GetItemOperationCompleted;

        private bool useDefaultCredentialsSetExplicitly;

        /// <remarks/>
        public Copy()
        {
            //this.Url = global::WebAcap.Tasks.Properties.Settings.Default.WebAcap_Tasks_Copy_Copy;
            if ((this.IsLocalFileSystemWebService(this.Url) == true))
            {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else
            {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        public new string Url
        {
            get
            {
                return base.Url;
            }
            set
            {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true)
                            && (this.useDefaultCredentialsSetExplicitly == false))
                            && (this.IsLocalFileSystemWebService(value) == false)))
                {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }

        public new bool UseDefaultCredentials
        {
            get
            {
                return base.UseDefaultCredentials;
            }
            set
            {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        /// <remarks/>
        public event CopyIntoItemsLocalCompletedEventHandler CopyIntoItemsLocalCompleted;

        /// <remarks/>
        public event CopyIntoItemsCompletedEventHandler CopyIntoItemsCompleted;

        /// <remarks/>
        public event GetItemCompletedEventHandler GetItemCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/CopyIntoItemsLocal", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public uint CopyIntoItemsLocal(string SourceUrl, string[] DestinationUrls, out CopyResult[] Results)
        {
            object[] results1 = this.Invoke("CopyIntoItemsLocal", new object[] {
                        SourceUrl,
                        DestinationUrls});
            Results = ((CopyResult[])(results1[1]));
            return ((uint)(results1[0]));
        }

        /// <remarks/>
        public void CopyIntoItemsLocalAsync(string SourceUrl, string[] DestinationUrls)
        {
            this.CopyIntoItemsLocalAsync(SourceUrl, DestinationUrls, null);
        }

        /// <remarks/>
        public void CopyIntoItemsLocalAsync(string SourceUrl, string[] DestinationUrls, object userState)
        {
            if ((this.CopyIntoItemsLocalOperationCompleted == null))
            {
                this.CopyIntoItemsLocalOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCopyIntoItemsLocalOperationCompleted);
            }
            this.InvokeAsync("CopyIntoItemsLocal", new object[] {
                        SourceUrl,
                        DestinationUrls}, this.CopyIntoItemsLocalOperationCompleted, userState);
        }

        private void OnCopyIntoItemsLocalOperationCompleted(object arg)
        {
            if ((this.CopyIntoItemsLocalCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CopyIntoItemsLocalCompleted(this, new CopyIntoItemsLocalCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/CopyIntoItems", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public uint CopyIntoItems(string SourceUrl, string[] DestinationUrls, FieldInformation[] Fields, [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] Stream, out CopyResult[] Results)
        {
            object[] results1 = this.Invoke("CopyIntoItems", new object[] {
                        SourceUrl,
                        DestinationUrls,
                        Fields,
                        Stream});
            Results = ((CopyResult[])(results1[1]));
            return ((uint)(results1[0]));
        }

        /// <remarks/>
        public void CopyIntoItemsAsync(string SourceUrl, string[] DestinationUrls, FieldInformation[] Fields, byte[] Stream)
        {
            this.CopyIntoItemsAsync(SourceUrl, DestinationUrls, Fields, Stream, null);
        }

        /// <remarks/>
        public void CopyIntoItemsAsync(string SourceUrl, string[] DestinationUrls, FieldInformation[] Fields, byte[] Stream, object userState)
        {
            if ((this.CopyIntoItemsOperationCompleted == null))
            {
                this.CopyIntoItemsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCopyIntoItemsOperationCompleted);
            }
            this.InvokeAsync("CopyIntoItems", new object[] {
                        SourceUrl,
                        DestinationUrls,
                        Fields,
                        Stream}, this.CopyIntoItemsOperationCompleted, userState);
        }

        private void OnCopyIntoItemsOperationCompleted(object arg)
        {
            if ((this.CopyIntoItemsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CopyIntoItemsCompleted(this, new CopyIntoItemsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/GetItem", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public uint GetItem(string Url, out FieldInformation[] Fields, [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] out byte[] Stream)
        {
            object[] results = this.Invoke("GetItem", new object[] {
                        Url});
            Fields = ((FieldInformation[])(results[1]));
            Stream = ((byte[])(results[2]));
            return ((uint)(results[0]));
        }

        /// <remarks/>
        public void GetItemAsync(string Url)
        {
            this.GetItemAsync(Url, null);
        }

        /// <remarks/>
        public void GetItemAsync(string Url, object userState)
        {
            if ((this.GetItemOperationCompleted == null))
            {
                this.GetItemOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetItemOperationCompleted);
            }
            this.InvokeAsync("GetItem", new object[] {
                        Url}, this.GetItemOperationCompleted, userState);
        }

        private void OnGetItemOperationCompleted(object arg)
        {
            if ((this.GetItemCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetItemCompleted(this, new GetItemCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        public new void CancelAsync(object userState)
        {
            base.CancelAsync(userState);
        }

        private bool IsLocalFileSystemWebService(string url)
        {
            if (((url == null)
                        || (url == string.Empty)))
            {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024)
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0)))
            {
                return true;
            }
            return false;
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.microsoft.com/sharepoint/soap/")]
    public partial class CopyResult
    {

        private CopyErrorCode errorCodeField;

        private string errorMessageField;

        private string destinationUrlField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public CopyErrorCode ErrorCode
        {
            get
            {
                return this.errorCodeField;
            }
            set
            {
                this.errorCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ErrorMessage
        {
            get
            {
                return this.errorMessageField;
            }
            set
            {
                this.errorMessageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DestinationUrl
        {
            get
            {
                return this.destinationUrlField;
            }
            set
            {
                this.destinationUrlField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.microsoft.com/sharepoint/soap/")]
    public enum CopyErrorCode
    {

        /// <remarks/>
        Success,

        /// <remarks/>
        DestinationInvalid,

        /// <remarks/>
        DestinationMWS,

        /// <remarks/>
        SourceInvalid,

        /// <remarks/>
        DestinationCheckedOut,

        /// <remarks/>
        InvalidUrl,

        /// <remarks/>
        Unknown,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.microsoft.com/sharepoint/soap/")]
    public partial class FieldInformation
    {

        private FieldType typeField;

        private string displayNameField;

        private string internalNameField;

        private System.Guid idField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public FieldType Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DisplayName
        {
            get
            {
                return this.displayNameField;
            }
            set
            {
                this.displayNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string InternalName
        {
            get
            {
                return this.internalNameField;
            }
            set
            {
                this.internalNameField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.Guid Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.225")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://schemas.microsoft.com/sharepoint/soap/")]
    public enum FieldType
    {

        /// <remarks/>
        Invalid,

        /// <remarks/>
        Integer,

        /// <remarks/>
        Text,

        /// <remarks/>
        Note,

        /// <remarks/>
        DateTime,

        /// <remarks/>
        Counter,

        /// <remarks/>
        Choice,

        /// <remarks/>
        Lookup,

        /// <remarks/>
        Boolean,

        /// <remarks/>
        Number,

        /// <remarks/>
        Currency,

        /// <remarks/>
        URL,

        /// <remarks/>
        Computed,

        /// <remarks/>
        Threading,

        /// <remarks/>
        Guid,

        /// <remarks/>
        MultiChoice,

        /// <remarks/>
        GridChoice,

        /// <remarks/>
        Calculated,

        /// <remarks/>
        File,

        /// <remarks/>
        Attachments,

        /// <remarks/>
        User,

        /// <remarks/>
        Recurrence,

        /// <remarks/>
        CrossProjectLink,

        /// <remarks/>
        ModStat,

        /// <remarks/>
        AllDayEvent,

        /// <remarks/>
        Error,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void CopyIntoItemsLocalCompletedEventHandler(object sender, CopyIntoItemsLocalCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CopyIntoItemsLocalCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal CopyIntoItemsLocalCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public uint Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((uint)(this.results[0]));
            }
        }

        /// <remarks/>
        public CopyResult[] Results
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((CopyResult[])(this.results[1]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void CopyIntoItemsCompletedEventHandler(object sender, CopyIntoItemsCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CopyIntoItemsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal CopyIntoItemsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public uint Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((uint)(this.results[0]));
            }
        }

        /// <remarks/>
        public CopyResult[] Results
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((CopyResult[])(this.results[1]));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetItemCompletedEventHandler(object sender, GetItemCompletedEventArgs e);

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetItemCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
    {

        private object[] results;

        internal GetItemCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
            base(exception, cancelled, userState)
        {
            this.results = results;
        }

        /// <remarks/>
        public uint Result
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((uint)(this.results[0]));
            }
        }

        /// <remarks/>
        public FieldInformation[] Fields
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((FieldInformation[])(this.results[1]));
            }
        }

        /// <remarks/>
        public byte[] Stream
        {
            get
            {
                this.RaiseExceptionIfNecessary();
                return ((byte[])(this.results[2]));
            }
        }
    }
}