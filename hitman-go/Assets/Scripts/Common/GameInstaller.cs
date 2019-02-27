using UnityEngine;
using Zenject;
using Player;
using Enemy;
using InputSystem;
using PathSystem;
using System.Collections;
using Common;
using GameState;
using GameState.Signals;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<PlayerMoveSignal>();
        Container.DeclareSignal<PlayerSpawnSignal>();
        Container.DeclareSignal<PlayerDeathSignal>();
        Container.DeclareSignal<PlayerKillSignal>();
        Container.DeclareSignal<EnemyDeathSignal>();
        Container.DeclareSignal<StateChangeSignal>();
        

        Container.Bind<IEnemyService>()
            .To<EnemyService>()
            .AsSingle()
            .NonLazy();
        Container.Bind<IPlayerService>()
            .To<PlayerService>()
            .AsSingle()
            .NonLazy();

        Container.Bind(typeof(IInputService),typeof(ITickable))
            .To<InputService>()
            .AsSingle()
            .NonLazy();


        Container.Bind<IPathService>()
            .To<PathService>()
            .AsSingle()
            .NonLazy();

        Container.Bind<GameService>()
            .To<GameService>()
            .AsSingle()
            .NonLazy();

        Container.BindSignal<StateChangeSignal>().ToMethod<GameService>(x=>x.ChangeState).FromResolve();
        //Container.BindSignal<EnemyDeathSignal>().ToMethod<EnemyService>(x=>x.EnemyDead).FromResolve();
        

        //Container.Bind<IPickupService>()
        //  .To<PickupService>()
        //.AsSingle()
        //.NonLazy();
        //Container.BindSignal<EnemyDeathSignal>().ToMethod<PlayerService>(x => x.IncreaseScore).FromResolve();
        //Container.BindSignal<PlayerDeathSignal>().ToMethod<UIanimatoretc>().FromResolve();


    }
}
