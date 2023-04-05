using System.Collections;

namespace LINQSpeechExamples;

public class IDisposableExample
{
    static IEnumerable GetLines(string path)
    {
        using var reader = new StreamReader(path);
        while (!reader.EndOfStream)
            yield return reader.ReadLine();
    }
}