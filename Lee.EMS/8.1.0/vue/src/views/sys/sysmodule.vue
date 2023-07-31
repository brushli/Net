
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
                        <Button @click="edit" icon="android-add" type="primary" size="large">新增</Button>
                        <Button icon="ios-search" type="primary" size="large" @click="getpage" class="toolbar-btn">查找</Button>
                    </Row>
                </Form>
                <div class="margin-top-10">
                    <Table :loading="loading" :columns="columns" :no-data-text="L('NoDatas')" border :data="list">
                    </Table>
                    <Page  show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage"></Page>
                </div>
            </div>
        </Card>        
        <edit-sysmodule v-model="editModalShow" @save-success="getpage"></edit-sysmodule>
    </div>
</template>
<script lang="ts">
    import { Component, Vue,Inject, Prop,Watch } from 'vue-property-decorator';
    import Util from '@/lib/util'
    import AbpBase from '@/lib/abpbase'
    import PageRequest from '@/store/entities/page-request'    
    import EditSysModule from './edit-sysmodule.vue'
    class  PageSysModuleRequest extends PageRequest{
        keyword:string;       
    }

    @Component({
       components:{               
               "edit-sysmodule": EditSysModule
            }
    })
    export default class SysModuleIndex extends AbpBase{
        edit(){
            this.editModalShow=true;
        }
        //filters
        pagerequest:PageSysModuleRequest=new PageSysModuleRequest();
        creationTime:Date[]=[];        
        editModalShow:boolean=false;
        get list(){
            return this.$store.state.sysmodule.list;
        };
        get loading(){
            return this.$store.state.sysmodule.loading;
        }              
        pageChange(page:number){
            this.$store.commit('sysmodule/setCurrentPage',page);
            this.getpage();
        }
        pagesizeChange(pagesize:number){
            this.$store.commit('sysmodule/setPageSize',pagesize);
            this.getpage();
        }
        async getpage(){
          
            this.pagerequest.maxResultCount=this.pageSize;
            this.pagerequest.skipCount=(this.currentPage-1)*this.pageSize;
            await this.$store.dispatch({
                type:'sysmodule/getAll',
                data:this.pagerequest
            })
        }
        get pageSize(){
            return this.$store.state.sysmodule.pageSize;
        }
        get totalCount(){
            return this.$store.state.sysmodule.totalCount;
        }
        get currentPage(){
            return this.$store.state.sysmodule.currentPage;
        }
        async created(){
            this.getpage();           
        }
        columns=[
        {
              title:"名称",
              key:'name'
         },            
        {
              title:"图标",
              key:'icon'
         },            
        {
              title:"序号",
              key:'sort'
         },            
        {
              title:"是否显示",
              key:'visible'
         },            
        {
              title:"页面地址",
              key:'pageRoute'
         },            
        {
              title:"描述",
              key:'describe'
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
                                this.$store.commit('sysmodule/edit',params.row);
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
                                        content:"模块关联",
                                        okText:"是",
                                        cancelText:"否",
                                        onOk:async()=>{
                                            await this.$store.dispatch({
                                                type:'sysmodule/delete',
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