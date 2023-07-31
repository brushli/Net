
<template>
    <div>
        <Modal
         :title="modalTitle"
         :value="value"
         @on-ok="save"
         @on-visible-change="visibleChange"
        >
            <Form ref="sysmoduleForm"  label-position="left" :rules="sysmoduleRule" :model="sysmodule">
              <FormItem label="名称" prop="name">
                <Input v-model="sysmodule.name" :maxlength="50"></Input>
              </FormItem>                                                    
              <FormItem label="图标" prop="icon">
                <Input v-model="sysmodule.icon" :maxlength="50"></Input>
              </FormItem>                                                                                         
              <FormItem label="显示" prop="visible">
                <Checkbox v-model="sysmodule.visible" ></Checkbox>
              </FormItem>                                                    
              <FormItem label="页面地址" prop="pageRoute">
                <Input v-model="sysmodule.pageRoute" :maxlength="50"></Input>
              </FormItem>                                                    
              <FormItem label="描述" prop="describe">
                <Input v-model="sysmodule.describe" :maxlength="50"></Input>
              </FormItem>  
              <FormItem label="序号" prop="sort">
                <InputNumber v-model="sysmodule.sort" ></InputNumber>
              </FormItem>                                                    
            </Form>
            <div slot="footer">
                <Button @click="cancel">取消</Button>
                <Button @click="save" type="primary">保存</Button>
            </div>
        </Modal>
    </div>
</template>
<script lang="ts">
    import { Component, Vue,Inject, Prop,Watch } from 'vue-property-decorator';
    import Util from '../../lib/util'
    import AbpBase from '../../lib/abpbase'
    import SysModule from '../../store/entities/sysmodule'
    @Component
    export default class EditSysModule extends AbpBase{
        @Prop({type:Boolean,default:false}) value:boolean;
        sysmodule:SysModule=new SysModule();       
        commitUrl:string=this.$store.state.sysmodule.isEdit?"sysmodule/update":"sysmodule/create"; 
        modalTitle:string= this.$store.state.sysmodule.isEdit?"编辑":"创建";
        save(){
            (this.$refs.sysmoduleForm as any).validate(async (valid:boolean)=>{
                if(valid){
                    await this.$store.dispatch({
                      type:this.commitUrl,
                      data:this.sysmodule
                      }).then(res=>{
                        (this.$refs.sysmoduleForm as any).resetFields();
                        this.$emit('save-success');
                        this.$emit('input',false);
                    });                  
                }
            })
        }
        cancel(){
            (this.$refs.sysmoduleForm as any).resetFields();
            this.$emit('input',false);
        }
        visibleChange(value:boolean){
            if(!value){
                this.$store.state.sysmodule.isEdit=false;
                this.$store.state.sysmodule.editSysModule=new SysModule();      
                (this.$refs.sysmoduleForm as any).resetFields();
                this.$emit('input',value);
            }else{                
                 if(this.$store.state.sysmodule.isEdit){
                    this.sysmodule=Util.extend(true,{},this.$store.state.sysmodule.editSysModule);                    
                }
                this.commitUrl=this.$store.state.sysmodule.isEdit?"sysmodule/update":"sysmodule/create";
                this.modalTitle= this.$store.state.sysmodule.isEdit?"编辑":"创建";
            }
        }        
        sysmoduleRule={
           //校验器：
           name:[{required: true,message:"必须填写",trigger: 'blur'}],
        }
    }
</script>

