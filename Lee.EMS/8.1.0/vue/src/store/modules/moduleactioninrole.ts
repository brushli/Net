
import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import ModuleActionInRole from '../entities/moduleactioninrole'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import ListMutations from './list-mutations'

interface ModuleActionInRoleState extends ListState<ModuleActionInRole>{
    editModuleActionInRole:ModuleActionInRole
    isEdit:boolean
}
class ModuleActionInRoleMutations extends ListMutations<ModuleActionInRole>{

}
class ModuleActionInRoleModule extends ListModule<ModuleActionInRoleState,any,ModuleActionInRole>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<ModuleActionInRole>(),
        loading:false,
        isEdit:false,
        editModuleActionInRole:new ModuleActionInRole()
    }
    actions={
        async getAll(context:ActionContext<ModuleActionInRoleState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/ModuleActionInRole/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<ModuleActionInRole>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async create(context:ActionContext<ModuleActionInRoleState,any>,payload:any){
            await Ajax.post('/api/services/app/ModuleActionInRole/Create',payload.data);
        },
        async update(context:ActionContext<ModuleActionInRoleState,any>,payload:any){
            await Ajax.put('/api/services/app/ModuleActionInRole/Update',payload.data);
        },
        async delete(context:ActionContext<ModuleActionInRoleState,any>,payload:any){
            await Ajax.delete('/api/services/app/ModuleActionInRole/Delete?Id='+payload.data.id);
        },        
        async get(context:ActionContext<ModuleActionInRoleState,any>,payload:any){
            let reponse=await Ajax.get('/api/services/app/ModuleActionInRole/Get?Id='+payload.id);         
            return reponse.data.result as ModuleActionInRole;
        }
    };
    mutations={
        setCurrentPage(state:ModuleActionInRoleState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:ModuleActionInRoleState,pagesize:number){
            state.pageSize=pagesize;
        },
        edit(state:ModuleActionInRoleState,moduleActionInRole:ModuleActionInRole){
            state.isEdit=true;
            state.editModuleActionInRole=moduleActionInRole;
        }
    }
}
const moduleActionInRoleModule=new ModuleActionInRoleModule();
export default moduleActionInRoleModule;