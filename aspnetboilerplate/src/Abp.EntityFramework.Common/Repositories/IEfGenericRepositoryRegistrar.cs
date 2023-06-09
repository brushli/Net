﻿using System;
using Abp.Dependency;
using Abp.Domain.Repositories;

namespace Abp.EntityFramework.Repositories
{
    public interface IEfGenericRepositoryRegistrar
    {
        void RegisterForDbContext(
            Type dbContextType,
            IIocManager iocManager,
            AutoRepositoryTypesAttribute defaultAutoRepositoryTypesAttribute
        );

        void RegisterForEntity(
            Type dbContextType,
            Type entityType,
            IIocManager iocManager,
            AutoRepositoryTypesAttribute defaultAutoRepositoryTypesAttribute
        );
    }
}
