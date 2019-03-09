using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundSystem
{
    [System.Serializable]
    public struct SoundData
    {
        public string name;
        public SoundName gameSounds;
        public AudioClip audioClip; 
    }

    [System.Serializable]
    public struct BackgroundMusic
    {
        public SoundName soundName;
        public List<AudioClip> audioClips;
    }

    [CreateAssetMenu(fileName = "GameSounds",menuName ="Custom Objects/SoundData", order = 8)]
    public class SoundScriptable : ScriptableObject
    {
        public BackgroundMusic backgroundMusic;
        public List<SoundData> soundDatas;
    }
}