using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

public class CustomAttribute : Attribute
{
    public string Name { get; set; }
    public void Write()
    {
        Console.WriteLine("This is Custom Attribute");
    }
}

[Custom]
public class ReflectionClass
{
    [Custom]
    private int _id;
    private string _name;
    private string _content;

    public int ID
    { 
        get => _id; 
        set => _id = value; 
    }
    public string Name
    {
        get => _name;
        set => _name = value;
    }    
    public string Content
    {
        get => _content;
        set => _content = value;
    }

    [Custom]
    public ReflectionClass() { }
    public ReflectionClass(int id)
    {
        _id = id;
    }
    public ReflectionClass(string name, string content)
    {
        _name = name;
        _content = content;
    }
    public ReflectionClass(int id, string name, string content)
    {
        _id = id;
        _name = name;
        _content = content;
    }

    public void Write()
    {
        System.Console.WriteLine($"ID: {_id}");
        System.Console.WriteLine($"Name: {_name}");
    }

    public void Write(string content)
    {
        System.Console.WriteLine($"Content: {content}");
    }

    private void PrintAll()
    {
        System.Console.WriteLine($"ID: {_id}");
        System.Console.WriteLine($"Name: {_name}");
        System.Console.WriteLine($"Content: {_content}");
    }
}

public class ReflectionCsharp
{
    public void Demo01()
    {
        string fullName = string.Empty;
        string assemblyName = string.Empty;
        List<string> constructors = new List<string>();

        Type type;

        type = typeof(int);
        GetData(type, ref fullName, ref assemblyName, ref constructors);
        Print(fullName, assemblyName, constructors);

        // Print:
        // Type.FullName: System.Int32
        // Type.Assembly.FullName: System.Private.CoreLib, Version = 4.0.0.0, Culture = neutral, PublicKeyToken = 7cec85d7bea7798e
        // Type.GetConstructors:

        // Type.FullName: System.Int32 => struct Int32 in namespace System
        // Type.Assembly.FullName: System.Private.CoreLib => Defaul Lib of CSharp
        // Type.GetConstructors: => struct hasn't Constructor

        double i = 100d;
        type = i.GetType();
        GetData(type, ref fullName, ref assemblyName, ref constructors);
        Print(fullName, assemblyName, constructors);

        // Print:
        // Type.FullName: System.Double
        // Type.Assembly.FullName: System.Private.CoreLib, Version = 4.0.0.0, Culture = neutral, PublicKeyToken = 7cec85d7bea7798e
        // Type.GetConstructors:

        // Type.FullName: System.Double => struct Double in namespace System
        // Type.Assembly.FullName: System.Private.CoreLib => Defaul Lib of CSharp
        // Type.GetConstructors: => struct hasn't Constructor

        ReflectionClass reflectionInfo = new ReflectionClass("Name", "Value");
        type = reflectionInfo.GetType();
        GetData(type, ref fullName, ref assemblyName, ref constructors);
        Print(fullName, assemblyName, constructors);

        // Print:
        // Type.FullName: ReflectionClass
        // Type.Assembly.FullName: ConsoleApp, Version = 1.0.0.0, Culture = neutral, PublicKeyToken = null
        // Type.GetConstructors: Void.ctor(), Void.ctor(Int32), Void.ctor(System.String, System.String), Void.ctor(Int32, System.String, System.String)

        // Type.FullName: ReflectionClass => class ReflectionClass
        // Type.Assembly.FullName: ConsoleApp => ConsoleApp.exe - File build
        // Type.GetConstructors: => There are 4 contructors

        // Giải thích:
        // - Với những dòng có chuỗi Type.FullName ở đầu dòng: thể hiện thông tin tên class của loại đối tượng(bao gồm namespace).
        // - Với những dòng có chuỗi Type.Assembly.FullName ở đầu dòng: thể hiện thông tin assembly (tên file dll hoặc exe), phiên bản...
        // - Với những dòng có chuỗi Type.GetConstructors ở đầu dòng: thể hiện danh sách các constructor của class
    }
    private void Print(string fullName, string assemblyName, List<string> constructors)
    {
        System.Console.WriteLine("Type.FullName: " + fullName);
        System.Console.WriteLine("Type.Assembly.FullName: " + assemblyName);
        System.Console.WriteLine("Type.GetConstructors: " + string.Join(", ", constructors));
        System.Console.WriteLine("_________________________________________________________");
    }
    private void GetData(Type type, ref string fullName, ref string assemblyName, ref List<string> constructors)
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

    public void Demo02()
    {
        ReflectionClass reflectionClass = new ReflectionClass();
        Type type = reflectionClass.GetType();

        Console.WriteLine("_____ATTRIBUTES_____");

        TypeAttributes attributes = type.Attributes;
        System.Console.WriteLine("Class.Attribute: " + attributes);

        IEnumerable<CustomAttributeData> customAttributes = type.CustomAttributes;
        System.Console.WriteLine("Class.Custom.Attribute: " + customAttributes);

        // Print:
        // Class.Attribute: AutoLayout, AnsiClass, Class, Public, BeforeFieldInit
        // Class.Custom.Attribute: System.Collections.ObjectModel.ReadOnlyCollection`1[System.Reflection.CustomAttributeData]

        // Line 1: Default Attributes of CSharp
        // Line 2: User Defined's Attributes

        Console.WriteLine("");

        Console.WriteLine("_____CONTRUCTORS_____");

        var constructors = type.GetConstructors();
        System.Console.WriteLine(constructors.Length);
        foreach (var item in constructors) Console.WriteLine(item);

        // Print:
        // 4
        // Void.ctor()
        // Void.ctor(Int32)
        // Void.ctor(System.String, System.String)
        // Void.ctor(Int32, System.String, System.String)

        // => There are 4 Constructors
        // => Know Type of Param of each Contructor

        Console.WriteLine("");

        Console.WriteLine("_____METHODS_____");

        var methods = type.GetMethods();
        Console.WriteLine($"Public method: {methods.Length}");
        foreach (var item in methods) Console.WriteLine(item);

        // Print:
        // Public method: 12
        // Int32 get_ID()
        // Void set_ID(Int32)
        // System.String get_Name()
        // Void set_Name(System.String)
        // System.String get_Content()
        // Void set_Content(System.String)
        // Void Write()
        // Void Write(System.String)
        // System.Type GetType()
        // System.String ToString()
        // Boolean Equals(System.Object)
        // Int32 GetHashCode()

        // Bên cạnh xác định các Public Methods chúng ta có thể xác định các Private - Protected Methods thông qua hàm GetMethods() với tham số BindingFlags:
        methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
        Console.WriteLine($"NonPublic method: {methods.Length}");
        foreach (var item in methods) Console.WriteLine(item);

        // Print:
        // NonPublic method: 3
        // Void PrintAll()
        // System.Object MemberwiseClone()
        // Void Finalize()

        Console.WriteLine("");

        Console.WriteLine("_____PROPERTY_INFOS_____");

        var props = type.GetProperties();
        foreach (var item in props) Console.WriteLine(item);

        // Print:
        // Int32 ID
        // System.String Name
        // System.String Content

        // Ở đây cũng có thể sử dụng BindingFlags Để lấy các PropertyInfo

        Console.WriteLine("");

        Console.WriteLine("_____FIELD_INFOS_____");

        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
        foreach (var item in fields) Console.WriteLine(item);

        // Print:
        // Int32 _id
        // System.String _name
        // System.String _content

        Console.WriteLine("");
    }
}