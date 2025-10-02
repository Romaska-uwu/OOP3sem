using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using System.Xml;
using System.Xml.Serialization;
public interface ISerializer
{
    void Serialize<T>(T obj, string path);
    T Deserialize<T>(string path);
}
public class BinarySerializer : ISerializer
{
    public void Serialize<T>(T obj, string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            formatter.Serialize(fs, obj);
        }
    }

    public T Deserialize<T>(string path)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            return (T)formatter.Deserialize(fs);
        }
    }
}
public class SoapSerializer : ISerializer
{
    public void Serialize<T>(T obj, string path)
    {
        SoapFormatter formatter = new SoapFormatter();

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            formatter.Serialize(fs, obj);
        }
    }

    public T Deserialize<T>(string path)
    {
        SoapFormatter formatter = new SoapFormatter();

        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            return (T)formatter.Deserialize(fs);
        }
    }
}

public class JsonCustomSerializer : ISerializer
{
    public void Serialize<T>(T obj, string path)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        string json = System.Text.Json.JsonSerializer.Serialize(obj, options);

        File.WriteAllText(path, json);
    }

    public T Deserialize<T>(string path)
    {
        string json = File.ReadAllText(path);

        return System.Text.Json.JsonSerializer.Deserialize<T>(json);
    }
}

public class XmlCustomSerializer : ISerializer
{
    public void Serialize<T>(T obj, string path)
    {
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

        using (FileStream fs = new FileStream(path, FileMode.Create))
        {
            serializer.Serialize(fs, obj);
        }
    }

    public T Deserialize<T>(string path)
    {
        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            return (T)serializer.Deserialize(fs);
        }
    }
}

[Serializable]
public class Engine
{
    public int HorsePower { get; set; }

    public Engine() { }

    public Engine(int horsePower)
    {
        HorsePower = horsePower;
    }

    public override string ToString()
    {
        return $"{HorsePower} лошадок";
    }
}

[Serializable]
public class CarControl
{
    public void Accelerate()
    {
        Console.WriteLine("Ускоряемся");
    }

    public void Brake()
    {
        Console.WriteLine("Тормозим");
    }

    public override string ToString()
    {
        return "Контроль управления";
    }
}

[Serializable]
public abstract class AbstractVehicle
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public Engine Engine { get; set; }

    protected AbstractVehicle() { }

    protected AbstractVehicle(string brand, string model, Engine engine)
    {
        Brand = brand;
        Model = model;
        Engine = engine;
    }

    public abstract void Start();
    public abstract void Stop();
    public abstract bool DoDrive();

    public override string ToString()
    {
        return $"{GetType().Name}: {Brand} {Model}, {Engine}";
    }
}

[Serializable]
public sealed class Car : AbstractVehicle
{
    [NonSerialized]
    private CarControl control;

    [JsonIgnore]
    [XmlIgnore]
    public CarControl Control
    {
        get { return control; }
        set { control = value; }
    }

    public Car() { }

    public Car(string brand, string model, Engine engine) : base(brand, model, engine)
    {
        Control = new CarControl();
    }

    public override void Start()
    {
        Console.WriteLine($"{Brand} {Model} стартует");
        Control?.Accelerate();
    }

    public override void Stop()
    {
        Control?.Brake();
        Console.WriteLine($"{Brand} {Model} остановлен");
    }

    public override bool DoDrive()
    {
        Console.WriteLine($"{Brand} {Model} водим");
        return true;
    }

    public override string ToString()
    {
        string controlStatus = Control != null ? Control.ToString() : "Control не сериализован";
        return base.ToString() + $", {controlStatus}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // 1
        Car car = new Car("Toyota", "Corolla", new Engine(150));
        car.Control = new CarControl();

        ISerializer binarySerializer = new BinarySerializer();
        ISerializer soapSerializer = new SoapSerializer();
        ISerializer jsonSerializer = new JsonCustomSerializer();
        ISerializer xmlSerializer = new XmlCustomSerializer();

        string binaryPath = "car.dat";
        string soapPath = "car.soap";
        string jsonPath = "car.json";
        string xmlPath = "car.xml";

        Console.WriteLine("BINARY");
        binarySerializer.Serialize(car, binaryPath);
        var binaryDeserialized = binarySerializer.Deserialize<Car>(binaryPath);
        Console.WriteLine("После десериализации:");
        Console.WriteLine(binaryDeserialized);
        Console.WriteLine();

        Console.WriteLine("SOAP");
        soapSerializer.Serialize(car, soapPath);
        var soapDeserialized = soapSerializer.Deserialize<Car>(soapPath);
        Console.WriteLine("После десериализации:");
        Console.WriteLine(soapDeserialized);
        Console.WriteLine();

        Console.WriteLine("JSON");
        jsonSerializer.Serialize(car, jsonPath);
        var jsonDeserialized = jsonSerializer.Deserialize<Car>(jsonPath);
        Console.WriteLine("После десериализации:");
        Console.WriteLine(jsonDeserialized);
        Console.WriteLine();

        Console.WriteLine("XML");
        xmlSerializer.Serialize(car, xmlPath);
        var xmlDeserialized = xmlSerializer.Deserialize<Car>(xmlPath);
        Console.WriteLine("После десериализации:");
        Console.WriteLine(xmlDeserialized);
        Console.WriteLine();
        // 2 
        var cars = new List<Car>
        {
            new Car("Toyota", "Corolla", new Engine(150)),
            new Car("Honda", "Civic", new Engine(140)),
            new Car("Ford", "Focus", new Engine(160))
        };

        string xmlPath2 = "cars.xml";
        var xmlSerializer2 = new XmlCustomSerializer();

        Console.WriteLine("Сериализация массива объектов в XML:");
        xmlSerializer2.Serialize(cars, xmlPath2);
        var deserializedCars = xmlSerializer2.Deserialize<List<Car>>(xmlPath2);

        Console.WriteLine("После десериализации:");
        foreach (var carS in deserializedCars)
        {
            Console.WriteLine(carS);
        }
        Console.WriteLine();

        // 3
        Console.WriteLine("XPath-запросы:");
        XmlDocument document = new XmlDocument();
        document.Load(xmlPath2);

        XmlNodeList carNames = document.SelectNodes("/ArrayOfCar/Car/Brand");
        Console.WriteLine("Названия машин:");
        foreach (XmlNode name in carNames)
        {
            Console.WriteLine(name.InnerText);
        }

        XmlNodeList powerfulCars = document.SelectNodes("/ArrayOfCar/Car[Engine/HorsePower > 145]");
        Console.WriteLine("Машины с мощностью двигателя > 145:");
        foreach (XmlNode carNode in powerfulCars)
        {
            Console.WriteLine(carNode.SelectSingleNode("Brand").InnerText);
        }
        Console.WriteLine();

        // 4
        Console.WriteLine("LINQ to XML:");
        XDocument xDoc = XDocument.Load(xmlPath2);

        Console.WriteLine("Названия машин:");
        var carBrands = xDoc.Descendants("Car").Select(c => c.Element("Brand").Value);
        foreach (var brand in carBrands)
        {
            Console.WriteLine(brand);
        }

        Console.WriteLine("Мощности двигателей:");
        var enginePowers = xDoc.Descendants("Engine").Select(e => e.Element("HorsePower").Value);
        foreach (var power in enginePowers)
        {
            Console.WriteLine(power);
        }

        Console.WriteLine("Создание нового XML-документа:");
        var newDoc = new XDocument(
            new XElement("Vehicles",
                cars.Select(car =>
                    new XElement("Car",
                        new XElement("Brand", car.Brand),
                        new XElement("Model", car.Model),
                        new XElement("HorsePower", car.Engine.HorsePower)
                    )
                )
            )
        );
        string newXmlPath = "new_cars.xml";
        newDoc.Save(newXmlPath);
        Console.WriteLine($"Новый XML сохранён: {newXmlPath}");

        Console.WriteLine("Машины из нового XML с мощностью > 150:");
        var powerfulCarsInNewXml = newDoc.Descendants("Car")
            .Where(c => int.Parse(c.Element("HorsePower").Value) > 150)
            .Select(c => c.Element("Brand").Value);
        foreach (var carSS in powerfulCarsInNewXml)
        {
            Console.WriteLine(carSS);
        }
    }
}