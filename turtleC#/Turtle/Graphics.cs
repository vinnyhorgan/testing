namespace Turtle;

public enum DrawMode
{
    Fill,
    Line
}

public enum FilterMode
{
    Linear,
    Nearest
}

public class Color
{
    public readonly int R;
    public readonly int G;
    public readonly int B;
    public readonly int A;

    public Color(int r, int g, int b, int a = 255)
    {
        this.R = r;
        this.G = g;
        this.B = b;
        this.A = a;
    }
}

public class Font
{
    internal Raylib_cs.Font RayFont;

    public Font(string filename, int size)
    {
        this.RayFont = Raylib.LoadFontEx(filename, size, Array.Empty<int>(), 0);
    }

    public Font(Raylib_cs.Font font)
    {
        this.RayFont = font;
    }

    public void Release()
    {
        Graphics.LoadedFonts.Remove(this);
        Raylib.UnloadFont(RayFont);
    }
}

public class Image
{
    internal Texture2D RayImage;

    public Image(string filename)
    {
        this.RayImage = Raylib.LoadTexture(filename);
    }

    public void Release()
    {
        Graphics.LoadedImages.Remove(this);
        Raylib.UnloadTexture(RayImage);
    }

    public Vector2 GetDimensions()
    {
        return new Vector2(RayImage.width, RayImage.height);
    }

    public int GetHeight()
    {
        return RayImage.height;
    }

    public int GetWidth()
    {
        return RayImage.width;
    }
}

public static class Graphics
{
    internal static List<Image> LoadedImages = new();
    internal static List<Font> LoadedFonts = new();

    private static Raylib_cs.Color _currentBackgroundColor = new(0, 0, 0, 255);
    private static Raylib_cs.Color _currentColor = new(255, 255, 255, 255);
    private static Font _currentFont = new(Raylib.GetFontDefault());
    private static FilterMode _filter = FilterMode.Linear;
    private static float _lineWidth = 1.0f;
    private static RenderTexture2D _renderTarget;
    private static int _virtualWidth;
    private static int _virtualHeight;
    private static float _windowScale;

    internal static Vector2 GetVirtualSize()
    {
        return new Vector2(_virtualWidth, _virtualHeight);
    }

    internal static void Init(int width, int height)
    {
        _virtualWidth = width;
        _virtualHeight = height;

        _renderTarget = Raylib.LoadRenderTexture(width, height);

        SetDefaultFilter(FilterMode.Linear);
    }

    internal static void Close()
    {
        foreach (Image image in LoadedImages)
        {
            Raylib.UnloadTexture(image.RayImage);
        }

        foreach (Font font in LoadedFonts)
        {
            Raylib.UnloadFont(font.RayFont);
        }

        Raylib.UnloadRenderTexture(_renderTarget);
    }

    public static void Begin()
    {
        if (Raylib.IsWindowResized())
        {
            Window.Width = Raylib.GetScreenWidth();
            Window.Height = Raylib.GetScreenHeight();
        }

        int width = GetWidth();
        int height = GetHeight();

        _windowScale = System.Math.Min((float)width / _virtualWidth, (float)height / _virtualHeight);

        Vector2 mouse = Raylib.GetMousePosition();
        Mouse.SetX((int)((mouse.X - (width - (_virtualWidth * _windowScale)) * 0.5f) / _windowScale));
        Mouse.SetY((int)((mouse.Y - (height - (_virtualHeight * _windowScale)) * 0.5f) / _windowScale));

        Vector2 max = new Vector2((float)width, (float)height);
        Mouse.SetPosition(Vector2.Clamp(Mouse.GetPosition(), Vector2.Zero, max));

        Window.ImguiController.Update(Timer.GetDelta());

        Raylib.BeginDrawing();

        Raylib.ClearBackground(new Raylib_cs.Color(0, 0, 0, 255));

        Raylib.BeginTextureMode(_renderTarget);

        Raylib.ClearBackground(_currentBackgroundColor);

        if (Raylib.WindowShouldClose())
        {
            Window.Quit = true;
        }
    }

    public static void End()
    {
        Raylib.EndTextureMode();

        Rectangle sourceRec = new Rectangle(
            0.0f,
            0.0f,
            (float)_renderTarget.texture.width,
            (float)-_renderTarget.texture.height
        );

        Rectangle destRec = new Rectangle(
            (GetWidth() - ((float)_virtualWidth * _windowScale)) * 0.5f,
            (GetHeight() - ((float)_virtualHeight * _windowScale)) * 0.5f,
            (float)_virtualWidth * _windowScale,
            (float)_virtualHeight * _windowScale
        );

        Raylib.DrawTexturePro(_renderTarget.texture, sourceRec, destRec, new Vector2(0, 0), 0.0f, new Raylib_cs.Color(255, 255, 255, 255));

        Window.ImguiController.Draw();

        Raylib.EndDrawing();
    }

    public static void Circle(DrawMode mode, int x, int y, float radius)
    {
        switch (mode)
        {
            case DrawMode.Fill:
                Raylib.DrawCircle(x, y, radius, _currentColor);
                break;
            case DrawMode.Line:
                Raylib.DrawCircleLines(x, y, radius, _currentColor);
                break;
            default:
                break;
        }
    }

    public static void Circle(DrawMode mode, Vector2 position, float radius)
    {
        switch (mode)
        {
            case DrawMode.Fill:
                Raylib.DrawCircleV(position, radius, _currentColor);
                break;
            case DrawMode.Line:
                Raylib.DrawCircleLines((int)position.X, (int)position.Y, radius, _currentColor);
                break;
            default:
                break;
        }
    }

    public static void Clear()
    {
        Raylib.ClearBackground(_currentBackgroundColor);
    }

    public static void Draw(Image image, int x, int y, float rotation = 0, float scale = 1)
    {
        Raylib.DrawTextureEx(image.RayImage, new Vector2(x, y), rotation, scale, _currentColor);
    }

    public static void Draw(Image image, Vector2 position, float rotation = 0, float scale = 1)
    {
        Raylib.DrawTextureEx(image.RayImage, position, rotation, scale, _currentColor);
    }

    public static void Ellipse(DrawMode mode, int x, int y, float radiusX, float radiusY)
    {
        switch (mode)
        {
            case DrawMode.Fill:
                Raylib.DrawEllipse(x, y, radiusX, radiusY, _currentColor);
                break;
            case DrawMode.Line:
                Raylib.DrawEllipseLines(x, y, radiusX, radiusY, _currentColor);
                break;
            default:
                break;
        }
    }

    public static void Ellipse(DrawMode mode, Vector2 position, float radiusX, float radiusY)
    {
        switch (mode)
        {
            case DrawMode.Fill:
                Raylib.DrawEllipse((int)position.X, (int)position.Y, radiusX, radiusY, _currentColor);
                break;
            case DrawMode.Line:
                Raylib.DrawEllipseLines((int)position.X, (int)position.Y, radiusX, radiusY, _currentColor);
                break;
            default:
                break;
        }
    }

    public static void Line(int x1, int y1, int x2, int y2)
    {
        Raylib.DrawLineEx(new Vector2(x1, y1), new Vector2(x2, y2), _lineWidth, _currentColor);
    }

    public static void Line(Vector2 pos1, Vector2 pos2)
    {
        Raylib.DrawLineEx(pos1, pos2, _lineWidth, _currentColor);
    }

    public static void Point(int x, int y)
    {
        Raylib.DrawPixel(x, y, _currentColor);
    }

    public static void Points(params Vector2[] points)
    {
        foreach (Vector2 point in points)
        {
            Raylib.DrawPixelV(point, _currentColor);
        }
    }

    public static void Polygon(DrawMode mode, int x, int y, int radius, int sides, float rotation = 0)
    {
        switch (mode)
        {
            case DrawMode.Fill:
                Raylib.DrawPoly(new Vector2(x, y), sides, radius, rotation, _currentColor);
                break;
            case DrawMode.Line:
                Raylib.DrawPolyLinesEx(new Vector2(x, y), sides, radius, rotation, _lineWidth, _currentColor);
                break;
            default:
                break;
        }
    }

    public static void Polygon(DrawMode mode, Vector2 position, int radius, int sides, float rotation = 0)
    {
        switch (mode)
        {
            case DrawMode.Fill:
                Raylib.DrawPoly(position, sides, radius, rotation, _currentColor);
                break;
            case DrawMode.Line:
                Raylib.DrawPolyLinesEx(position, sides, radius, rotation, _lineWidth, _currentColor);
                break;
            default:
                break;
        }
    }

    public static void Print(string text, int x, int y)
    {
        Raylib.DrawTextEx(_currentFont.RayFont, text, new Vector2(x, y), _currentFont.RayFont.baseSize, 0, _currentColor);
    }

    public static void Print(string text, Vector2 position)
    {
        Raylib.DrawTextEx(_currentFont.RayFont, text, position, _currentFont.RayFont.baseSize, 0, _currentColor);
    }

    public static void Rectangle(DrawMode mode, int x, int y, int width, int height)
    {
        switch (mode)
        {
            case DrawMode.Fill:
                Raylib.DrawRectangle(x, y, width, height, _currentColor);
                break;
            case DrawMode.Line:
                Raylib.DrawRectangleLinesEx(new Rectangle(x, y, width, height), _lineWidth, _currentColor);
                break;
            default:
                break;
        }
    }

    public static void Rectangle(DrawMode mode, Vector2 position, Vector2 size)
    {
        switch (mode)
        {
            case DrawMode.Fill:
                Raylib.DrawRectangleV(position, size, _currentColor);
                break;
            case DrawMode.Line:
                Raylib.DrawRectangleLinesEx(new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y), _lineWidth, _currentColor);
                break;
            default:
                break;
        }
    }

    public static void CaptureScreenshot(string filename)
    {
        Raylib.TakeScreenshot(filename);
    }

    public static Font NewFont(string filename, int size)
    {
        Font newFont = new(filename, size);
        LoadedFonts.Add(newFont);

        return newFont;
    }

    public static Image NewImage(string filename)
    {
        Image newImage = new(filename);
        LoadedImages.Add(newImage);

        return newImage;
    }

    public static Font SetNewFont(string filename, int size)
    {
        Font newFont = new(filename, size);
        LoadedFonts.Add(newFont);

        SetFont(newFont);

        return newFont;
    }

    public static Color GetBackgroundColor()
    {
        return new Color(_currentBackgroundColor.r, _currentBackgroundColor.g, _currentBackgroundColor.b, _currentBackgroundColor.a);
    }

    public static Color GetColor()
    {
        return new Color(_currentColor.r, _currentColor.g, _currentColor.b, _currentColor.a);
    }

    public static FilterMode GetDefaultFilter()
    {
        return _filter;
    }

    public static Font GetFont()
    {
        return _currentFont;
    }

    public static float GetLineWidth()
    {
        return _lineWidth;
    }

    public static void SetBackgroundColor(Color color)
    {
        _currentBackgroundColor = new Raylib_cs.Color(color.R, color.G, color.B, color.A);
    }

    public static void SetColor(Color color)
    {
        _currentColor = new Raylib_cs.Color(color.R, color.G, color.B, color.A);
    }

    public static void SetDefaultFilter(FilterMode filter)
    {
        switch (filter)
        {
            case FilterMode.Linear:
                Raylib.SetTextureFilter(_renderTarget.texture, TextureFilter.TEXTURE_FILTER_BILINEAR);
                break;
            case FilterMode.Nearest:
                Raylib.SetTextureFilter(_renderTarget.texture, TextureFilter.TEXTURE_FILTER_POINT);
                break;
        }

        _filter = filter;
    }

    public static void SetFont(Font font)
    {
        _currentFont = font;
    }

    public static void SetLineWidth(float width)
    {
        _lineWidth = width;
    }

    public static Vector2 GetDpiScale()
    {
        return Raylib.GetWindowScaleDPI();
    }

    public static Vector2 GetDimensions()
    {
        return new Vector2(GetWidth(), GetHeight());
    }

    public static int GetHeight()
    {
        return Window.Height;
    }

    public static int GetWidth()
    {
        return Window.Width;
    }
}