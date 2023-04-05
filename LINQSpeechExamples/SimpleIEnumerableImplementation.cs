using System.Collections;

namespace LINQSpeechExamples;

public class SimpleIEnumerableImplementation
{
    public static void Execute()
    {
        var newArray = new MyArray<string>(new[] {"sdfdsfs", "fsdfsdf", "sxcxcxc"});

        foreach (var item in newArray)
        {
            Console.WriteLine(item);
        }
    }
}

public class MyArrayEnumerator<T> : IEnumerator<T>
{
    private readonly T[] _array;
    private int _currentIndex = -1;

    public MyArrayEnumerator(T[] array)
    {
        _array = array;
    }

    public bool MoveNext()
    {
        return ++_currentIndex < _array.Length;
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public T Current => _array[_currentIndex];

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }
}

public class MyArray<T> : IEnumerable<T>
{
    private readonly T[] _array;

    public MyArray(T[] array)
    {
        _array = array;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new MyArrayEnumerator<T>(_array);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

#region Interfaces
// public interface IEnumerable
// {
//     IEnumerator GetEnumerator();
// }
//
// public interface IEnumerable<out T> : IEnumerable
// {
//     new IEnumerator<T> GetEnumerator();
// }
//
//
// public interface IEnumerator
// {
//     bool MoveNext();
//     object Current { get; }
//     void Reset();
// }
//
// public interface IEnumerator<out T> : IDisposable, IEnumerator
// {
//     new T Current { get; }
// }
#endregion