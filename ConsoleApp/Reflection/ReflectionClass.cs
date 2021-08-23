using System;
using System.Linq;
using System.Collections.Generic;

public class ReflectionClass
{
    public int ID{ get; set; }
    public string Name { get; set; }
    public string Content { get; set; }

    public ReflectionClass() { }
    public ReflectionClass(int id)
    {
        ID = id;
    }
    public ReflectionClass(string name, string content)
    {
        Name = name;
        Content = content;
    }
    public ReflectionClass(int id, string name, string content)
    {
        ID = id;
        Name = name;
        Content = content;
    }
}

public class ReflectionCsharp
{
    public void Run()
    {
        string fullName = string.Empty;
        string assemblyName = string.Empty;
        List<string> constructors = new List<string>();

        Type type;

        type = typeof(int);
        GetMetadata(type, ref fullName, ref assemblyName, ref constructors);
        Print(fullName, assemblyName, constructors);

        double i = 100d;
        type = i.GetType();
        GetMetadata(type, ref fullName, ref assemblyName, ref constructors);
        Print(fullName, assemblyName, constructors);

        ReflectionClass reflectionInfo = new ReflectionClass("Name", "Value");
        type = reflectionInfo.GetType();
        GetMetadata(type, ref fullName, ref assemblyName, ref constructors);
        Print(fullName, assemblyName, constructors);
    }

    public void Print(string fullName, string assemblyName, List<string> constructors)
    {
        System.Console.WriteLine("Type.FullName: " + fullName);
        System.Console.WriteLine("Type.Assembly.FullName: " + assemblyName);
        System.Console.WriteLine("Type.GetConstructors: " + string.Join(", ", constructors));
        System.Console.WriteLine("_________________________________________________________");
    }

    public void GetMetadata(Type type, ref string fullName, ref string assemblyName, ref List<string> constructors)
    {
        fullName = type.FullName;
        assemblyName = type.Assembly.FullName;
        constructors.Clear();
        var listConstructors = type.GetConstructors().ToList();
        foreach (var item in listConstructors)
        {
            constructors.Add(item.ToString());
        }
    }
}