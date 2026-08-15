using Plugin.Maui.Audio;

namespace MusicApp.Services;

public class AudioService
{
    private readonly IAudioManager _audioManager;
    private IAudioPlayer? _player;

    public event Action? PlaybackEnded;

    public AudioService(IAudioManager audioManager)
    {
        _audioManager = audioManager;
    }

    public async Task PlayAsync(string audioFile)
    {
        Stop();

        var stream = await FileSystem.OpenAppPackageFileAsync(audioFile);

        _player = _audioManager.CreatePlayer(stream);

        _player.PlaybackEnded += Player_PlaybackEnded;

        _player.Play();
    }

    private void Player_PlaybackEnded(object? sender, EventArgs e)
    {
        PlaybackEnded?.Invoke();
    }

    public void Pause()
    {
        if (_player?.IsPlaying == true)
        {
            _player.Pause();
        }
    }

    public void Resume()
    {
        if (_player != null && !_player.IsPlaying)
        {
            _player.Play();
        }
    }

    public void Stop()
    {
        if (_player == null)
            return;

        _player.PlaybackEnded -= Player_PlaybackEnded;

        _player.Stop();
        _player.Dispose();

        _player = null;
    }

    public void Seek(double seconds)
    {
        if (_player == null)
            return;

        _player.Seek(seconds);
    }

    public bool IsPlaying =>
        _player?.IsPlaying ?? false;

    public double Duration =>
        _player?.Duration ?? 0;

    public double CurrentPosition =>
        _player?.CurrentPosition ?? 0;
}