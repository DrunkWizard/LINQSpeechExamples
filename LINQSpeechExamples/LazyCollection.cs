using System.Collections;

namespace LINQSpeechExamples;

public class LazyCollection
{
    public static void Execute()
    {
        var fibonacci = new FibonacciSequence();
        foreach (var number in fibonacci)
        {
            Console.WriteLine(number);
        }
    }
}

public class BadFibonacciSequence
{
    private readonly List<int> _fibonacci = new();
    private readonly int _maxValue;


    public BadFibonacciSequence(int maxValue)
    {
        _maxValue = maxValue;
    }

    public IEnumerable GetSequence()
    {
        _fibonacci.Add(1);
        _fibonacci.Add(1);

        for (var i = 3; i < _maxValue; i++)
        {
            _fibonacci.Add(_fibonacci[^1] + _fibonacci[^2]);
        }
        return _fibonacci;
    }
}


public class FibonacciSequence : IEnumerable<int>
{
    public IEnumerator<int> GetEnumerator()
    {
        return new FibonacciEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class FibonacciEnumerator : IEnumerator<int>
{
    private int _current = 1;
    private int _previous = 0;
    private int _currentIndex = -1;

    public bool MoveNext()
    {
        if (_currentIndex == -1)
        {
            _currentIndex++;
            return true;
        }
        var temp = _current;
        _current += _previous;
        _previous = temp;
        
        _currentIndex++;

        return true;
    }
    public int Current => _current;

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }
    public void Reset()
    {
        throw new NotImplementedException();
    }

}