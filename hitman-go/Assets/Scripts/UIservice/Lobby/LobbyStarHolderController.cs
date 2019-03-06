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

        public void SetStarEarned(List<StarData> starDatas, LobbyUIView lobbyUIView, int levelIndex)
        {
            this.starDatas = starDatas;

            for (int i = 0; i < starDatas.Count; i++)
            {

                GameObject star = Instantiate(starPrefab);
                star.transform.SetParent(this.transform);
                star.GetComponent<LobbyStarController>().SetStarColor(startNotAchievedColor);

                if (lobbyUIView.ReturnSaveService().ReadStarTypeForLevel(levelIndex, starDatas[i].type))
                {
                    star.GetComponent<LobbyStarController>().SetStarColor(starAchievedColor);
                }
            }

        }

    }
}