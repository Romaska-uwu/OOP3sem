using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

// Перечисление для категорий ПО
public enum SoftwareCategory
{
    Security,
    TextEditor,
    Virus,
    Toy,
    Developer
}

// Структура для информации о лицензии
public struct LicenseInfo
{
    public string LicenseKey { get; set; }
    public DateTime ExpiryDate { get; set; }

    public override string ToString()
    {
        return $"Ключ лицензии: {LicenseKey}, Срок действия: {ExpiryDate.ToShortDateString()}";
    }
}

// Пользовательские исключения
public class InvalidSoftwareNameException : Exception
{
    public InvalidSoftwareNameException(string message) : base(message) { }
}

public class InvalidLicenseException : Exception
{
    public InvalidLicenseException(string message) : base(message) { }
}

public class OperationNotSupportedException : Exception
{
    public OperationNotSupportedException(string message) : base(message) { }
}

// Абстрактный базовый класс
public abstract class BaseSoftware
{
    public string Name { get; set; }

    protected BaseSoftware(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidSoftwareNameException("Имя программного обеспечения не может быть пустым или содержать только пробелы.");

        Name = name;
    }

    public abstract string GetDescription();
}

// Partial-класс Software (Часть 1)
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

// Partial-класс Software (Часть 2)
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

// Производные классы
public class OperationSet : Software
{
    public string OperationType { get; set; }

    public OperationSet(string name, string operationType) : base(name, SoftwareCategory.Security)
    {
        if (string.IsNullOrWhiteSpace(operationType))
            throw new OperationNotSupportedException("Тип операции не может быть пустым.");

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
        if (string.IsNullOrWhiteSpace(format))
            throw new InvalidSoftwareNameException("Формат текстового процессора не может быть пустым.");

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
        if (string.IsNullOrWhiteSpace(virusType))
            throw new InvalidSoftwareNameException("Тип вируса не может быть пустым.");

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
        if (string.IsNullOrWhiteSpace(toyType))
            throw new InvalidSoftwareNameException("Тип игрушки не может быть пустым.");

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
        if (string.IsNullOrWhiteSpace(language))
            throw new InvalidSoftwareNameException("Язык разработки не может быть пустым.");

        Language = language;
    }

    public override string GetDescription()
    {
        return $"Разработчик: {Name}, Язык: {Language}, {License}";
    }
}

// Класс-Контейнер
public class SoftwareContainer
{
    private List<BaseSoftware> softwareList = new();

    public void Add(BaseSoftware software)
    {
        if (software == null)
            throw new ArgumentNullException(nameof(software), "Объект программного обеспечения не может быть null.");

        softwareList.Add(software);
    }

    public void Remove(BaseSoftware software)
    {
        if (!softwareList.Remove(software))
            throw new KeyNotFoundException("Объект программного обеспечения не найден в контейнере.");
    }

    public BaseSoftware Get(int index)
    {
        if (index < 0 || index >= softwareList.Count)
            throw new IndexOutOfRangeException("Индекс вне диапазона контейнера.");

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

// Класс-Контроллер
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

// Точка входа
class Program
{
    static void Main(string[] args)
    {
        try
        {
            SoftwareContainer container = new();

            // Создание объектов с обработкой исключений
            try
            {
                container.Add(new Word("Microsoft Word"));
                container.Add(new Toy("Сапер", "Головоломка"));
                container.Add(new TextProcessor("Notepad", "TXT"));
                container.Add(new Virus("Conficker", "Conficker"));
                container.Add(new Developer("Джон Доу", "C#"));

                // Пример исключения ArgumentNullException
                container.Add(null); // Это вызовет ArgumentNullException
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
            }
            catch (InvalidSoftwareNameException ex)
            {
                Console.WriteLine($"Исключение: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Неожиданное исключение: {ex.Message}");
            }

            // Работа с контроллером
            SoftwareController controller = new(container);

            // Найти игрушки определенного типа
            var toys = controller.FindToysByType("Головоломка");
            Console.WriteLine("\nИгрушки типа 'Головоломка':");
            toys.ForEach(Console.WriteLine);

            // Найти текстовые редакторы определенного формата
            var editors = controller.FindTextEditorsByFormat("TXT");
            Console.WriteLine("\nТекстовые редакторы с форматом 'TXT':");
            editors.ForEach(Console.WriteLine);

            // Вывести ПО в алфавитном порядке
            var sortedSoftware = controller.GetSoftwareSortedByName();
            Console.WriteLine("\nПрограммное обеспечение в алфавитном порядке:");
            sortedSoftware.ForEach(Console.WriteLine);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Общее исключение: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("\nЗавершение программы. Освобождение ресурсов.");
        }

        // Демонстрация использования Debug.Assert
        int[] numbers = { 1, 2, 3 }; // Убедимся, что numbers не null
        Debug.Assert(numbers != null, "Массив numbers не должен быть null");
        Console.WriteLine("\nПрограмма завершена успешно.");
    }
}
