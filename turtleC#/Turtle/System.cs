namespace Turtle;

public enum Os
{
    Linux,
    Macos,
    Other,
    Windows
}

public static class SystemT
{
    public static string GetClipboardText()
    {
        return Raylib.GetClipboardText_();
    }

    public static Os GetOs()
    {
        Os os;

        switch (Environment.OSVersion.Platform)
        {
            case PlatformID.Unix:
                os = Os.Linux;
                break;
            case PlatformID.MacOSX:
                os = Os.Macos;
                break;
            case PlatformID.Win32NT:
                os = Os.Windows;
                break;
            default:
                os = Os.Other;
                break;
        }

        return os;
    }

    public static int GetProcessorCount()
    {
        return Environment.ProcessorCount;
    }

    public static void OpenUrl(string url)
    {
        byte[] bytes = Encoding.ASCII.GetBytes(url);

        unsafe
        {
            fixed (byte* p = bytes)
            {
                sbyte* sp = (sbyte*)p;

                Raylib.OpenURL(sp);
            }
        }
    }

    public static void SetClipboardText(string text)
    {
        Raylib.SetClipboardText(text);
    }
}