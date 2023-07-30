
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
                        <Col span="6">
                            <FormItem :label="'创建时间:'" style="width:100%">
                                <DatePicker  v-model="creationTime" type="datetimerange" format="yyyy-MM-dd" style="width:100%" placement="bottom-end" placeholder="创建时间"></DatePicker>
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
                            <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
                            </Table>
                            <Page  show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage"></Page>
                        </Col>
                    </Row>                    
                </div>
            </div>
        </Card>        
        <edit-dictionary v-model="editModalShow" @save-success="getpage"></edit-dictionary>
    </div>
</template>
<script lang="ts">
    import { Component, Vue,Inject, Prop,Watch } from 'vue-property-decorator';
    import Util from '@/lib/util'
    import AbpBase from '@/lib/abpbase'
    import PageRequest from '@/store/entities/page-request'    
    import EditDictionary from './edit-dictionary.vue'
    class  PageDictionaryRequest extends PageRequest{
        keyword:string;       
        from:Date;
        to:Date;
    }

    @Component({
       components:{               
               "edit-dictionary": EditDictionary
            }
    })
    export default class DictionaryIndex extends AbpBase{
        edit(){
            this.editModalShow=true;
        }
        //filters
        pagerequest:PageDictionaryRequest=new PageDictionaryRequest();
        creationTime:Date[]=[];        
        editModalShow:boolean=false;
        get list(){
            return this.$store.state.dictionary.list;
        };
        get loading(){
            return this.$store.state.dictionary.loading;
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
            //filters
            
            if (this.creationTime.length>0) {
                this.pagerequest.from=this.creationTime[0];
            }
            if (this.creationTime.length>1) {
                this.pagerequest.to=this.creationTime[1];
            }

            await this.$store.dispatch({
                type:'dictionary/getAll',
                data:this.pagerequest
            })
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
        columns=[
        {
              title:"字典名称",
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
                                this.$store.commit('dictionary/edit',params.row);
                                this.edit();
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
                                        content:"公共字典",
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
    }
</script>