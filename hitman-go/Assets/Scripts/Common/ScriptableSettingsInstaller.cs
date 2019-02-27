using UnityEngine;
using Zenject;
using Player;
using Enemy;
using System.Collections;
using PathSystem;

namespace Common
{
    [CreateAssetMenu(fileName = "Scriptable Settings", menuName = "Custom Objects/Installer/Scriptable Settings Attribute", order = 0)]
    public class ScriptableSettingsInstaller : ScriptableObjectInstaller
    {

        public ScriptableGraph scriptableGraph;
        public PlayerScriptableObject playerScriptableObject;

        public EnemyScriptableObjectList enemyScriptableObjectList;      

        public override void InstallBindings()
        {
           Container.BindInstance(playerScriptableObject);
           Container.BindInstance(enemyScriptableObjectList);
           Container.BindInstance(scriptableGraph);
        }
    }
}