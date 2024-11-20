using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] AudioSource seAudioSource;

    [SerializeField] List<BGMSoundData> bgmSoundDatas;
    [SerializeField] List<SESoundData> seSoundDatas;

    public float masterVolume;
    public float bgmMasterVolume;
    public float seMasterVolume;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(BGMSoundData.BGM bgm)//BGM再生
    {
        BGMSoundData data = bgmSoundDatas.Find(data => data.bgm == bgm);
        bgmAudioSource.clip = data.audioClip;
        bgmAudioSource.volume = data.volume * bgmMasterVolume * masterVolume;
        bgmAudioSource.Play();
    }


    public void PlaySE(SESoundData.SE se)//SE再生
    {
        SESoundData data = seSoundDatas.Find(data => data.se == se);
        seAudioSource.volume = data.volume * seMasterVolume * masterVolume;
        seAudioSource.PlayOneShot(data.audioClip);
    }

    public void PauseandResume(BGMSoundData.BGM bgm)
    {
        bgmAudioSource.Pause();
        BGMSoundData data = bgmSoundDatas.Find(data => data.bgm == bgm);
        bgmAudioSource.volume = data.volume * bgmMasterVolume * masterVolume;
        bgmAudioSource.UnPause();
    }
}

[System.Serializable]
public class BGMSoundData
{
    public enum BGM
    {
        Title,
        LevelSelect,
        Stage,
        Result,
        Ending,
        Stage2,
        Stage3,
        Stage4
    }

    public BGM bgm;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 1;
}

[System.Serializable]
public class SESoundData
{
    public enum SE
    {
        Cursor,
        Select,
        Jump,
        Flip,
        SwapTrail,
        Death,
        Door,
        ItemGet,
        Damage,
        Line,
        CannonShoot,
        ResultHead,
        ResultRank,
        SceneLoad,
        KeyOpen,
        DoorOpen,
        Pause,
        MenuIn,
        MenuOut,
        Steps,
        KeyGet,
        SwapGet,
        DrumRoll,
        PlatformFall

    }

    public SE se;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume = 1;
}