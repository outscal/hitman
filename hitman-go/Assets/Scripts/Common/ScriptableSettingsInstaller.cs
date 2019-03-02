using UnityEngine;
using Zenject;
using Player;
using Enemy;
using System.Collections;
using PathSystem;
using InteractableSystem;
using ScriptableObjSystem;

namespace Common
{
    [CreateAssetMenu(fileName = "Scriptable Settings", menuName = "Custom Objects/Installer/Scriptable Settings Attribute", order = 0)]
    public class ScriptableSettingsInstaller : ScriptableObjectInstaller
    {

        public ScriptableLevels scriptableLevels;
        public PlayerScriptableObject playerScriptableObject;
        public InteractableScriptableObj interactableScriptableObj;
        public EnemyScriptableObjectList enemyScriptableObjectList;
        public GameBasicObjects gameBasicObjects;

        public override void InstallBindings()
        {
            Container.BindInstances(interactableScriptableObj);
            Container.BindInstance(playerScriptableObject);
            Container.BindInstance(enemyScriptableObjectList);
            Container.BindInstance(scriptableLevels);
            Container.BindInstances(gameBasicObjects);
        }
    }
}