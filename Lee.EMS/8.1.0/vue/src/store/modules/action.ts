
import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import Action from '../entities/action'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import ListMutations from './list-mutations'

interface ActionState extends ListState<Action>{
    editAction:Action
    isEdit:boolean
}
class ActionMutations extends ListMutations<Action>{

}
class ActionModule extends ListModule<ActionState,any,Action>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<Action>(),
        loading:false,
        isEdit:false,
        editAction:new Action()
    }
    actions={
        async getAll(context:ActionContext<ActionState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/Action/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<Action>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async create(context:ActionContext<ActionState,any>,payload:any){
            await Ajax.post('/api/services/app/Action/Create',payload.data);
        },
        async update(context:ActionContext<ActionState,any>,payload:any){
            await Ajax.put('/api/services/app/Action/Update',payload.data);
        },
        async delete(context:ActionContext<ActionState,any>,payload:any){
            await Ajax.delete('/api/services/app/Action/Delete?Id='+payload.data.id);
        },        
        async get(context:ActionContext<ActionState,any>,payload:any){
            let reponse=await Ajax.get('/api/services/app/Action/Get?Id='+payload.id);         
            return reponse.data.result as Action;
        }
    };
    mutations={
        setCurrentPage(state:ActionState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:ActionState,pagesize:number){
            state.pageSize=pagesize;
        },
        edit(state:ActionState,action:Action){
            state.isEdit=true;
            state.editAction=action;
        }
    }
}
const actionModule=new ActionModule();
export default actionModule;