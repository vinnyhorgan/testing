namespace Turtle;

public static class Window
{
    internal static ImguiController ImguiController = new();
    internal static bool Quit = false;
    internal static int Width;
    internal static int Height;

    private static string _title = "";
    private static bool _vsync = true;

    public static void Init(int width, int height, string title, bool resizable = false)
    {
        Raylib.SetTraceLogLevel(TraceLogLevel.LOG_NONE);

        if (resizable)
        {
            Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE);
        }

        Raylib.InitWindow(width, height, title);
        Raylib.SetExitKey(KeyboardKey.KEY_NULL);
        Raylib.SetTargetFPS(Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor()));
        Raylib.SetWindowMinSize(width /2, height / 2);

        _title = title;

        Width = width;
        Height = height;

        Audio.Init();
        Graphics.Init(width, height);

        ImguiController.Load(width, height);
    }

    public static void Close()
    {
        Audio.Close();
        Graphics.Close();

        ImguiController.Dispose();

        Raylib.ClearDroppedFiles();

        Raylib.CloseWindow();
    }

    public static bool ShouldClose()
    {
        return Quit;
    }

    public static Vector2 GetDpiScale()
    {
        return Raylib.GetWindowScaleDPI();
    }

    public static Vector2 GetDesktopDimensions()
    {
        return new Vector2(Raylib.GetMonitorWidth(Raylib.GetCurrentMonitor()), Raylib.GetMonitorHeight(Raylib.GetCurrentMonitor()));
    }

    public static int GetDisplayCount()
    {
        return Raylib.GetMonitorCount();
    }

    public static string GetDisplayName(int index)
    {
        unsafe
        {
            return new string(Raylib.GetMonitorName(index));
        }
    }

    public static bool GetFullscreen()
    {
        return Raylib.IsWindowFullscreen();
    }

    public static Vector2 GetPosition()
    {
        return Raylib.GetWindowPosition();
    }

    public static string GetTitle()
    {
        return _title;
    }

    public static bool GetVSync()
    {
        return _vsync;
    }

    public static bool HasFocus()
    {
        return Raylib.IsWindowFocused();
    }

    public static string[] IsFileDropped()
    {
        string[] files = Array.Empty<string>();

        if (Raylib.IsFileDropped())
        {
            files = Raylib.GetDroppedFiles();
        }

        return files;
    }

    public static bool IsMaximized()
    {
        return Raylib.IsWindowMaximized();
    }

    public static bool IsMinimized()
    {
        return Raylib.IsWindowMinimized();
    }

    public static bool IsOpen()
    {
        return Raylib.IsWindowReady();
    }

    public static bool IsResized()
    {
        return Raylib.IsWindowResized();
    }

    public static bool IsVisible()
    {
        return !Raylib.IsWindowHidden();
    }

    public static void Maximize()
    {
        Raylib.MaximizeWindow();
    }

    public static void Minimize()
    {
        Raylib.MinimizeWindow();
    }

    public static void SetFullscreen(bool fullscreen)
    {
        if (fullscreen)
        {
            if (!Raylib.IsWindowFullscreen())
            {
                if (!Raylib.IsWindowMaximized())
                {
                    if (Graphics.GetVirtualSize() != Graphics.GetDimensions())
                    {
                        SetMode((int)Graphics.GetVirtualSize().X, (int)Graphics.GetVirtualSize().Y);
                    }
                    else
                    {
                        Raylib.ToggleFullscreen();
                    }
                }
            }
        }
        else
        {
            if (Raylib.IsWindowFullscreen())
            {
                Raylib.ToggleFullscreen();
            }
        }
    }

    public static void SetIcon(string filename)
    {
        Raylib.SetWindowIcon(Raylib.LoadImage(filename));
    }

    public static void SetMode(int width, int height)
    {
        if (!Raylib.IsWindowMaximized())
        {
            if (Raylib.IsWindowFullscreen())
            {
                SetFullscreen(false);
            }

            Raylib.SetWindowSize(width, height);
            Graphics.Init(width, height);

            Window.Width = width;
            Window.Height = height;
        }
    }

    public static void SetPosition(int x, int y)
    {
        Raylib.SetWindowPosition(x, y);
    }

    public static void SetTitle(string title)
    {
        Raylib.SetWindowTitle(title);

        _title = title;
    }

    public static void SetVSync(bool vsync)
    {
        if (vsync)
        {
            if (!_vsync)
            {
                Raylib.SetTargetFPS(Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor()));
                _vsync = true;
            }
        }
        else
        {
            if (_vsync)
            {
                Raylib.SetTargetFPS(0);
                _vsync = false;
            }
        }
    }
}