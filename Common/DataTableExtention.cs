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
            // Parse the JSON string into a JArray
            JArray jsonArray = JArray.Parse(jsonString);

            // Create a new DataTable
            DataTable dataTable = new DataTable();

            // Assume the first object in the array represents the schema
            if (jsonArray.Count > 0)
            {
                JObject firstObject = (JObject)jsonArray[0];

                // Add columns to the DataTable based on the properties of the first object
                foreach (var property in firstObject.Properties())
                {
                    dataTable.Columns.Add(property.Name, typeof(string));
                }

                // Add rows to the DataTable based on the objects in the array
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