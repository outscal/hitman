using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundSystem
{
    public interface ISound
    {
        void PlayMusic(SoundName gameSounds);
        void StopFXSound();
        void StopBackground();
        void PlayAudioOneShot(SoundName gameSounds);
    }
}