using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Robin_Wcf_CustomMessageEncoder_SvcLib;
using System.ServiceModel.Channels;
using RobinLib;

namespace Robin_Wcf_CustomMessageEncoder_Host
{
    class Program
    {
        static void Main(string[] args)
        {
            //服务地址
            Uri baseAddress = new Uri("http://127.0.0.1:8081/Robin_Wcf_Formatter");
            ServiceHost host = new ServiceHost(typeof(Service1), new Uri[] { baseAddress });
            //服务绑定
            ICollection<BindingElement> bindingElements = new List<BindingElement>();
            HttpTransportBindingElement httpBindingElement = new HttpTransportBindingElement();
            string key = "JggkieIw7JM=";
            string iv = "XdTkT85fZ0U=";
            CryptEncodingBindingElement textBindingElement = new CryptEncodingBindingElement(new BinaryMessageEncodingBindingElement(), key, iv);
            bindingElements.Add(textBindingElement);
            bindingElements.Add(httpBindingElement);
            CustomBinding bind = new CustomBinding(bindingElements);  
            host.AddServiceEndpoint(typeof(IService1), bind, "");
            if (host.Description.Behaviors.Find<System.ServiceModel.Description.ServiceMetadataBehavior>() == null)
            {
                System.ServiceModel.Description.ServiceMetadataBehavior svcMetaBehavior = new System.ServiceModel.Description.ServiceMetadataBehavior();
                svcMetaBehavior.HttpGetEnabled = true;
                svcMetaBehavior.HttpGetUrl = new Uri("http://127.0.0.1:8001/Mex");
                host.Description.Behaviors.Add(svcMetaBehavior);
            }
            host.Opened += new EventHandler(delegate(object obj, EventArgs e)
            {
                Console.WriteLine("服务已经启动！");
            }); 
            host.Open();
            Console.Read();
        }
    }
}
