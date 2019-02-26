using UnityEngine;
using Zenject;
using Player;
using Enemy;
using InputSystem;
using PathSystem;
using System.Collections;

public class GameInstaller : MonoInstaller
{
<<<<<<< HEAD
    // public override void InstallBindings()
    // {
        
    //     Container.Bind<IPlayerService>()
    //         .To<PlayerService>()
    //         .AsSingle()
    //         .NonLazy();

    //     Container.Bind(typeof(IInputService),typeof(ITickable))
    //         .To<InputService>()
    //         .AsSingle()
    //         .NonLazy();

    //     Container.Bind<IEnemyService>()
    //         .To<EnemyService>()
    //         .AsSingle()
    //         .NonLazy();

    //     Container.Bind<IPathService>()
    //         .To<PathService>()
    //         .AsSingle()
    //         .NonLazy();
=======
    public override void InstallBindings()
    {
        
        Container.Bind<IPlayerService>()
            .To<PlayerService>()
            .AsSingle()
            .NonLazy();

        Container.Bind(typeof(IInputService),typeof(ITickable))
            .To<InputService>()
            .AsSingle()
            .NonLazy();

        Container.Bind<IEnemyService>()
            .To<EnemyService>()
            .AsSingle()
            .NonLazy();

        //Container.Bind<IPathService>()
            //.To<PathService>()
            //.AsSingle()
            //.NonLazy();
>>>>>>> origin/MergeTest/CoreGameplay

        //Container.Bind<IPickupService>()
        //  .To<PickupService>()
        //.AsSingle()
        //.NonLazy();

<<<<<<< HEAD
       // Container.BindSignal<EnemyDeathSignal>().ToMethod<PlayerService>(x => x.IncreaseScore).FromResolve();
      

   // }
=======
        Container.BindSignal<EnemyDeathSignal>().ToMethod<PlayerService>(x => x.IncreaseScore).FromResolve();
        //Container.BindSignal<PlayerDeathSignal>().ToMethod<UI/animatoretc>().FromResolve();


    }
>>>>>>> origin/MergeTest/CoreGameplay

}
