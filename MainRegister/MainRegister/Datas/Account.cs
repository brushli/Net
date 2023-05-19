using System;

namespace MainRegister.Datas
{
    /// <summary>
    /// 
    /// </summary>
    public class Account
    {
        //[PrimaryKey, AutoIncrement]
        public int ID { set; get; }
        public string Name { set; get; }
        public string Area { set; get; }
        public DateTime CreateDate { set; get; }
    }
}
