﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Updater.StockCoreServices {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="StockCoreServices.StockCoreServicesSoap")]
    public interface StockCoreServicesSoap {
        
        // CODEGEN: Generating message contract since element name accountNo from namespace http://tempuri.org/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ReceiveOrder", ReplyAction="*")]
        Updater.StockCoreServices.ReceiveOrderResponse ReceiveOrder(Updater.StockCoreServices.ReceiveOrderRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ReceiveOrderRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ReceiveOrder", Namespace="http://tempuri.org/", Order=0)]
        public Updater.StockCoreServices.ReceiveOrderRequestBody Body;
        
        public ReceiveOrderRequest() {
        }
        
        public ReceiveOrderRequest(Updater.StockCoreServices.ReceiveOrderRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ReceiveOrderRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public long clientOrderID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string accountNo;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string stockSymbol;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public long price;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public short volume;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public char side;
        
        public ReceiveOrderRequestBody() {
        }
        
        public ReceiveOrderRequestBody(long clientOrderID, string accountNo, string stockSymbol, long price, short volume, char side) {
            this.clientOrderID = clientOrderID;
            this.accountNo = accountNo;
            this.stockSymbol = stockSymbol;
            this.price = price;
            this.volume = volume;
            this.side = side;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ReceiveOrderResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ReceiveOrderResponse", Namespace="http://tempuri.org/", Order=0)]
        public Updater.StockCoreServices.ReceiveOrderResponseBody Body;
        
        public ReceiveOrderResponse() {
        }
        
        public ReceiveOrderResponse(Updater.StockCoreServices.ReceiveOrderResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class ReceiveOrderResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public short ReceiveOrderResult;
        
        public ReceiveOrderResponseBody() {
        }
        
        public ReceiveOrderResponseBody(short ReceiveOrderResult) {
            this.ReceiveOrderResult = ReceiveOrderResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface StockCoreServicesSoapChannel : Updater.StockCoreServices.StockCoreServicesSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class StockCoreServicesSoapClient : System.ServiceModel.ClientBase<Updater.StockCoreServices.StockCoreServicesSoap>, Updater.StockCoreServices.StockCoreServicesSoap {
        
        public StockCoreServicesSoapClient() {
        }
        
        public StockCoreServicesSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public StockCoreServicesSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StockCoreServicesSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StockCoreServicesSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Updater.StockCoreServices.ReceiveOrderResponse Updater.StockCoreServices.StockCoreServicesSoap.ReceiveOrder(Updater.StockCoreServices.ReceiveOrderRequest request) {
            return base.Channel.ReceiveOrder(request);
        }
        
        public short ReceiveOrder(long clientOrderID, string accountNo, string stockSymbol, long price, short volume, char side) {
            Updater.StockCoreServices.ReceiveOrderRequest inValue = new Updater.StockCoreServices.ReceiveOrderRequest();
            inValue.Body = new Updater.StockCoreServices.ReceiveOrderRequestBody();
            inValue.Body.clientOrderID = clientOrderID;
            inValue.Body.accountNo = accountNo;
            inValue.Body.stockSymbol = stockSymbol;
            inValue.Body.price = price;
            inValue.Body.volume = volume;
            inValue.Body.side = side;
            Updater.StockCoreServices.ReceiveOrderResponse retVal = ((Updater.StockCoreServices.StockCoreServicesSoap)(this)).ReceiveOrder(inValue);
            return retVal.Body.ReceiveOrderResult;
        }
    }
}
