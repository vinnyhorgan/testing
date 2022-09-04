namespace Turtle;

public enum FileType
{
    Directory,
    File,
    Symlink
}

public class PathInfo
{
    public bool Exists;
    public DateTime ModTime;
    public long Size;
    public FileType Type;
}

public static class Filesystem
{
    public static void Append(string path, string data)
    {
        File.AppendAllText(path, data);
    }

    public static void CreateDirectory(string path)
    {
        Directory.CreateDirectory(path);
    }

    public static string GetAppdataDirectory()
    {
        return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    }

    public static string[] GetDirectoryItems(string directory)
    {
        return Directory.GetFileSystemEntries(directory);
    }

    public static PathInfo GetInfo(string path)
    {
        PathInfo info = new();

        FileAttributes attributes = File.GetAttributes(path);

        switch (attributes)
        {
            case FileAttributes.Directory:
                if (Directory.Exists(path))
                {
                    info.Exists = true;

                    if (new DirectoryInfo(path).Attributes.HasFlag(FileAttributes.ReparsePoint))
                    {
                        info.Type = FileType.Symlink;
                    }
                    else
                    {
                        info.Type = FileType.Directory;
                    }
                }
                else
                {
                    info.Exists = false;
                }

                break;
            default:
                if (File.Exists(path))
                {
                    info.Exists = true;
                    info.ModTime = File.GetLastWriteTime(path);
                    info.Size = new FileInfo(path).Length;

                    if (new FileInfo(path).Attributes.HasFlag(FileAttributes.ReparsePoint))
                    {
                        info.Type = FileType.Symlink;
                    }
                    else
                    {
                        info.Type = FileType.File;
                    }
                }
                else
                {
                    info.Exists = false;
                }

                break;
        }

        return info;
    }

    public static string GetRealDirectory(string path)
    {
        return Path.GetFullPath(path);
    }

    public static string GetUserDirectory()
    {
        return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    }

    public static string GetWorkingDirectory()
    {
        return Directory.GetCurrentDirectory();
    }

    public static string[] Lines(string path)
    {
        return File.ReadAllLines(path);
    }

    public static string Read(string path)
    {
        return File.ReadAllText(path);
    }

    public static void Remove(string path)
    {
        FileAttributes attributes = File.GetAttributes(path);

        switch (attributes)
        {
            case FileAttributes.Directory:
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }

                break;
            default:
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                break;
        }
    }

    public static void Write(string path, string data)
    {
        File.WriteAllText(path, data);
    }
}