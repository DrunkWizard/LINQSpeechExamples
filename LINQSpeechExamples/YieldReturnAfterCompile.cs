using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace LINQSpeechExamples;

public class YieldReturnAfterCompile
{
    public static void Execute()
    {
        var tree = new Tree2<int>(12);
        var child = new Tree2<int>(13);
        var child1 = new Tree2<int>(15);
        var child3 = new Tree2<int>(15);

        child1.Nodes.Add(new Tree2<int>(21));
        child1.Nodes.Add(new Tree2<int>(2));
        child1.Nodes.Add(new Tree2<int>(22));
        child.Nodes.Add(new Tree2<int>(21));
        child.Nodes.Add(child1);
        child.Nodes.Add(new Tree2<int>(22));
        
        child3.Nodes.Add(child1);

        tree.Nodes.Add(child);
        tree.Nodes.Add(new Tree2<int>(14));
        tree.Nodes.Add(child3);
        tree.Nodes.Add(new Tree2<int>(16));

        foreach (var nodeValue in tree)
        {
            Console.WriteLine(nodeValue);
        }

    }
}
    public class Tree2<T> : IEnumerable<T>, IEnumerable
    {
        private sealed class TreeEnumerator : IEnumerator<T>, IEnumerator, IDisposable
        {
            private int _state;

            private T _current;

            public Tree2<T> thisTree;

            private List<Tree2<T>>.Enumerator _nodeListEnumerator;

            private Tree2<T> _node;

            private IEnumerator<T> _treeEnumerator;

            private T _value;

            T IEnumerator<T>.Current => _current;

            object IEnumerator.Current => _current;

            //[DebuggerHidden]
            public TreeEnumerator(int state)
            {
                _state = state;
            }

            //[DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = _state;
                if ((uint)(num - -4) > 1u && num != 2)
                {
                    return;
                }
                try
                {
                    if (num == -4 || num == 2)
                    {
                        try
                        {
                        }
                        finally
                        {
                            Finally2();
                        }
                    }
                }
                finally
                {
                    Finally1();
                }
            }

            private bool MoveNext()
            {
                try
                {
                    switch (_state)
                    {
                        default:
                            return false;
                        case 0:
                            _state = -1;
                            _current = thisTree.Value;
                            _state = 1;
                            return true;
                        case 1:
                            _state = -1;
                            _nodeListEnumerator = thisTree.Nodes.GetEnumerator();
                            _state = -3;
                            goto IL_010c;
                        case 2:
                        {
                            _state = -4;
                            _value = default(T);
                            goto IL_00e9;
                        }
                        IL_010c:
                        if (_nodeListEnumerator.MoveNext())
                        {
                            _node = _nodeListEnumerator.Current;
                            _treeEnumerator = _node.GetEnumerator();
                            _state = -4;
                            goto IL_00e9;
                        }
                        Finally1();
                        _nodeListEnumerator = default(List<Tree2<T>>.Enumerator);
                        return false;
                        IL_00e9:
                        if (_treeEnumerator.MoveNext())
                        {
                            _value = _treeEnumerator.Current;
                            _current = _value;
                            _state = 2;
                            return true;
                        }
                        Finally2();
                        _treeEnumerator = null;
                        _node = null;
                        goto IL_010c;
                    }
                }
                catch
                {
                    //try-fault
                    ((IDisposable)this).Dispose();
                    throw;
                }
            }

            bool IEnumerator.MoveNext()
            {
                //ILSpy generated this explicit interface implementation from .override directive in MoveNext
                return this.MoveNext();
            }

            private void Finally1()
            {
                _state = -1;
                _nodeListEnumerator.Dispose();
            }

            private void Finally2()
            {
                _state = -3;
                if (_treeEnumerator != null)
                {
                    _treeEnumerator.Dispose();
                }
            }

            //[DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }
        }

        public List<Tree2<T>> Nodes = new List<Tree2<T>>();

        public T Value;

        public Tree2(T value)
        {
            Value = value;
        }

        //[IteratorStateMachine(typeof(Tree2<>.TreeEnumerator))]
        public IEnumerator<T> GetEnumerator()
        {
            TreeEnumerator treeEnumerator= new TreeEnumerator(0);
            treeEnumerator.thisTree = this;
            return treeEnumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
