using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RobinLib;
using System.ServiceModel.Channels;
using Robin_Wcf_CustomMessageEncoder_ClientApp.ServiceReference1;

namespace Robin_Wcf_CustomMessageEncoder_ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(5300);
            ICollection<BindingElement> bindingElements = new List<BindingElement>();
            HttpTransportBindingElement httpBindingElement = new HttpTransportBindingElement();
            string key = "JggkieIw7JM=";
            string iv = "XdTkT85fZ0U=";
            CryptEncodingBindingElement textBindingElement = new CryptEncodingBindingElement(new BinaryMessageEncodingBindingElement(), key, iv);
            bindingElements.Add(textBindingElement);
            bindingElements.Add(httpBindingElement); 
            CustomBinding bind = new CustomBinding(bindingElements);  
            ServiceReference1.IService1 svc = new ServiceReference1.Service1Client(bind, new System.ServiceModel.EndpointAddress("http://127.0.0.1:8081/Robin_Wcf_Formatter"));
            string pres = svc.GetData(10);
            Console.WriteLine(pres);
            CompositeType ct = svc.GetDataUsingDataContract(new CompositeType());
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            for (int i = 0; i < 1000000; i++)
            {
                byte[] buffer = BitConverter.GetBytes(i);
                ms.Write(buffer, 0, buffer.Length);
            }
            System.IO.Stream stream = svc.GetStream(ms);
            Console.Read();
        }
    }
}
