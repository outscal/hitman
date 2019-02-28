using UnityEngine;
using Zenject;
using Player;
using Enemy;
using System.Collections;
using PathSystem;
using InteractableSystem;

namespace Common
{
    [CreateAssetMenu(fileName = "Scriptable Settings", menuName = "Custom Objects/Installer/Scriptable Settings Attribute", order = 0)]
    public class ScriptableSettingsInstaller : ScriptableObjectInstaller
    {

        public ScriptableLevels scriptableGraph;
        public PlayerScriptableObject playerScriptableObject;
        public InteractableScriptableObj interactableScriptableObj;

        public EnemyScriptableObjectList enemyScriptableObjectList;      

        public override void InstallBindings()
        {
            Container.BindInstances(interactableScriptableObj);
            Container.BindInstance(playerScriptableObject);
            Container.BindInstance(enemyScriptableObjectList);
            Container.BindInstance(scriptableGraph);
        }
    }
}