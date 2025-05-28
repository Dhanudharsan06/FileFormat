using Core;
using System.Xml.Serialization;
namespace FileFormat;

public class Xml : IExporter
{
    public string ConvertToString<T>(IEnumerable<T> list)
    {
        if (list == null || !list.Any())
            return string.Empty;

        // Convert to List<T>
        var typedList = list.ToList();

        // Get the runtime type of T
        Type itemType = typedList.First()!.GetType();

        // Create an XmlSerializer for List<T>
        var serializer = new XmlSerializer(typeof(List<>).MakeGenericType(itemType));

        using StringWriter writer = new StringWriter();
        serializer.Serialize(writer, typedList);
        return writer.ToString();

    }



    private static object CastList(List<object> list, Type targetType)
    {
        // Create List<targetType> instance
        var genericListType = typeof(List<>).MakeGenericType(targetType);
        var typedList = Activator.CreateInstance(genericListType) as System.Collections.IList;

        // Add each item casted to targetType
        foreach (var item in list)
            typedList!.Add(item);

        return typedList!;
    }



    public string GetExtension()
    {
        return ".xml";
    }
}