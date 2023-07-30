
<template>
    <div>
        <Modal
         :title="modalTitle"
         :value="value"
         @on-ok="save"
         @on-visible-change="visibleChange"
        >
            <Form ref="dictionarydetailForm"  label-position="left" :rules="dictionarydetailRule" :model="dictionarydetail">
              <FormItem label="name" prop="name">
                <Input v-model="dictionarydetail.name" :maxlength="50"></Input>
              </FormItem>                                                    
              <FormItem label="value" prop="value">
                <Input v-model="dictionarydetail.value" :maxlength="50"></Input>
              </FormItem>                                                    
              <FormItem label="sort" prop="sort">
                <InputNumber v-model="dictionarydetail.sort" ></InputNumber>
              </FormItem>                          
              <FormItem label="describe" prop="describe">
                <Input v-model="dictionarydetail.describe" :maxlength="50"></Input>
              </FormItem>                                                    
              <FormItem label="isDefualt" prop="isDefualt">
                <InputNumber v-model="dictionarydetail.isDefualt" ></InputNumber>
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
    import DictionaryDetail from '../../store/entities/dictionarydetail'
    @Component
    export default class EditDictionaryDetail extends AbpBase{
        @Prop({type:Boolean,default:false}) value:boolean;
        dictionarydetail:DictionaryDetail=new DictionaryDetail();       
        commitUrl:string=this.$store.state.dictionarydetail.isEdit?"dictionarydetail/update":"dictionarydetail/create"; 
        modalTitle:string= this.$store.state.dictionarydetail.isEdit?"编辑":"创建";
        save(){
            (this.$refs.dictionarydetailForm as any).validate(async (valid:boolean)=>{
                if(valid){
                    await this.$store.dispatch({
                      type:this.commitUrl,
                      data:this.dictionarydetail
                      }).then(res=>{
                        (this.$refs.dictionarydetailForm as any).resetFields();
                        this.$emit('save-success');
                        this.$emit('input',false);
                    });                  
                }
            })
        }
        cancel(){
            (this.$refs.dictionarydetailForm as any).resetFields();
            this.$emit('input',false);
        }
        visibleChange(value:boolean){
            if(!value){
                this.$store.state.dictionarydetail.isEdit=false;
                this.$store.state.dictionarydetail.editDictionaryDetail=new DictionaryDetail();      
                (this.$refs.dictionarydetailForm as any).resetFields();
                this.$emit('input',value);
            }else{                
                 if(this.$store.state.dictionarydetail.isEdit){
                    this.dictionarydetail=Util.extend(true,{},this.$store.state.dictionarydetail.editDictionaryDetail);                    
                }
                this.commitUrl=this.$store.state.dictionarydetail.isEdit?"dictionarydetail/update":"dictionarydetail/create";
                this.modalTitle= this.$store.state.dictionarydetail.isEdit?"编辑":"创建";
            }
        }        
        dictionarydetailRule={
           //校验器：name:[{required: true,message:this.L('FieldIsRequired',undefined,this.L('TenantName')),trigger: 'blur'}],
        }
    }
</script>

