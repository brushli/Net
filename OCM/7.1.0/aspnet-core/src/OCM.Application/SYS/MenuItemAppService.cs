using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.IdentityFramework;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.UI;
using OCM.Authorization;
using OCM.Authorization.Accounts;
using OCM.Authorization.Roles;
using OCM.Roles.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OCM.SYS;
using OCM.SYS.Dto;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.Authorization.Roles;

namespace OCM.MenuItems
{
    /// <summary>
    /// 
    /// </summary>
    public class MenuItemAppService : AsyncCrudAppService<MenuItem, MenuItemDto, int, PagedMenuItemResultRequestDto, CreateMenuItemDto, MenuItemDto>, IMenuItemAppService
    {
        private readonly IRepository<MenuItemInRole, int> _menuItemInroleRepository;
        private readonly PermissionManager _permissionManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<RolePermissionSetting, long> _rolePermissionSettingRepository;
        /// <summary>
        /// 菜单服务
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="menuItemInroleRepository"></param>
        /// <param name="permissionManager"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="rolePermissionSettingRepository"></param>
        public MenuItemAppService(
            IRepository<MenuItem, int> repository,
            IRepository<MenuItemInRole,int> menuItemInroleRepository,
            PermissionManager permissionManager,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<RolePermissionSetting, long> rolePermissionSettingRepository)
            : base(repository)
        {
            _menuItemInroleRepository = menuItemInroleRepository;
            //_permissionContext = new PermissionDependencyContext(iocResolver);
            _permissionManager = permissionManager;
            _unitOfWorkManager = unitOfWorkManager;
            _rolePermissionSettingRepository = rolePermissionSettingRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public override async Task<MenuItemDto> CreateAsync(CreateMenuItemDto input)
        {
            try
            {
                //var ss = new PermissionManager();
                //ss.CreatePermission
                var result= await base.CreateAsync(input);
                _permissionManager.CreatePermission(result.Id.ToString(),
                    displayName: new LocalizableString(input.Lable, OCMConsts.LocalizationSourceName),
                    description: new LocalizableString(input.Description, OCMConsts.LocalizationSourceName),
                    properties: new Dictionary<string, object>() {
                    {"tag", result}});
                return result;
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("保存出现异常",ex.Message);
            }
        }
        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public override async Task<MenuItemDto> UpdateAsync(MenuItemDto input)
        {
            try
            {
                var result= await base.UpdateAsync(input);
                _permissionManager.RemovePermission(input.Id.ToString());
                var ps=_permissionManager.CreatePermission(result.Id.ToString(),
                    displayName: new LocalizableString(input.Lable, OCMConsts.LocalizationSourceName),
                    description: new LocalizableString(input.Description, OCMConsts.LocalizationSourceName),
                    properties: new Dictionary<string, object>() {
                    {"tag", result}});
                return result;
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("保存出现异常", ex.Message);
            }
        }
        /// <summary>
        /// 初始化权限数据
        /// </summary>
        [UnitOfWork]
        public void InitPermissions()
        {
            using (var unitOfWork = _unitOfWorkManager.Begin())
            {
                var menuItems = Repository.GetAllList();
                foreach (var item in menuItems)
                {
                    _permissionManager.RemovePermission(item.Id.ToString());
                    _permissionManager.CreatePermission(item.Id.ToString(),
                        displayName: new LocalizableString(item.Lable, OCMConsts.LocalizationSourceName),
                        description: new LocalizableString(item.Description, OCMConsts.LocalizationSourceName),
                        properties: new Dictionary<string, object>() {
                    {"tag", item}});
                }
            }
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public override Task DeleteAsync(EntityDto<int> input)
        {
            try
            {
                _rolePermissionSettingRepository.Delete(w => w.Name == input.Id.ToString());
                //var grans=_rolePermissionSettingRepository.GetAll().Where(w => w.Name == input.Id.ToString()&&w.IsGranted);
                //foreach (var item in grans)
                //{
                //    item.IsGranted = false;
                //}
                //_rolePermissionSettingRepository.DeleteAsync(
                //    permissionSetting => permissionSetting.Name == input.Id.ToString()&&
                //                         permissionSetting.IsGranted == permissionGrant.IsGranted
                //);
                _permissionManager.RemovePermission(input.Id.ToString());
                return base.DeleteAsync(input); ;
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("删除出现错误:", ex.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override IQueryable<MenuItem> CreateFilteredQuery(PagedMenuItemResultRequestDto input)
        {
            var result= base.CreateFilteredQuery(input);
            return result;
        }
    }
}

