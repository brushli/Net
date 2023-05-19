using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using WCFServer.参数;

namespace WCFServer
{
    [ServiceContract(Namespace ="空间1")]
    public interface I服务1
    {
        [OperationContract]
        string 服务1方法1(对象一 one);
        [OperationContract]
        string 服务1方法2();
    }
}
