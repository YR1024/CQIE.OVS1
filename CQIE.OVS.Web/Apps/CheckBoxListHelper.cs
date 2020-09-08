using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace CQIE.OVS.Web.Apps
{
    public static class CheckBoxListHelper
    {
        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> selectList)
        {
            return CheckBoxList(helper, name, selectList, new { });
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> selectList, int listCount)
        {
            return CheckBoxList(helper, name, selectList, new { }, listCount);
        }
        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            int index = 0;
            StringBuilder stringBuilder = new StringBuilder();
            if (selectList != null)
            {
                foreach (SelectListItem selectItem in selectList)
                {
                    IDictionary<string, object> newHtmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                    index++;
                    newHtmlAttributes.Add("id", name + "_" + index.ToString());
                    newHtmlAttributes.Add("name", name);
                    newHtmlAttributes.Add("type", "checkbox");
                    newHtmlAttributes.Add("value", selectItem.Value);
                    if (selectItem.Selected)
                    {
                        newHtmlAttributes.Add("checked", "checked");
                    }
                    TagBuilder tagBuilder = new TagBuilder("input");
                    tagBuilder.MergeAttributes<string, object>(newHtmlAttributes);
                    string inputAllHtml = tagBuilder.ToString(TagRenderMode.SelfClosing);
                    stringBuilder.AppendFormat(@"<div style='margin-right:20px; float:left;'> {0}{1}</div>", inputAllHtml, selectItem.Text);
                }
            }
            return MvcHtmlString.Create(stringBuilder.ToString());
        }

        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes, int listCount)
        {
            //listCount = 4;
            int index = 0;
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<div style='margin-right:20px; float:left;'>");
            if (selectList != null)
            {
                int count = 0;
                foreach (SelectListItem selectItem in selectList)
                {
                    IDictionary<string, object> newHtmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                    index++;
                    newHtmlAttributes.Add("id", name + "_" + index.ToString());
                    newHtmlAttributes.Add("name", name);
                    newHtmlAttributes.Add("type", "checkbox");
                    newHtmlAttributes.Add("value", selectItem.Value);
                    if (selectItem.Selected)
                    {
                        newHtmlAttributes.Add("checked", "checked");
                    }
                    TagBuilder tagBuilder = new TagBuilder("input");
                    tagBuilder.MergeAttributes<string, object>(newHtmlAttributes);
                    string inputAllHtml = tagBuilder.ToString(TagRenderMode.SelfClosing);
                    stringBuilder.AppendFormat(@"<div > {0}{1}</div>", inputAllHtml, selectItem.Text);
                    count++;
                    if (count % listCount == 0)
                    {
                        stringBuilder.AppendFormat(@"</div><div style='margin-right:20px; float:left;'>");
                    }
                }
                stringBuilder.AppendFormat("</div>");
            }
            return MvcHtmlString.Create(stringBuilder.ToString());
        }
    }
}