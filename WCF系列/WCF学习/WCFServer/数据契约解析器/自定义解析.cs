using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace WCFServer.数据契约解析器
{
    public class 自定义解析 : DataContractResolver
    {
        string Namespace
        {
            get
            {
                return typeof(自定义解析).Namespace ?? "global";
            }
        }
        string Name
        {
            get
            {
                return typeof(自定义解析).Name;
            }
        }
        public override Type ResolveName(string typeName, string typeNamespace, Type declaredType, DataContractResolver knownTypeResolver)
        {
            if (typeName == Name && typeNamespace == Namespace)
            {
                return typeof(自定义解析);
            }
            else
            {
                return knownTypeResolver.ResolveName(typeName, typeNamespace, declaredType, knownTypeResolver);
            }
        }

        public override bool TryResolveType(Type type, Type declaredType, DataContractResolver knownTypeResolver, out XmlDictionaryString typeName, out XmlDictionaryString typeNamespace)
        {
            if (type == typeof(自定义解析))
            {
                XmlDictionary dictionary = new XmlDictionary();
                typeName = dictionary.Add(Name);
                typeNamespace = dictionary.Add(Namespace);
                return true;
            }
            else
            {
                return knownTypeResolver.TryResolveType(type, declaredType, knownTypeResolver, out typeName, out typeNamespace);    
            }
        }
    }
}
