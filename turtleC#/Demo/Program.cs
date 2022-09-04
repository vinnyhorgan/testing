namespace Demo;

public static class Program
{
    public static void Main()
    {
        Demo demo = new();

        Window.Init(800, 600, "Turtle Demo", true);

        Graphics.SetNewFont(Filesystem.GetWorkingDirectory() + "/Assets/Vera.ttf", 32);

        demo.Load();

        while (!Window.ShouldClose())
        {
            float dt = Timer.GetDelta();

            demo.Update(dt);

            Graphics.Begin();

            demo.Draw();

            Graphics.End();
        }

        Window.Close();
    }
}