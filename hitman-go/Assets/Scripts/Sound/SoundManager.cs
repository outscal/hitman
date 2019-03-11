using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using GameState;

namespace SoundSystem
{
    public class SoundManager : ISound
    {
        readonly SignalBus signalBus;
        private GameInstaller gameInstaller;
        private SoundScriptable soundScriptable;
        private List<SoundData> soundDatas;
        private BackgroundMusic backgroundMusics;

        public SoundManager(SoundScriptable soundScriptable, SignalBus signalBus)
        {
            soundDatas = new List<SoundData>();
            this.signalBus = signalBus;
            this.soundScriptable = soundScriptable;
            soundDatas = soundScriptable.soundDatas;
            backgroundMusics = soundScriptable.backgroundMusic;
            signalBus.Subscribe<SignalPlayAudio>(ListenPlayAudioSignal);
            signalBus.Subscribe<SignalPlayOneShot>(ListenPlayOneShotSignal);
            signalBus.Subscribe<SignalStopAudio>(StopFXSound);
            signalBus.Subscribe<StateChangeSignal>(GameStart);
        }

        void GameStart(StateChangeSignal signal)
        {
            if (gameInstaller == null)
                gameInstaller = GameObject.FindObjectOfType<GameInstaller>();

            if(signal.newGameState == Common.GameStatesType.LOBBYSTATE
            || signal.newGameState == Common.GameStatesType.LOADLEVELSTATE)
            {
                PlayMusic(SoundName.gameMusic);
            }

        }

        void ListenPlayAudioSignal(SignalPlayAudio signalPlayAudio)
        {
            PlayMusic(signalPlayAudio.soundName);
        }

        void ListenPlayOneShotSignal(SignalPlayOneShot signalPlayOneShot)
        {
            PlayAudioOneShot(signalPlayOneShot.soundName);
        }

        public void PlayMusic(SoundName gameSounds)
        {
            if (gameInstaller != null)
            {
                gameInstaller.musicSource.clip = ReturnBackground();
                if (gameInstaller.musicSource.isPlaying == false)
                    gameInstaller.musicSource.Play();
            }
        }

        public void PlayAudioOneShot(SoundName audioName)
        {
            if (gameInstaller != null)
                gameInstaller.fxSource.PlayOneShot(ReturnAudio(audioName));
        }

        AudioClip ReturnAudio(SoundName gameSounds)
        {
            AudioClip audioClip = null;
            for (int i = 0; i < soundDatas.Count; i++)
            {
                if (gameSounds == soundDatas[i].gameSounds)
                {
                    audioClip = soundDatas[i].audioClip;
                    break;
                }
            }

            return audioClip;
        }

        AudioClip ReturnBackground()
        {
            return backgroundMusics.audioClips[Random.Range(0, backgroundMusics.audioClips.Count)];
        }

        public void StopFXSound()
        {
            if (gameInstaller != null)
                gameInstaller.fxSource.Stop();
        }

        public void StopBackground()
        {
            if (gameInstaller != null)
                gameInstaller.musicSource.Stop();
        }
    }
}