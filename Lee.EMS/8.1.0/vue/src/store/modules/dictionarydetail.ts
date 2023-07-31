
import {Store,Module,ActionContext} from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import DictionaryDetail from '../entities/dictionarydetail'
import Dictionary from '../entities/dictionary'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import ListMutations from './list-mutations'

interface DictionaryDetailState extends ListState<DictionaryDetail>{
    editDictionaryDetail:DictionaryDetail
    isEdit:boolean,
    dictionary:Dictionary
}
class DictionaryDetailMutations extends ListMutations<DictionaryDetail>{

}
class DictionaryDetailModule extends ListModule<DictionaryDetailState,any,DictionaryDetail>{
    state={
        totalCount:0,
        currentPage:1,
        pageSize:10,
        list: new Array<DictionaryDetail>(),
        loading:false,
        isEdit:false,
        editDictionaryDetail:new DictionaryDetail()
    }
    actions={
        async getAll(context:ActionContext<DictionaryDetailState,any>,payload:any){
            context.state.loading=true;
            let reponse=await Ajax.get('/api/services/app/DictionaryDetail/GetAll',{params:payload.data});
            context.state.loading=false;
            let page=reponse.data.result as PageResult<DictionaryDetail>;
            context.state.totalCount=page.totalCount;
            context.state.list=page.items;
        },
        async create(context:ActionContext<DictionaryDetailState,any>,payload:any){
            await Ajax.post('/api/services/app/DictionaryDetail/Create',payload.data);
        },
        async update(context:ActionContext<DictionaryDetailState,any>,payload:any){
            await Ajax.put('/api/services/app/DictionaryDetail/Update',payload.data);
        },
        async delete(context:ActionContext<DictionaryDetailState,any>,payload:any){
            await Ajax.delete('/api/services/app/DictionaryDetail/Delete?Id='+payload.data.id);
        },        
        async get(context:ActionContext<DictionaryDetailState,any>,payload:any){
            let reponse=await Ajax.get('/api/services/app/DictionaryDetail/Get?Id='+payload.id);         
            return reponse.data.result as DictionaryDetail;
        }
    };
    mutations={
        setCurrentPage(state:DictionaryDetailState,page:number){
            state.currentPage=page;
        },
        setPageSize(state:DictionaryDetailState,pagesize:number){
            state.pageSize=pagesize;
        },
        edit(state:DictionaryDetailState,dictionaryDetail:DictionaryDetail){
            state.isEdit=true;
            state.editDictionaryDetail=dictionaryDetail;
        },
        create(state:DictionaryDetailState,dictionary:Dictionary){
            state.isEdit=false;
            state.editDictionaryDetail=new DictionaryDetail();
            state.editDictionaryDetail.dictionaryid=dictionary.id;
            state.dictionary=dictionary;
        }
    }
}
const dictionaryDetailModule=new DictionaryDetailModule();
export default dictionaryDetailModule;