using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Reflection;

namespace MyWebApp.Utils
{
    public class UriParamsParser
    {
        public static string ParsFromeObjectFields(object obj)
        {
            string result = "";
            PropertyInfo[] properties = obj.GetType().GetProperties();
            List<ClassProperty> objProperties = new List<ClassProperty>();
            foreach (PropertyInfo property in properties)
            {
                var value = property.GetValue(obj);
                if (value != null)
                {
                    objProperties.Add(new ClassProperty() { Name = property.Name, Value = value });
                }
            }
            if (objProperties.Count > 0)
            {
                result += "?";

                foreach (var property in objProperties)
                {
                    if (property.isEnumerable())
                    {
                        foreach(var propertyItem in property.Value as IEnumerable)
                        {
                            result += $"{property.Name}={propertyItem}&";
                        } 
                    }
                    else
                    {
                        result += $"{property.Name}={property.Value}&";
                    }
                }
                result = result.TrimEnd('&');
            }
            return result;
        }
    }

    public class ClassProperty
    {
        public object? Value { get; set; }
        public string Name { get; set; }

        public bool isEnumerable()
        {
            IEnumerable enumerable = this.Value as IEnumerable;
            if (enumerable != null && this.Value is not string) return true;
            return false;
        }
    }
}
