using System.Collections;

namespace LINQSpeechExamples;

public class ExampleOfDifferentEnumerator
{
    public static void Execute()
    {
/*
                             ┌──┐
          ┌──────────────────┤12├──┬─────────────┐
          │                  └┬─┘  │             │
          │                   │    │             │
          │              ┌────┘    │             │
         ┌▼─┐            │        ┌▼─┐         ┌─▼┐
 ┌───────┤13├──────┐   ┌─▼┐       │15│         │16│
 │       └┬─┘      │   │14│       └──┴─┐       └──┘
 │        │        │   └──┘            │
 │        │        │                   │
 │        │        │                   │
┌▼─┐     ┌▼─┐     ┌▼─┐               ┌─▼┐
│21│ ┌───┤15│     │22│           ┌───┤15├──────┐
└──┘ │   └┬─┤     └──┘           │   └──┤      │
     │    │ │                    │      │      │
     │    │ │                    │      │      │
     │    │ │                    │      │      │
   ┌─▼┐   │ └───▲──┐             │      │      │
   │21│  ┌▼┐    │22│            ┌▼─┐   ┌▼┐    ┌▼─┐
   └──┘  │2│    └──┘            │21│   │2│    │22│
         └─┘                    └──┘   └─┘    └──┘
 */
        var tree = new Tree<int>(12, TreeEnumerationType.BreadthFirst);
        var child = new Tree<int>(13);
        var child1 = new Tree<int>(15);
        var child3 = new Tree<int>(15);

        child1.Nodes.Add(new Tree<int>(21));
        child1.Nodes.Add(new Tree<int>(2));
        child1.Nodes.Add(new Tree<int>(22));
        child.Nodes.Add(new Tree<int>(21));
        child.Nodes.Add(child1);
        child.Nodes.Add(new Tree<int>(22));
        
        child3.Nodes.Add(child1);

        tree.Nodes.Add(child);
        tree.Nodes.Add(new Tree<int>(14));
        tree.Nodes.Add(child3);
        tree.Nodes.Add(new Tree<int>(16));

        foreach (var nodeValue in tree)
        {
            Console.WriteLine(nodeValue);
        }

    }
}

public enum TreeEnumerationType
{
    DepthFirst = 0,
    BreadthFirst = 1
}

public class Tree<T> : IEnumerable<T>
{
    public List<Tree<T>> Nodes = new();
    public T Value;
    public TreeEnumerationType EnumerationType;

    public Tree(T value, TreeEnumerationType type = TreeEnumerationType.DepthFirst)
    {
        Value = value;
        EnumerationType = type;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return EnumerationType switch
        {
            TreeEnumerationType.DepthFirst => new DepthFirstEnumerator<T>(this),
            TreeEnumerationType.BreadthFirst => new BreadthFirstEnumerator<T>(this)
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class DepthFirstEnumerator<T> : IEnumerator<T>
{
    private readonly Stack<Tree<T>> _visited = new();
    private Tree<T> _current;

    public DepthFirstEnumerator(Tree<T> current)
    {
        _current = current;
        _visited.Push(current);
    }

    public bool MoveNext()
    {
        if (_visited.Count == 0)
        {
            return false;
        }

        _current = _visited.Pop();

        foreach (var node in _current.Nodes)
        {
            _visited.Push(node);
        }

        return true;
    }
    public T Current => _current.Value;

    object IEnumerator.Current => Current;

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
    }
}

public class BreadthFirstEnumerator<T> : IEnumerator<T>
{
    private Tree<T> _current;
    private Tree<T> _parent;
    private readonly Queue<Tree<T>> _visitedQueue = new();
    private bool _firstCall = true;
    private IEnumerator<Tree<T>> _nodesEnumerator;


    public BreadthFirstEnumerator(Tree<T> tree)
    {
        _current = tree;
        _parent = tree;
        _nodesEnumerator = _parent.Nodes.GetEnumerator();
    }

    public bool MoveNext()
    {
        if (_firstCall)
        {
            _firstCall = false;
            return true;
        }

        if (_nodesEnumerator.MoveNext())
        {
            var node = _nodesEnumerator.Current;
            _visitedQueue.Enqueue(node);
            _current = node;
            return true;
        }

        if (_visitedQueue.Count == 0)
        {
            return false;
        }
        
        _parent = _visitedQueue.Dequeue();
        _nodesEnumerator = _parent.Nodes.GetEnumerator();
        return MoveNext();
    }

    public T Current => _current.Value;

    object IEnumerator.Current => Current;
    
    public void Reset()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
    }
}