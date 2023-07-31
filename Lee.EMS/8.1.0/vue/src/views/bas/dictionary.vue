
<template>
    <div>
        <Card dis-hover>
            <div class="page-body">
                <Form ref="queryForm" :label-width="80" label-position="left" inline>
                    <Row :gutter="16">
                        <Col span="6">
                            <FormItem :label="'关键字:'" style="width:100%">
                                <Input v-model="pagerequest.keyword" placeholder="关键字"></Input>
                            </FormItem>
                        </Col>                                              
                    </Row>
                    <Row>
                        <Button @click="edit" icon="android-add" type="primary" size="large">新增字典</Button>
                        <Button icon="ios-search" type="primary" size="large" @click="getpage" class="toolbar-btn">查找</Button>
                    </Row>
                </Form>
                <div class="margin-top-10">
                    <Row>
                        <Col span="8">
                            <Table :loading="loading" :highlight-row="true" :columns="columns" @on-row-click="onDictionaryRowClick" :no-data-text="L('NoDatas')" border :data="list">
                            </Table>
                            <Page  show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage"></Page>
                        </Col>
                        <Col span="16">
                            <Table :loading="detailloading" :columns="detailColumns" :no-data-text="L('NoDatas')" border :data="detaillist">
                            </Table>
                        </Col>
                    </Row>                    
                </div>
            </div>
        </Card>        
        <edit-dictionary v-model="editModalShow" @save-success="getpage"></edit-dictionary>
        <edit-dictionarydetail v-model="editDetailModalShow" @save-success="detailgetpage"></edit-dictionarydetail>
    </div>
</template>
<script lang="ts">
    import { Component, Vue,Inject, Prop,Watch } from 'vue-property-decorator';
    import Util from '@/lib/util'
    import AbpBase from '@/lib/abpbase'
    import PageRequest from '@/store/entities/page-request'    
    import EditDictionary from './edit-dictionary.vue'
    import EditDictionaryDetail from './edit-dictionarydetail.vue'
    import DictionaryDetail from '../../store/entities/dictionarydetail'
    class  PageDictionaryRequest extends PageRequest{
        keyword:string;  
    }
    class  PageDictionaryDetailRequest extends PageRequest{
        keyword:string;
        dictionaryId:string;
        from:Date;
        to:Date;
    }
    @Component({
       components:{               
               "edit-dictionary": EditDictionary,
               "edit-dictionarydetail": EditDictionaryDetail
            }
    })
    export default class DictionaryIndex extends AbpBase{
        edit(){
            this.editModalShow=true;
        }
        editdetail(){
            this.editDetailModalShow=true;
        }
        //filters
        pagerequest:PageDictionaryRequest=new PageDictionaryRequest();
        pageredetailquest:PageDictionaryDetailRequest=new PageDictionaryDetailRequest();
        editModalShow:boolean=false;
        editDetailModalShow:boolean=false;
        currentRow:any={};
        get list(){
            return this.$store.state.dictionary.list;
        };
        get detaillist(){
            return this.$store.state.dictionarydetail.list;
        };
        get loading(){
            return this.$store.state.dictionary.loading;
        }   
        get detailloading(){
            return this.$store.state.dictionarydetail.loading;
        }                
        pageChange(page:number){
            this.$store.commit('dictionary/setCurrentPage',page);
            this.getpage();
        }
        pagesizeChange(pagesize:number){
            this.$store.commit('dictionary/setPageSize',pagesize);
            this.getpage();
        }
        async getpage(){
            this.pagerequest.maxResultCount=this.pageSize;
            this.pagerequest.skipCount=(this.currentPage-1)*this.pageSize;            
            await this.$store.dispatch({
                type:'dictionary/getAll',
                data:this.pagerequest
            })
            this.$store.state.dictionarydetail.list=new Array<DictionaryDetail>();
        }
        get pageSize(){
            return this.$store.state.dictionary.pageSize;
        }
        get totalCount(){
            return this.$store.state.dictionary.totalCount;
        }
        get currentPage(){
            return this.$store.state.dictionary.currentPage;
        }        
        async created(){
            this.getpage();           
        }
        async detailgetpage(){
            this.pageredetailquest.dictionaryId=this.currentRow.id;
            this.pageredetailquest.maxResultCount=99999;
            this.pageredetailquest.skipCount=0;          
            await this.$store.dispatch({
                type:'dictionarydetail/getAll',
                data:this.pageredetailquest
            })
        }
        async detailcreated(){
            this.detailgetpage();           
        }        
        async onDictionaryRowClick( currentRow: object, index: number){
            this.currentRow=currentRow;
            this.detailgetpage();
        }                
        columns=[
        {
              title:"名称",
              key:'name'
         },            
        {
              title:"编码",
              key:'code'
         },    
        {
            title:"操作",
            key:'Actions',
            width:200,
            render:(h:any,params:any)=>{
                return h('div',[
                    h('Button',{
                        props:{
                            type:'success',
                            size:'small'
                        },
                        style:{
                            marginRight:'5px'
                        },
                        on:{
                            click:()=>{
                                this.$store.commit('dictionarydetail/create',params.row);
                                this.editdetail();
                            }
                        }
                    },"添加"),
                    h('Button',{
                        props:{
                            type:'primary',
                            size:'small'
                        },
                        style:{
                            marginRight:'5px'
                        },
                        on:{
                            click:()=>{
                                this.$store.commit('dictionary/edit',params.row);
                                this.edit();
                            }
                        }
                    },"编辑"),
                    h('Button',{
                        props:{
                            type:'error',
                            size:'small'
                        },
                        on:{
                            click:async ()=>{
                                this.$Modal.confirm({
                                        title:"系统提示",
                                        content:"是否删除:"+params.row.name+"?",
                                        okText:"是",
                                        cancelText:"否",
                                        onOk:async()=>{
                                            await this.$store.dispatch({
                                                type:'dictionary/delete',
                                                data:params.row
                                            })
                                            await this.getpage();
                                        }
                                })
                            }
                        }
                    },"删除")
                ])
            }
        }]       
        detailColumns=[
        {
              title:"名称",
              key:'name'
         },            
        {
              title:"值",
              key:'value'
         },            
        {
              title:"序号",
              key:'sort'
         },            
        {
              title:"描述",
              key:'describe'
         },            
        {
              title:"默认",
              key:'isDefualt'
         },        
        {
            title:"操作",
            key:'Actions',
            width:150,
            render:(h:any,params:any)=>{
                return h('div',[
                    h('Button',{
                        props:{
                            type:'primary',
                            size:'small'
                        },
                        style:{
                            marginRight:'5px'
                        },
                        on:{
                            click:()=>{
                                this.$store.commit('dictionarydetail/edit',params.row);
                                this.editdetail();
                            }
                        }
                    },"编辑"),
                    h('Button',{
                        props:{
                            type:'error',
                            size:'small'
                        },
                        on:{
                            click:async ()=>{
                                this.$Modal.confirm({
                                        title:"系统提示",
                                        content:"是否删除："+params.row.name+"?",
                                        okText:"是",
                                        cancelText:"否",
                                        onOk:async()=>{
                                            await this.$store.dispatch({
                                                type:'dictionarydetail/delete',
                                                data:params.row
                                            })
                                            await this.detailgetpage();
                                        }
                                })
                            }
                        }
                    },"删除")
                ])
            }
        }]  
    }
</script>