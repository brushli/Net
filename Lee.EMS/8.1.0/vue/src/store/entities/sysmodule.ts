
/*-------------------------------------------------------
// Copyright (C) 2020 Lic
//
// 功能描述:模块关联  实体对象
//
//
// 创建标识: Lee -- 2023-07-30 15:48:53
//
//------------------------------------------------------*/
 
import Entity from './entity'
// 模块关联 
export default class SysModule extends Entity<number>{
    parentId:number;
    name:string;
    icon:string;
    sort:number;
    visible:boolean;
    pageRoute:string;
    describe:string;
    constructor(){
        super();
        this.visible=true;
    }
}
