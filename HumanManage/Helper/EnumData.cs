using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanManage.Helpers
{
    public class EnumData
    {
        /// <summary>
        /// 性别
        /// </summary>
        public enum EnumSex
        {
            [EnumTitle("先生")]
            Sir = 1,

            [EnumTitle("女士")]
            Miss = 2
        }
       
    }
}