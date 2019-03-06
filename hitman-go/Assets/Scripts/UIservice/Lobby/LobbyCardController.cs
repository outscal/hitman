using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameState;
using Common;

namespace UIservice
{
    public class LobbyCardController : MonoBehaviour
    {
        [SerializeField] private Text levelText;
        [SerializeField] private GameObject lockObject;
        [SerializeField] private LobbyStarHolderController lobbyStarHolder;

        int levelIndex;
        LobbyUIView lobbyUIView;

        void SetLevelText()
        {
            this.levelText.text = "LEVEL " + (levelIndex + 1);
        }

        public void DefaultSettings(bool unlocked, int levelIndex, LobbyUIView lobbyUIView)
        {
            this.lobbyUIView = lobbyUIView;
            this.levelIndex = levelIndex;
            SetLevelText();
            if (unlocked == true)
            {
                GetComponent<Button>().interactable = true;
                levelText.gameObject.SetActive(true);
                lobbyStarHolder.gameObject.SetActive(true);
                lockObject.SetActive(false);
            }
            else
            {
                GetComponent<Button>().interactable = false;
                lobbyStarHolder.gameObject.SetActive(false);
                levelText.gameObject.SetActive(false);
                lockObject.SetActive(true);
            }
            lobbyStarHolder.SetStarEarned(lobbyUIView.ReturnGameService().GetStarsForLevel(levelIndex),
            lobbyUIView, levelIndex);
        }

        public void LoadLevel()
        {
            lobbyUIView.ReturnGameService().SetCurrentLevel(levelIndex);
            lobbyUIView.ReturnGameService().ChangeToLoadLevelState();
        }

    }
}