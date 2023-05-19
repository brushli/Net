using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace WCFServer.参数
{
    [DataContract]
    public class 对象一
    {
        [DataMember]
        public string Name { set; get; }
        [DataMember]
        public string Sex { set; get; }
        [DataMember]
        public string Address { set; get; }
    }
}
