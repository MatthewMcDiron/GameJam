using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager Inst;

    AudioSource mMusicSource;
    AudioSource[] mSFXSources;

    Dictionary<string, AudioSource> mSFXDictionarySources;

    public AudioClip MainMenuMusic;
    public AudioClip LevelMusic;
    public AudioClip VictoryJingle;

    public AudioClip PowerUpSFX;
    public AudioClip PaintBombSFX;
    public AudioClip EnemyFrozenSFX;
    public AudioClip CloneAppearingSFX;
    public AudioClip CloneDisappearingSFX;
    public AudioClip MenuItemEnterSFX;
    public AudioClip MenuItemClickedSFX;
    public AudioClip PaintSFX;

    public static AudioManager Instance()
    {
        return Inst;
    }

    private void Awake()
    {
        if (Inst == null) { Inst = this; }
        else { Destroy(gameObject); }

        mMusicSource = new GameObject().AddComponent<AudioSource>();
        mMusicSource.gameObject.name = "Music Source";

        mSFXSources = new AudioSource[10];
        for (int i = 0; i < mSFXSources.Length; i++)
        {
            mSFXSources[i] = new GameObject().AddComponent<AudioSource>();
            mSFXSources[i].gameObject.name = "SFX Source " + i;
        }

        mSFXDictionarySources = new Dictionary<string, AudioSource>();
    }

    public void PlayMusic(AudioClip _track, bool _loop = true)
    {
        if (_track != null)
        {
            mMusicSource.clip = _track;
            mMusicSource.loop = _loop;
            mMusicSource.Play();
        }
    }
    public void PauseMusic() { mMusicSource.Pause(); }
    public void StopMusic() { mMusicSource.Stop(); }
    public void ResumeMusic() { if (mMusicSource.clip != null) { mMusicSource.Play(); } }

    public void PlaySFX(AudioClip _sfx, string _channel = "", bool _overridechannel = true)
    {
        if (_sfx != null)
        {
            //If Channel exists
            if (_channel != null && mSFXDictionarySources.ContainsKey(_channel))
            {
                if (!mSFXDictionarySources[_channel].isPlaying || _overridechannel)
                {
                    mSFXDictionarySources[_channel].clip = _sfx;
                    mSFXDictionarySources[_channel].Play();
                    return;
                }
            }
            for (int i = 0; i < mSFXSources.Length; i++)
            {
                if (mSFXSources[i] != null)
                {
                    if (!mSFXSources[i].isPlaying)
                    {
                        if (_channel != "" && mSFXDictionarySources.ContainsValue(mSFXSources[i])) { continue; }
                        else
                        {
                            mSFXSources[i].clip = _sfx;
                            mSFXSources[i].Play();
                            if (_channel != "") { mSFXDictionarySources.Add(_channel, mSFXSources[i]); }
                            break;
                        }
                    }
                }
            }
        }
    }

    public void StopSFXAtChannel(string _channel)
    {
        if (mSFXDictionarySources.ContainsKey(_channel))
        {
            mSFXDictionarySources[_channel].Stop();
            mSFXDictionarySources[_channel].clip = null;
        }
    }

    public void PauseAllSFX()
    {
        for (int i = 0; i < mSFXSources.Length; i++)
        {
            if (mSFXSources[i] != null) { mSFXSources[i].Pause(); }
        }
    }

    public void StopAllSFX()
    {
        for (int i = 0; i < mSFXSources.Length; i++)
        {
            if (mSFXSources[i] != null) { mSFXSources[i].Stop(); }
        }
    }

    public void ResumeAllSFX()
    {
        for (int i = 0; i < mSFXSources.Length; i++)
        {
            if (mSFXSources[i] != null) { mSFXSources[i].Play(); }
        }
    }
}
