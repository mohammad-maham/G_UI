using System.IO;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Web.Services.Description;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

public static class HtmlExtensions
{
    public static IDisposable Box(this HtmlHelper htmlHelper, string title = "", int col = 12, string cssClass = "", Dictionary<string, string> attributes = null)
    {
        var tagBuilder = new TagBuilder("div");

        tagBuilder.MergeAttributes(attributes);

        var _cssClass = $"statbox widget box box-shadow col-xl-{col} col-md-{col} col-sm-12 col-12 overflow-hidden";
        tagBuilder.AddCssClass($"{cssClass}");
        tagBuilder.AddCssClass(_cssClass);

        htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));

        // Write the title
        var titleTagBuilder = new TagBuilder("div");
        titleTagBuilder.AddCssClass("widget-header");
        var header = $@"
                        <div class=""row"">
                            <div class=""col-xl-12 col-md-12 col-sm-12 col-12"">
                                <h4>{title}</h4>
                            </div>
                        </div>";


        if (!string.IsNullOrEmpty(title))
            titleTagBuilder.InnerHtml = header;

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

        var sb = new StringBuilder();
        sb.Append("<table class=\"datatable table table-hover non-hover table-striped\" style=\"width:100%\">");
        sb.Append("<thead><tr class=\"gra\">");
        foreach (var selector in columnSelectors)
        {
            var memberExpression = GetMemberExpression(selector);
            var displayName = memberExpression.Member.GetCustomAttribute<DisplayAttribute>()?.Name ?? memberExpression.Member.Name;
            sb.AppendFormat("<th>{0}</th>", displayName);

        }
        sb.Append("</tr></thead>");

        // Generate table rows
        sb.Append("<tbody>");
        foreach (var item in list)
        {
            sb.Append("<tr>");
            foreach (var selector in columnSelectors)
            {
                var value = selector.Compile()(item);
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
}