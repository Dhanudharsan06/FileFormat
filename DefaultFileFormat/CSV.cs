using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace FileFormat;

public class Csv : IExporter
{
    public string ConvertToString<T>(IEnumerable<T> list) /*where T:new()*/
    {
        if (list.FirstOrDefault() is null)
            return string.Empty;

        var type = list.First()!.GetType();

        var properties = type.GetProperties();
        var p = typeof(T).GetProperties();
        // Header
        var header = string.Join(",", properties.Select(p => p.Name));

        // Rows
        var rows = list.Cast<object>().Select(item =>
        {
            return string.Join(",", properties.Select(p =>
            {
                var value = p.GetValue(item)?.ToString() ?? "";
                // Escape commas, quotes, and newlines
                value = value.Replace("\"", "\"\"");
                if (value.Contains(",") || value.Contains("\n"))
                    value = $"\"{value}\"";
                return value;
            }));
        });

        return string.Join("\n", new[] { header }.Concat(rows));

    }

    public string GetExtension()
    {
        return ".csv";
    }
}