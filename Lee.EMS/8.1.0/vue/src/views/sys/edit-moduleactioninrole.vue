
<template>
    <div>
        <Modal
         :title="modalTitle"
         :value="value"
         @on-ok="save"
         @on-visible-change="visibleChange"
        >
            <Form ref="moduleactioninroleForm"  label-position="left" :rules="moduleactioninroleRule" :model="moduleactioninrole">
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
    import ModuleActionInRole from '../../store/entities/moduleactioninrole'
    @Component
    export default class EditModuleActionInRole extends AbpBase{
        @Prop({type:Boolean,default:false}) value:boolean;
        moduleactioninrole:ModuleActionInRole=new ModuleActionInRole();       
        commitUrl:string=this.$store.state.moduleactioninrole.isEdit?"moduleactioninrole/update":"moduleactioninrole/create"; 
        modalTitle:string= this.$store.state.moduleactioninrole.isEdit?"编辑":"创建";
        save(){
            (this.$refs.moduleactioninroleForm as any).validate(async (valid:boolean)=>{
                if(valid){
                    await this.$store.dispatch({
                      type:this.commitUrl,
                      data:this.moduleactioninrole
                      }).then(res=>{
                        (this.$refs.moduleactioninroleForm as any).resetFields();
                        this.$emit('save-success');
                        this.$emit('input',false);
                    });                  
                }
            })
        }
        cancel(){
            (this.$refs.moduleactioninroleForm as any).resetFields();
            this.$emit('input',false);
        }
        visibleChange(value:boolean){
            if(!value){
                this.$store.state.moduleactioninrole.isEdit=false;
                this.$store.state.moduleactioninrole.editModuleActionInRole=new ModuleActionInRole();      
                (this.$refs.moduleactioninroleForm as any).resetFields();
                this.$emit('input',value);
            }else{                
                 if(this.$store.state.moduleactioninrole.isEdit){
                    this.moduleactioninrole=Util.extend(true,{},this.$store.state.moduleactioninrole.editModuleActionInRole);                    
                }
                this.commitUrl=this.$store.state.moduleactioninrole.isEdit?"moduleactioninrole/update":"moduleactioninrole/create";
                this.modalTitle= this.$store.state.moduleactioninrole.isEdit?"编辑":"创建";
            }
        }        
        moduleactioninroleRule={
           //校验器：name:[{required: true,message:this.L('FieldIsRequired',undefined,this.L('TenantName')),trigger: 'blur'}],
        }
    }
</script>

