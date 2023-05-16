using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCEMREditor
{
    internal class GlobStaticData
    {
        public static Patient Patient;
    }
    public class Patient
    {
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string Name { get; set; } = "张三";

        /// <summary>
        /// 科室
        /// </summary>
        public string Department { get; set; } = "内科";

        /// <summary>
        /// 床号
        /// </summary>
        public string BedNo { get; set; } = "001";

        /// <summary>
        /// 患者 ID
        /// </summary>
        public string PatientId { get; set; } = "000001";

        /// <summary>
        /// 住院号
        /// </summary>
        public string AdmissionNo { get; set; } = "001";

        /// <summary>
        /// 职业
        /// </summary>
        public string Occupation { get; set; } = "教师";

        /// <summary>
        /// 出生地
        /// </summary>
        public string Birthplace { get; set; } = "北京";

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; } = "男";

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday { get; set; } = new DateTime(1990, 1, 1);

        /// <summary>
        /// 婚姻状况
        /// </summary>
        public string MaritalStatus { get; set; } = "未婚";

        /// <summary>
        /// 入院时间
        /// </summary>
        public DateTime AdmissionTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 民族
        /// </summary>
        public string Ethnic { get; set; } = "汉族";

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; } = "北京市朝阳区";

        /// <summary>
        /// 电话
        /// </summary>
        public string PhoneNumber { get; set; } = "010-12345678";

        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactPerson { get; set; } = "李四";

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ContactPhoneNumber { get; set; } = "010-87654321";
    }

}
