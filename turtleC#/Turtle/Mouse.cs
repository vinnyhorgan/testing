namespace Turtle;

public enum MouseConstant
{
    Left = 0,
    Right = 1,
    Middle = 2,
    Side = 3,
    Extra = 4,
    Forward = 5,
    Back = 6,
}

public enum CursorType
{
    Default = 0,
    Arrow = 1,
    Ibeam = 2,
    Crosshair = 3,
    PointingHand = 4,
    ResizeEw = 5,
    ResizeNs = 6,
    ResizeNwse = 7,
    ResizeNesw = 8,
    ResizeAll = 9,
    NotAllowed = 10
}

public static class Mouse
{
    private static CursorType _cursor = CursorType.Default;
    private static bool _grabbed = false;
    private static bool _visible = true;
    private static int _x;
    private static int _y;

    public static CursorType GetCursor()
    {
        return _cursor;
    }

    public static Vector2 GetPosition()
    {
        return new Vector2(_x, _y);
    }

    public static int GetX()
    {
        return _x;
    }

    public static int GetY()
    {
        return _y;
    }

    public static bool IsDown(MouseConstant button)
    {
        return Raylib.IsMouseButtonDown((MouseButton)button);
    }

    public static bool IsGrabbed()
    {
        return _grabbed;
    }

    public static bool IsPressed(MouseConstant button)
    {
        return Raylib.IsMouseButtonPressed((MouseButton)button);
    }

    public static bool IsReleased(MouseConstant button)
    {
        return Raylib.IsMouseButtonReleased((MouseButton)button);
    }

    public static bool IsVisible()
    {
        return _visible;
    }

    public static float IsWheelMoved()
    {
        return Raylib.GetMouseWheelMove();
    }

    public static void SetCursor(CursorType type)
    {
        Raylib.SetMouseCursor((MouseCursor)type);

        _cursor = type;
    }

    public static void SetGrabbed(bool grabbed)
    {
        if (grabbed)
        {
            Raylib.DisableCursor();
            _grabbed = true;
        }
        else
        {
            Raylib.EnableCursor();
            _grabbed = false;
        }
    }

    public static void SetPosition(int x, int y)
    {
        _x = x;
        _y = y;
    }

    public static void SetPosition(Vector2 position)
    {
        _x = (int)position.X;
        _y = (int)position.Y;
    }

    public static void SetVisible(bool visible)
    {
        if (visible)
        {
            Raylib.ShowCursor();
            _visible = true;
        }
        else
        {
            Raylib.HideCursor();
            _visible = false;
        }
    }

    public static void SetX(int x)
    {
        _x = x;
    }

    public static void SetY(int y)
    {
        _y = y;
    }
}