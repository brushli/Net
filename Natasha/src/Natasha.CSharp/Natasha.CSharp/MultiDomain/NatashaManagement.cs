﻿# if MULTI
using System;


public static partial class NatashaManagement
{
   
    /// <summary>
    /// 获取系统域
    /// </summary>
    /// <returns></returns>
    public static NatashaReferenceDomain GetDefaultDomain()
    {
        return NatashaReferenceDomain.DefaultDomain;
    }
    /// <summary>
    /// 新建一个域
    /// </summary>
    /// <param name="domainName"></param>
    /// <returns></returns>
    public static NatashaReferenceDomain CreateDomain(string domainName)
    {
        return DomainManagement.Create(domainName);
    }
    /// <summary>
    /// 新建一个随机域
    /// </summary>
    /// <returns></returns>
    public static NatashaReferenceDomain CreateRandomDomain()
    {
        return DomainManagement.Random();
    }

    /// <summary>
    /// 增加元数据引用,编译需要元数据支持.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="loadBehavior">加载行为,如果有相同类型的引用, 那么此枚举会比较新旧程序集版本</param>
    /// <returns></returns>
    public static bool AddGlobalReference(Type type, LoadBehaviorEnum loadBehavior = LoadBehaviorEnum.None)
    {
        if (type.Assembly.IsDynamic || type.Assembly.Location == null)
        {
            return false;
        }
        NatashaReferenceDomain.DefaultDomain.References.AddReference(type.Assembly, loadBehavior);
        return true;
    }

    /// <summary>
    /// 移除元数据引用,编译需要元数据支持.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="loadBehavior">加载行为,如果有相同类型的引用, 那么此枚举会比较新旧程序集版本</param>
    /// <returns></returns>
    public static bool RemoveGlobalReference(Type type, LoadBehaviorEnum loadBehavior = LoadBehaviorEnum.None)
    {
        if (type.Assembly.IsDynamic || type.Assembly.GetName() == null)
        {
            return false;
        }
        NatashaReferenceDomain.DefaultDomain.References.RemoveReference(type.Assembly.GetName());
        return true;
    }
}

#endif