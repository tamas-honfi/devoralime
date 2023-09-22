namespace ToBattle.Services;

public class RandomService
{
    private Random _randomGenerator;

    public RandomService()
    {
        _randomGenerator = new Random();
    }

    public void InitializeWithSeed(int seed)
    {
        _randomGenerator = new Random(seed);
    }

    public int Next(int minValueInclusive, int maxValueExclusive)
    {
        return _randomGenerator.Next(minValueInclusive, maxValueExclusive);
    }

    public T SelectRandomItem<T>(T[] array)
    {
        var randomIndex = Next(0, array.Length);
        return array[randomIndex];
    }
}