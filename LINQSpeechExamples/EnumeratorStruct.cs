namespace LINQSpeechExamples;

public class EnumeratorStruct
{
    public static void Execute()
    {
        var list = new List<int> {1, 2, 3};

        var x1 = new { Items = ((IEnumerable<int>)list).GetEnumerator() };
        while (x1.Items.MoveNext())
        {
            Console.WriteLine(x1.Items.Current);
        }

        var x2 = new { Items = list.GetEnumerator() };
        while (x2.Items.MoveNext())
        {
            Console.WriteLine(x2.Items.Current);
        }
    }
}