namespace Turtle;

class Con

public class Game
{
    protected virtual void Conf()
    {

    }

    protected virtual void Load()
    {

    }

    protected virtual void Update(float dt)
    {

    }

    protected virtual void Draw()
    {

    }

    public void Run()
    {
        Conf();

        Window.Init(800, 600, "Turtle Demo", true);

        Load();

        while (!Window.ShouldClose())
        {
            float dt = Timer.GetDelta();

            Update(dt);

            Graphics.Begin();

            Draw();

            Graphics.End();
        }

        Window.Close();
    }
}