using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

public static class Reflector
{
    // a. Определение имени сборки
    public static string GetAssemblyName(string className)
    {
        Type type = Type.GetType(className);
        if (type == null)
            throw new ArgumentException("Класс не найден.");

        return type.Assembly.FullName;
    }

    // b. Проверка наличия публичных конструкторов
    public static bool HasPublicConstructors(string className)
    {
        Type type = Type.GetType(className);
        if (type == null)
            throw new ArgumentException("Класс не найден.");

        return type.GetConstructors().Any();
    }

    // c. Извлечение всех публичных методов
    public static IEnumerable<string> GetPublicMethods(string className)
    {
        Type type = Type.GetType(className);
        if (type == null)
            throw new ArgumentException("Класс не найден.");

        return type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                   .Select(m => m.Name);
    }

    // d. Получение информации о полях и свойствах
    public static IEnumerable<string> GetFieldsAndProperties(string className)
    {
        Type type = Type.GetType(className);
        if (type == null)
            throw new ArgumentException("Класс не найден.");

        var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                         .Select(f => "Поле: " + f.Name);

        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                             .Select(p => "Свойство: " + p.Name);

        return fields.Concat(properties);
    }

    // e. Получение всех интерфейсов
    public static IEnumerable<string> GetImplementedInterfaces(string className)
    {
        Type type = Type.GetType(className);
        if (type == null)
            throw new ArgumentException("Класс не найден.");

        return type.GetInterfaces().Select(i => i.Name);
    }

    // f. Методы с заданным типом параметра
    public static IEnumerable<string> GetMethodsWithParameterType(string className, Type parameterType)
    {
        Type type = Type.GetType(className);
        if (type == null)
            throw new ArgumentException("Класс не найден.");

        return type.GetMethods()
                   .Where(m => m.GetParameters().Any(p => p.ParameterType == parameterType))
                   .Select(m => m.Name);
    }

    // g. Вызов метода
    public static object Invoke(string className, string methodName, object obj, object[] parameters)
    {
        Type type = Type.GetType(className);
        if (type == null)
            throw new ArgumentException("Класс не найден.");

        MethodInfo method = type.GetMethod(methodName);
        if (method == null)
            throw new ArgumentException("Метод не найден.");

        return method.Invoke(obj, parameters);
    }

    public static object InvokeFromFile(string className, string methodName, string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        var parameters = lines.Select(line => Convert.ChangeType(line, typeof(object))).ToArray();

        Type type = Type.GetType(className);
        if (type == null)
            throw new ArgumentException("Класс не найден.");

        MethodInfo method = type.GetMethod(methodName);
        if (method == null)
            throw new ArgumentException("Метод не найден.");

        var obj = Activator.CreateInstance(type);
        return method.Invoke(obj, parameters);
    }

    // Обобщенный метод Create для создания объектов
    public static T Create<T>() where T : class
    {
        Type type = typeof(T);
        ConstructorInfo constructor = type.GetConstructors().FirstOrDefault();

        if (constructor == null)
            throw new InvalidOperationException("Нет доступных публичных конструкторов.");

        var parameters = constructor.GetParameters()
                                    .Select(p => Activator.CreateInstance(p.ParameterType))
                                    .ToArray();

        return (T)constructor.Invoke(parameters);
    }

    // Сохранение информации в файл
    public static void SaveToFile(string fileName, string content)
    {
        File.WriteAllText(fileName, content);
    }
}

// Демонстрация работы Reflector
public class TestClass
{
    public int Number { get; set; }
    public string Name { get; set; }

    public TestClass() { }
    public TestClass(int number, string name)
    {
        Number = number;
        Name = name;
    }

    public void Print(string message)
    {
        Console.WriteLine($"Сообщение: {message}");
    }

    public void AddNumbers(int a, int b)
    {
        Console.WriteLine($"Сумма: {a + b}");
    }
}

// Использование Reflector
class Program
{
    static void Main()
    {
        string className = "TestClass";

        // Использование методов Reflector
        Console.WriteLine("Имя сборки: " + Reflector.GetAssemblyName(className));
        Console.WriteLine("Есть публичные конструкторы: " + Reflector.HasPublicConstructors(className));

        var methods = Reflector.GetPublicMethods(className);
        Console.WriteLine("Публичные методы: " + string.Join(", ", methods));

        var fieldsAndProperties = Reflector.GetFieldsAndProperties(className);
        Console.WriteLine("Поля и свойства: " + string.Join(", ", fieldsAndProperties));

        var interfaces = Reflector.GetImplementedInterfaces(className);
        Console.WriteLine("Реализованные интерфейсы: " + string.Join(", ", interfaces));

        var methodsWithParamType = Reflector.GetMethodsWithParameterType(className, typeof(string));
        Console.WriteLine("Методы с параметром типа string: " + string.Join(", ", methodsWithParamType));

        // Пример вызова метода
        TestClass obj = new TestClass();
        Reflector.Invoke(className, "Print", obj, new object[] { "Привет, мир!" });

        // Создание объекта через Reflector
        var newObject = Reflector.Create<TestClass>();
        Console.WriteLine("Объект создан: " + (newObject != null));
    }
}
