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
        private List<AudioClip> bgMusic;
        private List<SoundData> soundDatas;

        public SoundManager(SoundScriptable soundScriptable, SignalBus signalBus)
        {
            bgMusic = new List<AudioClip>();
            soundDatas = new List<SoundData>();
            this.signalBus = signalBus;
            this.soundScriptable = soundScriptable;
            soundDatas = soundScriptable.soundDatas;
            signalBus.Subscribe<SignalPlayAudio>(ListenPlayAudioSignal);
            signalBus.Subscribe<SignalPlayOneShot>(ListenPlayOneShotSignal);
            signalBus.Subscribe<SignalStopAudio>(StopFXSound);
            signalBus.Subscribe<StateChangeSignal>(GameStart);
        }

        void GameStart(StateChangeSignal signal)
        {
            if (gameInstaller == null)
                gameInstaller = GameObject.FindObjectOfType<GameInstaller>();

            if(signal.newGameState == Common.GameStatesType.LOBBYSTATE)
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

        public void PlayMusic(SoundName gameSounds)
        {
            if (gameInstaller != null)
            {
                int i = 0;

                if (bgMusic.Count <= 0)
                {
                    bgMusic.Add(ReturnAudio(gameSounds)); 
                }
                else
                {
                    i = Random.Range(0, bgMusic.Count); 
                }


                gameInstaller.musicSource.clip = bgMusic[i];
                if (gameInstaller.musicSource.isPlaying == false)
                    gameInstaller.musicSource.Play();
            }
        }

        public void PlayAudioOneShot(SoundName audioName)
        {
            if (gameInstaller != null)
                gameInstaller.fxSource.PlayOneShot(ReturnAudio(audioName));
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