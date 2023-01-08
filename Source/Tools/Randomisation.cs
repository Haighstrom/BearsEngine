namespace BearsEngine;

public static class Randomisation
{
    public static T RandomElement<T>(this IList<T> list)
    {
        return list[HF.Randomisation.Rand(list.Count)];
    }
}