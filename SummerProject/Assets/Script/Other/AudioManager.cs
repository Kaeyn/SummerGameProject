using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SoundType{
    ROCKET,
    EXPLOSION,
    CHAINSAW,
    CHAIN,
    WALL,
    PLAYER_SHOOTING,
    PLAYER_HURT,
    PLAYER_MOVE,
    CAPY_SHOOTING,
    BOSS_LASER,
    COIN

}
// [ExecuteInEditMode]
public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioClip backgroundMusic;
    [SerializeField] SoundList[] soundList;
    // [SerializeField] AudioClip SFXSourceAudio;
    private void Awake() {
        instance = this;
    }
    void Start()
    {
        if(backgroundMusic != null){
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
    }

    void Update()
    {
        
    }
    public static void PlaySFX(SoundType sound, float volume = 1){
        AudioClip[] audioClips = instance.soundList[(int)sound].Sounds;
        AudioClip randomClip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
        instance.SFXSource.PlayOneShot(randomClip, volume);
    }
    #if UNITY_EDITOR
    private void OnEnable() {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList,names.Length);
        for (int i = 0; i < soundList.Length; i++)
        {
            soundList[i].name = names[i];
        }
    }
    #endif
}
[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds{get => sounds;}
    [HideInInspector] public string name;
    public AudioClip[] sounds;
}