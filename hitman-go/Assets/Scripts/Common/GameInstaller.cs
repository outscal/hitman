using UnityEngine;
using Zenject;
using Player;
using Enemy;
using InputSystem;
using PathSystem;
using System.Collections;
using Common;
using GameState;
using InteractableSystem;
using StarSystem;
using UIservice;
using SavingSystem;
using CameraSystem;
using SoundSystem;


public class GameInstaller : MonoInstaller
{
    public AudioSource musicSource, fxSource;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<PlayerMoveSignal>();
        Container.DeclareSignal<PlayerSpawnSignal>();
        Container.DeclareSignal<PlayerDeathSignal>();
        Container.DeclareSignal<PlayerKillSignal>();
        Container.DeclareSignal<EnemyDeathSignal>();
        Container.DeclareSignal<GameStartSignal>();
        Container.DeclareSignal<ResetSignal>();
        Container.DeclareSignal<LevelFinishedSignal>();
        Container.DeclareSignal<StateChangeSignal>();
        Container.DeclareSignal<SignalAlertGuards>();
        Container.DeclareSignal<BriefCaseSignal>();
        Container.DeclareSignal<SignalPlayAudio>();
        Container.DeclareSignal<SignalPlayOneShot>();
        Container.DeclareSignal<EnemyKillSignal>();
        Container.DeclareSignal<SignalStopAudio>();

        Container.Bind<ISound>()
            .To<SoundManager>()
            .AsSingle()
            .NonLazy();

        Container.Bind<IUIService>()
                   .To<UIService>()
                   .AsSingle()
                   .NonLazy();
        Container.Bind<IEnemyService>()
            .To<EnemyService>()
            .AsSingle()
            .NonLazy();
        Container.Bind<IPlayerService>()
            .To<PlayerService>()
            .AsSingle()
            .NonLazy();
        Container.Bind<IStarService>()
        .To<StarSystemService>()
        .AsSingle()
        .NonLazy();
        Container.Bind(typeof(IInputService), typeof(ITickable))
            .To<InputService>()
            .AsSingle()
            .NonLazy();

        Container.Bind<IInteractable>()
        .To<InteractableManager>()
        .AsSingle()
        .NonLazy();
        Container.Bind<ICamera>()
        .To<CameraManager>()
        .AsSingle()
        .NonLazy();

        Container.Bind<IPathService>()
            .To<PathService>()
            .AsSingle()
            .NonLazy();
            Container.Bind<ISaveService>()
            .To<SaveService>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<GameService>()
            .AsSingle()
            .NonLazy();
    }
}
