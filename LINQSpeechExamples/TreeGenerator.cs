using System.Collections;

namespace LINQSpeechExamples;

public class TreeGenerator
{
    public static void Execute()
    {
        var tree = new GeneratedTree(12);
        var iteration = 0;
        foreach (var node in tree)
        {
            iteration++;
            Console.WriteLine(node);
            if (iteration == 1000)
            {
                break;
            }
        }
    }
}
public class GeneratedTree: IEnumerable<int>
{
    public List<GeneratedTree> Nodes = new();
    public int Value;
    private readonly Random _random = new();

    public GeneratedTree(int value)
    {
        Value = value;
    }

    public IEnumerator<int> GetEnumerator()
    {
        yield return Value;

        for (var i = 0; i <= _random.Next(0, 10); i++)
        {
            Nodes.Add(new GeneratedTree(_random.Next(1, 100)));
        }
        foreach (var node in Nodes)
        {
            foreach (var value in node)
            {
                yield return value;
            }
        }

    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}