using ConsoleClient.客户端五福;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace ConsoleClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            I服务1Client a = new I服务1Client();
            //var ss=a.服务1方法1();
            //a.服务1方法1Async();
            //*********管道1：使用ChannelFactory
            //{
            //    ChannelFactory<I服务1> factory = new ChannelFactory<I服务1>();
            //    var proxy1 = factory.CreateChannel(new EndpointAddress("http://localhost:8000/%E6%9C%8D%E5%8A%A11"));
            //    using (proxy1 as IDisposable)
            //    {
            //        var ss = proxy1.服务1方法1();
            //    }
            //}
            //*********管道2：使用ChannelFactory静态的方法CreateChannel
            //Binding binding = new WSHttpBinding();
            //EndpointAddress endpointAddress = new EndpointAddress("http://localhost:8000/%E6%9C%8D%E5%8A%A11");
            //I服务1 proService1 = ChannelFactory<I服务1>.CreateChannel(binding, endpointAddress);
            //using (proService1 as IDisposable)
            //{
            //    var ss = proService1.服务1方法1();
            //}
            //*********管道3：使用InProcFactory类
            //{
            //    InProcFactory
            //}
            //*********数据契约：缺失和必填
            {
                try
                {
                    var result = a.服务1方法1(new 对象一 { Name = "李成", Sex = "男" });
                    Console.WriteLine(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("遇到错误:" + ex.Message);
                }
            }
            Console.ReadKey();
        }
    }
}
