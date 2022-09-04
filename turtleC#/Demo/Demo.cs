namespace Demo;

public class Demo
{
    private PhysicsWorld _world;

    public void Load()
    {
        Window.SetVSync(false);

        _world = Physics.NewWorld(0, 10, true);

        Collider ground = _world.NewRectangleCollider(640, 500, 2000, 25);
        ground.SetType(BodyType.Static);

        Graphics.SetBackgroundColor(new Color(0, 0, 255));
    }

    public void Update(float dt)
    {
        _world.Update(dt);

        if (Mouse.IsPressed(MouseConstant.Left))
        {
            _world.NewRectangleCollider(Mouse.GetX(), Mouse.GetY(), 25, 25);
        }

        if (Keyboard.IsPressed(KeyConstant.Space))
        {
            Window.SetMode(1280, 720);
        }

        if (Keyboard.IsPressed(KeyConstant.Enter))
        {
            Window.SetFullscreen(!Window.GetFullscreen());
        }

        Console.WriteLine(_world.GetColliders().Length);
    }

    public void Draw()
    {
        _world.Draw();

        Graphics.Print("FPS: " + Timer.GetFps(), 10, 10);
    }
}