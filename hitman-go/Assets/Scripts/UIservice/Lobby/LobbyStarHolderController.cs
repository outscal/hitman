using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;

namespace UIservice
{
    public class LobbyStarHolderController : MonoBehaviour
    {
        [SerializeField]
        private Color starAchievedColor, startNotAchievedColor;
        [SerializeField]
        private GameObject starPrefab;
        List<StarData> starDatas;

        List<LobbyStarController> lobbyStarControllerList;

        private void Awake()
        {
            lobbyStarControllerList = new List<LobbyStarController>();
        }

        public void SetStarEarned(List<StarData> starDatas, LobbyUIView lobbyUIView, int levelIndex)
        {
            this.starDatas = starDatas;

            if (lobbyStarControllerList.Count <= 0)
            {
                for (int i = 0; i < starDatas.Count; i++)
                {
                    GameObject star = Instantiate(starPrefab);
                    star.transform.SetParent(this.transform);
                    star.transform.localScale = Vector3.one;
                    star.GetComponent<LobbyStarController>().SetStarColor(startNotAchievedColor);

                    if (lobbyUIView.ReturnSaveService().ReadStarTypeForLevel(levelIndex, starDatas[i].type))
                    {
                        star.GetComponent<LobbyStarController>().SetStarColor(starAchievedColor);
                    }
                    lobbyStarControllerList.Add(star.GetComponent<LobbyStarController>());
                }
            }
            else
            {
                for (int i = 0; i < lobbyStarControllerList.Count; i++)
                {
                    lobbyStarControllerList[i].SetStarColor(startNotAchievedColor);

                    if (lobbyUIView.ReturnSaveService().ReadStarTypeForLevel(levelIndex, starDatas[i].type))
                    {
                        lobbyStarControllerList[i].SetStarColor(starAchievedColor);
                    }
                }
            }

        }

    }
}