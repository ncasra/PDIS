﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace PDIS.DataAccess.NAVService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SalesInvoiceManagement_Binding", Namespace="urn:microsoft-dynamics-schemas/codeunit/SalesInvoiceManagement")]
    public partial class SalesInvoiceManagement : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback SalesHeaderOperationCompleted;
        
        private System.Threading.SendOrPostCallback SalesLineOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public SalesInvoiceManagement() {
            this.Url = global::PDIS.DataAccess.Properties.Settings.Default.PDIS_DataAccess_NAVService_SalesInvoiceManagement;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event SalesHeaderCompletedEventHandler SalesHeaderCompleted;
        
        /// <remarks/>
        public event SalesLineCompletedEventHandler SalesLineCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/codeunit/SalesInvoiceManagement:SalesHeader", RequestNamespace="urn:microsoft-dynamics-schemas/codeunit/SalesInvoiceManagement", ResponseElementName="SalesHeader_Result", ResponseNamespace="urn:microsoft-dynamics-schemas/codeunit/SalesInvoiceManagement", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void SalesHeader(string docNo, string custNo, [System.Xml.Serialization.XmlElementAttribute(DataType="date")] System.DateTime postDate, string type) {
            this.Invoke("SalesHeader", new object[] {
                        docNo,
                        custNo,
                        postDate,
                        type});
        }
        
        /// <remarks/>
        public void SalesHeaderAsync(string docNo, string custNo, System.DateTime postDate, string type) {
            this.SalesHeaderAsync(docNo, custNo, postDate, type, null);
        }
        
        /// <remarks/>
        public void SalesHeaderAsync(string docNo, string custNo, System.DateTime postDate, string type, object userState) {
            if ((this.SalesHeaderOperationCompleted == null)) {
                this.SalesHeaderOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSalesHeaderOperationCompleted);
            }
            this.InvokeAsync("SalesHeader", new object[] {
                        docNo,
                        custNo,
                        postDate,
                        type}, this.SalesHeaderOperationCompleted, userState);
        }
        
        private void OnSalesHeaderOperationCompleted(object arg) {
            if ((this.SalesHeaderCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SalesHeaderCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:microsoft-dynamics-schemas/codeunit/SalesInvoiceManagement:SalesLine", RequestNamespace="urn:microsoft-dynamics-schemas/codeunit/SalesInvoiceManagement", ResponseElementName="SalesLine_Result", ResponseNamespace="urn:microsoft-dynamics-schemas/codeunit/SalesInvoiceManagement", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void SalesLine(string no, decimal qty, decimal unitPrice, string docNo) {
            this.Invoke("SalesLine", new object[] {
                        no,
                        qty,
                        unitPrice,
                        docNo});
        }
        
        /// <remarks/>
        public void SalesLineAsync(string no, decimal qty, decimal unitPrice, string docNo) {
            this.SalesLineAsync(no, qty, unitPrice, docNo, null);
        }
        
        /// <remarks/>
        public void SalesLineAsync(string no, decimal qty, decimal unitPrice, string docNo, object userState) {
            if ((this.SalesLineOperationCompleted == null)) {
                this.SalesLineOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSalesLineOperationCompleted);
            }
            this.InvokeAsync("SalesLine", new object[] {
                        no,
                        qty,
                        unitPrice,
                        docNo}, this.SalesLineOperationCompleted, userState);
        }
        
        private void OnSalesLineOperationCompleted(object arg) {
            if ((this.SalesLineCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SalesLineCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void SalesHeaderCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void SalesLineCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
}

#pragma warning restore 1591