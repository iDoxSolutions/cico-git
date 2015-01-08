﻿namespace Cico.Models.SharePoint.SpWs
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name = "ListsSoap", Namespace = "http://schemas.microsoft.com/sharepoint/soap/")]
    public partial class Lists : System.Web.Services.Protocols.SoapHttpClientProtocol
    {

        private System.Threading.SendOrPostCallback GetListOperationCompleted;

        private System.Threading.SendOrPostCallback GetListAndViewOperationCompleted;

        private System.Threading.SendOrPostCallback DeleteListOperationCompleted;

        private System.Threading.SendOrPostCallback AddListOperationCompleted;

        private System.Threading.SendOrPostCallback AddListFromFeatureOperationCompleted;

        private System.Threading.SendOrPostCallback UpdateListOperationCompleted;

        private System.Threading.SendOrPostCallback GetListCollectionOperationCompleted;

        private System.Threading.SendOrPostCallback GetListItemsOperationCompleted;

        private System.Threading.SendOrPostCallback GetListItemChangesOperationCompleted;

        private System.Threading.SendOrPostCallback GetListItemChangesSinceTokenOperationCompleted;

        private System.Threading.SendOrPostCallback UpdateListItemsOperationCompleted;

        private System.Threading.SendOrPostCallback AddDiscussionBoardItemOperationCompleted;

        private System.Threading.SendOrPostCallback GetVersionCollectionOperationCompleted;

        private System.Threading.SendOrPostCallback AddAttachmentOperationCompleted;

        private System.Threading.SendOrPostCallback GetAttachmentCollectionOperationCompleted;

        private System.Threading.SendOrPostCallback DeleteAttachmentOperationCompleted;

        private System.Threading.SendOrPostCallback CheckOutFileOperationCompleted;

        private System.Threading.SendOrPostCallback UndoCheckOutOperationCompleted;

        private System.Threading.SendOrPostCallback CheckInFileOperationCompleted;

        private System.Threading.SendOrPostCallback GetListContentTypesOperationCompleted;

        private System.Threading.SendOrPostCallback GetListContentTypeOperationCompleted;

        private System.Threading.SendOrPostCallback CreateContentTypeOperationCompleted;

        private System.Threading.SendOrPostCallback UpdateContentTypeOperationCompleted;

        private System.Threading.SendOrPostCallback DeleteContentTypeOperationCompleted;

        private System.Threading.SendOrPostCallback UpdateContentTypeXmlDocumentOperationCompleted;

        private System.Threading.SendOrPostCallback UpdateContentTypesXmlDocumentOperationCompleted;

        private System.Threading.SendOrPostCallback DeleteContentTypeXmlDocumentOperationCompleted;

        private System.Threading.SendOrPostCallback ApplyContentTypeToListOperationCompleted;

        private bool useDefaultCredentialsSetExplicitly;

        /// <remarks/>
        public Lists()
        {
            //this.Url = global::WebAcap.Tasks.Properties.Settings.Default.WebAcap_Tasks_Lists_Lists;
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
        public event GetListCompletedEventHandler GetListCompleted;

        /// <remarks/>
        public event GetListAndViewCompletedEventHandler GetListAndViewCompleted;

        /// <remarks/>
        public event DeleteListCompletedEventHandler DeleteListCompleted;

        /// <remarks/>
        public event AddListCompletedEventHandler AddListCompleted;

        /// <remarks/>
        public event AddListFromFeatureCompletedEventHandler AddListFromFeatureCompleted;

        /// <remarks/>
        public event UpdateListCompletedEventHandler UpdateListCompleted;

        /// <remarks/>
        public event GetListCollectionCompletedEventHandler GetListCollectionCompleted;

        /// <remarks/>
        public event GetListItemsCompletedEventHandler GetListItemsCompleted;

        /// <remarks/>
        public event GetListItemChangesCompletedEventHandler GetListItemChangesCompleted;

        /// <remarks/>
        public event GetListItemChangesSinceTokenCompletedEventHandler GetListItemChangesSinceTokenCompleted;

        /// <remarks/>
        public event UpdateListItemsCompletedEventHandler UpdateListItemsCompleted;

        /// <remarks/>
        public event AddDiscussionBoardItemCompletedEventHandler AddDiscussionBoardItemCompleted;

        /// <remarks/>
        public event GetVersionCollectionCompletedEventHandler GetVersionCollectionCompleted;

        /// <remarks/>
        public event AddAttachmentCompletedEventHandler AddAttachmentCompleted;

        /// <remarks/>
        public event GetAttachmentCollectionCompletedEventHandler GetAttachmentCollectionCompleted;

        /// <remarks/>
        public event DeleteAttachmentCompletedEventHandler DeleteAttachmentCompleted;

        /// <remarks/>
        public event CheckOutFileCompletedEventHandler CheckOutFileCompleted;

        /// <remarks/>
        public event UndoCheckOutCompletedEventHandler UndoCheckOutCompleted;

        /// <remarks/>
        public event CheckInFileCompletedEventHandler CheckInFileCompleted;

        /// <remarks/>
        public event GetListContentTypesCompletedEventHandler GetListContentTypesCompleted;

        /// <remarks/>
        public event GetListContentTypeCompletedEventHandler GetListContentTypeCompleted;

        /// <remarks/>
        public event CreateContentTypeCompletedEventHandler CreateContentTypeCompleted;

        /// <remarks/>
        public event UpdateContentTypeCompletedEventHandler UpdateContentTypeCompleted;

        /// <remarks/>
        public event DeleteContentTypeCompletedEventHandler DeleteContentTypeCompleted;

        /// <remarks/>
        public event UpdateContentTypeXmlDocumentCompletedEventHandler UpdateContentTypeXmlDocumentCompleted;

        /// <remarks/>
        public event UpdateContentTypesXmlDocumentCompletedEventHandler UpdateContentTypesXmlDocumentCompleted;

        /// <remarks/>
        public event DeleteContentTypeXmlDocumentCompletedEventHandler DeleteContentTypeXmlDocumentCompleted;

        /// <remarks/>
        public event ApplyContentTypeToListCompletedEventHandler ApplyContentTypeToListCompleted;

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/GetList", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetList(string listName)
        {
            object[] results = this.Invoke("GetList", new object[] {
                listName});
            return ((System.Xml.XmlNode)(results[0]));
        }

        /// <remarks/>
        public void GetListAsync(string listName)
        {
            this.GetListAsync(listName, null);
        }

        /// <remarks/>
        public void GetListAsync(string listName, object userState)
        {
            if ((this.GetListOperationCompleted == null))
            {
                this.GetListOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetListOperationCompleted);
            }
            this.InvokeAsync("GetList", new object[] {
                listName}, this.GetListOperationCompleted, userState);
        }

        private void OnGetListOperationCompleted(object arg)
        {
            if ((this.GetListCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetListCompleted(this, new GetListCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/GetListAndView", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetListAndView(string listName, string viewName)
        {
            object[] results = this.Invoke("GetListAndView", new object[] {
                listName,
                viewName});
            return ((System.Xml.XmlNode)(results[0]));
        }

        /// <remarks/>
        public void GetListAndViewAsync(string listName, string viewName)
        {
            this.GetListAndViewAsync(listName, viewName, null);
        }

        /// <remarks/>
        public void GetListAndViewAsync(string listName, string viewName, object userState)
        {
            if ((this.GetListAndViewOperationCompleted == null))
            {
                this.GetListAndViewOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetListAndViewOperationCompleted);
            }
            this.InvokeAsync("GetListAndView", new object[] {
                listName,
                viewName}, this.GetListAndViewOperationCompleted, userState);
        }

        private void OnGetListAndViewOperationCompleted(object arg)
        {
            if ((this.GetListAndViewCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetListAndViewCompleted(this, new GetListAndViewCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/DeleteList", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void DeleteList(string listName)
        {
            this.Invoke("DeleteList", new object[] {
                listName});
        }

        /// <remarks/>
        public void DeleteListAsync(string listName)
        {
            this.DeleteListAsync(listName, null);
        }

        /// <remarks/>
        public void DeleteListAsync(string listName, object userState)
        {
            if ((this.DeleteListOperationCompleted == null))
            {
                this.DeleteListOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteListOperationCompleted);
            }
            this.InvokeAsync("DeleteList", new object[] {
                listName}, this.DeleteListOperationCompleted, userState);
        }

        private void OnDeleteListOperationCompleted(object arg)
        {
            if ((this.DeleteListCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteListCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/AddList", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode AddList(string listName, string description, int templateID)
        {
            object[] results = this.Invoke("AddList", new object[] {
                listName,
                description,
                templateID});
            return ((System.Xml.XmlNode)(results[0]));
        }

        /// <remarks/>
        public void AddListAsync(string listName, string description, int templateID)
        {
            this.AddListAsync(listName, description, templateID, null);
        }

        /// <remarks/>
        public void AddListAsync(string listName, string description, int templateID, object userState)
        {
            if ((this.AddListOperationCompleted == null))
            {
                this.AddListOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddListOperationCompleted);
            }
            this.InvokeAsync("AddList", new object[] {
                listName,
                description,
                templateID}, this.AddListOperationCompleted, userState);
        }

        private void OnAddListOperationCompleted(object arg)
        {
            if ((this.AddListCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddListCompleted(this, new AddListCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/AddListFromFeature", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode AddListFromFeature(string listName, string description, System.Guid featureID, int templateID)
        {
            object[] results = this.Invoke("AddListFromFeature", new object[] {
                listName,
                description,
                featureID,
                templateID});
            return ((System.Xml.XmlNode)(results[0]));
        }

        /// <remarks/>
        public void AddListFromFeatureAsync(string listName, string description, System.Guid featureID, int templateID)
        {
            this.AddListFromFeatureAsync(listName, description, featureID, templateID, null);
        }

        /// <remarks/>
        public void AddListFromFeatureAsync(string listName, string description, System.Guid featureID, int templateID, object userState)
        {
            if ((this.AddListFromFeatureOperationCompleted == null))
            {
                this.AddListFromFeatureOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddListFromFeatureOperationCompleted);
            }
            this.InvokeAsync("AddListFromFeature", new object[] {
                listName,
                description,
                featureID,
                templateID}, this.AddListFromFeatureOperationCompleted, userState);
        }

        private void OnAddListFromFeatureOperationCompleted(object arg)
        {
            if ((this.AddListFromFeatureCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddListFromFeatureCompleted(this, new AddListFromFeatureCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/UpdateList", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode UpdateList(string listName, System.Xml.XmlNode listProperties, System.Xml.XmlNode newFields, System.Xml.XmlNode updateFields, System.Xml.XmlNode deleteFields, string listVersion)
        {
            object[] results = this.Invoke("UpdateList", new object[] {
                listName,
                listProperties,
                newFields,
                updateFields,
                deleteFields,
                listVersion});
            return ((System.Xml.XmlNode)(results[0]));
        }

        /// <remarks/>
        public void UpdateListAsync(string listName, System.Xml.XmlNode listProperties, System.Xml.XmlNode newFields, System.Xml.XmlNode updateFields, System.Xml.XmlNode deleteFields, string listVersion)
        {
            this.UpdateListAsync(listName, listProperties, newFields, updateFields, deleteFields, listVersion, null);
        }

        /// <remarks/>
        public void UpdateListAsync(string listName, System.Xml.XmlNode listProperties, System.Xml.XmlNode newFields, System.Xml.XmlNode updateFields, System.Xml.XmlNode deleteFields, string listVersion, object userState)
        {
            if ((this.UpdateListOperationCompleted == null))
            {
                this.UpdateListOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateListOperationCompleted);
            }
            this.InvokeAsync("UpdateList", new object[] {
                listName,
                listProperties,
                newFields,
                updateFields,
                deleteFields,
                listVersion}, this.UpdateListOperationCompleted, userState);
        }

        private void OnUpdateListOperationCompleted(object arg)
        {
            if ((this.UpdateListCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateListCompleted(this, new UpdateListCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/GetListCollection", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetListCollection()
        {
            object[] results = this.Invoke("GetListCollection", new object[0]);
            return ((System.Xml.XmlNode)(results[0]));
        }

        /// <remarks/>
        public void GetListCollectionAsync()
        {
            this.GetListCollectionAsync(null);
        }

        /// <remarks/>
        public void GetListCollectionAsync(object userState)
        {
            if ((this.GetListCollectionOperationCompleted == null))
            {
                this.GetListCollectionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetListCollectionOperationCompleted);
            }
            this.InvokeAsync("GetListCollection", new object[0], this.GetListCollectionOperationCompleted, userState);
        }

        private void OnGetListCollectionOperationCompleted(object arg)
        {
            if ((this.GetListCollectionCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetListCollectionCompleted(this, new GetListCollectionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/GetListItems", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetListItems(string listName, string viewName, System.Xml.XmlNode query, System.Xml.XmlNode viewFields, string rowLimit, System.Xml.XmlNode queryOptions, string webID)
        {
            object[] results = this.Invoke("GetListItems", new object[] {
                listName,
                viewName,
                query,
                viewFields,
                rowLimit,
                queryOptions,
                webID});
            return ((System.Xml.XmlNode)(results[0]));
        }

        /// <remarks/>
        public void GetListItemsAsync(string listName, string viewName, System.Xml.XmlNode query, System.Xml.XmlNode viewFields, string rowLimit, System.Xml.XmlNode queryOptions, string webID)
        {
            this.GetListItemsAsync(listName, viewName, query, viewFields, rowLimit, queryOptions, webID, null);
        }

        /// <remarks/>
        public void GetListItemsAsync(string listName, string viewName, System.Xml.XmlNode query, System.Xml.XmlNode viewFields, string rowLimit, System.Xml.XmlNode queryOptions, string webID, object userState)
        {
            if ((this.GetListItemsOperationCompleted == null))
            {
                this.GetListItemsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetListItemsOperationCompleted);
            }
            this.InvokeAsync("GetListItems", new object[] {
                listName,
                viewName,
                query,
                viewFields,
                rowLimit,
                queryOptions,
                webID}, this.GetListItemsOperationCompleted, userState);
        }

        private void OnGetListItemsOperationCompleted(object arg)
        {
            if ((this.GetListItemsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetListItemsCompleted(this, new GetListItemsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/GetListItemChanges", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetListItemChanges(string listName, System.Xml.XmlNode viewFields, string since, System.Xml.XmlNode contains)
        {
            object[] results = this.Invoke("GetListItemChanges", new object[] {
                listName,
                viewFields,
                since,
                contains});
            return ((System.Xml.XmlNode)(results[0]));
        }

        /// <remarks/>
        public void GetListItemChangesAsync(string listName, System.Xml.XmlNode viewFields, string since, System.Xml.XmlNode contains)
        {
            this.GetListItemChangesAsync(listName, viewFields, since, contains, null);
        }

        /// <remarks/>
        public void GetListItemChangesAsync(string listName, System.Xml.XmlNode viewFields, string since, System.Xml.XmlNode contains, object userState)
        {
            if ((this.GetListItemChangesOperationCompleted == null))
            {
                this.GetListItemChangesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetListItemChangesOperationCompleted);
            }
            this.InvokeAsync("GetListItemChanges", new object[] {
                listName,
                viewFields,
                since,
                contains}, this.GetListItemChangesOperationCompleted, userState);
        }

        private void OnGetListItemChangesOperationCompleted(object arg)
        {
            if ((this.GetListItemChangesCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetListItemChangesCompleted(this, new GetListItemChangesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/GetListItemChangesSinceToken", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetListItemChangesSinceToken(string listName, string viewName, System.Xml.XmlNode query, System.Xml.XmlNode viewFields, string rowLimit, System.Xml.XmlNode queryOptions, string changeToken, System.Xml.XmlNode contains)
        {
            object[] results = this.Invoke("GetListItemChangesSinceToken", new object[] {
                listName,
                viewName,
                query,
                viewFields,
                rowLimit,
                queryOptions,
                changeToken,
                contains});
            return ((System.Xml.XmlNode)(results[0]));
        }

        /// <remarks/>
        public void GetListItemChangesSinceTokenAsync(string listName, string viewName, System.Xml.XmlNode query, System.Xml.XmlNode viewFields, string rowLimit, System.Xml.XmlNode queryOptions, string changeToken, System.Xml.XmlNode contains)
        {
            this.GetListItemChangesSinceTokenAsync(listName, viewName, query, viewFields, rowLimit, queryOptions, changeToken, contains, null);
        }

        /// <remarks/>
        public void GetListItemChangesSinceTokenAsync(string listName, string viewName, System.Xml.XmlNode query, System.Xml.XmlNode viewFields, string rowLimit, System.Xml.XmlNode queryOptions, string changeToken, System.Xml.XmlNode contains, object userState)
        {
            if ((this.GetListItemChangesSinceTokenOperationCompleted == null))
            {
                this.GetListItemChangesSinceTokenOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetListItemChangesSinceTokenOperationCompleted);
            }
            this.InvokeAsync("GetListItemChangesSinceToken", new object[] {
                listName,
                viewName,
                query,
                viewFields,
                rowLimit,
                queryOptions,
                changeToken,
                contains}, this.GetListItemChangesSinceTokenOperationCompleted, userState);
        }

        private void OnGetListItemChangesSinceTokenOperationCompleted(object arg)
        {
            if ((this.GetListItemChangesSinceTokenCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetListItemChangesSinceTokenCompleted(this, new GetListItemChangesSinceTokenCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/UpdateListItems", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode UpdateListItems(string listName, System.Xml.XmlNode updates)
        {
            object[] results = this.Invoke("UpdateListItems", new object[] {
                listName,
                updates});
            return ((System.Xml.XmlNode)(results[0]));
        }

        /// <remarks/>
        public void UpdateListItemsAsync(string listName, System.Xml.XmlNode updates)
        {
            this.UpdateListItemsAsync(listName, updates, null);
        }

        /// <remarks/>
        public void UpdateListItemsAsync(string listName, System.Xml.XmlNode updates, object userState)
        {
            if ((this.UpdateListItemsOperationCompleted == null))
            {
                this.UpdateListItemsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateListItemsOperationCompleted);
            }
            this.InvokeAsync("UpdateListItems", new object[] {
                listName,
                updates}, this.UpdateListItemsOperationCompleted, userState);
        }

        private void OnUpdateListItemsOperationCompleted(object arg)
        {
            if ((this.UpdateListItemsCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateListItemsCompleted(this, new UpdateListItemsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/AddDiscussionBoardItem", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode AddDiscussionBoardItem(string listName, [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] message)
        {
            object[] results = this.Invoke("AddDiscussionBoardItem", new object[] {
                listName,
                message});
            return ((System.Xml.XmlNode)(results[0]));
        }

        /// <remarks/>
        public void AddDiscussionBoardItemAsync(string listName, byte[] message)
        {
            this.AddDiscussionBoardItemAsync(listName, message, null);
        }

        /// <remarks/>
        public void AddDiscussionBoardItemAsync(string listName, byte[] message, object userState)
        {
            if ((this.AddDiscussionBoardItemOperationCompleted == null))
            {
                this.AddDiscussionBoardItemOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddDiscussionBoardItemOperationCompleted);
            }
            this.InvokeAsync("AddDiscussionBoardItem", new object[] {
                listName,
                message}, this.AddDiscussionBoardItemOperationCompleted, userState);
        }

        private void OnAddDiscussionBoardItemOperationCompleted(object arg)
        {
            if ((this.AddDiscussionBoardItemCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddDiscussionBoardItemCompleted(this, new AddDiscussionBoardItemCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/GetVersionCollection", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetVersionCollection(string strlistID, string strlistItemID, string strFieldName)
        {
            object[] results = this.Invoke("GetVersionCollection", new object[] {
                strlistID,
                strlistItemID,
                strFieldName});
            return ((System.Xml.XmlNode)(results[0]));
        }

        /// <remarks/>
        public void GetVersionCollectionAsync(string strlistID, string strlistItemID, string strFieldName)
        {
            this.GetVersionCollectionAsync(strlistID, strlistItemID, strFieldName, null);
        }

        /// <remarks/>
        public void GetVersionCollectionAsync(string strlistID, string strlistItemID, string strFieldName, object userState)
        {
            if ((this.GetVersionCollectionOperationCompleted == null))
            {
                this.GetVersionCollectionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetVersionCollectionOperationCompleted);
            }
            this.InvokeAsync("GetVersionCollection", new object[] {
                strlistID,
                strlistItemID,
                strFieldName}, this.GetVersionCollectionOperationCompleted, userState);
        }

        private void OnGetVersionCollectionOperationCompleted(object arg)
        {
            if ((this.GetVersionCollectionCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetVersionCollectionCompleted(this, new GetVersionCollectionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/AddAttachment", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string AddAttachment(string listName, string listItemID, string fileName, [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")] byte[] attachment)
        {
            object[] results = this.Invoke("AddAttachment", new object[] {
                listName,
                listItemID,
                fileName,
                attachment});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void AddAttachmentAsync(string listName, string listItemID, string fileName, byte[] attachment)
        {
            this.AddAttachmentAsync(listName, listItemID, fileName, attachment, null);
        }

        /// <remarks/>
        public void AddAttachmentAsync(string listName, string listItemID, string fileName, byte[] attachment, object userState)
        {
            if ((this.AddAttachmentOperationCompleted == null))
            {
                this.AddAttachmentOperationCompleted = new System.Threading.SendOrPostCallback(this.OnAddAttachmentOperationCompleted);
            }
            this.InvokeAsync("AddAttachment", new object[] {
                listName,
                listItemID,
                fileName,
                attachment}, this.AddAttachmentOperationCompleted, userState);
        }

        private void OnAddAttachmentOperationCompleted(object arg)
        {
            if ((this.AddAttachmentCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.AddAttachmentCompleted(this, new AddAttachmentCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/GetAttachmentCollection", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetAttachmentCollection(string listName, string listItemID)
        {
            object[] results = this.Invoke("GetAttachmentCollection", new object[] {
                listName,
                listItemID});
            return ((System.Xml.XmlNode)(results[0]));
        }

        /// <remarks/>
        public void GetAttachmentCollectionAsync(string listName, string listItemID)
        {
            this.GetAttachmentCollectionAsync(listName, listItemID, null);
        }

        /// <remarks/>
        public void GetAttachmentCollectionAsync(string listName, string listItemID, object userState)
        {
            if ((this.GetAttachmentCollectionOperationCompleted == null))
            {
                this.GetAttachmentCollectionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetAttachmentCollectionOperationCompleted);
            }
            this.InvokeAsync("GetAttachmentCollection", new object[] {
                listName,
                listItemID}, this.GetAttachmentCollectionOperationCompleted, userState);
        }

        private void OnGetAttachmentCollectionOperationCompleted(object arg)
        {
            if ((this.GetAttachmentCollectionCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetAttachmentCollectionCompleted(this, new GetAttachmentCollectionCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/DeleteAttachment", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void DeleteAttachment(string listName, string listItemID, string url)
        {
            this.Invoke("DeleteAttachment", new object[] {
                listName,
                listItemID,
                url});
        }

        /// <remarks/>
        public void DeleteAttachmentAsync(string listName, string listItemID, string url)
        {
            this.DeleteAttachmentAsync(listName, listItemID, url, null);
        }

        /// <remarks/>
        public void DeleteAttachmentAsync(string listName, string listItemID, string url, object userState)
        {
            if ((this.DeleteAttachmentOperationCompleted == null))
            {
                this.DeleteAttachmentOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteAttachmentOperationCompleted);
            }
            this.InvokeAsync("DeleteAttachment", new object[] {
                listName,
                listItemID,
                url}, this.DeleteAttachmentOperationCompleted, userState);
        }

        private void OnDeleteAttachmentOperationCompleted(object arg)
        {
            if ((this.DeleteAttachmentCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteAttachmentCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/CheckOutFile", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool CheckOutFile(string pageUrl, string checkoutToLocal, string lastmodified)
        {
            object[] results = this.Invoke("CheckOutFile", new object[] {
                pageUrl,
                checkoutToLocal,
                lastmodified});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void CheckOutFileAsync(string pageUrl, string checkoutToLocal, string lastmodified)
        {
            this.CheckOutFileAsync(pageUrl, checkoutToLocal, lastmodified, null);
        }

        /// <remarks/>
        public void CheckOutFileAsync(string pageUrl, string checkoutToLocal, string lastmodified, object userState)
        {
            if ((this.CheckOutFileOperationCompleted == null))
            {
                this.CheckOutFileOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCheckOutFileOperationCompleted);
            }
            this.InvokeAsync("CheckOutFile", new object[] {
                pageUrl,
                checkoutToLocal,
                lastmodified}, this.CheckOutFileOperationCompleted, userState);
        }

        private void OnCheckOutFileOperationCompleted(object arg)
        {
            if ((this.CheckOutFileCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CheckOutFileCompleted(this, new CheckOutFileCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/UndoCheckOut", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool UndoCheckOut(string pageUrl)
        {
            object[] results = this.Invoke("UndoCheckOut", new object[] {
                pageUrl});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void UndoCheckOutAsync(string pageUrl)
        {
            this.UndoCheckOutAsync(pageUrl, null);
        }

        /// <remarks/>
        public void UndoCheckOutAsync(string pageUrl, object userState)
        {
            if ((this.UndoCheckOutOperationCompleted == null))
            {
                this.UndoCheckOutOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUndoCheckOutOperationCompleted);
            }
            this.InvokeAsync("UndoCheckOut", new object[] {
                pageUrl}, this.UndoCheckOutOperationCompleted, userState);
        }

        private void OnUndoCheckOutOperationCompleted(object arg)
        {
            if ((this.UndoCheckOutCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UndoCheckOutCompleted(this, new UndoCheckOutCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/CheckInFile", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool CheckInFile(string pageUrl, string comment, string CheckinType)
        {
            object[] results = this.Invoke("CheckInFile", new object[] {
                pageUrl,
                comment,
                CheckinType});
            return ((bool)(results[0]));
        }

        /// <remarks/>
        public void CheckInFileAsync(string pageUrl, string comment, string CheckinType)
        {
            this.CheckInFileAsync(pageUrl, comment, CheckinType, null);
        }

        /// <remarks/>
        public void CheckInFileAsync(string pageUrl, string comment, string CheckinType, object userState)
        {
            if ((this.CheckInFileOperationCompleted == null))
            {
                this.CheckInFileOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCheckInFileOperationCompleted);
            }
            this.InvokeAsync("CheckInFile", new object[] {
                pageUrl,
                comment,
                CheckinType}, this.CheckInFileOperationCompleted, userState);
        }

        private void OnCheckInFileOperationCompleted(object arg)
        {
            if ((this.CheckInFileCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CheckInFileCompleted(this, new CheckInFileCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/GetListContentTypes", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetListContentTypes(string listName, string contentTypeId)
        {
            object[] results = this.Invoke("GetListContentTypes", new object[] {
                listName,
                contentTypeId});
            return ((System.Xml.XmlNode)(results[0]));
        }

        /// <remarks/>
        public void GetListContentTypesAsync(string listName, string contentTypeId)
        {
            this.GetListContentTypesAsync(listName, contentTypeId, null);
        }

        /// <remarks/>
        public void GetListContentTypesAsync(string listName, string contentTypeId, object userState)
        {
            if ((this.GetListContentTypesOperationCompleted == null))
            {
                this.GetListContentTypesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetListContentTypesOperationCompleted);
            }
            this.InvokeAsync("GetListContentTypes", new object[] {
                listName,
                contentTypeId}, this.GetListContentTypesOperationCompleted, userState);
        }

        private void OnGetListContentTypesOperationCompleted(object arg)
        {
            if ((this.GetListContentTypesCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetListContentTypesCompleted(this, new GetListContentTypesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/GetListContentType", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetListContentType(string listName, string contentTypeId)
        {
            object[] results = this.Invoke("GetListContentType", new object[] {
                listName,
                contentTypeId});
            return ((System.Xml.XmlNode)(results[0]));
        }

        /// <remarks/>
        public void GetListContentTypeAsync(string listName, string contentTypeId)
        {
            this.GetListContentTypeAsync(listName, contentTypeId, null);
        }

        /// <remarks/>
        public void GetListContentTypeAsync(string listName, string contentTypeId, object userState)
        {
            if ((this.GetListContentTypeOperationCompleted == null))
            {
                this.GetListContentTypeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetListContentTypeOperationCompleted);
            }
            this.InvokeAsync("GetListContentType", new object[] {
                listName,
                contentTypeId}, this.GetListContentTypeOperationCompleted, userState);
        }

        private void OnGetListContentTypeOperationCompleted(object arg)
        {
            if ((this.GetListContentTypeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetListContentTypeCompleted(this, new GetListContentTypeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/CreateContentType", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string CreateContentType(string listName, string displayName, string parentType, System.Xml.XmlNode fields, System.Xml.XmlNode contentTypeProperties, string addToView)
        {
            object[] results = this.Invoke("CreateContentType", new object[] {
                listName,
                displayName,
                parentType,
                fields,
                contentTypeProperties,
                addToView});
            return ((string)(results[0]));
        }

        /// <remarks/>
        public void CreateContentTypeAsync(string listName, string displayName, string parentType, System.Xml.XmlNode fields, System.Xml.XmlNode contentTypeProperties, string addToView)
        {
            this.CreateContentTypeAsync(listName, displayName, parentType, fields, contentTypeProperties, addToView, null);
        }

        /// <remarks/>
        public void CreateContentTypeAsync(string listName, string displayName, string parentType, System.Xml.XmlNode fields, System.Xml.XmlNode contentTypeProperties, string addToView, object userState)
        {
            if ((this.CreateContentTypeOperationCompleted == null))
            {
                this.CreateContentTypeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCreateContentTypeOperationCompleted);
            }
            this.InvokeAsync("CreateContentType", new object[] {
                listName,
                displayName,
                parentType,
                fields,
                contentTypeProperties,
                addToView}, this.CreateContentTypeOperationCompleted, userState);
        }

        private void OnCreateContentTypeOperationCompleted(object arg)
        {
            if ((this.CreateContentTypeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CreateContentTypeCompleted(this, new CreateContentTypeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/UpdateContentType", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode UpdateContentType(string listName, string contentTypeId, System.Xml.XmlNode contentTypeProperties, System.Xml.XmlNode newFields, System.Xml.XmlNode updateFields, System.Xml.XmlNode deleteFields, string addToView)
        {
            object[] results = this.Invoke("UpdateContentType", new object[] {
                listName,
                contentTypeId,
                contentTypeProperties,
                newFields,
                updateFields,
                deleteFields,
                addToView});
            return ((System.Xml.XmlNode)(results[0]));
        }

        /// <remarks/>
        public void UpdateContentTypeAsync(string listName, string contentTypeId, System.Xml.XmlNode contentTypeProperties, System.Xml.XmlNode newFields, System.Xml.XmlNode updateFields, System.Xml.XmlNode deleteFields, string addToView)
        {
            this.UpdateContentTypeAsync(listName, contentTypeId, contentTypeProperties, newFields, updateFields, deleteFields, addToView, null);
        }

        /// <remarks/>
        public void UpdateContentTypeAsync(string listName, string contentTypeId, System.Xml.XmlNode contentTypeProperties, System.Xml.XmlNode newFields, System.Xml.XmlNode updateFields, System.Xml.XmlNode deleteFields, string addToView, object userState)
        {
            if ((this.UpdateContentTypeOperationCompleted == null))
            {
                this.UpdateContentTypeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateContentTypeOperationCompleted);
            }
            this.InvokeAsync("UpdateContentType", new object[] {
                listName,
                contentTypeId,
                contentTypeProperties,
                newFields,
                updateFields,
                deleteFields,
                addToView}, this.UpdateContentTypeOperationCompleted, userState);
        }

        private void OnUpdateContentTypeOperationCompleted(object arg)
        {
            if ((this.UpdateContentTypeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateContentTypeCompleted(this, new UpdateContentTypeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/DeleteContentType", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode DeleteContentType(string listName, string contentTypeId)
        {
            object[] results = this.Invoke("DeleteContentType", new object[] {
                listName,
                contentTypeId});
            return ((System.Xml.XmlNode)(results[0]));
        }

        /// <remarks/>
        public void DeleteContentTypeAsync(string listName, string contentTypeId)
        {
            this.DeleteContentTypeAsync(listName, contentTypeId, null);
        }

        /// <remarks/>
        public void DeleteContentTypeAsync(string listName, string contentTypeId, object userState)
        {
            if ((this.DeleteContentTypeOperationCompleted == null))
            {
                this.DeleteContentTypeOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteContentTypeOperationCompleted);
            }
            this.InvokeAsync("DeleteContentType", new object[] {
                listName,
                contentTypeId}, this.DeleteContentTypeOperationCompleted, userState);
        }

        private void OnDeleteContentTypeOperationCompleted(object arg)
        {
            if ((this.DeleteContentTypeCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteContentTypeCompleted(this, new DeleteContentTypeCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/UpdateContentTypeXmlDocument", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode UpdateContentTypeXmlDocument(string listName, string contentTypeId, System.Xml.XmlNode newDocument)
        {
            object[] results = this.Invoke("UpdateContentTypeXmlDocument", new object[] {
                listName,
                contentTypeId,
                newDocument});
            return ((System.Xml.XmlNode)(results[0]));
        }

        /// <remarks/>
        public void UpdateContentTypeXmlDocumentAsync(string listName, string contentTypeId, System.Xml.XmlNode newDocument)
        {
            this.UpdateContentTypeXmlDocumentAsync(listName, contentTypeId, newDocument, null);
        }

        /// <remarks/>
        public void UpdateContentTypeXmlDocumentAsync(string listName, string contentTypeId, System.Xml.XmlNode newDocument, object userState)
        {
            if ((this.UpdateContentTypeXmlDocumentOperationCompleted == null))
            {
                this.UpdateContentTypeXmlDocumentOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateContentTypeXmlDocumentOperationCompleted);
            }
            this.InvokeAsync("UpdateContentTypeXmlDocument", new object[] {
                listName,
                contentTypeId,
                newDocument}, this.UpdateContentTypeXmlDocumentOperationCompleted, userState);
        }

        private void OnUpdateContentTypeXmlDocumentOperationCompleted(object arg)
        {
            if ((this.UpdateContentTypeXmlDocumentCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateContentTypeXmlDocumentCompleted(this, new UpdateContentTypeXmlDocumentCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/UpdateContentTypesXmlDocument", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode UpdateContentTypesXmlDocument(string listName, System.Xml.XmlNode newDocument)
        {
            object[] results = this.Invoke("UpdateContentTypesXmlDocument", new object[] {
                listName,
                newDocument});
            return ((System.Xml.XmlNode)(results[0]));
        }

        /// <remarks/>
        public void UpdateContentTypesXmlDocumentAsync(string listName, System.Xml.XmlNode newDocument)
        {
            this.UpdateContentTypesXmlDocumentAsync(listName, newDocument, null);
        }

        /// <remarks/>
        public void UpdateContentTypesXmlDocumentAsync(string listName, System.Xml.XmlNode newDocument, object userState)
        {
            if ((this.UpdateContentTypesXmlDocumentOperationCompleted == null))
            {
                this.UpdateContentTypesXmlDocumentOperationCompleted = new System.Threading.SendOrPostCallback(this.OnUpdateContentTypesXmlDocumentOperationCompleted);
            }
            this.InvokeAsync("UpdateContentTypesXmlDocument", new object[] {
                listName,
                newDocument}, this.UpdateContentTypesXmlDocumentOperationCompleted, userState);
        }

        private void OnUpdateContentTypesXmlDocumentOperationCompleted(object arg)
        {
            if ((this.UpdateContentTypesXmlDocumentCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.UpdateContentTypesXmlDocumentCompleted(this, new UpdateContentTypesXmlDocumentCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/DeleteContentTypeXmlDocument", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode DeleteContentTypeXmlDocument(string listName, string contentTypeId, string documentUri)
        {
            object[] results = this.Invoke("DeleteContentTypeXmlDocument", new object[] {
                listName,
                contentTypeId,
                documentUri});
            return ((System.Xml.XmlNode)(results[0]));
        }

        /// <remarks/>
        public void DeleteContentTypeXmlDocumentAsync(string listName, string contentTypeId, string documentUri)
        {
            this.DeleteContentTypeXmlDocumentAsync(listName, contentTypeId, documentUri, null);
        }

        /// <remarks/>
        public void DeleteContentTypeXmlDocumentAsync(string listName, string contentTypeId, string documentUri, object userState)
        {
            if ((this.DeleteContentTypeXmlDocumentOperationCompleted == null))
            {
                this.DeleteContentTypeXmlDocumentOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteContentTypeXmlDocumentOperationCompleted);
            }
            this.InvokeAsync("DeleteContentTypeXmlDocument", new object[] {
                listName,
                contentTypeId,
                documentUri}, this.DeleteContentTypeXmlDocumentOperationCompleted, userState);
        }

        private void OnDeleteContentTypeXmlDocumentOperationCompleted(object arg)
        {
            if ((this.DeleteContentTypeXmlDocumentCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteContentTypeXmlDocumentCompleted(this, new DeleteContentTypeXmlDocumentCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://schemas.microsoft.com/sharepoint/soap/ApplyContentTypeToList", RequestNamespace = "http://schemas.microsoft.com/sharepoint/soap/", ResponseNamespace = "http://schemas.microsoft.com/sharepoint/soap/", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode ApplyContentTypeToList(string webUrl, string contentTypeId, string listName)
        {
            object[] results = this.Invoke("ApplyContentTypeToList", new object[] {
                webUrl,
                contentTypeId,
                listName});
            return ((System.Xml.XmlNode)(results[0]));
        }

        /// <remarks/>
        public void ApplyContentTypeToListAsync(string webUrl, string contentTypeId, string listName)
        {
            this.ApplyContentTypeToListAsync(webUrl, contentTypeId, listName, null);
        }

        /// <remarks/>
        public void ApplyContentTypeToListAsync(string webUrl, string contentTypeId, string listName, object userState)
        {
            if ((this.ApplyContentTypeToListOperationCompleted == null))
            {
                this.ApplyContentTypeToListOperationCompleted = new System.Threading.SendOrPostCallback(this.OnApplyContentTypeToListOperationCompleted);
            }
            this.InvokeAsync("ApplyContentTypeToList", new object[] {
                webUrl,
                contentTypeId,
                listName}, this.ApplyContentTypeToListOperationCompleted, userState);
        }

        private void OnApplyContentTypeToListOperationCompleted(object arg)
        {
            if ((this.ApplyContentTypeToListCompleted != null))
            {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ApplyContentTypeToListCompleted(this, new ApplyContentTypeToListCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
}