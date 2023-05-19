using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;

namespace Robin_Wcf_CustomMessageEncoder_SvcLib
{
    // 注意: 如果更改此处的类名“IService1”，也必须更新 App.config 中对“IService1”的引用。
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}{0}{0}{0}{0}{0}{0}{0}{0}{0}{0}{0}{0}{0}{0}{0}{0}{0}{0}{0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            for (int i = 0; i < 100; i++)
            {
                composite.StringValue += "Suffix,哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈啊和哈哈哈哈哈哈哈哈";
            }
            if (composite.BoolValue)
            {
               
            }
            return composite;
        }

        public Stream GetStream(Stream stream)
        { 
            return stream;
        }
    }
}
