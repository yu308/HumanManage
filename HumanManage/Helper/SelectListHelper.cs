using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SJCRM.Repository;

namespace HumanManage.Helpers
{
    public static class SelectListHelper
    {
        #region 字典类型
        /// <summary>
        /// 状态
        /// </summary>
        /// <param name="selectvalue"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetDictionaryTypeSelectList(int? selectvalue)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "客户来源", Value = "1" });
            list.Add(new SelectListItem { Text = "跟进类型", Value = "2" });
            list.Add(new SelectListItem { Text = "联系人职务", Value = "3" });
            list.Add(new SelectListItem { Text = "转账银行", Value = "4" });

            foreach (SelectListItem sli in list)
            {
                if (sli.Value == selectvalue.ToString())
                    sli.Selected = true;
            }

            return list;
        }
        public static string GetDictionaryTypeSelectName(int? selectvalue)
        {
            string name = "";
            switch (selectvalue)
            {
                case 1:
                    name = "客户来源";
                    break;
                case 2:
                    name = "跟进类型";
                    break;
                case 3:
                    name = "联系人职务";
                    break;
                case 4:
                    name = "转账银行";
                    break;
            }

            return name;
        }

        public static List<SelectListItem> GetDictionarySelectList(int type, int? selectvalue)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            DictionaryRepository dictionaryRepository = new DictionaryRepository();
            var dictionarylist = dictionaryRepository.
                GetModelListByState(1).
                Where(d => d.DictionaryTypeID == type);
            foreach (var item in dictionarylist)
            {
                list.Add(new SelectListItem { Text = item.DictionaryName, Value = item.DictionaryValue.ToString() });
            }
            foreach (SelectListItem sli in list)
            {
                if (sli.Value == selectvalue.ToString())
                    sli.Selected = true;
            }

            return list;
        }
        public static string GetDictionarySelectName(int type, int selectvalue)
        {
            string name = "";
            DictionaryRepository dictionaryRepository = new DictionaryRepository();
            var dictionary = dictionaryRepository.
                GetModelList().
                Where(d => d.DictionaryTypeID == type).
                Where(d => d.DictionaryValue == selectvalue).
                FirstOrDefault();
            if (dictionary != null)
            {
                name = dictionary.DictionaryName;
            }

            return name;
        }
        #endregion

        #region 状态
        /// <summary>
        /// 状态
        /// </summary>
        /// <param name="selectvalue"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetStateSelectList(int? selectvalue)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "有效", Value = "1" });
            list.Add(new SelectListItem { Text = "无效", Value = "0" });

            foreach (SelectListItem sli in list)
            {
                if (sli.Value == selectvalue.ToString())
                    sli.Selected = true;
            }

            return list;
        }
        #endregion

        #region 客户来源
        /// <summary>
        /// 状态
        /// </summary>
        /// <param name="selectvalue"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetOriginSelectList(int? selectvalue)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "自己搜集", Value = "1" });
            list.Add(new SelectListItem { Text = "他人介绍", Value = "2" });
            list.Add(new SelectListItem { Text = "陌生拜访", Value = "3" });
            list.Add(new SelectListItem { Text = "展会交流", Value = "4" });
            list.Add(new SelectListItem { Text = "客服预约", Value = "5" });

            foreach (SelectListItem sli in list)
            {
                if (sli.Value == selectvalue.ToString())
                    sli.Selected = true;
            }

            return list;
        }

        public static string GetOriginSelectName(int? selectvalue)
        {
            string name = "";
            switch (selectvalue)
            {
                case 1:
                    name = "自己搜集";
                    break;
                case 2:
                    name = "他人介绍";
                    break;
                case 3:
                    name = "陌生拜访";
                    break;
                case 4:
                    name = "展会交流";
                    break;
                case 5:
                    name = "客服预约";
                    break;
            }

            return name;
        }
        #endregion

        #region 优先级
        /// <summary>
        /// 状态
        /// </summary>
        /// <param name="selectvalue"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetPrioritySelectList(int? selectvalue)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "一般", Value = "1" });
            list.Add(new SelectListItem { Text = "急", Value = "2" });
            list.Add(new SelectListItem { Text = "紧急", Value = "3" });
            list.Add(new SelectListItem { Text = "非常急", Value = "4" });

            foreach (SelectListItem sli in list)
            {
                if (sli.Value == selectvalue.ToString())
                    sli.Selected = true;
            }

            return list;
        }

        public static string GetPrioritySelectName(int? selectvalue)
        {
            string name = "";
            switch (selectvalue)
            {
                case 1:
                    name = "一般";
                    break;
                case 2:
                    name = "急";
                    break;
                case 3:
                    name = "紧急";
                    break;
                case 4:
                    name = "非常急";
                    break;
            }

            return name;
        }
        #endregion

        #region 付款状态
        public static List<SelectListItem> GetPayState(int? selectvalue)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "部分付款", Value = "-1" });
            list.Add(new SelectListItem { Text = "未付款", Value = "0" });
            list.Add(new SelectListItem { Text = "全款付清", Value = "1" });
            foreach (SelectListItem sli in list)
            {
                if (sli.Value == selectvalue.ToString())
                    sli.Selected = true;
            }

            return list;
        }
        public static string GetPayStateName(int? selectvalue)
        {
            string name = "";
            switch (selectvalue)
            {
                case -1:
                    name = "部分付款";
                    break;
                case 0:
                    name = "未付款";
                    break;
                case 1:
                    name = "全款付清";
                    break;
            }
            return name;
        }
        #endregion

        #region 付款方式
        public static List<SelectListItem> GetPaymentType(int? selectvalue)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "现金", Value = "1" });
            list.Add(new SelectListItem { Text = "刷卡", Value = "2" });
            list.Add(new SelectListItem { Text = "支票", Value = "3" });
            list.Add(new SelectListItem { Text = "转账", Value = "4" });
            foreach (SelectListItem sli in list)
            {
                if (sli.Value == selectvalue.ToString())
                    sli.Selected = true;
            }

            return list;
        }

        public static string GetPaymentTypeName(int selectvalue)
        {
            string name = "";
            switch (selectvalue)
            {
                case 1:
                    name = "现金";
                    break;
                case 2:
                    name = "刷卡";
                    break;
                case 3:
                    name = "支票";
                    break;
                case 4:
                    name = "转账";
                    break;
            }
            return name;
        }
        #endregion

        #region 审核状态
        public static List<SelectListItem> GetCheckState(int? selectvalue)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "未审核", Value = "0" });
            list.Add(new SelectListItem { Text = "已审核", Value = "1" });
            foreach (SelectListItem sli in list)
            {
                if (sli.Value == selectvalue.ToString())
                    sli.Selected = true;
            }

            return list;
        }
        #endregion

        #region 审核状态1
        public static List<SelectListItem> GetCheckState1(int? selectvalue)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "未审核", Value = "0" });
            list.Add(new SelectListItem { Text = "通过", Value = "1" });
            list.Add(new SelectListItem { Text = "不通过", Value = "2" });
            foreach (SelectListItem sli in list)
            {
                if (sli.Value == selectvalue.ToString())
                    sli.Selected = true;
            }

            return list;
        }
        public static string GetCheckStateName1(int? selectvalue)
        {
            string name = "";
            switch (selectvalue)
            {
                case 1:
                    name = "通过";
                    break;
                case 2:
                    name = "不通过";
                    break;
                default:
                    name = "未审核";
                    break;
            }
            return name;
        }
        #endregion

        #region TruOrFalse
        public static List<SelectListItem> GetIsOrNoSelectList(int? selectvalue)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "是", Value = "1" });
            list.Add(new SelectListItem { Text = "否", Value = "0" });

            foreach (SelectListItem sli in list)
            {
                if (sli.Value == selectvalue.ToString())
                    sli.Selected = true;
            }

            return list;
        }

        public static string GetIsOrNoSelectName(int? selectvalue)
        {
            string name = "";
            switch (selectvalue)
            {
                case 1:
                    name = "是";
                    break;
                default:
                    name = "否";
                    break;
            }
            return name;
        }
        #endregion

        #region TruOrFalseOrAll
        public static List<SelectListItem> GetIsOrNoSelectList(int? selectvalue, bool isall)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (isall)
                list.Add(new SelectListItem { Text = "全部", Value = "-1" });
            list.Add(new SelectListItem { Text = "是", Value = "1" });
            list.Add(new SelectListItem { Text = "否", Value = "0" });


            foreach (SelectListItem sli in list)
            {
                if (sli.Value == selectvalue.ToString())
                    sli.Selected = true;
            }

            return list;
        }
        #endregion

        #region Moudle类别
        public static List<SelectListItem> GetMoudleType(int? selectvalue, bool all)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (all)
                list.Add(new SelectListItem { Text = "请选择类别", Value = "-1" });
            list.Add(new SelectListItem { Text = "一级菜单", Value = "1" });
            list.Add(new SelectListItem { Text = "二级菜单", Value = "2" });
            list.Add(new SelectListItem { Text = "三级菜单", Value = "3" });
            list.Add(new SelectListItem { Text = "功能按钮", Value = "4" });

            foreach (SelectListItem sli in list)
            {
                if (sli.Value == selectvalue.ToString())
                    sli.Selected = true;
            }

            return list;
        }

        public static string GetMoudleTypeName(int selectvalue)
        {
            string typename = "";
            switch (selectvalue)
            {
                case 1:
                    typename = "一级菜单";
                    break;
                case 2:
                    typename = "二级菜单";
                    break;
                case 3:
                    typename = "三级菜单";
                    break;
                case 4:
                    typename = "功能按钮";
                    break;
            }
            return typename;
        }
        #endregion

        #region 客户附件类别
        public static List<SelectListItem> GetAttachmentType(int selectvalue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem { Text = "基本资料", Value = "1" });
            list.Add(new SelectListItem { Text = "合同", Value = "2" });
            list.Add(new SelectListItem { Text = "项目资料", Value = "3" });
            foreach (SelectListItem sli in list)
            {
                if (sli.Value == selectvalue.ToString())
                    sli.Selected = true;
            }

            return list;
        }

        public static string GetAttachmentTypeName(int selectvalue)
        {
            string typename = "";
            switch (selectvalue)
            {
                case 1:
                    typename = "基本资料";
                    break;
                case 2:
                    typename = "合同";
                    break;
                case 3:
                    typename = "项目资料";
                    break;
            }
            return typename;
        }
        #endregion

        #region 客户类别
        public static List<SelectListItem> GetCustomerClass(int? selectvalue)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (selectvalue == 0)
            {
                list.Add(new SelectListItem { Text = "请选择", Value = "0" });
            }
            list.Add(new SelectListItem { Text = "A类", Value = "1" });
            list.Add(new SelectListItem { Text = "B类", Value = "2" });
            list.Add(new SelectListItem { Text = "C类", Value = "3" });
            list.Add(new SelectListItem { Text = "D类", Value = "4" });
            foreach (SelectListItem sli in list)
            {
                if (sli.Value == selectvalue.ToString())
                    sli.Selected = true;
            }

            return list;
        }
        public static string GetCustomerClassName(int? selectvalue)
        {
            string name = "";
            switch (selectvalue)
            {
                case 1:
                    name = "A类";
                    break;
                case 2:
                    name = "B类";
                    break;
                case 3:
                    name = "C类";
                    break;
                case 4:
                    name = "D类";
                    break;
                default:
                    name = "无";
                    break;
            }
            return name;
        }
        #endregion

        #region 时间
        public static List<SelectListItem> GetDateNumbe(string selectvalue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem { Text = "06:00", Value = "6" });
            list.Add(new SelectListItem { Text = "07:00", Value = "7" });
            list.Add(new SelectListItem { Text = "08:00", Value = "8" });
            list.Add(new SelectListItem { Text = "09:00", Value = "9" });
            list.Add(new SelectListItem { Text = "10:00", Value = "10" });
            list.Add(new SelectListItem { Text = "11:00", Value = "11" });
            list.Add(new SelectListItem { Text = "12:00", Value = "12" });
            list.Add(new SelectListItem { Text = "13:00", Value = "13" });
            list.Add(new SelectListItem { Text = "14:00", Value = "14" });
            list.Add(new SelectListItem { Text = "15:00", Value = "15" });
            list.Add(new SelectListItem { Text = "16:00", Value = "16" });
            list.Add(new SelectListItem { Text = "17:00", Value = "17" });
            list.Add(new SelectListItem { Text = "18:00", Value = "18" });
            list.Add(new SelectListItem { Text = "19:00", Value = "19" });
            list.Add(new SelectListItem { Text = "20:00", Value = "20" });
            list.Add(new SelectListItem { Text = "21:00", Value = "21" });
            list.Add(new SelectListItem { Text = "22:00", Value = "22" });
            list.Add(new SelectListItem { Text = "23:00", Value = "23" });
            list.Add(new SelectListItem { Text = "00:00", Value = "24" });
            list.Add(new SelectListItem { Text = "01:00", Value = "1" });
            list.Add(new SelectListItem { Text = "02:00", Value = "2" });
            list.Add(new SelectListItem { Text = "03:00", Value = "3" });
            list.Add(new SelectListItem { Text = "04:00", Value = "4" });
            list.Add(new SelectListItem { Text = "05:00", Value = "5" });
            foreach (SelectListItem sli in list)
            {
                if (sli.Value == selectvalue)
                    sli.Selected = true;
            }

            return list;
        }
        #endregion

        #region 库存地址
        public static List<SelectListItem> GetStockNumbe(string selectvalue)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            //list.Add(new SelectListItem { Text = "全国", Value = "0" });
            list.Add(new SelectListItem { Text = "北京", Value = "1" });
            list.Add(new SelectListItem { Text = "上海", Value = "2" });
            list.Add(new SelectListItem { Text = "天津", Value = "3" });
            list.Add(new SelectListItem { Text = "重庆", Value = "4" });
            list.Add(new SelectListItem { Text = "河北", Value = "5" });
            list.Add(new SelectListItem { Text = "山西", Value = "6" });
            list.Add(new SelectListItem { Text = "河南", Value = "7" });
            list.Add(new SelectListItem { Text = "辽宁", Value = "8" });
            list.Add(new SelectListItem { Text = "吉林", Value = "9" });
            list.Add(new SelectListItem { Text = "黑龙江", Value = "10" });
            list.Add(new SelectListItem { Text = "内蒙古", Value = "11" });
            list.Add(new SelectListItem { Text = "江苏", Value = "12" });
            list.Add(new SelectListItem { Text = "山东", Value = "13" });
            list.Add(new SelectListItem { Text = "安徽", Value = "14" });
            list.Add(new SelectListItem { Text = "浙江", Value = "15" });
            list.Add(new SelectListItem { Text = "福建", Value = "16" });
            list.Add(new SelectListItem { Text = "湖北", Value = "17" });
            list.Add(new SelectListItem { Text = "湖南", Value = "18" });
            list.Add(new SelectListItem { Text = "广东", Value = "19" });
            list.Add(new SelectListItem { Text = "广西", Value = "20" });
            list.Add(new SelectListItem { Text = "江西", Value = "21" });
            list.Add(new SelectListItem { Text = "四川", Value = "22" });
            list.Add(new SelectListItem { Text = "海南", Value = "23" });
            list.Add(new SelectListItem { Text = "贵州", Value = "25" });
            list.Add(new SelectListItem { Text = "云南", Value = "26" });
            list.Add(new SelectListItem { Text = "西藏", Value = "27" });
            list.Add(new SelectListItem { Text = "陕西", Value = "28" });
            list.Add(new SelectListItem { Text = "甘肃", Value = "29" });
            list.Add(new SelectListItem { Text = "青海", Value = "30" });
            list.Add(new SelectListItem { Text = "宁夏", Value = "31" });
            list.Add(new SelectListItem { Text = "新疆", Value = "32" });
            list.Add(new SelectListItem { Text = "台湾", Value = "33" });
            list.Add(new SelectListItem { Text = "香港", Value = "34" });
            list.Add(new SelectListItem { Text = "澳门", Value = "35" });
            list.Add(new SelectListItem { Text = "钓鱼岛", Value = "36" });



            foreach (SelectListItem sli in list)
            {
                if (sli.Value == selectvalue)
                    sli.Selected = true;
            }

            return list;
        }
        #endregion

        #region 接收状态
        public static List<SelectListItem> GetReceiveState(int? selectvalue)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "拒绝", Value = "-1" });
            list.Add(new SelectListItem { Text = "未接收", Value = "0" });
            list.Add(new SelectListItem { Text = "接收", Value = "1" });
            foreach (SelectListItem sli in list)
            {
                if (sli.Value == selectvalue.ToString())
                    sli.Selected = true;
            }

            return list;
        }
        public static string GetReceiveStateName(int? selectvalue)
        {
            string name = "";
            switch (selectvalue)
            {
                case -1:
                    name = "拒绝";
                    break;
                case 0:
                    name = "未接收";
                    break;
                case 1:
                    name = "接收";
                    break;
            }
            return name;
        }
        #endregion

        #region 面试职位
        /// <summary>
        /// 状态
        /// </summary>
        /// <param name="selectvalue"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetOfficeSelectList(int? selectvalue)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "职位选择", Value = "0" });
            list.Add(new SelectListItem { Text = "客服", Value = "1" });
            list.Add(new SelectListItem { Text = "PHP", Value = "2" });
            list.Add(new SelectListItem { Text = "财务", Value = "3" });
            list.Add(new SelectListItem { Text = ".NET", Value = "4" });
            list.Add(new SelectListItem { Text = "销售经理", Value = "5" });
            list.Add(new SelectListItem { Text = "主管", Value = "6" });
            list.Add(new SelectListItem { Text = "销售专员", Value = "7" });
            list.Add(new SelectListItem { Text = "人事专员", Value = "8" });
            list.Add(new SelectListItem { Text = "人事经理", Value = "9" });
            list.Add(new SelectListItem { Text = "美工", Value = "10" });

            foreach (SelectListItem sli in list)
            {
                if (sli.Value == selectvalue.ToString())
                    sli.Selected = true;
            }

            return list;
        }

        public static string GetOfficeSelectName(int? selectvalue)
        {
            string name = "";
            switch (selectvalue)
            {
                case 1:
                    name = "客服";
                    break;
                case 2:
                    name = "PHP";
                    break;
                case 3:
                    name = "财务";
                    break;
                case 4:
                    name = ".NET";
                    break;
                case 5:
                    name = "销售经理";
                    break;
                case 6:
                    name = "主管";
                    break;
                case 7:
                    name = "销售专员";
                    break;
                case 8:
                    name = "人事专员";
                    break;
                case 9:
                    name = "人事经理";
                    break;
                case 10:
                    name = "美工";
                    break;
            }

            return name;
        }
        #endregion

        #region 渠道来源
        /// <summary>
        /// 状态
        /// </summary>
        /// <param name="selectvalue"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetChannelSelectList(int? selectvalue)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "智联招聘", Value = "1" });
            list.Add(new SelectListItem { Text = "51job", Value = "2" });
            list.Add(new SelectListItem { Text = "58同城", Value = "3" });
            list.Add(new SelectListItem { Text = "内部介绍", Value = "4" });

            foreach (SelectListItem sli in list)
            {
                if (sli.Value == selectvalue.ToString())
                    sli.Selected = true;
            }

            return list;
        }

        public static string GetChannelSelectName(int? selectvalue)
        {
            string name = "";
            switch (selectvalue)
            {
                case 1:
                    name = "智联招聘";
                    break;
                case 2:
                    name = "51job";
                    break;
                case 3:
                    name = "58同城";
                    break;
                case 4:
                    name = "内部介绍";
                    break;
            }

            return name;
        }
        #endregion

        #region 面试状态
        /// <summary>
        /// 状态
        /// </summary>
        /// <param name="selectvalue"></param>
        /// <returns></returns>
        public static List<SelectListItem> GetAuditionStateSelectList(int? selectvalue)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem { Text = "状态选择", Value = "0" });
            list.Add(new SelectListItem { Text = "已通知", Value = "1" });
            list.Add(new SelectListItem { Text = "已面试", Value = "2" });
            list.Add(new SelectListItem { Text = "已复试", Value = "3" });
            list.Add(new SelectListItem { Text = "已报道", Value = "4" });

            foreach (SelectListItem sli in list)
            {
                if (sli.Value == selectvalue.ToString())
                    sli.Selected = true;
            }

            return list;
        }

        public static string GetAuditionStateSelectName(int? selectvalue)
        {
            string name = "";
            switch (selectvalue)
            {
                case 1:
                    name = "已通知";
                    break;
                case 2:
                    name = "已面试";
                    break;
                case 3:
                    name = "已复试";
                    break;
                case 4:
                    name = "已报道";
                    break;
            }

            return name;
        }
        #endregion
    }
}