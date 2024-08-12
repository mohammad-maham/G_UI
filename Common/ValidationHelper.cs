using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

public static class ValidationHelper
{

    public static string Validate(System.Web.Mvc.ModelStateDictionary modelState)
    {

        foreach (var value in modelState.Values)
        {
            if(value != null)
                if(value.Errors.Count>0)
                    return value.Errors.First().ErrorMessage;
        }


        return "";
    }
}


