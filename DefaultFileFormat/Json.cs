using System.Runtime.CompilerServices;
using System.Text.Json;
using Core;
[assembly: InternalsVisibleTo("App")]

namespace FileFormat;

public class Json : IExporter

{
    public string ConvertToString<T>(IEnumerable<T> list)
    {

        var concreteList = list.Cast<object>().ToList();
        return JsonSerializer.Serialize(concreteList, new JsonSerializerOptions
        {
            WriteIndented = true
        });
    }

    public string GetExtension()
    {
        return ".json";
    }
}