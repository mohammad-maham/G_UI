using System.Web.Mvc;

public static class HtmlAttributesExtensions
{
    public static object Merge(this object attributes, object additionalAttributes)
    {
        var dictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(attributes);
        var attr = "";

        if (additionalAttributes != null)
        {
            foreach (var kvp in HtmlHelper.AnonymousObjectToHtmlAttributes(additionalAttributes))
            {
                if (dictionary.ContainsKey(kvp.Key))
                {
                    dictionary[kvp.Key] = kvp.Value;
                }
                else
                {
                    attr += kvp.Key.ToString() + kvp.Value.ToString();
                }
            }
        }
        return attr;
    }
}