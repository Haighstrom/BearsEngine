namespace BearsEngine;

public class WeightedRandomiser<T>
{
    private readonly List<(T item, double accumWeight)> _items = new();
    private double _totalWeight;

    public void AddItem(T item, double weight)
    {
        _totalWeight += weight;
        _items.Add((item, _totalWeight));
    }

    public T? GetRandomItem()
    {
        double r = HF.Randomisation.RandD(_totalWeight);

        foreach (var (item, accumWeight) in _items)
            if (accumWeight >= r)
                return item;

        BE.Logging.Warning("WeightedRandomiser.GetRandomItem: List was empty when requesting an item.");
        return default;
    }
}
