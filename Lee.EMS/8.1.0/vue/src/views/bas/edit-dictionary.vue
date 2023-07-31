
<template>
    <div>
        <Modal
         :title="modalTitle"
         :value="value"
         @on-ok="save"
         @on-visible-change="visibleChange"
        >
            <Form ref="dictionaryForm"  label-position="left" :rules="dictionaryRule" :model="dictionary">
              <FormItem label="名称" prop="name">
                <Input v-model="dictionary.name" :maxlength="50"></Input>
              </FormItem>                                                              
              <FormItem label="编码" prop="code">
                <Input v-model="dictionary.code" :maxlength="50"></Input>
              </FormItem>                                                    
              <FormItem label="说明" prop="mem">
                <Input aria-multiline="true" v-model="dictionary.mem" :maxlength="50"></Input>
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
    import Dictionary from '../../store/entities/dictionary'
    @Component
    export default class EditDictionary extends AbpBase{
        @Prop({type:Boolean,default:false}) value:boolean;
        dictionary:Dictionary=new Dictionary();       
        commitUrl:string=this.$store.state.dictionary.isEdit?"dictionary/update":"dictionary/create"; 
        modalTitle:string= this.$store.state.dictionary.isEdit?"编辑":"创建";
        save(){
            (this.$refs.dictionaryForm as any).validate(async (valid:boolean)=>{
                if(valid){
                    await this.$store.dispatch({
                      type:this.commitUrl,
                      data:this.dictionary
                      }).then(res=>{
                        (this.$refs.dictionaryForm as any).resetFields();
                        this.$emit('save-success');
                        this.$emit('input',false);
                    });                  
                }
            })
        }
        cancel(){
            (this.$refs.dictionaryForm as any).resetFields();
            this.$emit('input',false);
        }
        visibleChange(value:boolean){
            if(!value){
                this.$store.state.dictionary.isEdit=false;
                this.$store.state.dictionary.editDictionary=new Dictionary();      
                (this.$refs.dictionaryForm as any).resetFields();
                this.$emit('input',value);
            }else{                
                 if(this.$store.state.dictionary.isEdit){
                    this.dictionary=Util.extend(true,{},this.$store.state.dictionary.editDictionary);                    
                }
                this.commitUrl=this.$store.state.dictionary.isEdit?"dictionary/update":"dictionary/create";
                this.modalTitle= this.$store.state.dictionary.isEdit?"编辑":"创建";
            }
        }        
        dictionaryRule={
           //校验器：
           name:[{required: true,message:"必须填写",trigger: 'blur'}],
           code:[{required: true,message:"必须填写",trigger: 'blur'}]
        }
    }
</script>

