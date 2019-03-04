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

    [CreateAssetMenu(fileName = "GameSounds",menuName ="Custom Objects/SoundData", order = 8)]
    public class SoundScriptable : ScriptableObject
    {
        public List<SoundData> soundDatas;
    }
}