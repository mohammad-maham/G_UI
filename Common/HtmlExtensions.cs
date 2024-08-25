using G_APIs.Models.ComponentModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

public static class HtmlExtensions
{
    public static IDisposable Box(this HtmlHelper htmlHelper, string title = "", string icon = "", int col = 12, string cssClass = "", Dictionary<string, string> attributes = null, BoxIconTypes iconType = BoxIconTypes.@default)
    {
        TagBuilder tagBuilder = new TagBuilder("div");

        tagBuilder.MergeAttributes(attributes);

        string _cssClass = $"statbox widget box box-shadow col-xl-{col} col-md-{col} col-sm-12 col-12 overflow-hidden";
        tagBuilder.AddCssClass($"{cssClass}");
        tagBuilder.AddCssClass(_cssClass);

        htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));

        // box icons
        icon = !string.IsNullOrEmpty(icon) ? icon : boxIconsSVGMapping(iconType);

        // Write the title
        TagBuilder titleTagBuilder = new TagBuilder("div");
        titleTagBuilder.AddCssClass("widget-header");
        string header = $@"
                        <div class=""row"">
                            <div class=""col-xl-12 col-md-12 col-sm-12 col-12"">   
                                <h5><div class=""w-icon"">{icon}</div>{title}</h5>
                            </div>
                        </div>";


        if (!string.IsNullOrEmpty(title))
        {
            titleTagBuilder.InnerHtml = header;
        }

        htmlHelper.ViewContext.Writer.Write(titleTagBuilder.ToString(TagRenderMode.Normal));

        return new BoxWrapper(htmlHelper.ViewContext.Writer);

    }

    private class BoxWrapper : IDisposable
    {
        private readonly TextWriter _writer;

        public BoxWrapper(TextWriter writer)
        {
            _writer = writer;
        }

        public void Dispose()
        {
            _writer.Write("</div>");
        }
    }


    public static MvcHtmlString GridFor<T>(this HtmlHelper htmlHelper, List<T> list, params Expression<Func<T, object>>[] columnSelectors)
    {
        if (list == null || !list.Any())
        {
            return MvcHtmlString.Create("<p>No data available</p>");
        }

        StringBuilder sb = new StringBuilder();
        sb.Append("<table class=\"datatable table table-hover non-hover table-striped\" style=\"width:100%\">");
        sb.Append("<thead><tr class=\"gra\">");
        foreach (Expression<Func<T, object>> selector in columnSelectors)
        {
            MemberExpression memberExpression = GetMemberExpression(selector);
            string displayName = memberExpression.Member.GetCustomAttribute<DisplayAttribute>()?.Name ?? memberExpression.Member.Name;
            sb.AppendFormat("<th>{0}</th>", displayName);

        }
        sb.Append("</tr></thead>");

        // Generate table rows
        sb.Append("<tbody>");
        foreach (T item in list)
        {
            sb.Append("<tr>");
            foreach (Expression<Func<T, object>> selector in columnSelectors)
            {
                object value = selector.Compile()(item);
                sb.AppendFormat("<td>{0}</td>", value);
            }
            sb.Append("</tr>");
        }
        sb.Append("</tbody>");

        sb.Append("</table>");
        return MvcHtmlString.Create(sb.ToString());
    }

    private static MemberExpression GetMemberExpression<T>(Expression<Func<T, object>> expression)
    {
        if (expression.Body is MemberExpression memberExpression)
        {
            return memberExpression;
        }
        else if (expression.Body is UnaryExpression unaryExpression && unaryExpression.Operand is MemberExpression unaryMemberExpression)
        {
            return unaryMemberExpression;
        }
        throw new ArgumentException("Expression is not a member access", nameof(expression));
    }

    private static string boxIconsSVGMapping(BoxIconTypes iconType)
    {
        string result = string.Empty;
        switch (iconType)
        {
            case BoxIconTypes.none:
                result = string.Empty;
                break;
            case BoxIconTypes.@default:
                result = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\" width=\"24\" height=\"24\" class=\"main-grid-item-icon\" fill=\"none\" stroke=\"currentColor\" stroke-linecap=\"round\" stroke-linejoin=\"round\" stroke-width=\"2\">\r\n  <rect height=\"18\" rx=\"2\" ry=\"2\" width=\"18\" x=\"3\" y=\"3\" />\r\n  <line x1=\"3\" x2=\"21\" y1=\"9\" y2=\"9\" />\r\n  <line x1=\"9\" x2=\"9\" y1=\"21\" y2=\"9\" />\r\n</svg>";
                break;
            case BoxIconTypes.chart:
                result = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\" width=\"24\" height=\"24\" class=\"main-grid-item-icon\" fill=\"none\" stroke=\"currentColor\" stroke-linecap=\"round\" stroke-linejoin=\"round\" stroke-width=\"2\">\r\n  <path d=\"M21.21 15.89A10 10 0 1 1 8 2.83\" />\r\n  <path d=\"M22 12A10 10 0 0 0 12 2v10z\" />\r\n</svg>";
                break;
            case BoxIconTypes.search:
                result = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\" width=\"24\" height=\"24\" class=\"main-grid-item-icon\" fill=\"none\" stroke=\"currentColor\" stroke-linecap=\"round\" stroke-linejoin=\"round\" stroke-width=\"2\">\r\n  <circle cx=\"11\" cy=\"11\" r=\"8\" />\r\n  <line x1=\"21\" x2=\"16.65\" y1=\"21\" y2=\"16.65\" />\r\n</svg>";
                break;
            case BoxIconTypes.form:
                result = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\" width=\"24\" height=\"24\" class=\"main-grid-item-icon\" fill=\"none\" stroke=\"currentColor\" stroke-linecap=\"round\" stroke-linejoin=\"round\" stroke-width=\"2\">\r\n  <path d=\"M17 3a2.828 2.828 0 1 1 4 4L7.5 20.5 2 22l1.5-5.5L17 3z\" />\r\n</svg>";
                break;
            case BoxIconTypes.grid:
                result = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\" width=\"24\" height=\"24\" class=\"main-grid-item-icon\" fill=\"none\" stroke=\"currentColor\" stroke-linecap=\"round\" stroke-linejoin=\"round\" stroke-width=\"2\">\r\n  <path d=\"M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z\" />\r\n  <polyline points=\"14 2 14 8 20 8\" />\r\n  <line x1=\"16\" x2=\"8\" y1=\"13\" y2=\"13\" />\r\n  <line x1=\"16\" x2=\"8\" y1=\"17\" y2=\"17\" />\r\n  <polyline points=\"10 9 9 9 8 9\" />\r\n</svg>";
                break;
            case BoxIconTypes.detail:
                result = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\" width=\"24\" height=\"24\" class=\"main-grid-item-icon\" fill=\"none\" stroke=\"currentColor\" stroke-linecap=\"round\" stroke-linejoin=\"round\" stroke-width=\"2\">\r\n  <line x1=\"8\" x2=\"21\" y1=\"6\" y2=\"6\" />\r\n  <line x1=\"8\" x2=\"21\" y1=\"12\" y2=\"12\" />\r\n  <line x1=\"8\" x2=\"21\" y1=\"18\" y2=\"18\" />\r\n  <line x1=\"3\" x2=\"3.01\" y1=\"6\" y2=\"6\" />\r\n  <line x1=\"3\" x2=\"3.01\" y1=\"12\" y2=\"12\" />\r\n  <line x1=\"3\" x2=\"3.01\" y1=\"18\" y2=\"18\" />\r\n</svg>";
                break;
            case BoxIconTypes.alert:
                result = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\" width=\"24\" height=\"24\" class=\"main-grid-item-icon\" fill=\"none\" stroke=\"currentColor\" stroke-linecap=\"round\" stroke-linejoin=\"round\" stroke-width=\"2\">\r\n  <path d=\"M10.29 3.86L1.82 18a2 2 0 0 0 1.71 3h16.94a2 2 0 0 0 1.71-3L13.71 3.86a2 2 0 0 0-3.42 0z\" />\r\n  <line x1=\"12\" x2=\"12\" y1=\"9\" y2=\"13\" />\r\n  <line x1=\"12\" x2=\"12.01\" y1=\"17\" y2=\"17\" />\r\n</svg>";
                break;
            case BoxIconTypes.info:
                result = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\" width=\"24\" height=\"24\" class=\"main-grid-item-icon\" fill=\"none\" stroke=\"currentColor\" stroke-linecap=\"round\" stroke-linejoin=\"round\" stroke-width=\"2\">\r\n  <circle cx=\"12\" cy=\"12\" r=\"10\" />\r\n  <line x1=\"12\" x2=\"12\" y1=\"8\" y2=\"12\" />\r\n  <line x1=\"12\" x2=\"12.01\" y1=\"16\" y2=\"16\" />\r\n</svg>";
                break;
            case BoxIconTypes.command:
                result = "<svg xmlns=\"http://www.w3.org/2000/svg\" viewBox=\"0 0 24 24\" width=\"24\" height=\"24\" class=\"main-grid-item-icon\" fill=\"none\" stroke=\"currentColor\" stroke-linecap=\"round\" stroke-linejoin=\"round\" stroke-width=\"2\">\r\n  <polyline points=\"4 17 10 11 4 5\" />\r\n  <line x1=\"12\" x2=\"20\" y1=\"19\" y2=\"19\" />\r\n</svg>";
                break;
            default:
                result = "<svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"feather feather-home\"><path d=\"M3 9l9-7 9 7v11a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2z\"></path><polyline points=\"9 22 9 12 15 12 15 22\"></polyline></svg>";
                break;
        }
        return result;
    }
}