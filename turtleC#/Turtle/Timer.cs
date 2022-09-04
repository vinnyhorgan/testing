namespace Turtle;

public static class Timer
{
    public static float GetDelta()
    {
        return Raylib.GetFrameTime();
    }

    public static int GetFps()
    {
        return Raylib.GetFPS();
    }

    public static double GetTime()
    {
        return Raylib.GetTime();
    }

    public static void Sleep(float seconds)
    {
        Thread.Sleep((int)(seconds * 1000));
    }
}