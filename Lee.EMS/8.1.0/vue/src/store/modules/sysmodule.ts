
import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import SysModule from '../entities/sysmodule'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import ListMutations from './list-mutations'

interface SysModuleState extends ListState<SysModule>{
    editSysModule:SysModule
    isEdit:boolean
}
class SysModuleMutations extends ListMutations<SysModule>{

}
class SysModuleModule extends ListModule<SysModuleState,any,SysModule>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<SysModule>(),
        loading:false,
        isEdit:false,
        editSysModule:new SysModule()
    }
    actions={
        async getAll(context:ActionContext<SysModuleState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/SysModule/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<SysModule>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async create(context:ActionContext<SysModuleState,any>,payload:any){
            await Ajax.post('/api/services/app/SysModule/Create',payload.data);
        },
        async update(context:ActionContext<SysModuleState,any>,payload:any){
            await Ajax.put('/api/services/app/SysModule/Update',payload.data);
        },
        async delete(context:ActionContext<SysModuleState,any>,payload:any){
            await Ajax.delete('/api/services/app/SysModule/Delete?Id='+payload.data.id);
        },        
        async get(context:ActionContext<SysModuleState,any>,payload:any){
            let reponse=await Ajax.get('/api/services/app/SysModule/Get?Id='+payload.id);         
            return reponse.data.result as SysModule;
        }
    };
    mutations={
        setCurrentPage(state:SysModuleState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:SysModuleState,pagesize:number){
            state.pageSize=pagesize;
        },
        edit(state:SysModuleState,sysModule:SysModule){
            state.isEdit=true;
            state.editSysModule=sysModule;
        }
    }
}
const sysModuleModule=new SysModuleModule();
export default sysModuleModule;