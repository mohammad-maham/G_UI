﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

public static partial class HtmlHelperExtensions
{
    public static IDictionary<string, object> MergeHtmlAttributes(this HtmlHelper helper, object htmlAttributesObject, object defaultHtmlAttributesObject)
    {
        var concatKeys = new string[] { "class" };

        var htmlAttributesDict = htmlAttributesObject as IDictionary<string, object>;
        var defaultHtmlAttributesDict = defaultHtmlAttributesObject as IDictionary<string, object>;

        RouteValueDictionary htmlAttributes = (htmlAttributesDict != null)
            ? new RouteValueDictionary(htmlAttributesDict)
            : HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributesObject);
        RouteValueDictionary defaultHtmlAttributes = (defaultHtmlAttributesDict != null)
            ? new RouteValueDictionary(defaultHtmlAttributesDict)
            : HtmlHelper.AnonymousObjectToHtmlAttributes(defaultHtmlAttributesObject);

        foreach (var item in htmlAttributes)
        {
            if (concatKeys.Contains(item.Key))
            {
                defaultHtmlAttributes[item.Key] = (defaultHtmlAttributes[item.Key] != null)
                    ? string.Format("{0} {1}", defaultHtmlAttributes[item.Key].ToString().ToLower(), item.Value.ToString().ToLower())
                    : item.Value.ToString().ToLower();
            }
            else
            {
                defaultHtmlAttributes[item.Key] = item.Value.ToString().ToLower();
            }
        }

        return defaultHtmlAttributes;
    }
}