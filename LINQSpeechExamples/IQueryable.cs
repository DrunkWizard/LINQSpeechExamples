using System.Collections;
using System.Linq.Expressions;

namespace LINQSpeechExamples;

public interface IQueryable<out T> : IEnumerable<T>
{
    public Type ElementType { get; }
    public Expression Expression { get; }
    public IQueryProvider Provider { get; }
    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public interface IQueryProvider
{
    public IQueryable CreateQuery(Expression expression)
    {
        throw new NotImplementedException();
    }

    public System.Linq.IQueryable<TElement> CreateQuery<TElement>(Expression expression)
    {
        throw new NotImplementedException();
    }

    public object? Execute(Expression expression)
    {
        throw new NotImplementedException();
    }

    public TResult Execute<TResult>(Expression expression)
    {
        throw new NotImplementedException();
    }
}