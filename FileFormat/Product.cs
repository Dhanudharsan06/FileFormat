using System.Xml.Serialization;

namespace Core;

[XmlInclude(typeof(Product))]

public class Product : IData
{
    private int IdGen = 1;
    public int? Id { get; set; }
    public string? Name { get; set; }
    public double? Price { get; set; }
    public Product(int Id, string Name, double Price)
    {
        this.Id = Id;
        this.Name = Name;
        this.Price = Price;
    }
    public Product() { }

    public List<Product> DataList(int total)
    {
        var list = new List<Product>();

        for (int i = 0; i < total; i++)
        {

            int id = IdGen;

            Console.Write("Name : ");
            string name = Console.ReadLine()!;

            Console.Write("Price : ");
            double price = double.Parse(Console.ReadLine()!);

            list.Add(new Product(IdGen, name, price));
            Console.WriteLine("--------------------");
            IdGen++;
        }
        return list;
    }
}
