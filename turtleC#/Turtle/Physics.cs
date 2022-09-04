namespace Turtle;

public enum BodyType
{
    Dynamic,
    Static
}

public class Collider
{
    internal Body _body;
    internal PolygonDef _shape;
    internal PhysicsWorld _world;
    internal Vector2 _size;

    public void Destroy()
    {
        _body.Dispose();
        _world._colliders.Remove(this);
    }

    public void SetCollisionClass()
    {

    }

    public void Enter()
    {

    }

    public void SetObject()
    {

    }

    public void GetObject()
    {

    }

    public void SetType(BodyType type)
    {
        switch (type)
        {
            case BodyType.Dynamic:
                break;
            case BodyType.Static:
                _body.SetStatic();
                break;
            default:
                break;
        }
    }
}

public class PhysicsWorld
{
    internal World _world;

    internal List<Collider> _colliders = new();

    public void Update(float dt)
    {
        _world.Step(dt, 8, 3);
    }

    public void SetGravity(int x, int y)
    {
        _world.Gravity = new Vec2(x, y);
    }

    public void SetGravity(Vector2 gravity)
    {
        _world.Gravity = new Vec2(gravity.X, gravity.Y);
    }

    public void Draw()
    {
        foreach (Collider collider in _colliders)
        {
            Vec2 position = collider._body.GetPosition();
            Vector2 size = collider._size;
            float angle = collider._body.GetAngle() * (180 / (float)System.Math.PI);

            Raylib.DrawRectanglePro(new Rectangle(
                (int)(position.X * Physics.PPM),
                (int)(position.Y * Physics.PPM),
                (int)(size.X * Physics.PPM) * 2,
                (int)(size.Y * Physics.PPM) * 2
                ), new Vector2(
                size.X * Physics.PPM,
                size.Y * Physics.PPM
                ), angle, new Raylib_cs.Color(255, 255, 255, 255));
        }
    }

    public void Destroy()
    {

    }

    public void AddCollisionClass()
    {

    }

    public void NewCircleCollider()
    {

    }

    public Collider NewRectangleCollider(int x, int y, int width, int height)
    {
        Collider newCollider = new();
        newCollider._world = this;

        BodyDef newBodyDef = new();
        newBodyDef.Position.Set(x / Physics.PPM, y / Physics.PPM);

        Body newBody = _world.CreateBody(newBodyDef);

        PolygonDef newShape = new();
        newShape.SetAsBox(width / Physics.PPM, height / Physics.PPM);
        newShape.Density = 1.0f;

        newBody.CreateFixture(newShape);
        newBody.SetMassFromShapes();

        newCollider._body = newBody;
        newCollider._shape = newShape;
        newCollider._size = new Vector2(width / Physics.PPM, height / Physics.PPM);

        _colliders.Add(newCollider);

        return newCollider;
    }

    public void QueryCircleArea()
    {

    }

    public void QueryRectangleArea()
    {

    }

    public Collider[] GetColliders()
    {
        return _colliders.ToArray();
    }
}

public static class Physics
{
    internal static float PPM = 64.0f;

    public static PhysicsWorld NewWorld(int x, int y, bool allowSleep)
    {
        PhysicsWorld newWorld = new();

        AABB worldAabb = new();
        worldAabb.LowerBound.Set(-1000.0f);
        worldAabb.UpperBound.Set(1000.0f);

        newWorld._world = new World(worldAabb, new Vec2(x, y), allowSleep);

        return newWorld;
    }

    public static PhysicsWorld NewWorld(Vector2 gravity, bool allowSleep)
    {
        PhysicsWorld newWorld = new();

        AABB worldAabb = new();
        worldAabb.LowerBound.Set(-1000.0f);
        worldAabb.UpperBound.Set(1000.0f);

        newWorld._world = new World(worldAabb, new Vec2(gravity.X, gravity.Y), allowSleep);

        return newWorld;
    }
}