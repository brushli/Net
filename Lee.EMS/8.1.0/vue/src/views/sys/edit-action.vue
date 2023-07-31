
<template>
    <div>
        <Modal
         :title="modalTitle"
         :value="value"
         @on-ok="save"
         @on-visible-change="visibleChange"
        >
            <Form ref="actionForm"  label-position="left" :rules="actionRule" :model="action">
              <FormItem label="name" prop="name">
                <Input v-model="action.name" :maxlength="50"></Input>
              </FormItem>                                                    
              <FormItem label="icon" prop="icon">
                <Input v-model="action.icon" :maxlength="50"></Input>
              </FormItem>                                                    
              <FormItem label="sort" prop="sort">
                <InputNumber v-model="action.sort" ></InputNumber>
              </FormItem>                          
              <FormItem label="visible" prop="visible">
                <Input v-model="action.visible" :maxlength="50"></Input>
              </FormItem>                                                    
              <FormItem label="methodCode" prop="methodCode">
                <Input v-model="action.methodCode" :maxlength="50"></Input>
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
    import Action from '../../store/entities/action'
    @Component
    export default class EditAction extends AbpBase{
        @Prop({type:Boolean,default:false}) value:boolean;
        action:Action=new Action();       
        commitUrl:string=this.$store.state.action.isEdit?"action/update":"action/create"; 
        modalTitle:string= this.$store.state.action.isEdit?"编辑":"创建";
        save(){
            (this.$refs.actionForm as any).validate(async (valid:boolean)=>{
                if(valid){
                    await this.$store.dispatch({
                      type:this.commitUrl,
                      data:this.action
                      }).then(res=>{
                        (this.$refs.actionForm as any).resetFields();
                        this.$emit('save-success');
                        this.$emit('input',false);
                    });                  
                }
            })
        }
        cancel(){
            (this.$refs.actionForm as any).resetFields();
            this.$emit('input',false);
        }
        visibleChange(value:boolean){
            if(!value){
                this.$store.state.action.isEdit=false;
                this.$store.state.action.editAction=new Action();      
                (this.$refs.actionForm as any).resetFields();
                this.$emit('input',value);
            }else{                
                 if(this.$store.state.action.isEdit){
                    this.action=Util.extend(true,{},this.$store.state.action.editAction);                    
                }
                this.commitUrl=this.$store.state.action.isEdit?"action/update":"action/create";
                this.modalTitle= this.$store.state.action.isEdit?"编辑":"创建";
            }
        }        
        actionRule={
           //校验器：name:[{required: true,message:this.L('FieldIsRequired',undefined,this.L('TenantName')),trigger: 'blur'}],
        }
    }
</script>

