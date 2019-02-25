using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using InputSystem;
using Player;
using Zenject;
using TMPro;

public class SpawnServices : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("<color=blue>[SpawnService] Created:</color>");
        Container.Bind(typeof(IInputService), typeof(ITickable)).To<InputService>().AsSingle().NonLazy();
    }


}
