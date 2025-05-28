namespace Core;

public interface IExporter
{
    string ConvertToString<T>(IEnumerable<T> list);

    public string GetExtension();
}



