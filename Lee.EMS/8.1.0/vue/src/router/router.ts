declare global {
    interface RouterMeta {
        title: string;
    }
    interface Router {
        path: string;
        name: string;
        icon?: string;
        permission?: string;
        meta?: RouterMeta;
        component: any;
        children?: Array<Router>;
    }
    interface System {
        import(request: string): Promise<any>
    }
    var System: System
}
import login from '../views/login.vue'
import home from '../views/home/home.vue'
import main from '../views/main.vue'

export const locking = {
    path: '/locking',
    name: 'locking',
    component: () => import('../components/lockscreen/components/locking-page.vue')
};
export const loginRouter: Router = {
    path: '/',
    name: 'login',
    meta: {
        title: 'LogIn'
    },
    component: () => import('../views/login.vue')
};
export const otherRouters: Router = {
    path: '/main',
    name: 'main',
    permission: '',
    meta: { title: 'ManageMenu' },
    component: main,
    children: [
        { path: 'home', meta: { title: 'HomePage' }, name: 'home', component: () => import('../views/home/home.vue') }
    ]
}
export const appRouters: Array<Router> = [{
    path: '/setting',
    name: 'setting',
    permission: '',
    meta: { title: 'ManageMenu' },
    icon: '&#xe68a;',
    component: main,
    children: [
        { path: 'user', permission: 'Pages.Users', meta: { title: 'Users' }, name: 'user', component: () => import('../views/setting/user/user.vue') },
        { path: 'role', permission: 'Pages.Roles', meta: { title: 'Roles' }, name: 'role', component: () => import('../views/setting/role/role.vue') },
        { path: 'tenant', permission: 'Pages.Tenants', meta: { title: 'Tenants' }, name: 'tenant', component: () => import('../views/setting/tenant/tenant.vue') },
    ]
},
{
    path: '/bas',
    name: 'bas',
    permission: '',
    meta: { title: '基础资料' },
    icon: '&#xe68a;',
    component: main,
    children: [
        { path: 'dictionary', meta: { title: '公共字典' }, name: 'dictionary', component: () => import('../views/bas/dictionary.vue') }
    ]
},
{
    path: '/sys',
    name: 'sys',
    permission: '',
    meta: { title: '系统权限' },
    icon: '&#xe68a;',
    component: main,
    children: [
        { path: 'sysmodule', meta: { title: '模块管理' }, name: 'sysmodule', component: () => import('../views/sys/sysmodule.vue') },
    ]
}
]

export const routers = [
    loginRouter,
    locking,
    ...appRouters, 
    otherRouters,
    
];
