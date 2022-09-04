namespace Turtle;

public class Finder
{
    private readonly PathFinder _aStarFinder;

    public Finder(short[,] map)
    {
        WorldGrid grid = new(map);

        this._aStarFinder = new(grid, new PathFinderOptions
        {
            PunishChangeDirection = true,
            UseDiagonals = true
        });
    }

    public Vector2[] FindPath(Vector2 pos1, Vector2 pos2)
    {
        List<Vector2> path = new();

        Point[] points = _aStarFinder.FindPath(new Point((int)pos1.X, (int)pos1.Y), new Point((int)pos2.X, (int)pos2.Y));

        foreach (Point point in points)
        {
            path.Add(new Vector2(point.X, point.Y));
        }

        return path.ToArray();
    }
}

public static class AStar
{
    public static Finder NewFinder(short[,] map)
    {
        return new Finder(map);
    }
}