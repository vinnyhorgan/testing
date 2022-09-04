namespace Turtle;

public class Source
{
    internal Sound RaySound;

    private bool _paused;
    private float _pitch = 1.0f;
    private float _volume = 1.0f;

    public Source(string filename)
    {
        this.RaySound = Raylib.LoadSound(filename);
    }

    public void Release()
    {
        Audio.LoadedEffects.Remove(this);
        Raylib.UnloadSound(RaySound);
    }

    public float GetPitch()
    {
        return _pitch;
    }

    public float GetVolume()
    {
        return _volume;
    }

    public bool IsPaused()
    {
        return _paused;
    }

    public bool IsPlaying()
    {
        return Raylib.IsSoundPlaying(RaySound);
    }

    public bool IsStopped()
    {
        return !Raylib.IsSoundPlaying(RaySound);
    }

    public void Pause()
    {
        Raylib.PauseSound(RaySound);
        _paused = true;
    }

    public void Play()
    {
        if (_paused)
        {
            Raylib.ResumeSound(RaySound);
            _paused = false;
        }
        else
        {
            Raylib.PlaySound(RaySound);
        }
    }

    public void SetPitch(float pitch)
    {
        Raylib.SetSoundPitch(RaySound, pitch);
        _pitch = pitch;
    }

    public void SetVolume(float volume)
    {
        Raylib.SetSoundVolume(RaySound, volume);
        _volume = volume;
    }

    public void Stop()
    {
        Raylib.StopSound(RaySound);
    }
}

public static class Audio
{
    internal static List<Source> LoadedEffects = new();

    private static float _volume = 1.0f;

    internal static void Init()
    {
        Raylib.InitAudioDevice();
    }

    internal static void Close()
    {
        Raylib.CloseAudioDevice();

        foreach (Source source in LoadedEffects)
        {
            Raylib.UnloadSound(source.RaySound);
        }
    }

    public static Source[] GetActiveEffects()
    {
        List<Source> activeEffects = new();

        foreach (Source source in LoadedEffects)
        {
            if (source.IsPlaying())
            {
                activeEffects.Add(source);
            }
        }

        return activeEffects.ToArray();
    }

    public static int GetActiveSourceCount()
    {
        List<Source> activeEffects = new();

        foreach (Source source in LoadedEffects)
        {
            if (source.IsPlaying())
            {
                activeEffects.Add(source);
            }
        }

        return activeEffects.Count;
    }

    public static float GetVolume()
    {
        return _volume;
    }

    public static Source NewSource(string filename)
    {
        Source newSource = new(filename);
        LoadedEffects.Add(newSource);

        return newSource;
    }

    public static Source[] Pause()
    {
        List<Source> activeEffects = new();

        foreach (Source source in LoadedEffects)
        {
            if (source.IsPlaying())
            {
                activeEffects.Add(source);
                source.Pause();
            }
        }

        return activeEffects.ToArray();
    }

    public static void Pause(params Source[] sources)
    {
        foreach (Source source in sources)
        {
            source.Pause();
        }
    }

    public static void Play(params Source[] sources)
    {
        foreach (Source source in sources)
        {
            source.Play();
        }
    }

    public static void SetVolume(float volume)
    {
        Raylib.SetMasterVolume(volume);
        _volume = volume;
    }

    public static void Stop()
    {
        foreach (Source source in LoadedEffects)
        {
            if (source.IsPlaying())
            {
                source.Stop();
            }
        }
    }

    public static void Stop(params Source[] sources)
    {
        foreach (Source source in sources)
        {
            source.Stop();
        }
    }
}