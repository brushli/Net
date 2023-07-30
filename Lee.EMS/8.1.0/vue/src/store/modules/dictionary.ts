
import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import Dictionary from '../entities/dictionary'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import ListMutations from './list-mutations'

interface DictionaryState extends ListState<Dictionary>{
    editDictionary:Dictionary
    isEdit:boolean
}
class DictionaryMutations extends ListMutations<Dictionary>{

}
class DictionaryModule extends ListModule<DictionaryState,any,Dictionary>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<Dictionary>(),
        loading:false,
        isEdit:false,
        editDictionary:new Dictionary()
    }
    actions={
        async getAll(context:ActionContext<DictionaryState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/Dictionary/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<Dictionary>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async create(context:ActionContext<DictionaryState,any>,payload:any){
            await Ajax.post('/api/services/app/Dictionary/Create',payload.data);
        },
        async update(context:ActionContext<DictionaryState,any>,payload:any){
            await Ajax.put('/api/services/app/Dictionary/Update',payload.data);
        },
        async delete(context:ActionContext<DictionaryState,any>,payload:any){
            await Ajax.delete('/api/services/app/Dictionary/Delete?Id='+payload.data.id);
        },        
        async get(context:ActionContext<DictionaryState,any>,payload:any){
            let reponse=await Ajax.get('/api/services/app/Dictionary/Get?Id='+payload.id);         
            return reponse.data.result as Dictionary;
        }
    };
    mutations={
        setCurrentPage(state:DictionaryState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:DictionaryState,pagesize:number){
            state.pageSize=pagesize;
        },
        edit(state:DictionaryState,dictionary:Dictionary){
            state.isEdit=true;
            state.editDictionary=dictionary;
        }
    }
}
const dictionaryModule=new DictionaryModule();
export default dictionaryModule;