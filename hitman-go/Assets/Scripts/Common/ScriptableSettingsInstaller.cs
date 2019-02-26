using UnityEngine;
using Zenject;
using Player;
using Enemy;
using System.Collections;

namespace Common
{
    public class ScriptableSettingsInstaller : ScriptableObjectInstaller
    {
        private PlayerScriptableObject playerScriptableObject;
        private EnemyScriptableObjectList enemyScriptableObjectList;      

        public override void InstallBindings()
        {
           Container.BindInstance(playerScriptableObject);
           Container.BindInstance(enemyScriptableObjectList);
           //Container.BindInstance(ScriptableGraph);
        }
    }
}