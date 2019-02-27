using Common;
using Enemy;
using GameState.Interface;
using InputSystem;
using PathSystem;
using Player;
using Zenject;
using UnityEngine;

namespace GameState
{
    public class GameService{
        IGameStates currentGameState = new GamePlayerState();
        IGameStates previousGameState = new GameEnemyState();
        public GameService(IPlayerService playerService){
           //pathService.DrawGraph();

            playerService.SpawnPlayer();
        }
        public GameStatesType GetCurrentState()
        {
            return currentGameState.GetStatesType();
        }
        public void ChangeState()
        {
            IGameStates tempState = previousGameState;
            if (GetCurrentState() == GameStatesType.PLAYERSTATE)
            {
                previousGameState.OnStateExit();
                previousGameState = currentGameState;
                currentGameState = tempState;
                currentGameState.OnStateEneter();
                Debug.Log(currentGameState);
            }
        }
    }
}