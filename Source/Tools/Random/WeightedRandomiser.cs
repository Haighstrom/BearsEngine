namespace BearsEngine;

public class WeightedRandomiser<T>
{
    private readonly bool _removeWhenTakingItem;
    private readonly List<(T item, double accumWeight)> _items = new();
    private double _totalWeight;

    public WeightedRandomiser(bool removeWhenTakingItem) 
    { 
        _removeWhenTakingItem = removeWhenTakingItem;
    }

    public void AddItem(T item, double weight)
    {
        _totalWeight += weight;
        _items.Add((item, _totalWeight));
    }

    public T? GetRandomItem()
    {
        double r = Randomisation.RandD(_totalWeight);

        foreach (var (item, accumWeight) in _items)
            if (accumWeight >= r)
            {
                if (_removeWhenTakingItem)
                    _items.Remove((item, accumWeight));
                return item;
            }

        Log.Warning("WeightedRandomiser.GetRandomItem: List was empty when requesting an item.");
        return default;
    }
}
