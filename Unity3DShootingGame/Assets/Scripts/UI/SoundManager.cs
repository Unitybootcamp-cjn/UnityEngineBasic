using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;
//1. ���� ������ ���� enum�� �����մϴ�.


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
    //2. Ŭ������ �ʿ��� �ʵ� �� ����
    [Header("����� �ͼ�")]
    public AudioMixer audioMixer;
    public string bgmParameter = "BGM"; // ����� �ͼ��� ������ �̸�
    public string sfxParameter = "SFX";

    [Header("����� �ҽ�")]
    public AudioSource bgm;
    public AudioSource sfx;

    [Header("����� Ŭ��")]
    public List<BGMClip> bgm_list;
    public List<SFXClip> sfx_list;

    private Dictionary<BGM, AudioClip> bgm_dict; // BGM ������ ���� ����� Ŭ��
    private Dictionary<SFX, AudioClip> sfx_dict; // SFX ������ ���� ����� Ŭ��

    //3. ���� �Ŵ����� ��ü ���ӿ��� 1���� �ʿ��ϴ�. (�̱���)
    // ������Ƽ ���·� ������ �ν��Ͻ�
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


