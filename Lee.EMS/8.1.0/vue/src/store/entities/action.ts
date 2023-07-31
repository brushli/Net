
/*-------------------------------------------------------
// Copyright (C) 2020 Lic
//
// 功能描述:动作管理  实体对象
//
//
// 创建标识: Lee -- 2023-07-30 15:32:44
//
//------------------------------------------------------*/
 
import Entity from './entity'
// 动作管理 
export default class Action extends Entity<number>{
    moduleid:number;
    name:string;
    icon:string;
    sort:number;
    visible:string;
    methodCode:string;
}
