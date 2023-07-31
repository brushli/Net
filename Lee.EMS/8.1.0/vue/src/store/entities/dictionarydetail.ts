
/*-------------------------------------------------------
// Copyright (C) 2020 Lic
//
// 功能描述:公共字典  实体对象
//
//
// 创建标识: Lee -- 2023-07-29 22:36:10
//
//------------------------------------------------------*/
 
import Entity from './entity'
// 公共字典 
export default class DictionaryDetail extends Entity<number>{
    dictionaryid:number;
    name:string;
    value:string;
    sort:number;
    describe:string;
    isDefualt:boolean;
}
