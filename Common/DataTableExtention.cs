using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace G_Wallet_API.Common
{

    public static class DataTableExtention
    {
        public static IEnumerable<T> AsEnumerable<T>(this DataTable dataTable) where T : new()
        {

            var properties = typeof(T).GetProperties()
                                      .Where(p => p.CanWrite);

            foreach (DataRow row in dataTable.Rows)
            {
                var item = new T();

                foreach (var property in properties)
                {
                    if (row.Table.Columns.Contains(property.Name))
                    {
                        var value = row[property.Name];
                        if (value != DBNull.Value)
                        {
                            property.SetValue(item, Convert.ChangeType(value, property.PropertyType));
                        }
                    }
                }

                yield return item;
            }
        }

        public static T AsOne<T>(this DataTable dataTable) where T : new()
        {

            return dataTable.AsEnumerable<T>().FirstOrDefault();

        }

        public static DataTable AsDataTable(this string jsonString)
        {
            JArray jsonArray = JArray.Parse(jsonString);

            DataTable dataTable = new DataTable();

            if (jsonArray.Count > 0)
            {
                JObject firstObject = (JObject)jsonArray[0];

                foreach (var property in firstObject.Properties())
                {
                    dataTable.Columns.Add(property.Name, typeof(string));
                }

                foreach (JObject jsonObject in jsonArray)
                {
                    DataRow row = dataTable.NewRow();
                    foreach (var property in jsonObject.Properties())
                    {
                        row[property.Name] = property.Value.ToString();
                    }
                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }
    }

}