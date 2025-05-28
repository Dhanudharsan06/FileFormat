using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Core;
namespace App;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("----------------------------------------\n" +
            "  File Exporter   - Console Application   "+
            "\n-----------------------gdsg-----------------");


        Console.WriteLine("Select a model to export:\n");
        Console.WriteLine(" 1 -> Employee");
        Console.WriteLine(" 2 -> Product");

        var modelChoice = Console.ReadLine();

        var dataSet = GetDataSet(modelChoice!);


        AssemblyMethodSelector assemble = new();

        
        string dllDirectory = @"C:\Users\DhanudharsanDE\source\repos\FileExporter\ProjectDlls";

        string[] dllPaths = Directory.GetFiles(dllDirectory, "*FileFormat.dll");

        List<Type> allExporterTypes = new();

        foreach (var dllPath in dllPaths)
        {
           
            var exporterTypes = assemble.AssemblySelector<IExporter>(dllPath, "FileFormat"); 
            allExporterTypes.AddRange(exporterTypes);
        }
       
        int loopFlag = 1;

        while (loopFlag == 1)
        {
            int count = 1;

            foreach (var item in allExporterTypes)
            {
                Console.WriteLine($"{count} -> {item.Name}");
                count++;
            }

            Console.WriteLine("Enter the type you wanted (S.No) : ");

            if (int.TryParse(Console.ReadLine(), out int indexOfExporterType)) { }

            else
            {
                Console.WriteLine("invaild input!");
            }

            var exporterInstance = (IExporter)Activator.CreateInstance(allExporterTypes[indexOfExporterType - 1])!;
            string dataString = "";
            if (modelChoice == "1")
            {
                List<Employee> concreteList = dataSet.Cast<Employee>().ToList();
                dataString = exporterInstance.ConvertToString(concreteList);
            }
            else if (modelChoice == "2")
            {
                List<Product> concreteList = dataSet.Cast<Product>().ToList();
                dataString = exporterInstance.ConvertToString(concreteList);
            }

            Console.WriteLine("Enter the name of the file need to be stored : ");

            string fileName = Console.ReadLine() + exporterInstance.GetExtension();

            fileName = Path.Combine(@"C:\Users\DhanudharsanDE\source\repos\Output\", fileName);

            File.WriteAllText(fileName, dataString);

            Console.WriteLine($"{allExporterTypes[indexOfExporterType - 1]} is Successfully stored in {Path.GetFullPath(fileName)}");

            Console.WriteLine("Do you want to convert this Data into another format (Y\\N)");

            string userResponseForRepeatConversion = Console.ReadLine()!;
            if (userResponseForRepeatConversion == "y" || userResponseForRepeatConversion == "Y")
            {
                loopFlag = 1;
            }
            else
            {
                loopFlag = 0;
            }
        }
    }

    public static IEnumerable<IData> GetDataSet(string dataSetType)
    {
        if (dataSetType == "1")
        {
            Employee employee = new();

            Console.WriteLine("No of Employee data to be added : ");

            if (int.TryParse(Console.ReadLine(), out int employeeCount)) { }

            else
            {
                Console.WriteLine("invalid input!");
            }

            return employee.DataList(employeeCount);
        }

        else if (dataSetType == "2")
        {
            Product product = new();

            Console.WriteLine("No of Product data to be added : ");

            if (int.TryParse(Console.ReadLine(), out int productCount)) { }

            else
            {
                Console.WriteLine("invalid input!");
            }

            return product.DataList(productCount); 
        }
        else
        {
            return [];
        }
    }
    public class AssemblyMethodSelector
    {
        public List<Type> AssemblySelector<T>(string dllPath, string targetNamespace)
        {
            Assembly assembly = Assembly.LoadFrom(dllPath);
            Type targetInterface = typeof(T);

            return assembly.GetTypes()
                .Where(type => type.IsClass && type.IsPublic &&
                               type.Namespace == targetNamespace &&
                               targetInterface.IsAssignableFrom(type))
            .ToList();
        }
    }

}

