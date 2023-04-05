using System.Collections;

namespace LINQSpeechExamples;

public class ExampleTreeEnumerationWIthYieldReturn
{
    public static void Execute()
    {
        var tree = new Tree1<int>(12);
        var child = new Tree1<int>(13);
        var child1 = new Tree1<int>(15);
        var child3 = new Tree1<int>(15);

        child1.Nodes.Add(new Tree1<int>(21));
        child1.Nodes.Add(new Tree1<int>(2));
        child1.Nodes.Add(new Tree1<int>(22));
        child.Nodes.Add(new Tree1<int>(21));
        child.Nodes.Add(child1);
        child.Nodes.Add(new Tree1<int>(22));
        
        child3.Nodes.Add(child1);

        tree.Nodes.Add(child);
        tree.Nodes.Add(new Tree1<int>(14));
        tree.Nodes.Add(child3);
        tree.Nodes.Add(new Tree1<int>(16));

        foreach (var nodeValue in tree)
        {
            Console.WriteLine(nodeValue);
        }

    }
}

public class Tree1<T> : IEnumerable<T>
{
    public List<Tree1<T>> Nodes = new();
    public T Value;

    public Tree1(T value)
    {
        Value = value;
    }

    public IEnumerator<T> GetEnumerator()
    {
        yield return Value;

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