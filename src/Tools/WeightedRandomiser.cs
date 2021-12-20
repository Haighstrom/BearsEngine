using HaighFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearsEngine
{
    public class WeightedRandomiser<T>
    {
        private List<(T item, double accumWeight)> _items = new List<(T item, double accumWeight)>();
        private double _totalWeight;

        public void AddItem(T item, double weight)
        {
            _totalWeight += weight;
            _items.Add((item, _totalWeight));
        }

        public T GetRandomItem()
        {
            double r = HF.Randomisation.RandD(_totalWeight);

            foreach (var iw in _items)
                if (iw.accumWeight >= r)
                    return iw.item;

            HConsole.Warning("WeightedRandomiser.GetRandomItem: List was empty when requesting an item.");
            return default(T);
        }
    }
}