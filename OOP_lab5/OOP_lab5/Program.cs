using System;
using System.Collections.Generic;
using System.Linq;


public enum SoftwareCategory
{
    Security,
    TextEditor,
    Virus,
    Toy,
    Developer
}


public struct LicenseInfo
{
    public string LicenseKey { get; set; }
    public DateTime ExpiryDate { get; set; }

    public override string ToString()
    {
        return $"Ключ лицензии: {LicenseKey}, Срок действия: {ExpiryDate.ToShortDateString()}";
    }
}


public abstract class BaseSoftware
{
    public string Name { get; set; }

    protected BaseSoftware(string name)
    {
        Name = name;
    }

    public abstract string GetDescription();
}


public partial class Software : BaseSoftware, ICloneable
{
    public SoftwareCategory Category { get; set; }

    public LicenseInfo License { get; set; }

    public Software(string name, SoftwareCategory category) : base(name)
    {
        Category = category;
        License = new LicenseInfo { LicenseKey = "XXXX-XXXX", ExpiryDate = DateTime.Now.AddYears(1) };
    }

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}


public partial class Software
{
    public override string GetDescription()
    {
        return $"Программное обеспечение: {Name}, Категория: {Category}, {License}";
    }

    public override string ToString()
    {
        return GetDescription();
    }
}


public class OperationSet : Software
{
    public string OperationType { get; set; }

    public OperationSet(string name, string operationType) : base(name, SoftwareCategory.Security)
    {
        OperationType = operationType;
    }

    public override string GetDescription()
    {
        return $"Набор операций: {Name}, Тип операции: {OperationType}, {License}";
    }
}

public class TextProcessor : Software
{
    public string Format { get; set; }

    public TextProcessor(string name, string format) : base(name, SoftwareCategory.TextEditor)
    {
        Format = format;
    }

    public override string GetDescription()
    {
        return $"Текстовый процессор: {Name}, Формат: {Format}, {License}";
    }
}

public class Word : TextProcessor
{
    public Word(string name) : base(name, "DOCX") { }

    public override string ToString()
    {
        return $"Word - Имя: {Name}, Формат: DOCX";
    }
}

public class Virus : Software
{
    public string VirusType { get; set; }

    public Virus(string name, string virusType) : base(name, SoftwareCategory.Virus)
    {
        VirusType = virusType;
    }

    public override string GetDescription()
    {
        return $"Вирус: {Name}, Тип вируса: {VirusType}, {License}";
    }
}

public class CConficker : Virus
{
    public CConficker(string name) : base(name, "Conficker") { }

    public override string ToString()
    {
        return $"CConficker - Имя: {Name}, Тип вируса: Conficker";
    }
}

public class Toy : Software
{
    public string ToyType { get; set; }

    public Toy(string name, string toyType) : base(name, SoftwareCategory.Toy)
    {
        ToyType = toyType;
    }

    public override string GetDescription()
    {
        return $"Игрушка: {Name}, Тип игрушки: {ToyType}, {License}";
    }
}

public class Minesweeper : Toy
{
    public Minesweeper(string name) : base(name, "Головоломка") { }

    public override string ToString()
    {
        return $"Сапер - Имя: {Name}, Тип игрушки: Головоломка";
    }
}

public class Developer : Software
{
    public string Language { get; set; }

    public Developer(string name, string language) : base(name, SoftwareCategory.Developer)
    {
        Language = language;
    }

    public override string GetDescription()
    {
        return $"Разработчик: {Name}, Язык: {Language}, {License}";
    }
}


public class SoftwareContainer
{
    private List<BaseSoftware> softwareList = new();

    public void Add(BaseSoftware software)
    {
        softwareList.Add(software);
    }

    public void Remove(BaseSoftware software)
    {
        softwareList.Remove(software);
    }

    public BaseSoftware Get(int index)
    {
        return softwareList[index];
    }

    public List<BaseSoftware> GetAll()
    {
        return softwareList;
    }

    public void DisplayAll()
    {
        foreach (var software in softwareList)
        {
            Console.WriteLine(software);
        }
    }
}


public class SoftwareController
{
    private SoftwareContainer container;

    public SoftwareController(SoftwareContainer container)
    {
        this.container = container;
    }

    public List<BaseSoftware> FindToysByType(string toyType)
    {
        return container.GetAll().Where(s => s is Toy toy && toy.ToyType == toyType).ToList();
    }

    public List<BaseSoftware> FindTextEditorsByFormat(string format)
    {
        return container.GetAll().Where(s => s is TextProcessor editor && editor.Format == format).ToList();
    }

    public List<BaseSoftware> GetSoftwareSortedByName()
    {
        return container.GetAll().OrderBy(s => s.Name).ToList();
    }
}


class Program
{
    static void Main(string[] args)
    {
        
        SoftwareContainer container = new();
        container.Add(new Word("Microsoft Word"));
        container.Add(new Toy("Сапер", "Головоломка"));
        container.Add(new TextProcessor("Notepad", "TXT"));
        container.Add(new Virus("Conficker", "Conficker"));
        container.Add(new Developer("Джон Доу", "C#"));

        
        SoftwareController controller = new(container);

        Console.WriteLine("Все программное обеспечение:");
        container.DisplayAll();

        Console.WriteLine("\nИгрушки типа 'Головоломка':");
        var toys = controller.FindToysByType("Головоломка");
        toys.ForEach(Console.WriteLine);

        Console.WriteLine("\nТекстовые редакторы формата 'TXT':");
        var editors = controller.FindTextEditorsByFormat("TXT");
        editors.ForEach(Console.WriteLine);

        Console.WriteLine("\nПрограммное обеспечение в алфавитном порядке:");
        var sortedSoftware = controller.GetSoftwareSortedByName();
        sortedSoftware.ForEach(Console.WriteLine);
    }
}
