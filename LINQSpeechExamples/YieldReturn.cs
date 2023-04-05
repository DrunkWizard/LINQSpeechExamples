using System.Collections;

namespace LINQSpeechExamples;

public class YieldReturn
{
    public static void Execute()
    {
        var sas = GetUniqueFields().Any(s => s == "String2");
        
        Console.WriteLine(sas);
        // foreach (var field in GetUniqueFields())
        // {
        //     Console.WriteLine(field);
        // }
    }
    
    public static IEnumerable<string> GetUniqueFields()
    {
        var list = new List<string>()
        {
            "string1",
            "string2",
            "string3"
        };
        foreach (var item in list)
        {
            yield return item;
        }

        for (var i = 0; i < 25; i++)
        {
            yield return $"string{i}";
        }
        
        var counter = 0;
        Console.WriteLine("Before first");
        counter++;
        yield return $"String{counter}";
        Console.WriteLine("Before second");
        counter++;
        yield return $"String{counter}";
        counter++;
        yield return $"String{counter}";
        counter++;
        yield return $"String{counter}";
    }
}
 
public class GetUniqueFields : IEnumerable<string>, IEnumerator<string>
{
    private string _current;
    private int _state = 0;
    private int _counter = 0;

    public IEnumerator<string> GetEnumerator()
    {
        return this;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool MoveNext()
    {
        switch (_state)
        {
            default:
            return false;
            case 0:
                _state = -1;
                _current = "String1";
                _state = 1;
            return true;
            case 1:
                _state = -1;
                _current = "String2";
                _state = 2;
            return true;
            case 2:
                _state = -1;
            return false;
        }
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    string IEnumerator<string>.Current => _current;

    public object Current => Current;
    public void Dispose()
    {
    }
}

public class GetUniqueFields1 : IEnumerable<string>, IEnumerator<string>
{
    private string _current;
    private int _state = 0;
    private int _index = 0;
    private List<string>.Enumerator _listEnumerator;

    public IEnumerator<string> GetEnumerator()
    {
        return this;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool MoveNext()
    {
        switch (_state)
        {
            default:
                return false;
            case 0:
                _state = -1;
                _current = "String1";
                _state = 1;
                return true;
            case 1:
                _state = -1;
                _current = "String2";
                _state = 2;
                return true;
            case 2:
                _state = -1;
                return false;
        }
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    string IEnumerator<string>.Current => _current;

    public object Current => Current;
    public void Dispose()
    {
    }
}

