using System.Xml.Serialization;

namespace Core;

[XmlInclude(typeof(Employee))]
public class Employee : IData
{
    private int IdGen = 1;
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Department { get; set; }
    public Employee(int Id, string Name, string Department)
    {
        this.Id = Id;
        this.Name = Name;
        this.Department = Department;
    }
    public Employee()
    {

    }

    public List<Employee> DataList(int total)
    {
        var list = new List<Employee>();

        for (int i = 0; i < total; i++)
        {
            Console.Write("Name : ");
            string name = Console.ReadLine()!;

            Console.Write("Department : ");
            string department = Console.ReadLine()!;

            list.Add(new Employee(IdGen, name!, department));
            Console.WriteLine("--------------------");
            IdGen++;
        }
        return list;
    }
}

public interface IData { }

