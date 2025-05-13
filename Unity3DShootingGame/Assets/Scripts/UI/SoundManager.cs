using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;
//1. 사운드 구분을 위한 enum을 설계합니다.


public enum BGM
{
    Title,InGame,Boss
}
public enum SFX
{
    Bullet, Bomb
}

[Serializable]
public class BGMClip
{
    public BGM type;
    public AudioClip clip;
}
[Serializable]
public class SFXClip
{
    public SFX type;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    //2. 클래스에 필요한 필드 값 설계
    [Header("오디오 믹서")]
    public AudioMixer audioMixer;
    public string bgmParameter = "BGM"; // 오디오 믹서에 만들어둔 이름
    public string sfxParameter = "SFX";

    [Header("오디오 소스")]
    public AudioSource bgm;
    public AudioSource sfx;

    [Header("오디오 클립")]
    public List<BGMClip> bgm_list;
    public List<SFXClip> sfx_list;

    private Dictionary<BGM, AudioClip> bgm_dict; // BGM 유형에 따른 오디오 클립
    private Dictionary<SFX, AudioClip> sfx_dict; // SFX 유형에 따른 오디오 클립

    //3. 사운드 매니저는 전체 게임에서 1개만 필요하다. (싱글톤)
    // 프로퍼티 형태로 만들어보는 인스턴스
    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}


