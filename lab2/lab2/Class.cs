using System;
using System.Linq;

public partial class Vector
{
    private static int _instanceCount = 0;
    private readonly int _id;
    public const string ClassName = "Класс Вектор";
    private int[] _elements;
    private int _elementCount;
    private int _errorCode;

    static Vector()
    {
        _instanceCount = 0;
    }

    public Vector() : this(new int[0]) { }

    public Vector(int[] elements)
    {
        _elements = elements ?? throw new ArgumentNullException(nameof(elements));
        _elementCount = elements.Length;
        _id = CreateUniqueId();
        _errorCode = 0;
        _instanceCount++;
    }

    public Vector(int initialSize, int initialValue = 0)
    {
        if (initialSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(initialSize));

        _elements = new int[initialSize];
        for (int i = 0; i < initialSize; i++)
        {
            _elements[i] = initialValue;
        }
        _elementCount = initialSize;
        _id = CreateUniqueId();
        _errorCode = 0;
        _instanceCount++;
    }

    private Vector(int id, int[] elements)
    {
        _id = id;
        _elements = elements ?? throw new ArgumentNullException(nameof(elements));
        _elementCount = elements.Length;
        _errorCode = 0;
        _instanceCount++;
    }

    public int Id => _id;

    public int this[int index]
    {
        get
        {
            if (index < 0 || index >= _elementCount)
                throw new IndexOutOfRangeException();
            return _elements[index];
        }
        set
        {
            if (index < 0 || index >= _elementCount)
                throw new IndexOutOfRangeException();
            _elements[index] = value;
        }
    }

    public Vector Add(int value)
    {
        var result = new Vector(_elements.Take(_elementCount).ToArray());
        for (int i = 0; i < result._elementCount; i++)
        {
            result._elements[i] += value;
        }
        return result;
    }

    public Vector Multiply(int value)
    {
        var result = new Vector(_elements.Take(_elementCount).ToArray());
        for (int i = 0; i < result._elementCount; i++)
        {
            result._elements[i] *= value;
        }
        return result;
    }

    public void ProcessElements(ref int multiplier, out int errorCode)
    {
        errorCode = _errorCode;
        for (int i = 0; i < _elementCount; i++)
        {
            _elements[i] *= multiplier;
        }
    }

    public override bool Equals(object obj)
    {
        if (obj is Vector vector)
        {
            return _id == vector._id;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return _id.GetHashCode();
    }

    public override string ToString()
    {
        return $"ID Вектора: {_id}, Элементы: [{string.Join(", ", _elements.Take(_elementCount))}], Код ошибки {_errorCode}";
    }

    public static void PrintClassInfo()
    {
        Console.WriteLine($"Имя класса: {ClassName}, Количество экземпляров: {_instanceCount}");
    }

    public double Magnitude()
    {
        return Math.Sqrt(_elements.Take(_elementCount).Sum(e => e * e));
    }

    private int CreateUniqueId()
    {
        return ++_instanceCount; // Генерация уникального ID
    }

    // Новый метод для проверки наличия нуля
    public bool ContainsZero()
    {
        return _elements.Take(_elementCount).Any(e => e == 0);
    }
}


























