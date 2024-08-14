using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System;
using System.Linq;

public static class Util
{
    public static GridBuilder GridFor(Func<object, object> action)
    {
        return new GridBuilder(action);
    }
}

public class GridBuilder
{
    private readonly Func<object, object> _action;
    private int _recordSize;
    private List<ColumnConfig> _columns = new List<ColumnConfig>();

    public GridBuilder(Func<object, object> action)
    {
        _action = action;
    }

    public GridBuilder RecordSize(int recordSize)
    {
        _recordSize = recordSize;
        return this;
    }

    public GridBuilder GenerateColumns<T>(params Func<T, object>[] columnSelectors)
    {
        foreach (var selector in columnSelectors)
        {
            _columns.Add(new ColumnConfig { Selector = selector });
        }
        return this;
    }

    public GridBuilder Column(string columnName, Action<ColumnConfig> configAction)
    {
        var column = _columns.FirstOrDefault(c => c.ColumnName == columnName);
        if (column != null)
        {
            configAction(column);
        }
        return this;
    }

    public MvcHtmlString Build()
    {
      
        var html = new StringBuilder();
        html.Append("<table>");
        // Generate column headers and rows based on _columns and _recordSize
        // This is a simplified example, you would need to implement the actual logic
        html.Append("</table>");
        return MvcHtmlString.Create(html.ToString());
    }
}

public class ColumnConfig
{
    public object Selector { get; set; }
    public string ColumnName { get; set; }
    public bool Visible { get; set; } = true;
    public string Class { get; set; }
    public string Title { get; set; }

    public ColumnConfig Visible1(bool visible)
    {
        Visible = visible;
        return this;
    }

    public ColumnConfig Class1(string className)
    {
        Class = className;
        return this;
    }

    public ColumnConfig Title1(string title)
    {
        Title = title;
        return this;
    }
}