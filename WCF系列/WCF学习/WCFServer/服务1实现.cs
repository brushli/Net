using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCFServer.参数;

namespace WCFServer
{
    public class 服务1实现 : I服务1
    {
        public string 服务1方法1(对象一 one)
        {
            return $"我的名字是：{one.Name},性别:{one.Sex},地址{one.Address}";
        }

        public string 服务1方法2()
        {
            return "我是服务1方法2";
        }
    }
}
