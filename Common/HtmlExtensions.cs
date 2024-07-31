using System.IO;
using System.Web.Mvc;
using System;
using System.Collections.Generic;

public static class HtmlExtensions
{
    public static IDisposable Box(this HtmlHelper htmlHelper, string title = "", string cssClass = "", Dictionary<string, string> attributes = null)
    {
        var tagBuilder = new TagBuilder("div");

        tagBuilder.MergeAttributes(attributes);
        tagBuilder.AddCssClass($"{cssClass}");

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
}