namespace BearsEngine;

public class BiDictionary<T1, T2> 
    where T1 : notnull 
    where T2 : notnull
{
    private readonly Dictionary<T1, T2> _forwardDict = new();
    private readonly Dictionary<T2, T1> _backwardDict = new();

    public void Add(T1 first, T2 second)
    {
        if (_forwardDict.ContainsKey(first) ||
            _backwardDict.ContainsKey(second))
        {
            throw new Exception($"Duplicate key or value: {first}, {second}");
        }
        _forwardDict.Add(first, second);
        _backwardDict.Add(second, first);
    }

    public void Remove(T1 first)
    {
        if (!Contains(first))
            throw new InvalidOperationException($"Tried to remove value {first} when it was not within the dictionary.");

        _backwardDict.Remove(GetByFirst(first)!);
        _forwardDict.Remove(first);
    }

    public void Remove(T2 second)
    {
        if (!Contains(second))
            throw new InvalidOperationException($"Tried to remove value {second} when it was not within the dictionary.");

        _forwardDict.Remove(GetBySecond(second)!);
        _backwardDict.Remove(second);
    }

    public T2? this[T1 first] => GetByFirst(first);

    public T1? this[T2 second] => GetBySecond(second);

    public bool Contains(T1 first) => _forwardDict.ContainsKey(first);
    public bool Contains(T2 second) => _backwardDict.ContainsKey(second);

    public int Count => _forwardDict.Count;

    public T2? GetByFirst(T1 first)
    {
        if (_forwardDict.TryGetValue(first, out T2? second))
            return second;
        else
            return default;
    }

    public T1? GetBySecond(T2 second)
    {
        if (_backwardDict.TryGetValue(second, out T1? first))
            return first;
        else
            return default;
    }

    public override string ToString()
    {
        string s = "";

        s += Count + " Elements\n";
        int i = 0;
        foreach (var f in _forwardDict.Keys)
        {
            s += "  " + i.ToString();
            s += " " + f.ToString() + " : " + _forwardDict[f].ToString() + "\n";
            i++;
        }

        return s;
    }
}
