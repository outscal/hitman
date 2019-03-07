using System.Collections;
using System.Collections.Generic;
using Common;
using UnityEngine;
using GameState;

namespace UIservice
{
    public class LobbyUIController : IUIController
    {
        IUIView view;
        GameStatesType state = GameStatesType.LOBBYSTATE;

        public LobbyUIController(IUIView view)
        {
            this.view = view;
            view.DisplayUI();
        }
        public void DestroyUI()
        {
            view.DestroyUI();
        }

        public GameStatesType GetUIState()
        {
            return state;
        }
    }
}