using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace WCFServer
{
    [ServiceContract(Namespace ="空间2")]
    public interface I服务2
    {
        [OperationContract]
        string 服务2方法1();
        string 服务2方法2();
    }
}
