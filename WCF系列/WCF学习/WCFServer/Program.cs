using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Text;
using WCFServer.数据契约解析器;
using WCFServer.消息加密;

namespace WCFServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(服务1实现));
            ServiceMetadataBehavior 行为=host.Description.Behaviors.Find<ServiceMetadataBehavior>();
            //if (行为==null)
            //{
            //    //Debug.Assert(BaseAddresses)
            //    行为 = new ServiceMetadataBehavior();
            //    行为.HttpGetEnabled = true;
            //    host.Description.Behaviors.Add(行为);
            //}
            ////安装解析器
            //{
            //    foreach (ServiceEndpoint item in host.Description.Endpoints)
            //    {
            //        foreach (OperationDescription operation in item.Contract.Operations)
            //        {
            //            DataContractSerializerOperationBehavior behavior = operation.Behaviors.Find<DataContractSerializerOperationBehavior>();
            //            behavior.DataContractResolver = new 自定义解析();
            //        }
            //    }
            //}
            //服务绑定
            ICollection<BindingElement> bindingElements = new List<BindingElement>();
            HttpTransportBindingElement httpBindingElement = new HttpTransportBindingElement();
            string key = "JggkieIw7JM=";
            string iv = "XdTkT85fZ0U=";
            CryptEncodingBindingElement textBindingElement = new CryptEncodingBindingElement(new BinaryMessageEncodingBindingElement(), key, iv);
            bindingElements.Add(textBindingElement);
            bindingElements.Add(httpBindingElement);
            CustomBinding bind = new CustomBinding(bindingElements);
            host.AddServiceEndpoint(typeof(I服务1), bind, "");
            if (host.Description.Behaviors.Find<System.ServiceModel.Description.ServiceMetadataBehavior>() == null)
            {
                System.ServiceModel.Description.ServiceMetadataBehavior svcMetaBehavior = new System.ServiceModel.Description.ServiceMetadataBehavior();
                svcMetaBehavior.HttpGetEnabled = true;
                svcMetaBehavior.HttpGetUrl = new Uri("http://127.0.0.1:8000/Mex");
                host.Description.Behaviors.Add(svcMetaBehavior);
            }
            host.Open();
            Console.WriteLine("服务已经启动完成");
            Console.ReadKey();
        }
    }
}
