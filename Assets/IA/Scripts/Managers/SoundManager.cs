using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Enum_Sound
{
	Bgm,
	Effect,
	Speech,
	Max,
}

public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    private AudioSource[] _audioSources = new AudioSource[(int)Enum_Sound.Max];
    private Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
    
    private void Awake()
    {
        Object.DontDestroyOnLoad(this.gameObject);

        string[] soundTypeNames = System.Enum.GetNames(typeof(Enum_Sound));
        for (int count = 0; count < soundTypeNames.Length - 1; count++)
        {
            GameObject go = new GameObject { name = soundTypeNames[count] };
            _audioSources[count] = go.AddComponent<AudioSource>();
            go.transform.parent = this.transform;
        }

        _audioSources[(int)Enum_Sound.Bgm].loop = true;
    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
            audioSource.Stop();
        _audioClips.Clear();
    }

    public void SetPitch(Enum_Sound type, float pitch = 1.0f)
	{
		AudioSource audioSource = _audioSources[(int)type];
        if (audioSource == null)
            return;

        audioSource.pitch = pitch;
	}

    public bool Play(Enum_Sound type, string path, float volume = 1.0f, float pitch = 1.0f)
    {
        if (string.IsNullOrEmpty(path))
            return false;

        AudioSource audioSource = _audioSources[(int)type];
        if (path.Contains("Sound/") == false)
            path = string.Format("Sound/{0}", path);
        
        
        if (type == Enum_Sound.Bgm)
        {
            AudioClip audioClip = ResourceManager.Instance.Load<AudioClip>(path);
            if (audioClip == null)
                return false;

            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = audioClip;
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.Play();
            return true;
        }
        else if (type == Enum_Sound.Effect)
        {
            AudioClip audioClip = GetAudioClip(path);
            if (audioClip == null)
                return false;
            
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
            return true;
        }
        else if (type == Enum_Sound.Speech)
		{
			AudioClip audioClip = GetAudioClip(path);
			if (audioClip == null)
				return false;

			if (audioSource.isPlaying)
				audioSource.Stop();
            
            audioSource.volume = volume;
            audioSource.pitch = pitch;
			audioSource.clip = audioClip;
            audioSource.Play();
			return true;
		}

        return false;
    }

    public void Stop(Enum_Sound type)
	{
        AudioSource audioSource = _audioSources[(int)type];
        audioSource.Stop();
    }

	public float GetAudioClipLength(string path)
    {
        AudioClip audioClip = GetAudioClip(path);
        if (audioClip == null)
            return 0.0f;
        return audioClip.length;
    }

    private AudioClip GetAudioClip(string path)
    {
        AudioClip audioClip = null;
        if (_audioClips.TryGetValue(path, out audioClip))
            return audioClip;

        audioClip = ResourceManager.Instance.Load<AudioClip>(path);
        _audioClips.Add(path, audioClip);
        return audioClip;
    }

    public void PlayRandomAttack(float volume = 0.7f, float pitch = 1f)
    {
        var random = Random.Range(0, 101);

        var sound = random > 50 ? "Sound_Attack0" : "Sound_Attack1";

        Play(Enum_Sound.Effect, sound,volume,pitch);
    }
}
