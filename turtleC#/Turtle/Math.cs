namespace Turtle;

public enum NoiseType
{
    Perlin,
    Simplex,
    Value
}

public static class Math
{
    private static FastNoiseLite _noise = new();

    public static float Noise(float x, float y, NoiseType type = NoiseType.Simplex)
    {
        switch (type)
        {
            case NoiseType.Perlin:
                _noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
                break;
            case NoiseType.Simplex:
                _noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
                break;
            case NoiseType.Value:
                _noise.SetNoiseType(FastNoiseLite.NoiseType.Value);
                break;
            default:
                break;
        }

        return _noise.GetNoise(x, y);
    }

    public static float Noise(float x, float y, float z, NoiseType type = NoiseType.Simplex)
    {
        switch (type)
        {
            case NoiseType.Perlin:
                _noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
                break;
            case NoiseType.Simplex:
                _noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
                break;
            case NoiseType.Value:
                _noise.SetNoiseType(FastNoiseLite.NoiseType.Value);
                break;
            default:
                break;
        }

        return _noise.GetNoise(x, y, z);
    }

    public static double Random()
    {
        Random random = new();

        return random.NextDouble();
    }

    public static int Random(int max)
    {
        Random random = new();

        return random.Next(max);
    }

    public static int Random(int min, int max)
    {
        Random random = new();

        return random.Next(min, max);
    }
}