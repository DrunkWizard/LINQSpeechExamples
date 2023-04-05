using System.Collections;
using System.Diagnostics;

namespace LINQSpeechExamples;

public class IEnumerableProblems
{
    public static void Execute()
    {
        var array = new MyArray<MyDataStruct>(new []
        {
            new MyDataStruct("data1"),
            new MyDataStruct("data2"),
            new MyDataStruct("data3")
        });

        var lazyArray = GetLazyCollection();
        
        ChangeCollection(array);
        ChangeCollection(lazyArray);
        // MultipleEnum.Excecute();
        //
        // foreach (var num in new RandomNumber())
        // {
        //     Console.WriteLine(num);
        // }
    }
    
    private static IEnumerable<MyDataStruct> GetLazyCollection()
    {
        yield return new MyDataStruct("data1");
        yield return new MyDataStruct("data2");
        yield return new MyDataStruct("data3");
    }

    private static void ChangeCollection(IEnumerable<MyDataStruct> collection)
    {
        foreach (var data in collection)
        {
            if (data.Something == "data2")
            {
                data.Something = "DATA2";
            }
        }

        foreach (var data in collection)
        {
            Console.WriteLine(data.Something);
        }
    }
    
}

public class MultipleEnum
{
    public static void Excecute()
    {
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        var lines = File.ReadLines("data.txt");
        var lastLine = lines.ElementAt(lines.Count() - 1);

        stopWatch.Stop();
        
        var stopWatch1 = new Stopwatch();
        stopWatch1.Start();
        var lines1 = File.ReadLines("data.txt").ToList();
        var lastLine1 = lines1.ElementAt(lines1.Count - 1);

        stopWatch1.Stop();
        
        
        Console.WriteLine(stopWatch.Elapsed);
        Console.WriteLine(stopWatch1.Elapsed);
        Console.WriteLine(lastLine);
    }
}

public class MyDataStruct
{
    public string Something;

    public MyDataStruct(string something)
    {
        Something = something;
    }
}

class RandomNumber : IEnumerable<int>
{
    private readonly Random _rnd = new Random();

    public IEnumerator<int> GetEnumerator()
    {
        while (true)
            yield return _rnd.Next();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
