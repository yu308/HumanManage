﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.ComponentModel;
using System.Text;

namespace HumanManage.Helpers
{
    /// <summary>
    /// 提供将泛型集合数据导出Excel文档。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ExcelResult<T> : ActionResult where T : new()
    {
        public ExcelResult(IList<T> entity, string fileName)
        {
            this.Entity = entity;
            this.FileName = fileName;
        }

        public ExcelResult(IList<T> entity)
        {
            this.Entity = entity;

            DateTime time = DateTime.Now;
            this.FileName = string.Format("{0}_{1}_{2}_{3}",
                time.Month, time.Day, time.Hour, time.Minute);
        }

        public IList<T> Entity
        {
            get;
            set;
        }

        public string FileName
        {
            get;
            set;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (Entity == null)
            {
                new EmptyResult().ExecuteResult(context);
                return;
            }

            SetResponse(context);
        }

        /// <summary>
        /// 设置并向客户端发送请求响应。
        /// </summary>
        /// <param name="context"></param>
        private void SetResponse(ControllerContext context)
        {
            StringBuilder sBuilder = ConvertEntity();
            byte[] bytestr = Encoding.Unicode.GetBytes(sBuilder.ToString());

            context.HttpContext.Response.Clear();
            context.HttpContext.Response.ClearContent();
            context.HttpContext.Response.Buffer = true;
            context.HttpContext.Response.Charset = "GB2312";
            context.HttpContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.HttpContext.Response.ContentType = "application/ms-excel";
            context.HttpContext.Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ".xls");
            context.HttpContext.Response.AddHeader("Content-Length", bytestr.Length.ToString());
            context.HttpContext.Response.Write(sBuilder);
            context.HttpContext.Response.End();
        }

        /// <summary>
        /// 把泛型集合转换成组合Excel表格的字符串。
        /// </summary>
        /// <returns></returns>
        private StringBuilder ConvertEntity()
        {
            StringBuilder sb = new StringBuilder();

            AddTableHead(sb);
            AddTableBody(sb);

            return sb;
        }

        /// <summary>
        /// 根据IList泛型集合中的每项的属性值来组合Excel表格。
        /// </summary>
        /// <param name="sb"></param>
        private void AddTableBody(StringBuilder sb)
        {
            if (Entity == null || Entity.Count <= 0)
            {
                return;
            }

            PropertyDescriptorCollection properties = FindProperties();

            if (properties.Count <= 0)
            {
                return;
            }

            for (int i = 0; i < Entity.Count; i++)
            {
                for (int j = 0; j < properties.Count; j++)
                {
                    string sign = j == properties.Count - 1 ? "\n" : "\t";
                    object obj = properties[j].GetValue(Entity[i]);
                    obj = obj == null ? string.Empty : obj.ToString();
                    sb.Append(obj + sign);
                }
            }
        }

        /// <summary>
        /// 根据指定类型T的所有属性名称来组合Excel表头。
        /// </summary>
        /// <param name="sb"></param>
        private void AddTableHead(StringBuilder sb)
        {
            PropertyDescriptorCollection properties = FindProperties();

            if (properties.Count <= 0)
            {
                return;
            }

            for (int i = 0; i < properties.Count; i++)
            {
                string sign = i == properties.Count - 1 ? "\n" : "\t";
                sb.Append(properties[i].Name + sign);
            }
        }

        /// <summary>
        /// 返回指定类型T的属性集合。
        /// </summary>
        /// <returns></returns>
        private static PropertyDescriptorCollection FindProperties()
        {
            return TypeDescriptor.GetProperties(typeof(T));
        }
    }
}