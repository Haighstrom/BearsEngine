namespace BearsEngine;

public class BiDictionary<T1, T2>
{
    Dictionary<T1, T2> _forwardDict = new();
    Dictionary<T2, T1> _backwardDict = new();

    public List<T1> FirstElements { get; private set; } = new List<T1>();

    public void Add(T1 first, T2 second)
    {
        if (_forwardDict.ContainsKey(first) ||
            _backwardDict.ContainsKey(second))
        {
            throw new Exception($"Duplicate key or value: {first}, {second}");
        }
        _forwardDict.Add(first, second);
        _backwardDict.Add(second, first);
        FirstElements.Add(first);
    }

    public void Remove(T1 first)
    {
        _backwardDict.Remove(GetByFirst(first));
        _forwardDict.Remove(first);
        FirstElements.Remove(first);
    }

    public void Remove(T2 second)
    {
        _forwardDict.Remove(GetBySecond(second));
        _backwardDict.Remove(second);
        FirstElements.Remove(this[second]);
    }

    public T2 this[T1 first] => GetByFirst(first);

    public T1 this[T2 second] => GetBySecond(second);

    public bool Contains(T1 first) => _forwardDict.ContainsKey(first);
    public bool Contains(T2 second) => _backwardDict.ContainsKey(second);

    public int Count => _forwardDict.Count;


    public T2 GetByFirst(T1 first)
    {
        T2 second;
        if (_forwardDict.TryGetValue(first, out second))
            return second;
        else 
            return default(T2);
    }
    public T1 GetBySecond(T2 second)
    {
        T1 first;
        if (_backwardDict.TryGetValue(second, out first))
            return first;
        else 
            return default(T1);
    }

    public override string ToString()
    {
        string s = "";

        s += Count + " Elements\n";
        int i = 0;
        foreach (T1 f in _forwardDict.Keys)
        {
            s += "  " + i.ToString();
            s += " " + f.ToString() + " : " + GetByFirst(f).ToString() + "\n";
            i++;
        }

        return s;
    }
}
