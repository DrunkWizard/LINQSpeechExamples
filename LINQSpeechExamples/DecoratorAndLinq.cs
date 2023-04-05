using System.Collections;

namespace LINQSpeechExamples;

public class DecoratorAndLinq
{
    public static void Execute()
    {
        var list = new List<int>() { 1, 2, 3, 5, 6, 8 };

        #region MyWhere
        foreach (var item in new MyWhere<int>(list, (num) => num > 3))
        {
            //Console.WriteLine(item);
        }
        #endregion

        #region MySelect
        foreach (var item in new MySelect<int, string>(list, (num) => "string" + num))
        {
            //Console.WriteLine(item);
        }
        #endregion

        #region SelectAndWhere
        var selectAndWhere = new MySelect<int, string>(new MyWhere<int>(list, (num) => num > 3), (num) => "string" + num);
        
        foreach (var item in selectAndWhere)
        {
            Console.WriteLine(item);
        }
        #endregion

        #region Extension
        var selectAndWhere1 = list.MyWhereEx(num => num > 3).MySelectEx(num => num + 6).ToString();
        
        // foreach (var item in selectAndWhere1)
        // {
        //     //Console.WriteLine(item);
        // }
        #endregion
    }
}

#region MySelectImpl
public class MySelect<TIn, TOut> : IEnumerable<TOut>, IEnumerator<TOut>
{
    private readonly Func<TIn, TOut> _converter;
    private readonly IEnumerator<TIn> _enumerator;

    public MySelect(IEnumerable<TIn> enumerable, Func<TIn, TOut> converter)
    {
        _converter = converter;
        _enumerator = enumerable.GetEnumerator();
    }

    public IEnumerator<TOut> GetEnumerator()
    {
        return this;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool MoveNext() => _enumerator.MoveNext();

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public TOut Current => _converter(_enumerator.Current);

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }
}

#endregion

#region MyWhereImpl
public class MyWhere<T> : IEnumerable<T>, IEnumerator<T>
{
    private readonly Predicate<T> _predicate;
    private readonly IEnumerator<T> _enumerator;

    public MyWhere(IEnumerable<T> enumerable, Predicate<T> predicate)
    {
        _enumerator = enumerable.GetEnumerator();
        _predicate = predicate;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return this;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool MoveNext()
    {
        while (_enumerator.MoveNext())
        {
            if (_predicate(_enumerator.Current))
            {
                return true;
            }
        }

        return false;

    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public T Current => _enumerator.Current;

    object IEnumerator.Current => Current;

    public void Dispose()
    {
    }
}
#endregion

#region MyExtensions
public static class MyEnumerableExtensions
{
    public static IEnumerable<TOut> MySelectEx<TIn, TOut>(this IEnumerable<TIn> enumerable, Func<TIn, TOut> converter)
    {
        return new MySelect<TIn, TOut>(enumerable, converter);
    }
    
    public static IEnumerable<T> MyWhereEx<T>(this IEnumerable<T> enumerable, Predicate<T> predicate)
    {
        return new MyWhere<T>(enumerable, predicate);
    }
}
#endregion
