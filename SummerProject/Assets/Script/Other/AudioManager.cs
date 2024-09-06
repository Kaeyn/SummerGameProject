using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum SoundType{
    ROCKET_MOVING,
    EXPLOSION,
    SHIELDED,
    SHIELD_DEPLET,
    PLAYER_SHOOTING,
    COIN,
    BOSS_HURT,
    BOSS_SCREAM,
    VICTORY,
    GAMEOVER

}
[ExecuteInEditMode]
public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;
    [SerializeField] SoundList[] soundList;
    private void Awake() {
        instance = this;
    }
    void Start()
    {
        if(PlayerPrefs.HasKey("musicVol")){
            loadVol();
        }else{
            setMusicVolume();
            setSFXVolume();
        }
        
    }

    void Update()
    {
        
    }
    public static void muteMusic(bool mute){
        instance.musicSource.mute = mute;
    }
    void loadVol(){
        musicSlider.value = PlayerPrefs.GetFloat("musicVol", 0);
        SFXSlider.value = PlayerPrefs.GetFloat("sfxVol", 0);
        setMusicVolume();
        setSFXVolume();
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
    public void setMusicVolume(){
        instance.musicSource.volume = musicSlider.value;
        PlayerPrefs.SetFloat("musicVol",musicSlider.value);
    }
    public void setSFXVolume(){
        instance.SFXSource.volume = SFXSlider.value;
        PlayerPrefs.SetFloat("sfxVol",SFXSlider.value);
    }
}
[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds{get => sounds;}
    [HideInInspector] public string name;
    public AudioClip[] sounds;
}